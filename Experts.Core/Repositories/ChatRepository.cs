using System;
using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class ChatRepository : AuditableRepository<Chat>
    {
        public ChatRepository(DataContext db)
            :base(db)
        {}


        public override void Add(Chat chat)
        {
            chat.IsClosed = false;
            chat.LastReadTime = DateTime.Now;

            base.Add(chat);
        }
    }

    public static class ChatQueryExtensions
    {
        public static IQueryable<Chat> ByOwner(this IQueryable<Chat> query, User owner)
        {
            return query.Where(cm => cm.Owner.Id == owner.Id);
        }

        public static IQueryable<Chat> ByIsClosed(this IQueryable<Chat> query, bool isClosed)
        {
            return query.Where(cm => cm.IsClosed == isClosed);
        }

        public static IQueryable<Chat> ByWaitingForResponse(this IQueryable<Chat> query)
        {
            return query.Where(cm => (from m in cm.Messages
                                      orderby m.Id descending
                                      select m.Author).FirstOrDefault() == cm.Owner);
        }

        
    }
}
