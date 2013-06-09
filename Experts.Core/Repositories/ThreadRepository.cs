using System;
using System.Collections.Generic;
using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;
using Experts.Core.ViewModels;

namespace Experts.Core.Repositories
{
    public class ThreadRepository : EntityRepository<Thread>
    {
        public const int ThreadAutoCloseIntervalInDays = 2;

        public ThreadRepository(DataContext db)
            : base(db)
        {
        }
        
        public override void Add(Thread thread)
        {
            thread.SetDatesToNow();

            foreach (var post in thread.Posts)
                post.SetDatesToNow();

            base.Add(thread);
        }

        public override void Update(Thread entity)
        {
            UpdatePostsReadOnlyStatus(entity);
            entity.UpdateModificationDate();
            base.Update(entity);
        }

        public void AddPost(Thread thread, Post post)
        {
            post.SetDatesToNow();
            thread.UpdateModificationDate();
            thread.Posts.Add(post);

            Update(thread);
        }

        public void DeletePost(Post post)
        {
            post.Thread.UpdateModificationDate();
            var thread = post.Thread;

            Db.Posts.Remove(post);
            Db.SaveChanges();

            Update(thread);
        }

        public void DeletePriceProposal(PriceProposal priceProposal)
        {
            var thread = priceProposal.Thread;

            Db.PriceProposals.Remove(priceProposal);
            Db.SaveChanges();

            Update(thread);
        }
        
        public void DeleteAttachment(Attachment attachment)
        {
            attachment.Post.UpdateModificationDate();
            attachment.Post.Thread.UpdateModificationDate();

            Db.Attachments.Remove(attachment);
            Db.SaveChanges();
        }

        public Attachment GetAttachment(int attachmentId)
        {
            return Db.Attachments.Find(attachmentId);
        }

        public PriceProposal GetPriceProposal(int priceProposalId)
        {
            return Db.PriceProposals.Find(priceProposalId);
        }

        public Post GetPost(int postId)
        {
            return Db.Posts.Find(postId);
        }

        public AdditionalService GetAdditionalService(int additionalServiceId)
        {
            return Db.AdditionalServices.Find(additionalServiceId);
        }

        public void UpdateThreadByAdditionalService(AdditionalService additionalService)
        {
            var thread = additionalService.Thread;

            UpdatePostsReadOnlyStatus(thread);
            thread.UpdateModificationDate();
            base.Update(thread);
        }

        //public void AcceptAdditionalService(int additionalServiceId)
        //{
        //    var additionalService = Db.AdditionalServices.Find(additionalServiceId);
        //    var thread = additionalService.Thread;

        //    additionalService.LastModificationDate = DateTime.Now;
        //    additionalService.IsAccepted = true;

        //    thread.Value += additionalService.Value;

        //    Update(thread);
        //}

        public void DeclineAdditionalService(int additionalServiceId)
        {
            var additionalService = Db.AdditionalServices.Find(additionalServiceId);
            var thread = additionalService.Thread;

            additionalService.LastModificationDate = DateTime.Now;
            additionalService.IsAccepted = false;

            Update(thread);
        }

        public void AddFeedback(Thread thread, Feedback feedback)
        {
            feedback.Thread = thread;
            feedback.SetDatesToNow();

            thread.Feedback = feedback;
            Update(thread);
        }

        public void AddFeedbackComment(Feedback feedback, string comment)
        {
            feedback.Comment = comment;
            feedback.UpdateModificationDate();

            Update(feedback.Thread);
        }

        public void AddIssue(Thread thread, ThreadIssue issue)
        {
            issue.SetDatesToNow();

            thread.Issues.Add(issue);
            Update(thread);
        }

        public void AddPriceProposal(Thread thread, PriceProposal priceProposal)
        {
            priceProposal.SetDatesToNow();

            thread.PriceProposals.Add(priceProposal);
            thread.UpdateModificationDate();
            Update(thread);
        }

