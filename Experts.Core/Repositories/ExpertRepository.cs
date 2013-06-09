using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;
using Experts.Core.Utils;
using Experts.Core.ViewModels;
using System;

namespace Experts.Core.Repositories
{
    public class ExpertRepository : AuditableRepository<Expert>
    {
        public ExpertRepository(DataContext db)
            : base(db)
        {
        }

        public override void Add(Expert expert)
        {
            base.Add(expert);
            UpdateExpertStats(expert);
        }

        public void UpdateExpertStats(Expert expert)
        {
            var positiveAnswers = 0;
            var negativeAnswers = 0;
            var acceptedAnswers = 0;
            if (expert.Answers == null) expert.Answers = new Collection<Thread>();
            foreach (var answer in expert.Answers)
            {
                if (answer.Feedback != null)
                {
                    if (answer.Feedback.Mark == FeedbackMark.Positive) positiveAnswers += 1;
                    if (answer.Feedback.Mark == FeedbackMark.Negative) negativeAnswers += 1;
                }
                
                acceptedAnswers++;
            }
            expert.AcceptedAnswers = acceptedAnswers;

            var allAnswers = expert.Answers.Count();
            expert.PositiveFeedback = allAnswers == 0
                ? 0.0
                : (double) positiveAnswers / allAnswers;
            
            Update(expert);
        }

        public ExpertOverviewViewModel GetExpertViewModel(int expertId, string boxTitle = null, int? threadCategoryId = null, bool showVerificationDetails = true, bool isEmbedded = false)
        {
            var expert = Get(expertId);

            var model = new ExpertOverviewViewModel
                {
                    Id = expertId,
                    PublicName = expert.PublicName,
                    User = expert.User,
                    PhoneNumber = expert.PhoneNumber,
                    Category = expert.Categories.First().Name,
                    CategoryId = expert.Categories.First().Id,
                    IsVerified = expert.IsVerified,
                    Position = expert.Microprofiles.First().Position,
                    Description = expert.Microprofiles.First().Description,
                    Threads =
                        Db.Threads.Where(t => t.Expert.Id == expertId && t.ThreadAcceptanceDate != null)
                          .OrderByDescending(t => t.CreationDate),
                    Feedbacks =
                        Db.Feedbacks.Where(f => f.Thread.Expert.Id == expertId).OrderByDescending(f => f.CreationDate),
                    PositivePercentage = expert.PositiveFeedback*100,
                    VerificationDetails = expert.VerificationDetails,
                    BoxTitle = boxTitle,
                    ShowVerificationDetails = showVerificationDetails,
                    IsEmbedded = isEmbedded,
                    ThreadCategoryId = threadCategoryId ?? 0
                };

            model.ExpertOverviewCharts = new ExpertOverviewCharts
                                             {
                                                 FeedbackStatistic = GetFeedbackStatistics(expert.Id),
                                                 AnswerStatistic = GetAnswerStatistics(expert.Id),
                                                 PaymentStatistic = new UserRepository(Db).GetPaymentStatistics(expert.Id)
                                             };

            model.AcceptedAnswers = model.Threads.Count();
            model.AcceptedAnswersPerMonth = model.Threads.Count(t => t.CreationDate.Month == DateTime.Now.Month);

            model.PositiveCount = model.Feedbacks.Count(f => f.Mark == FeedbackMark.Positive);
            model.NeutralCount = model.Feedbacks.Count(f => f.Mark == FeedbackMark.Neutral);
            model.NegativeCount = model.Feedbacks.Count(f => f.Mark == FeedbackMark.Negative);

            return model;
        }

