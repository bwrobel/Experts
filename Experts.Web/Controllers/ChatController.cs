using System.Collections.Generic;
﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Repositories;
using Experts.Web.Filters;
using Experts.Web.Helpers;
using Experts.Web.Models.Chat;
using Experts.Web.Models.Forms;
using Experts.Web.Models.Shared;

namespace Experts.Web.Controllers
{
    public partial class ChatController : BaseController
    {
        private static class SessionKey
        {
            public const string ChatEmail = "chat-email";
            public const string ChatId = "chat-id";
            public const string ChatOpen = "chat-open";
        }

        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult ChatsOverview([QueryParameter]int? page = null, ChatsOverviewFilter? filter = null)
        {
            ActiveUsersHelper.SetCurrentModeratorChatActive();

            if (filter == null)
                filter = ChatsOverviewFilter.Open;

            if (page == null)
                page = 1;

            ViewBag.HideChat = true;

            return View(new ChatsOverviewModel { Filter = filter.Value, Page = page.Value });
        }

        [AuthorizeRoles(Role.Moderator)]
        [DefaultRouting]
        public virtual ActionResult ModeratorChatList(ChatsOverviewFilter filter, [QueryParameter] int page)
        {
            SortableGridModel<Chat> model;

            switch (filter)
            {
                case ChatsOverviewFilter.WaitingForResponse:
                    model = Repository.Chat.GetChatsOverviewsListModel(cm => cm.ByWaitingForResponse().ByIsClosed(false), null, page);//zmienić warunek
                    break;
                case ChatsOverviewFilter.Closed:
                    model = Repository.Chat.GetChatsOverviewsListModel(cm => cm.ByIsClosed(true), null, page);
                    break;
                default:
                    model = Repository.Chat.GetChatsOverviewsListModel(cm => cm.ByIsClosed(false), null, page);
                    break;
            }

            return PartialView(model);
        }

        [DefaultRouting]
        public virtual ActionResult UserChatList([QueryParameter] int page)
        {
            var model = new UserChatListModel
                            {
                                CurrentChatId = (int?) Session[SessionKey.ChatId],
                                UserChats =
                                    Repository.Chat.GetChatsOverviewsListModel(
                                        c => c.ByOwner(AuthenticationHelper.CurrentUser), null, page)
                            };

            return PartialView(model);
        }

        [Authorize]
        public virtual ActionResult Support([QueryParameter] int? page)
        {
            ViewBag.HideChat = true;

            if (!page.HasValue)
                page = 1;

            var model = new SupportModel
                            {
                                Page = page.Value,
                                NoCurrentChat = Session[SessionKey.ChatId] == null
                            };

            return View(model);
        }

        [DefaultRouting]
        public virtual ActionResult ChatFrame()
        {
            var isOpen = Session[SessionKey.ChatOpen];
            var model = new ChatModel
                            {
                                IsFrameOpen = isOpen != null && (bool) isOpen,
                            };

            if (!AuthenticationHelper.IsAuthenticated && Session[SessionKey.ChatId] == null)
                model.AskForEmail = true;

            return PartialView(model);
        }