        public override void Delete(Thread entity)
        {
            foreach (var post in entity.Posts.ToList())
                Db.Posts.Remove(post);

            foreach (var threadIssue in entity.Issues.ToList())
                Db.ThreadIssues.Remove(threadIssue);

            foreach (var priceProposal in entity.PriceProposals.ToList())
                Db.PriceProposals.Remove(priceProposal);

            if (entity.Feedback != null)
                Db.Feedbacks.Remove(entity.Feedback);
            
            base.Delete(entity);
        }

        public IEnumerable<Thread> FindClosestMatches(Category category, IEnumerable<CategoryAttributeValue> attributeValues, int limit, Func<IQueryable<Thread>, IQueryable<Thread>> additionalQuery = null)
        {
            var selectedOptionsIds = attributeValues.SelectMany(ca => ca.SelectedOptions.Select(so => so.Id));

            var queryable = additionalQuery != null ? additionalQuery(Db.Threads.AsQueryable()) : Db.Threads.AsQueryable();

            var query = from thread in queryable
                        where thread.Category.Id == category.Id
                        orderby 
                                 thread.CategoryAttributes
                                 .SelectMany(ca => ca.SelectedOptions.Where(so => selectedOptionsIds.Contains(so.Id)))
                                 .Sum(so => so.Attribute.Importance)
                        descending 
                        select thread;

            return query.Take(limit).ToList();
        }

        public IEnumerable<ThreadWithMatch> FindThreadsWithMatches(Expert expert, int itemsPerPage = int.MaxValue, int page = 1, Func<IQueryable<Thread>, IQueryable<Thread>> additionalQuery = null, Func<ThreadWithMatch, IComparable> order = null, bool ascending = true)
        {
            var selectedOptionsIds =
                expert.CategoryAttributes.SelectMany(ca => ca.CategoryAttributes).SelectMany(
                    ca => ca.SelectedOptions.Select(so => so.Id));


            var queryable = additionalQuery != null ? additionalQuery(Db.Threads.AsQueryable()) : Db.Threads.AsQueryable();

            var query = from thread in queryable
                        let maxImportance = thread.CategoryAttributes.Any(ca => ca.SelectedOptions.Any())
                                            ? thread.CategoryAttributes.SelectMany(ca => ca.SelectedOptions).Sum(ca => ca.Attribute.Importance)
                                            : 0
                        let expertCoverage =
                            thread.CategoryAttributes.SelectMany(
                                ca => ca.SelectedOptions.Where(so => selectedOptionsIds.Contains(so.Id))).Sum(
                                    so => so.Attribute.Importance)
                        let categoryMaxImportance = thread.Category.Attributes.Sum(a => a.Importance * a.Options.Count)
                        let categoryExpertCoverage = thread.Category.Attributes.SelectMany(a => a.Options).Where(o => selectedOptionsIds.Contains(o.Id)).Sum(o => o.Attribute.Importance)
                        let defaultCategoryMatch = categoryExpertCoverage == null ? 0 : (double)categoryExpertCoverage/categoryMaxImportance
                        let match = expertCoverage == null || maxImportance == 0 ? defaultCategoryMatch : (double)expertCoverage / maxImportance
                        select new ThreadWithMatch
                                   {
                                       Thread = thread,
                                       Match = match
                                   };


            var ordered = query.OrderBy(t => t.Match).AsEnumerable();
            if (order != null)
                ordered = ascending ? query.OrderBy(order) : query.OrderByDescending(order);

            return ordered
                .Skip((page - 1)*itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
        }

        private void UpdatePostsReadOnlyStatus(Thread thread)
        {
            var posts = thread.Posts;
            foreach (var post in posts)
            {
                if (thread.State == ThreadState.Closed
                    || (thread.State == ThreadState.Accepted && post.CreationDate < thread.ThreadAcceptanceDate))
                {
                    post.IsReadOnly = true;
                }
                else
                {
                    post.IsReadOnly = false;

                    var newer = posts.Where(p => p != post && p.CreationDate > post.CreationDate && p.Type != PostType.Hidden);
                    if (newer.Any(p => p.Author != post.Author || (p.Author == post.Author && p.Type == PostType.GiveUp)))
                        post.IsReadOnly = true;
                }
            }
        }
    }