        public ExpertMonthlyStatistics GetExpertMonthlyStatistics(int expertId)
        {
            var month = StatisticsHelper.GetLastMonthNumber();
            var year = month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year;

            var expert = Get(expertId);
            var expertThreads =
                Db.Threads.Where(t => t.Expert.Id == expertId && 
                    t.ThreadAcceptanceDate != null && 
                    t.CreationDate.Year == year && 
                    t.CreationDate.Month == month).OrderByDescending(
                    t => t.CreationDate).ToList();

            var expertFeedbacks =
                Db.Feedbacks.Where(f => f.Thread.Expert.Id == expertId && 
                    f.CreationDate.Year == year && 
                    f.CreationDate.Month == month).OrderByDescending(
                    f => f.CreationDate).ToList();

            var payments = new UserRepository(Db).GetPaymentStatistics(expert.Id);

            var model = new ExpertMonthlyStatistics
            {
                AcceptedAnswersPerMonth = expertThreads.Count(),

                PositiveFeedbackPerMonth = expertFeedbacks.Count(f => f.Mark == FeedbackMark.Positive),
                NeutralFeedbackPerMonth = expertFeedbacks.Count(f => f.Mark == FeedbackMark.Neutral),
                NegativeFeedbackPerMonth = expertFeedbacks.Count(f => f.Mark == FeedbackMark.Negative),

                Balance = payments.Single(p => p.Month == month).Value
            };

            return model;
        }

        public IEnumerable<Feedback> FindFeedbacks(Expert expert)
        {
            return Db.Feedbacks.Where(f => f.Thread.Expert == expert).ToList();
        }

        public Feedback FindFeedback(int id)
        {
            return Db.Feedbacks.SingleOrDefault(f => f.Id == id);
        }

        private IEnumerable<AnswerStatistic> GetAnswerStatistics(int expertId)
        {
            var list = Db.Threads.Where(t => t.Expert.Id == expertId && t.IntState != (int)ThreadState.Accepted && t.IntState != (int)ThreadState.Reserved && (t.Posts.FirstOrDefault().CreationDate.Year == DateTime.Now.Year || t.Posts.FirstOrDefault().CreationDate.Year == DateTime.Now.Year - 1))
                            .GroupBy(t => new { t.Posts.FirstOrDefault().CreationDate.Month, t.IntState })
                            .Select(g => new AnswerStatistic() { Count = g.Count(),
                                                                 IntState = (g.Key.IntState == (int)ThreadState.Closed || g.Key.IntState == (int)ThreadState.Accepted) ? (int)AnswerStatistic.AnswerStatisticState.Accepted : (int)AnswerStatistic.AnswerStatisticState.Occupied,
                                                                 Month = g.Key.Month }).ToList();
            
            var givenUpList = Db.Posts.Where(p => p.Author.Expert.Id == expertId && (p.CreationDate.Year == DateTime.Now.Year || p.CreationDate.Year == DateTime.Now.Year - 1) && (PostType)p.IntType == PostType.GiveUp)
                .GroupBy(p => p.CreationDate.Month)
                .Select(g => new AnswerStatistic()
                {
                    Count = g.Count(),
                    IntState = (int)AnswerStatistic.AnswerStatisticState.GivenUp,
                    Month = g.Key
                }).ToList();

            for (var i = 1; i <= 12; i++)
            {
                if (!list.Any(t => t.Month == i && t.State == AnswerStatistic.AnswerStatisticState.Accepted))
                    list.Add(new AnswerStatistic {Month = i, State = AnswerStatistic.AnswerStatisticState.Accepted, Count = 0});

                if (!list.Any(t => t.Month == i && t.State == AnswerStatistic.AnswerStatisticState.Occupied))
                    list.Add(new AnswerStatistic {Month = i, State = AnswerStatistic.AnswerStatisticState.Occupied, Count = 0});

                if (!givenUpList.Any(t => t.Month == i && t.State == AnswerStatistic.AnswerStatisticState.GivenUp))
                    list.Add(new AnswerStatistic  {Month = i, State = AnswerStatistic.AnswerStatisticState.GivenUp, Count = 0});
            }

            return (list.Concat(givenUpList)).OrderBy(p => p.Month);
        }

