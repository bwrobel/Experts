using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Core.ViewModels;
using Experts.Web.Models.Shared;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid.Syntax;
using MvcContrib.UI.Grid;

namespace Experts.Web.Helpers
{
    public static class GridHelper
    {
        public static IGridWithOptions<Thread> BasicThreadGrid(this HtmlHelper htmlHelper, SortableGridModel<Thread> model, Func<Thread, ActionResult> detailsActionResult)
        {
            var grid = htmlHelper.Grid(model.Data);

            grid.Empty(Resources.Global.GridEmptyText);

            grid.Columns(column =>
                         {
                             column.For(t => htmlHelper.ActionLink(t.Name, detailsActionResult(t)));
                                 //column.For(t => t.Category.Name).Named(Resources.Thread.ThreadCategory).SortColumnName(
                                 //    ThreadOrder.Category).SortInitialDirection(SortDirection.Ascending);
                                 //column.For(t => t.Posts.Count - 1).Named(Resources.Thread.ThreadNumberOfReplies).
                                 //    SortColumnName(ThreadOrder.PostCount).SortInitialDirection(SortDirection.Ascending);
                                 column.For(t => t.Posts.First().CreationDate.ToTimeSinceFormat()).Named(
                                     Resources.Thread.ThreadCreateDate).SortColumnName(ThreadOrder.CreationDate).
                                     SortInitialDirection(SortDirection.Descending);
                                 column.For(t => t.State.Describe(Resources.Thread.ResourceManager)).Named(
                                     Resources.Thread.ThreadState).SortColumnName(ThreadOrder.State).
                                     SortInitialDirection(SortDirection.Ascending);
                                 //column.For(t => t.Priority.Describe(Resources.Thread.ResourceManager)).Named(
                                 //    Resources.Thread.ThreadPriority).SortColumnName(ThreadOrder.Priority).
                                 //    SortInitialDirection(SortDirection.Descending);
                                 //column.For(t => t.Verbosity.Describe(Resources.Thread.ResourceManager)).Named(
                                 //    Resources.Thread.ThreadVerbosity).SortColumnName(ThreadOrder.Verbosity).
                                 //    SortInitialDirection(SortDirection.Ascending);
                             });

            grid.Sort(model.SortOptions);

            return grid;
        }

        public static IGridWithOptions<ThreadWithMatch> BasicThreadWithMatchGrid(this HtmlHelper htmlHelper, SortableGridModel<ThreadWithMatch> model, Func<ThreadWithMatch, ActionResult> detailsActionResult)
        {
            var grid = htmlHelper.Grid(model.Data);

            grid.Empty(Resources.Global.GridEmptyText);

            grid.Columns(column =>
                             {
                                 column.For(
                                     t => htmlHelper.ActionLink(t.Thread.Name, detailsActionResult(t)));
                                 //column.For(t => t.Thread.Category.Name).Named(Resources.Thread.ThreadCategory).
                                 //    SortColumnName(
                                 //        ThreadOrder.Category).SortInitialDirection(SortDirection.Ascending);
                                 //column.For(t => t.Thread.Posts.Count - 1).Named(Resources.Thread.ThreadNumberOfReplies)
                                 //    .
                                 //    SortColumnName(ThreadOrder.PostCount).SortInitialDirection(SortDirection.Ascending);
                                 column.For(t => t.Thread.Posts.First().CreationDate.ToTimeSinceFormat()).Named(
                                     Resources.Thread.ThreadCreateDate).SortColumnName(ThreadOrder.CreationDate).
                                     SortInitialDirection(SortDirection.Descending);
                                 column.For(t => t.Thread.State.Describe(Resources.Thread.ResourceManager)).Named(
                                     Resources.Thread.ThreadState).SortColumnName(ThreadOrder.State).
                                     SortInitialDirection(SortDirection.Ascending);
                                 //column.For(t => t.Thread.Priority.Describe(Resources.Thread.ResourceManager)).Named(
                                 //    Resources.Thread.ThreadPriority).SortColumnName(ThreadOrder.Priority).
                                 //    SortInitialDirection(SortDirection.Descending);
                                 //column.For(t => t.Thread.Verbosity.Describe(Resources.Thread.ResourceManager)).Named(
                                 //    Resources.Thread.ThreadVerbosity).SortColumnName(ThreadOrder.Verbosity).
                                 //    SortInitialDirection(SortDirection.Ascending);
                                 column.For(t => t.Thread.CalculatePotentialExpertValue(AuthenticationHelper.CurrentUser.Expert)).Named(Resources.Thread.ThreadExpertValue).
                                     SortColumnName(ThreadOrder.ExpertValue).SortInitialDirection(
                                         SortDirection.Descending);
                                 //column.For(t => t.MatchPercent).Named(Resources.Thread.ThreadMatch).SortColumnName(
                                 //    ThreadOrder.Match).SortInitialDirection(SortDirection.Descending);
                             });

            grid.Sort(model.SortOptions);

            return grid;
        }

