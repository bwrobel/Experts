using System;
using System.Collections.Generic;
using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;

namespace Experts.Core.Repositories
{
    public class EventRepository:EntityRepository<Event>
    {
        public EventRepository(DataContext db) : base(db)
        {
        }

        public bool HasPendingEvent<TAccountEvent>(User user)
            where TAccountEvent : AccountEvent
        {
            return Db.Set<TAccountEvent>()
                        .Any(e => !e.IsHandled && e.RelatedUser.Id == user.Id);
        }

        public bool HasPendingEvent<TThreadEvent>(Thread thread)
            where TThreadEvent:ThreadEvent
        {
            return Db.Set<TThreadEvent>()
                        .Any(e => !e.IsHandled && e.RelatedThread.Id == thread.Id);
        }

        public SystemFailureEvent GetLastSystemFailure()
        {
            return Db.Set<SystemFailureEvent>().OrderByDescending(e => e.OccurenceDate).FirstOrDefault();
        }

        public IEnumerable<Event> GetThreadEvents(int threadId)
        {
            return Db.Events.Where(e => (e as ThreadEvent).RelatedThread.Id == threadId).OrderBy(e => e.IsHandled).ToList();
        }

        public IEnumerable<Event> GetExpertOverviewEvents(int expertId)
        {
            return
                Db.Events.Where(
                    e =>
                    (e as AccountEvent).RelatedUser.Expert.Id == expertId ||
                    (e as ThreadEvent).RelatedThread.Expert.Id == expertId)
                  .OrderBy(e => e.IsHandled)
                  .ThenByDescending(e => e.OccurenceDate)
                  .ToList();
        }

        public Event GetLastExpertQualificationsChangedEvent(int expertId)
        {
            return Db.Events.Where(e => (e as ExpertQualificationsChangedEvent).RelatedUser.Expert.Id == expertId && !e.IsHandled).OrderByDescending(e => e.OccurenceDate).FirstOrDefault();
        }
    }

    public static class EventQueryExtensions
    {
        public static IQueryable<Event> ByThread(this IQueryable<Event> query, int threadId)
        {
            return query.Where(e => (e as ThreadEvent).RelatedThread.Id == threadId);
        }

        public static IQueryable<Event> ByHideOnMainList(this IQueryable<Event> query, bool hideOnMainList)
        {
            return query.Where(e => e.HideOnMainList == hideOnMainList);
        }
    }
}
