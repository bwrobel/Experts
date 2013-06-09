using System;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Quartz;

namespace Experts.Web.Jobs
{
    public class NoExpertResponseEmailJob : JobWithErrorHandling
    {
        public override  void DoJob(IJobExecutionContext context)
        {
            var repository = RepositoryHelper.Repository;

            var elapsedThreads = repository.Thread.Find(query: q => q.ByElapsedHours(24).ByNotOccupied().ByExpertResponseNotificationStatus(false));
            foreach (var thread in elapsedThreads)
            {
                var priceProposalsText = Resources.Thread.PreviousPriceProposals + "<br />";
                var count = 0;
                foreach (var priceProposal in thread.PriceProposals)
                {
                    if (priceProposal.VerificationStatus == PriceProposalVerificationStatus.Verified)
                    {
                        count += 1;
                        priceProposalsText += count.ToString() + ". " + priceProposal.Expert.PublicName +
                                              Resources.Thread.PriceProposalOffered + " '" +
                                              priceProposal.Comment + "' " +
                                              Resources.Thread.PriceProposalFor +
                                              priceProposal.SurchargeValue +
                                              "<br />";
                    }
                }

                if (thread.PriceProposals.Count == 0)
                    priceProposalsText += Resources.Thread.NoNewPriceProposals + "<br />";

                Email.Send<NoExpertResponseEmail>(thread, priceProposalsText);
                Log.Event<NoAnswerEvent>(thread);
                thread.IsExpertResponseNotified = true;
                repository.Thread.Update(thread);
            }
        }

    }

}