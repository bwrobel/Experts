using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Repositories;
using System.Linq;

namespace Experts.Web.Models.Events
{
    public abstract class EventReactionModel<T>
        where T:Event
    {
        public T Event { get; set; }
        public int EventId { get; set; }
        public abstract string ViewName { get; }

        public virtual void Initialize() {}
    }

    public class SystemBreakdownReactionModel : EventReactionModel<SystemBreakdownEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.SystemBreakdownReaction; } }
    }

    public class NewAdditionalServiceReactionModel : EventReactionModel<NewAdditionalServiceEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.NewAdditionalServiceReactionModel; } }

        public override void Initialize()
        {
            AdditionalService = Event.RelatedThread.AdditionalServices.Single(s => s.Id == Event.AdditionalId);
            IsVerified = true;
        }

        public AdditionalService AdditionalService { get; set; }
        public bool IsVerified { get; set; }
    }

    public class ExpertQualificationsChangedReactionModel : EventReactionModel<ExpertQualificationsChangedEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.ExpertQualificationsChangedReaction; } }

        public override void Initialize()
        {
            VerificationDetails = Event.RelatedUser.Expert.VerificationDetails;
        }

        public ExpertVerificationStatus CurrentVerificationStatus { get; set; }

        public bool IsAccepted { get; set; }
        public string VerificationDetails { get; set; }
    }

    public class ThreadIssueReactionModel : EventReactionModel<ThreadIssueReportEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.ThreadIssueReaction; } }

        private readonly RepositoryFactory _repository = new RepositoryFactory();

        public override void Initialize()
        {
            Categories = _repository.Category.All();
        }

        public bool RemoveDuplication { get; set; }
        public int ChosenCategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string IssueDetails { get; set; }
    }

    public class CashPaymentReactionModel : EventReactionModel<CashPaymentEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.CashPaymentReaction; } }

        private readonly RepositoryFactory _repository = new RepositoryFactory();

        public override void Initialize()
        {
            Transfer = _repository.Transfer.Get(Event.AdditionalId);
        }

        public Transfer Transfer { get; set; }
    }

    public class NoAnswerReactionModel : EventReactionModel<NoAnswerEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.NoAnswerReaction; } }
    }

    public class ExpertPriceProposalReactionModel : EventReactionModel<ExpertPriceProposalEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.ExpertPriceProposalReaction; } }

        private readonly RepositoryFactory _repository = new RepositoryFactory();

        public override void Initialize()
        {
            PriceProposal = _repository.Thread.GetPriceProposal(Event.AdditionalId);
        }

        public PriceProposal PriceProposal { get; set; }
        public PriceProposalVerificationStatus VerificationStatus { get; set; }
    }

    public class ExpertPublicDataChangedReactionModel : EventReactionModel<ExpertPublicDataChangedEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.ExpertQualificationsChangedReaction; } }
    }

    public class UserFailureReactionModel : EventReactionModel<UserFailureEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.UserFailureReaction; } }
    }

    public class BecomePartnerRequestReactionModel : EventReactionModel<BecomePartnerRequestEvent>
    {
        public override string ViewName { get { return MVC.Administration.Views.Events.BecomePartnerRequestReaction; } }

        public bool Accept { get; set; }
    }
}