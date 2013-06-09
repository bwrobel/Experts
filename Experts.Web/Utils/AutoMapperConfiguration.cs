using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Experts.Core.Entities;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Experts.Web.Models.Account;
using Experts.Web.Models.Chat;
using Experts.Web.Models.Forms;
using Experts.Web.Models.Payments;
using System;

namespace Experts.Web.Utils
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<ProfileForm, User>()
                  .ForMember(d => d.CreationDate, o => o.Ignore())
                  .ForMember(d => d.LastModificationDate, o => o.Ignore())
                  .ForMember(d => d.Id, o => o.Ignore())
                  .ForMember(d => d.IsActivated, o => o.Ignore())
                  .ForMember(d => d.PasswordSalt, o => o.Ignore())
                  .ForMember(d => d.Email, o => o.MapFrom(s => s.EmailForm.Email))
                  .ForMember(d => d.Password, o => o.MapFrom(s => s.PasswordForm.Password))
                  .ForMember(d => d.ActivationKey, o => o.Ignore())
                  .ForMember(d => d.NewEmail, o => o.Ignore())
                  .ForMember(d => d.Expert, o => o.Ignore())
                  .ForMember(d => d.AskedQuestions, o => o.Ignore())
                  .ForMember(d => d.AcceptedQuestions, o => o.Ignore())
                  .ForMember(d => d.ResetKey, o => o.Ignore())
                  .ForMember(d => d.Questions, o => o.Ignore())
                  .ForMember(d => d.Posts, o => o.Ignore())
                  .ForMember(d => d.IsModerator, o => o.Ignore())
                  .ForMember(d => d.IsModerator, o => o.Ignore())
                  .ForMember(d => d.IsNoSignUpUser, o => o.Ignore())
                  .ForMember(d => d.Transfers, o => o.Ignore())
                  .ForMember(d => d.LongEmailConfiguration, o => o.Ignore())
                  .ForMember(d => d.EmailConfiguration, o => o.Ignore())
                  .ForMember(d => d.Partner, o => o.Ignore())
                  .ForMember(d => d.LastAskEncouragementDate, o => o.Ignore())
                  .ForMember(d => d.Moderator, o => o.Ignore())
                  .ForMember(d => d.BankAccountNumber, o => o.Ignore())
                  .ForMember(d => d.IsConsultant, o => o.Ignore())
                  .ForMember(d => d.Consultant, o => o.Ignore())
                  .ForMember(d => d.Chats, o => o.Ignore());

            Mapper.CreateMap<User, ProfileForm>()
                  .ForMember(d => d.EmailForm, o => o.MapFrom(s => new EmailForm {Email = s.Email}))
                  .ForMember(d => d.PasswordForm, o => o.Ignore())
                  .ForMember(d => d.ExpertProfileFormModel, o => o.ResolveUsing<ExpertProfileFormModelResolver>())
                  .ForMember(d => d.Policy, o => o.Ignore())
                  .ForMember(d => d.MailConfigurationForm, o => o.ResolveUsing<MailCfgResolver>())
                  .ForMember(d => d.ModeratorPublicNameForm, o => o.ResolveUsing<ModeratorPublicNameFormResolver>());

            Mapper.CreateMap<ThreadForm, Thread>()
                  .ForMember(d => d.Id, o => o.Ignore())
                  .ForMember(d => d.IntPriority, o => o.Ignore())
                  .ForMember(d => d.IntVerbosity, o => o.Ignore())
                  .ForMember(d => d.Author, o => o.MapFrom(s => AuthenticationHelper.CurrentUser))
                  .ForMember(d => d.Category, o => o.ResolveUsing<CategoryResolver>())
                  .ForMember(d => d.Posts, o => o.ResolveUsing<PostsResolver>())
                  .ForMember(d => d.Expert, o => o.Ignore())
                  .ForMember(d => d.IntState, o => o.Ignore())
                  .ForMember(d => d.State, o => o.Ignore())
                  .ForMember(d => d.Feedback, o => o.Ignore())
                  .ForMember(d => d.ExpertReleaseDate, o => o.Ignore())
                  .ForMember(d => d.Issues, o => o.Ignore())
                  .ForMember(d => d.PriceProposals, o => o.Ignore())
                  .ForMember(d => d.IntSanitizationStatus, o => o.Ignore())
                  .ForMember(d => d.SanitizationStatus, o => o.Ignore())
                  .ForMember(d => d.CreationDate, o => o.Ignore())
                  .ForMember(d => d.LastModificationDate, o => o.Ignore())
                  .ForMember(d => d.Broker, o => o.Ignore())
                  .ForMember(d => d.BrokerProvision, o => o.Ignore())
                  .ForMember(d => d.ThreadAcceptanceDate, o => o.Ignore())
                  .ForMember(d => d.ThreadCloseDate, o => o.Ignore())
                  .ForMember(d => d.Transfers, o => o.Ignore())
                  .ForMember(d => d.IsPaid, o => o.Ignore())
                  .ForMember(d => d.ExpertProvision, o => o.Ignore())
                  .ForMember(d => d.CategoryAttributes, o => o.Ignore())
                  .ForMember(d => d.AdditionalServices, o => o.Ignore())
                  .ForMember(d => d.VerifiedAdditionalServices, o => o.Ignore())
                  .ForMember(d => d.IsNotified, o => o.Ignore())
                  .ForMember(d => d.IsExpertResponseNotified, o => o.Ignore())
                  .ForMember(d => d.AdditionalExpertProvisionValue, o => o.Ignore())
                  .ForMember(d => d.IsInner, o => o.Ignore())
                  .ForMember(d => d.CatalogSanitizedThreadTitle, o => o.Ignore())
                ;

            Mapper.CreateMap<ThreadForm, Post>()
                  .ForMember(d => d.Id, o => o.Ignore())
                  .ForMember(d => d.Thread, o => o.Ignore())
                  .ForMember(d => d.Author, o => o.MapFrom(s => AuthenticationHelper.CurrentUser))
                  .ForMember(d => d.IntType, o => o.Ignore())
                  .ForMember(d => d.CreationDate, o => o.Ignore())
                  .ForMember(d => d.LastModificationDate, o => o.Ignore())
                  .ForMember(d => d.Type, o => o.Ignore())
                  .ForMember(d => d.PublicContent, o => o.Ignore())
                  .ForMember(d => d.Attachments, o => o.Ignore())
                  .ForMember(d => d.IsPubliclyVisible, o => o.Ignore())
                  .ForMember(d => d.PublicContent, o => o.Ignore())
                  .ForMember(d => d.IsReadOnly, o => o.Ignore());

            Mapper.CreateMap<ExpertProfileForm, Expert>()
                  .ForMember(d => d.CreationDate, o => o.Ignore())
                  .ForMember(d => d.LastModificationDate, o => o.Ignore())
                  .ForMember(d => d.Id, o => o.Ignore())
                  .ForMember(d => d.User, o => o.MapFrom(s => AuthenticationHelper.CurrentUser))
                  .ForMember(d => d.Categories, o => o.ResolveUsing<CategoriesResolver>())
                  .ForMember(d => d.Microprofiles,
                             o => o.MapFrom(s => new List<ExpertMicroprofile> {new ExpertMicroprofile()}))
                  .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumberForm.PhoneNumber))
                  .ForMember(d => d.PublicName, o => o.Ignore())
                  .ForMember(d => d.PositiveFeedback, o => o.Ignore())
                  .ForMember(d => d.AcceptedAnswers, o => o.Ignore())
                  .ForMember(d => d.IsVerified, o => o.Ignore())
                  .ForMember(d => d.IntVerificationStatus, o => o.Ignore())
                  .ForMember(d => d.VerificationStatus, o => o.Ignore())
                  .ForMember(d => d.Answers, o => o.Ignore())
                  .ForMember(d => d.CategoryAttributes, o => o.Ignore())
                  .ForMember(d => d.VerificationDetails, o => o.Ignore())
                  .ForMember(d => d.Recommendation, o => o.Ignore())
                  .ForMember(d => d.IsInner, o => o.Ignore())
                  .ForMember(d => d.Provision, o => o.Ignore())
                  .ForMember(d => d.HasPicture, o => o.Ignore());

            Mapper.CreateMap<PostForm, Post>()
                  .ForMember(d => d.Author, o => o.MapFrom(s => AuthenticationHelper.CurrentUser))
                  .ForMember(d => d.CreationDate, o => o.Ignore())
                  .ForMember(d => d.Id, o => o.Ignore())
                  .ForMember(d => d.IntType, o => o.Ignore())
                  .ForMember(d => d.LastModificationDate, o => o.Ignore())
                  .ForMember(d => d.Thread, o => o.Ignore())
                  .ForMember(d => d.PublicContent, o => o.Ignore())
                  .ForMember(d => d.Attachments, o => o.Ignore())
                  .ForMember(d => d.IsPubliclyVisible, o => o.Ignore())
                  .ForMember(d => d.PublicContent, o => o.Ignore())
                  .ForMember(d => d.IsReadOnly, o => o.Ignore());

            Mapper.CreateMap<FeedbackForm, Feedback>()
                  .ForMember(d => d.Id, o => o.Ignore())
                  .ForMember(d => d.CreationDate, o => o.Ignore())
                  .ForMember(d => d.LastModificationDate, o => o.Ignore())
                  .ForMember(d => d.Thread, o => o.Ignore())
                  .ForMember(d => d.FeedbackMarkInt, o => o.Ignore())
                  .ForMember(d => d.Comment, o => o.Ignore());

            Mapper.CreateMap<ReportIssueForm, ThreadIssue>()
                  .ForMember(d => d.Id, o => o.Ignore())
                  .ForMember(d => d.CreationDate, o => o.Ignore())
                  .ForMember(d => d.LastModificationDate, o => o.Ignore())
                  .ForMember(d => d.IntIssueType, o => o.Ignore())
                  .ForMember(d => d.Thread, o => o.ResolveUsing<ReportIssueThreadResolver>());

            Mapper.CreateMap<PaymentForm, Payment>()
                  .ForMember(d => d.Id, o => o.Ignore())
                  .ForMember(d => d.ProviderPaymentId, o => o.Ignore())
                  .ForMember(d => d.CreationDate, o => o.Ignore())
                  .ForMember(d => d.LastModificationDate, o => o.Ignore())
                  .ForMember(d => d.Amount, o => o.Ignore())
                  .ForMember(d => d.User, o => o.Ignore())
                  .ForMember(d => d.Status, o => o.Ignore())
                  .ForMember(d => d.IntStatus, o => o.Ignore())
                  .ForMember(d => d.FirstName, o => o.MapFrom(s => s.PersonalData.FirstName))
                  .ForMember(d => d.LastName, o => o.MapFrom(s => s.PersonalData.LastName));

            Mapper.CreateMap<PriceProposalForm, PriceProposal>()
                  .ForMember(pp => pp.Id, o => o.Ignore())
                  .ForMember(pp => pp.Expert, o => o.Ignore())
                  .ForMember(pp => pp.SurchargeValue, o => o.ResolveUsing<PriceProposalThreadValueResolver>())
                  .ForMember(pp => pp.Thread, o => o.ResolveUsing<PriceProposalThreadResolver>())
                  .ForMember(pp => pp.CreationDate, o => o.Ignore())
                  .ForMember(pp => pp.Accepted, o => o.Ignore())
                  .ForMember(pp => pp.LastModificationDate, o => o.Ignore())
                  .ForMember(pp => pp.IntVerificationStatus, o => o.Ignore())
                  .ForMember(pp => pp.VerificationStatus, o => o.Ignore());

            Mapper.CreateMap<ChatMessageForm, ChatMessage>()
                  .ForMember(cm => cm.Id, o => o.Ignore())
                  .ForMember(cm => cm.CreationDate, o => o.MapFrom(s => DateTime.Now))
                  .ForMember(cm => cm.Author, o => o.MapFrom(s => AuthenticationHelper.CurrentUser))
                  .ForMember(cm => cm.Context, o => o.Ignore())
                  .ForMember(cm => cm.Chat, o => o.Ignore())
                  .ForMember(cm => cm.AuthorEmail, o => o.Ignore());

            Mapper.CreateMap<ChatMessage, ChatMessageViewModel>()
                  .ForMember(cm => cm.AuthorName, o => o.ResolveUsing<AuthorNameResolver>())
                  .ForMember(cm => cm.CreationDate, o => o.ResolveUsing<StringDateResolver>())
                  .ForMember(cm => cm.Timestamp, o => o.MapFrom(s => s.CreationDate.Ticks))
                  .ForMember(cm => cm.IsRead, o => o.MapFrom(s => s.CreationDate <= s.Chat.LastReadTime));

            Mapper.CreateMap<CategoryAttributeForm, CategoryAttribute>()
                  .ForMember(ca => ca.Id, o => o.Ignore())
                  .ForMember(ca => ca.IntType, o => o.Ignore())
                  .ForMember(ca => ca.ChildAttributes, o => o.Ignore())
                  .ForMember(ca => ca.ParentOptions, o => o.ResolveUsing<CategoryAttributeParentOptionsResolver>());

            Mapper.CreateMap<CategoryAttribute, CategoryAttributeForm>()
                  .ForMember(ca => ca.AttributeId, o => o.MapFrom(s => s.Id))
                  .ForMember(ca => ca.CategoryId, o => o.Ignore())
                  .ForMember(ca => ca.ParentAttributeId, o => o.Ignore())
                  .ForMember(ca => ca.AvailableParentOptions, o => o.Ignore())
                  .ForMember(ca => ca.SelectedParentOptions,
                             o => o.ResolveUsing<CategoryAttributeSelectedParentOptionsResolver>());
        }

        private abstract class DbValueResolver<TSource, TDestination> : ValueResolver<TSource, TDestination>
        {
            protected RepositoryFactory Repository
            {
                get { return RepositoryHelper.Repository; }
            }
        }

        private class MailCfgResolver : DbValueResolver<User, MailConfigurationForm>
        {
            protected override MailConfigurationForm ResolveCore(User source)
            {
                var recipientTypes = EmailRecipientType.User;

                if (source.IsExpert)
                    recipientTypes |= EmailRecipientType.Expert;

                if (source.IsPartner)
                    recipientTypes |= EmailRecipientType.Partner;

                return MailConfigurationForm.Create(source.EmailConfiguration, recipientTypes);
            }
        }

        private class CategoryResolver : DbValueResolver<ThreadForm, Category>
        {
            protected override Category ResolveCore(ThreadForm source)
            {
                return Repository.Category.Get(source.CategoryId.Value);
            }
        }

        private class CategoriesResolver : DbValueResolver<ExpertProfileForm, ICollection<Category>>
        {
            protected override ICollection<Category> ResolveCore(ExpertProfileForm source)
            {
                return
                    new List<Category>(
                        Repository.Category.Find(
                            query: q => q.ByIds(source.ExpertProfileCategoriesForm.SelectedCategories)));
            }
        }

        private class PostsResolver : DbValueResolver<ThreadForm, IEnumerable<Post>>
        {
            protected override IEnumerable<Post> ResolveCore(ThreadForm source)
            {
                return new List<Post>
                    {
                        Mapper.Map<ThreadForm, Post>(source)
                    };
            }
        }


        private class ExpertProfileFormModelResolver : DbValueResolver<User, ExpertProfileFormModel>
        {
            protected override ExpertProfileFormModel ResolveCore(User source)
            {
                if (source.IsExpert)
                {
                    return new ExpertProfileFormModel
                        {
                            ExpertProfileForm = new ExpertProfileForm
                                {
                                    PublicNameForm = new PublicNameForm
                                        {
                                            PublicName = source.Expert.PublicName
                                        },
                                    FirstName = source.Expert.FirstName,
                                    LastName = source.Expert.LastName,
                                    PhoneNumberForm = new PhoneNumberForm {PhoneNumber = source.Expert.PhoneNumber},
                                    ExpertMicroprofileForm = new ExpertMicroprofileForm
                                        {
                                            Position = source.Expert.Microprofiles.First().Position,
                                            Description = source.Expert.Microprofiles.First().Description,
                                        },
                                    ExpertProfileCategoriesForm =
                                        new ExpertProfileCategoriesForm
                                            {
                                                SelectedCategories =
                                                    source.Expert.Categories.Select(c => c.Id)
                                            }
                                },
                            AvailableCategories = Repository.Category.All()
                        };
                }
                return null;
            }
        }

        private class ReportIssueThreadResolver : DbValueResolver<ReportIssueForm, Thread>
        {
            protected override Thread ResolveCore(ReportIssueForm source)
            {
                return Repository.Thread.Get(source.ThreadId);
            }
        }

        private class PriceProposalThreadResolver : DbValueResolver<PriceProposalForm, Thread>
        {
            protected override Thread ResolveCore(PriceProposalForm source)
            {
                return Repository.Thread.Get(source.ThreadId);
            }
        }

        private class PriceProposalThreadValueResolver : DbValueResolver<PriceProposalForm, decimal>
        {
            protected override decimal ResolveCore(PriceProposalForm source)
            {
                return source.SurchargeValue.Value/
                       ThreadValueHelper.GetExpertProvision(AuthenticationHelper.CurrentUser.Expert.Provision);
            }
        }

        private class StringDateResolver : ValueResolver<ChatMessage, string>
        {
            protected override string ResolveCore(ChatMessage source)
            {
                var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                var format = string.Format("{0} {1}", dateTimeFormat.ShortDatePattern, dateTimeFormat.ShortTimePattern);
                return source.CreationDate.ToString(format);
            }
        }

        private class AuthorNameResolver : ValueResolver<ChatMessage, string>
        {
            protected override string ResolveCore(ChatMessage source)
            {
                if (source.Author == AuthenticationHelper.CurrentUser)
                    return Resources.Chat.Me;

                if (source.Author != null && source.Author.IsModerator)
                    return source.Author.Moderator.PublicName;

                return Resources.Thread.HiddenName;
            }
        }

        private class ModeratorPublicNameFormResolver : ValueResolver<User, PublicNameForm>
        {
            protected override PublicNameForm ResolveCore(User source)
            {
                var result = new PublicNameForm();
                if (source.IsModerator)
                    result.PublicName = source.Moderator.PublicName;

                return result;
            }
        }

        private class CategoryAttributeParentOptionsResolver :
            DbValueResolver<CategoryAttributeForm, IEnumerable<CategoryAttributeOption>>
        {
            protected override IEnumerable<CategoryAttributeOption> ResolveCore(CategoryAttributeForm source)
            {
                var result = new List<CategoryAttributeOption>();

                if (source.ParentAttributeId.HasValue)
                {
                    var parentAttribute = Repository.Category.GetCategoryAttribute(source.ParentAttributeId.Value);
                    var selectedIds = source.SelectedParentOptions.Where(o => o.IsSelected).Select(o => o.Id);
                    result.AddRange(parentAttribute.Options.Where(o => selectedIds.Contains(o.Id)));
                }

                return result;
            }
        }

        private class CategoryAttributeSelectedParentOptionsResolver :
            DbValueResolver<CategoryAttribute, IEnumerable<CategoryAttributeForm.SelectedParentOption>>
        {
            protected override IEnumerable<CategoryAttributeForm.SelectedParentOption> ResolveCore(
                CategoryAttribute source)
            {
                return
                    source.ParentOptions.Select(
                        o => new CategoryAttributeForm.SelectedParentOption {Id = o.Id, IsSelected = true});
            }
        }

    }
}