        public static class ThreadOrder
        {
            public const string Content = "Content";
            public const string Category = "Category";
            public const string PostCount = "PostCount";
            public const string CreationDate = "CreationDate";
            public const string State = "State";
            public const string Priority = "Priority";
            public const string Verbosity = "Verbosity";
            public const string Author = "Author";
            public const string ExpertValue = "ExpertValue";
            public const string SanitizationStatus = "SanitizationStatus";
            public const string Match = "Match";

            public static readonly IDictionary<string, Func<Thread, IComparable>> ColumnOrders =
                new Dictionary<string, Func<Thread, IComparable>>
                {
                    {Content, t => t.Posts.First().Content},
                    {Category, t => t.Category.Name},
                    {PostCount, t => t.Posts.Count},
                    {CreationDate, t => t.Posts.First().CreationDate},
                    {State, t => t.State},
                    {Priority, t => t.IntPriority},
                    {Verbosity, t => t.IntVerbosity},
                    {Author, t => t.Author.Email},
                    {ExpertValue, t => t.CalculatePotentialExpertValue(AuthenticationHelper.CurrentUser.Expert)},
                    {SanitizationStatus, t => t.SanitizationStatus}
                };
        }

        //seokeyword
        public static IGridWithOptions<SEOKeyword> BasicSeoKeywordGrid(this HtmlHelper htmlHelper, SortableGridModel<SEOKeyword> model, Func<SEOKeyword, ActionResult> detailsActionResult)
        {
            var grid = htmlHelper.Grid(model.Data);

            grid.Empty(Resources.Global.GridEmptyText);

            grid.Columns(column =>
            {
                    column.For(k => htmlHelper.ActionLink(k.Phrase, detailsActionResult(k))).Named(Resources.Catalog.Keyword_Phrase).SortColumnName(SeoKeywordOrder.Phrase).SortInitialDirection(
                        SortDirection.Descending);
                    column.For(k => k.Category.Name).Named(Resources.Catalog.Keyword_Category).SortColumnName(SeoKeywordOrder.Category).SortInitialDirection(
                        SortDirection.Descending);
            }
            );

            grid.Sort(model.SortOptions);

            return grid;
        }

        public static class SeoKeywordOrder
        {
            public const string Phrase = "Phrase";
            public const string Category = "Category";

            public static readonly IDictionary<string, Func<SEOKeyword, IComparable>> ColumnOrders =
                new Dictionary<string, Func<SEOKeyword, IComparable>>
                {
                    {Phrase, k => k.Phrase},
                    {Category, k => k.Category.Name}
                };
        }

        //catalog
        public static IGridWithOptions<Thread> CatalogThreadGrid(this HtmlHelper htmlHelper, SortableGridModel<Thread> model, Func<Thread, ActionResult> detailsActionResult)
        {
            var grid = htmlHelper.Grid(model.Data);

            grid.Empty(Resources.Global.GridEmptyText);

            grid.Columns(column =>
            {
                column.For(t => htmlHelper.ActionLink(t.Name, detailsActionResult(t))).Named("Pytanie").SortColumnName(ThreadOrder.Content).SortInitialDirection(
                        SortDirection.Descending);
                column.For(t => t.Category.Name).Named(Resources.Thread.ThreadCategory).SortColumnName(
                    ThreadOrder.Category).SortInitialDirection(SortDirection.Descending);
                column.For(t => t.Posts.Count - 1).Named(Resources.Thread.ThreadNumberOfReplies).
                                     SortColumnName(ThreadOrder.PostCount).SortInitialDirection(SortDirection.Ascending);
                column.For(t => t.Posts.First().CreationDate.ToTimeSinceFormat()).Named(
                    Resources.Thread.ThreadCreateDate).SortColumnName(ThreadOrder.CreationDate).
                    SortInitialDirection(SortDirection.Descending);
            });

            grid.Sort(model.SortOptions);

            return grid;
        }

