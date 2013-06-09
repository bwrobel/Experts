using System.Collections.Generic;
using Experts.Core.Entities;

namespace Experts.Web.Models.Threads
{
    public class ThreadDetailsMenuModel
    {
        public Thread Thread { get; set; }

        public Post Post { get; set; }
        
        public ICollection<MenuButtons> ActiveButtons{get;set;}

        public ThreadDetailsMenuModel(){}

        public ThreadDetailsMenuModel(Thread thread,ICollection<MenuButtons> activeButtons, Post post)
        {
            Thread = thread;
            ActiveButtons = activeButtons;
            Post = post;
        }

        public enum MenuButtons
        {
            Answer,
            DetailsRequest,
            Accept,
            Occupy,
            ReportIssue,
            GiveUp,
            AttachFile,
            PriceProposal,
            Delete,
            Pay,
            ReleaseReservedQuestion,
            ProposeAdditionalService,
            ModeratorAnswer,
            ExpertPM,
            Events,
            IncreaseExpertValue
        }
    }
}