        private IEnumerable<FeedbackStatistic> GetFeedbackStatistics(int expertId)
        {
            var list = Db.Feedbacks.Where(f => f.Thread.Expert.Id == expertId && (f.CreationDate.Year == DateTime.Now.Year || f.CreationDate.Year == DateTime.Now.Year - 1))
                            .GroupBy(f => new { f.CreationDate.Month, f.FeedbackMarkInt })
                            .Select(g => new FeedbackStatistic() { Count = g.Count(), Mark = (FeedbackMark)g.Key.FeedbackMarkInt, Month = g.Key.Month }).ToList();

            for (var i = 1; i <= 12; i++)
            {
                if (!list.Any(f => f.Month == i && f.Mark == FeedbackMark.Positive))
                {
                    list.Add(new FeedbackStatistic() { Month = i, Mark = FeedbackMark.Positive, Count = 0 });
                }

                if (!list.Any(f => f.Month == i && f.Mark == FeedbackMark.Negative))
                {
                    list.Add(new FeedbackStatistic() { Month = i, Mark = FeedbackMark.Negative, Count = 0 });
                }

                if (!list.Any(f => f.Month == i && f.Mark == FeedbackMark.Neutral))
                {
                    list.Add(new FeedbackStatistic() { Month = i, Mark = FeedbackMark.Neutral, Count = 0 });
                }
            }

            return list.OrderBy(f => f.Month);
        }

        public IEnumerable<Expert> FindClosestMatches(int categoryId, IEnumerable<CategoryAttributeValue> attributeValues, int limit, Func<IQueryable<Expert>, IQueryable<Expert>> additionalQuery = null)
        {
            var selectedOptionsIds = attributeValues.SelectMany(ca => ca.SelectedOptions.Select(so => so.Id));

            var queryable = additionalQuery != null ? additionalQuery(OrderedRows) : OrderedRows;

            var query = from expert in queryable
                        where expert.Categories.Select(c => c.Id).Contains(categoryId)
                        orderby
                                 expert.CategoryAttributes.FirstOrDefault(ca => ca.Category.Id == categoryId).CategoryAttributes
                                 .SelectMany(eca => eca.SelectedOptions.Where(so => selectedOptionsIds.Contains(so.Id)))
                                 .Sum(so => so.Attribute.Importance)
                        descending
                        select expert;

            return query.ByPublic().Take(limit).ToList();
        }

        public int GetMatch(Expert expert, int categoryId, IEnumerable<CategoryAttributeValue> attributeValues)
        {
            var selectedOptionsIds = attributeValues.SelectMany(ca => ca.SelectedOptions.Select(so => so.Id));
            var categoryAttribute = expert.CategoryAttributes.FirstOrDefault(ca => ca.Category.Id == categoryId);
            if (categoryAttribute == null)
                return 0;

            return
                categoryAttribute.CategoryAttributes.SelectMany(
                    eca => eca.SelectedOptions.Where(so => selectedOptionsIds.Contains(so.Id)))
                                 .Sum(so => so.Attribute.Importance);
        }

        public void AssignProvisionToExpert(Expert expert, ProvisionExpert provisionExpert)
        {
            var provision = Db.Provisions.Where(p => p.IntProvisionExpert == (int)provisionExpert).SingleOrDefault();
            expert.Provision = provision;
        }
    }

    public static class ExpertQueryExtensions
    {
        public static IQueryable<Expert> ByCategory(this IQueryable<Expert> query, Category category)
        {
            return query.Where(e => e.Categories.Any(c => c.Id == category.Id));
        }

        //public static IQueryable<Expert> ByAccountAge(this IQueryable<Expert> query, )

        public static IQueryable<Expert> ByActivationStatus(this IQueryable<Expert> query, bool activationStatus)
        {
            return query.Where(e => e.User.IsActivated == activationStatus);
        }

        public static IQueryable<Expert> ByInnerStatus(this IQueryable<Expert> query, bool isInner)
        {
            return query.Where(e => e.IsInner == isInner);
        }

        public static IQueryable<Expert> ByVerificationStatus(this IQueryable<Expert> query,
                                                              ExpertVerificationStatus verificationStatus)
        {
            return query.Where(e => e.IntVerificationStatus == (int) verificationStatus);
        }

        public static IQueryable<Expert> ByPublic(this IQueryable<Expert> query)
        {
            return query.ByVerificationStatus(ExpertVerificationStatus.Verified).ByInnerStatus(false);
        }

        public static IQueryable<Expert> ByIds(this IQueryable<Expert> query, IEnumerable<int> ids)
        {
            return query.Where(e => ids.Contains(e.Id));
        }
    }
}