        //balance
        public static IGridWithOptions<Transfer> BalanceGrid(this HtmlHelper htmlHelper, SortableGridModel<Transfer> model)
        {
            var grid = htmlHelper.Grid(model.Data);

            grid.Empty(Resources.Global.GridEmptyText);

            grid.Columns(column =>
            {
                column.For(t => t.OrderDate.ToTimeSinceFormat()).Named(Resources.Account.BalanceOrderDateColumn).SortColumnName(
                   BalanceOrder.OrderDate).SortInitialDirection(SortDirection.Descending);
                column.For(t => t.TransferDate.ToTimeSinceFormat()).Named(Resources.Account.BalanceTransferDateColumn).SortColumnName(
                   BalanceOrder.TransferDate).SortInitialDirection(SortDirection.Descending);
                column.For(t => t.Title).Named(Resources.Account.BalanceTitleColumn).SortColumnName(
                   BalanceOrder.Title).SortInitialDirection(SortDirection.Descending);
                column.For(t => t.Value).Named(Resources.Account.BalanceValueColumn).SortColumnName(
                   BalanceOrder.Value).SortInitialDirection(SortDirection.Descending);
            });

            grid.Sort(model.SortOptions);

            return grid;
        }

        public static class BalanceOrder
        {
            public const string OrderDate = "OrderDate";
            public const string TransferDate = "TransferDate";
            public const string Title = "Title";
            public const string Value = "Value";

            public static readonly IDictionary<string, Func<Transfer, IComparable>> ColumnOrders =
                new Dictionary<string, Func<Transfer, IComparable>>
                {
                    {OrderDate, t => t.OrderDate},
                    {TransferDate, t => t.TransferDate},
                    {Title, t => t.Title},
                    {Value, t => t.Value},
                };
        }



        //chats overview
        public static IGridWithOptions<Chat> ChatsOverviewGrid(this HtmlHelper htmlHelper, SortableGridModel<Chat> model, bool moderatorMode, int? clientChatId = null)
        {
            var grid = htmlHelper.Grid(model.Data);

            grid.Empty(Resources.Global.ChatEmptyGrid);

            grid
                .Columns(column =>
                {
                    if (moderatorMode)
                    {
                        column.For(c => c.Id.ToString()).Named(Resources.Administration.ChatIdColumn);
                        column.For(c => c.GetOwnerPublicName()).Named(Resources.Administration.ChatsOwnerColumn)
                            .SortColumnName(ChatsOverviewOrder.Owner).SortInitialDirection(SortDirection.Descending);
                    }

                    column.For(c => c.GetPublicModeratorName()).Named(Resources.Administration.ChatModeratorColumn)
                        .SortColumnName(ChatsOverviewOrder.Moderator).SortInitialDirection(SortDirection.Descending);
                    column.For(c => c.CreationDate.ToTimeSinceFormat()).Named(Resources.Administration.ChatCreationDateColumn)
                        .SortColumnName(ChatsOverviewOrder.CreationDate).SortInitialDirection(SortDirection.Descending);
                    column.For(c => c.LastModificationDate.ToTimeSinceFormat()).Named(Resources.Administration.ChatLastModificationDateColumn)
                        .SortColumnName(ChatsOverviewOrder.LastModificationDate).SortInitialDirection(SortDirection.Descending);

                    if (moderatorMode)
                        column.For(c => c.Messages.Count).Named(Resources.Administration.ChatNoOfMassegesColumn);
                })
                .RowAttributes(c => new Dictionary<string, object>
                                        {
                                            {"class", c.Item.AttentionObligatory(moderatorMode, clientChatId) ? "not-viewed" : null},
                                            {"data-chatId", c.Item.Id},
                                            {"data-isClosed", c.Item.IsClosed || (clientChatId.HasValue && clientChatId != c.Item.Id) }
                                        });

            return grid;
        }


        public static class ChatsOverviewOrder
        {
            public const string CreationDate = "CreationDate";
            public const string LastModificationDate = "LastModificationDate";
            public const string Owner = "Owner";
            public const string Moderator = "Moderator";
            

            public static readonly IDictionary<string, Func<Chat, IComparable>> ColumnOrders =
                new Dictionary<string, Func<Chat, IComparable>>
                {
                    {CreationDate, t => t.CreationDate},
                    {LastModificationDate, t => t.LastModificationDate},
                    {Owner, t => t.GetOwnerPublicName()},
                    {Moderator, t => t.GetPublicModeratorName()},
                };
        }