    public static class ThreadQueryExtensions
    {
        public static IQueryable<Thread> ByCategories(this IQueryable<Thread> query, IEnumerable<Category> categories)
        {
            var categoryIds = categories.Select(c => c.Id).ToList();
            return query.Where(t => categoryIds.Contains(t.Category.Id));
        }

        public static IQueryable<Thread> ByCategory(this IQueryable<Thread> query, Category category)
        {
            return query.Where(t => t.Category.Id == category.Id);
        }

        public static IQueryable<Thread> ByPostId(this IQueryable<Thread> query, int postId)
        {
            return query.Where(t => t.Posts.Any(p => p.Id == postId));
        }

        public static IQueryable<Thread> ByPostCount(this IQueryable<Thread> query, int postCount)
        {
            return query.Where(t => t.Posts.Count() == postCount);
        }

        public static IQueryable<Thread> WithoutAuthor(this IQueryable<Thread> query, User author)
        {
            return query.Where(t => t.Author.Id != author.Id);
        }

        public static IQueryable<Thread> ByAuthor(this IQueryable<Thread> query, User author)
        {
            return query.Where(t => t.Author.Id == author.Id);
        }

        public static IQueryable<Thread> ByExpert(this IQueryable<Thread> query, Expert expert)
        {
            return query.Where(q => q.Expert.Id == expert.Id);
        }

        public static IQueryable<Thread> ByExpertReleaseDateExpiration(this IQueryable<Thread> query)
        {
            return query.Where(q => q.ExpertReleaseDate < DateTime.Now);
        }

        public static IQueryable<Thread> ByIfThreadAcceptedDate(this IQueryable<Thread> query)
        {
            return query.Where(q => q.ThreadAcceptanceDate != null);
        }

        public static IQueryable<Thread> ByThreadWaitForAcceptance(this IQueryable<Thread> query)
        {
            var dueDate = DateTime.Now.AddDays(-ThreadRepository.ThreadAutoCloseIntervalInDays);
            return query.Where(q => q.ThreadAcceptanceDate < dueDate);
        }

        public static IQueryable<Thread> BySanitizationStatus(this IQueryable<Thread> query, ThreadSanitizationStatus? sanitizationStatus)
        {
            if (sanitizationStatus == null)
                return query;

            return query.Where(q => q.IntSanitizationStatus == (int)sanitizationStatus);
        }

        public static IQueryable<Thread> ByState(this IQueryable<Thread> query, params ThreadState[] threadStates)
        {
            var threadIntStates = threadStates.Select(t => (int)t).ToList();
            return query.Where(q => threadIntStates.Contains(q.IntState));
        }

        public static IQueryable<Thread> ByPaymentStatus(this IQueryable<Thread> query, bool isPaid)
        {
            return query.Where(s => s.IsPaid == isPaid);
        }

        public static IQueryable<Thread> ByInnerStatus(this IQueryable<Thread> query, bool showInner)
        {
            if (showInner)
                return query.Where(s => s.IsInner || s.IsInner == false);

            return query.Where(s => s.IsInner == false);
        }

        public static IQueryable<Thread> ByElapsedHours(this IQueryable<Thread> query, int hours)
        {
            var borderDate = DateTime.Now - TimeSpan.FromHours(hours);
            return query.Where(q => q.CreationDate < borderDate);
        }

        public static IQueryable<Thread> ByNotOccupied(this IQueryable<Thread> query)
        {
            return query.Where(q => q.Expert == null);
        }

        public static IQueryable<Thread> ByNotificationStatus(this IQueryable<Thread> query, bool isNotified)
        {
            return query.Where(q => q.IsNotified == isNotified);
        }

        public static IQueryable<Thread> ByOutgoingStates(this IQueryable<Thread> query)
        {
            return query.Where(q => q.IntState == (int)ThreadState.Occupied && q.IntState == (int)ThreadState.Reserved);
        }

        public static IQueryable<Thread> ByNotHidden(this IQueryable<Thread> query)
        {
            return query.Where(q => q.IntState != (int)ThreadState.Hidden);
        }

        public static IQueryable<Thread> ByExpertResponseNotificationStatus(this IQueryable<Thread> query, bool isNotified)
        {
            return query.Where(q => q.IsExpertResponseNotified == isNotified);
        }
    }
}