        [DefaultRouting]
        public virtual ActionResult Chat(bool loadChatFromSession, bool isModerator, bool isViewOnly)
        {
            var model = new ChatModel
                            {
                                IsModerator = isModerator,
                                IsViewOnly = isViewOnly,
                                IsOwner = !isModerator
                            };

            if (loadChatFromSession)
            {
                var chatId = Session[SessionKey.ChatId] as int?;

                var chat = chatId.HasValue ? Repository.Chat.Get(chatId.Value) : null;
                if (chat != null)
                {
                    model.Messages = Mapper.Map<IEnumerable<ChatMessageViewModel>>(chat.Messages);
                    model.IsOwner = chat.Owner == AuthenticationHelper.CurrentUser;
                }

                model.Messages = chatId.HasValue ? Mapper.Map<IEnumerable<ChatMessageViewModel>>(Repository.Chat.Get(chatId.Value).Messages) : null;
            }

            return PartialView(model);
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult SetChatEmail(EmailNotUniqueForm form)
        {
            if (!ModelState.IsValid)
                return PartialView(MVC.Chat.Views.ChatFrame, form);
            Session.Add(SessionKey.ChatEmail, form.Email);
            return PartialView(MVC.Chat.Views.ChatFrame);
        }

        [HttpPost]
        public virtual ActionResult CreateMessage([Bind(Prefix = "ChatMessageForm")] ChatMessageForm form)
        {
            if (!ModelState.IsValid)
                return PartialView(MVC.Chat.Views.ChatFrame);

            AddMessage(form);

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [DefaultRouting]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult CloseChat(int chatId, bool isSummarySent)
        {
            var chat = Repository.Chat.Get(chatId);
            chat.IsClosed = true;
            chat.IsSummarySent = isSummarySent;

            var closeMessage = new ChatMessage
                                   {
                                       Author = AuthenticationHelper.CurrentUser,
                                       CreationDate = DateTime.Now,
                                       Text = Resources.Chat.CloseChatMessage
                                   };
            chat.Messages.Add(closeMessage);

            Repository.Chat.Update(chat);

            if (chat.IsSummarySent)
                Email.Send<ChatSummaryEmail>(chat);

            return RedirectToAction(MVC.Chat.ChatsOverview());
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult SendMessage(ChatMessageForm form)
        {
            AddMessage(form);
            return null;
        }

        private void AddMessage(ChatMessageForm form)
        {
            var newMessage = Mapper.Map<ChatMessage>(form);

            var chatId = form.ChatId ?? Session[SessionKey.ChatId] as int?;
            Chat chat = null;

            if (chatId != null)
                chat = Repository.Chat.Get(chatId.Value);

            if (chat != null && !chat.IsClosed)
            {
                if (!AuthenticationHelper.IsModerator && chat.Owner != AuthenticationHelper.CurrentUser)
                    return;

                if (chat.Moderator == null && chat.Owner != AuthenticationHelper.CurrentUser && AuthenticationHelper.CurrentUser.IsModerator)
                    chat.Moderator = AuthenticationHelper.CurrentUser;

                if (AuthenticationHelper.CurrentUser == chat.Owner)
                {
                    chat.LastReadTime = DateTime.Now;
                    newMessage.Context = GetContext();
                }

                chat.Messages.Add(newMessage);
                Repository.Chat.Update(chat);
            }
            else
            {
                chat = new Chat
                           {
                               Messages = new Collection<ChatMessage>(),
                               Owner = AuthenticationHelper.CurrentUser,
                               OwnerEmail = Session[SessionKey.ChatEmail] as string
                           };

                newMessage.Context = GetContext();
                chat.Messages.Add(newMessage);

                Repository.Chat.Add(chat);

                Log.Event<NewChatEvent>(chat.Owner);

                Session[SessionKey.ChatId] = chat.Id;
            }
        }

        [DefaultRouting]
        public virtual ActionResult GetRecentMessages(bool includeOwnMessages = false, int? chatId = null, long? lastMessageTimestamp = null)
        {
            if (!chatId.HasValue)
                chatId = Session[SessionKey.ChatId] as int?;

            if (!chatId.HasValue)
                return null;

            var chat = Repository.Chat.Get(chatId.Value);

            if (!AuthenticationHelper.IsModerator && chat.Owner != AuthenticationHelper.CurrentUser)
                return null;

            var messages = chat.Messages.AsEnumerable();

            if (!includeOwnMessages)
                messages = messages.Where(m => m.Author != AuthenticationHelper.CurrentUser);

            if (lastMessageTimestamp.HasValue)
            {
                var lastMessageDateTime = new DateTime(lastMessageTimestamp.Value);
                messages = messages.Where(m => m.CreationDate > lastMessageDateTime);
            }

            var model = Mapper.Map<IEnumerable<ChatMessageViewModel>>(messages);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [DefaultRouting]
        public virtual ActionResult FinishChat()
        {
            if (Session[SessionKey.ChatId] == null)
                return null;

            var chat = Repository.Chat.Get((int) Session[SessionKey.ChatId]);

            var finishMessage = new ChatMessage
                                    {
                                        Author = AuthenticationHelper.CurrentUser,
                                        CreationDate = DateTime.Now,
                                        Text = Resources.Chat.FinishChatMessage
                                    };

            chat.Messages.Add(finishMessage);

            chat.LastReadTime = DateTime.Now;

            Repository.Chat.Update(chat);

            Session.Remove(SessionKey.ChatId);
            return null;
        }
        
        [DefaultRouting]
        public virtual ActionResult SetChatFrameState(bool isOpen)
        {
            Session[SessionKey.ChatOpen] = isOpen;
            return null;
        }

        [DefaultRouting]
        public virtual ActionResult SetChatEmail(string email)
        {
            Session[SessionKey.ChatEmail] = email;
            return null;
        }

        [DefaultRouting]
        public virtual ActionResult MarkChatMessagesRead(int? chatId = null)
        {
            if (!chatId.HasValue)
                chatId = (int?) Session[SessionKey.ChatId];

            if (chatId.HasValue)
            {
                var chat = Repository.Chat.Get(chatId.Value);
                chat.LastReadTime = DateTime.Now;
                Repository.Chat.Update(chat);
            }

            return null;
        }

        private string GetContext()
        {
            var template = Resources.Chat.ContextTemplate;
            var url = HttpContext.Request.UrlReferrer.PathAndQuery;
            return string.Format(template, url);
        }
    }
}