        //chats overview
        public static IGridWithOptions<ChatMessage> ChatsDetailsGrid(this HtmlHelper htmlHelper, SortableGridModel<ChatMessage> model)
        {
            var grid = htmlHelper.Grid(model.Data);

            grid.Empty(Resources.Global.GridEmptyText);

            grid
                .Columns(column =>
                {
                    column.For(c => c.CreationDate.ToTimeSinceFormat()).Named(Resources.Administration.ChatDetailsCreationDateColumn);
                    column.For(c => c.GetMessageAuthorName()).Named(Resources.Administration.ChatDetailsAuthorColumn);
                    column.For(c => c.Text).Named(Resources.Administration.ChatDetailsTextColumn);
                });
                

            return grid;
        }


        public static class ChatDetailsOrder
        {
            public const string CreationDate = "CreationDate";
            public const string LastModificationDate = "LastModificationDate";
            public const string Owner = "Owner";
            public const string Moderator = "Moderator";


            public static readonly IDictionary<string, Func<Chat, IComparable>> ColumnOrders =
                new Dictionary<string, Func<Chat, IComparable>>
                {
                    {CreationDate, t => t.CreationDate},
                    {LastModificationDate, t => t.LastModificationDate},
                    {Owner, t => t.GetOwnerPublicName()},
                    {Moderator, t => t.GetPublicModeratorName()},
                };
        }

        private static string GetMessageAuthorName(this ChatMessage chatMessage)
        {
            return chatMessage.Chat.Owner == chatMessage.Author
                       ? chatMessage.Chat.GetOwnerPublicName()
                       : chatMessage.Author.Email;
        }

        private static string GetOwnerPublicName(this Chat chat)
        {
            return Resources.Thread.HiddenName;
        }

        private static bool AttentionObligatory(this Chat chat, bool isModerator, int? clientChatId)
        {
            if (isModerator)
                return chat.Messages.Last().Author == chat.Owner;

            if (!clientChatId.HasValue)
                return false;

            return chat.Id == clientChatId && chat.Messages.Any(m => m.Author != chat.Owner && m.CreationDate > chat.LastReadTime);
        }

        private static string GetPublicModeratorName(this Chat chat)
        {
            return chat.Moderator == null ? Resources.Chat.NoModerator : chat.Moderator.Moderator.PublicName;
        }

        public static SortableGridModel<Chat> GetChatsOverviewsListModel(this ChatRepository repository, Func<IQueryable<Chat>, IQueryable<Chat>> query,
                                                               GridSortOptions sortOptions, int? page)
        {
            if (sortOptions == null) sortOptions = new GridSortOptions();
            if (sortOptions.Column == null)
            {
                sortOptions.Column = ChatsOverviewOrder.CreationDate;
                sortOptions.Direction = SortDirection.Descending;
            }

            return new SortableGridModel<Chat>
            {
                SortOptions = sortOptions,

                Data = repository.Find(
                    PagerHelper.PageSize,
                    page ?? 1,
                    query,
                    ChatsOverviewOrder.ColumnOrders[sortOptions.Column],
                    sortOptions.Direction == SortDirection.Ascending),

                Pagination = PagerHelper.Pagination(page, repository.Count(query))
            };
        }

        public static IGridWithOptions<User> UserGrid(this HtmlHelper htmlHelper, SortableGridModel<User> model)
        {
            var grid = htmlHelper.Grid(model.Data);

            grid.Empty(Resources.Administration.UserGridEmptyText);

            grid.Columns(column =>
                             {
                                 column.For(u => u.Email).Named(Resources.Administration.UserEmail).SortColumnName(
                                     UserOrder.Email).SortInitialDirection(SortDirection.Ascending);
                                 column.For(u => u.CreationDate).Named(Resources.Administration.UserCreationDate).
                                     SortColumnName(UserOrder.CreationDate).SortInitialDirection(
                                         SortDirection.Descending);
                                 column.For(
                                     u =>
                                     htmlHelper.ActionLink(u.IsExpert ? u.Expert.PublicName : "brak", u.IsExpert ? MVC.Profile.ExpertOverview(u.Expert.Id, u.Expert.Categories.First().Id) : MVC.Administration.UserList(model.SortOptions, 1)))
                                     .Named("Wizytówka eksperta");
                             }
                );

            grid.Sort(model.SortOptions);

            return grid;
        }

        public static class UserOrder
        {
            public const string Email = "Email";
            public const string CreationDate = "CreationDate";

            public static readonly IDictionary<string, Func<User, IComparable>> ColumnOrders =
                new Dictionary<string, Func<User, IComparable>>
                    {
                        {Email, u => u.Email},
                        {CreationDate, u => u.CreationDate},
                    };
        }
    }
}