using System;
using System.Collections.Generic;
using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class QueuedEmailRepository:EntityRepository<QueuedEmail>
    {
        public QueuedEmailRepository(DataContext db) : base(db)
        { 
        }

        public QueuedEmail GetFirstToSend()
        {
            return Db.QueuedEmails
                     .Where(qe => qe.IntStatus == (int)QueuedEmailStatus.Queued || qe.IntStatus == (int)QueuedEmailStatus.ReQueued)
                     .FirstOrDefault(qe => qe.SendTime < DateTime.Now);
        }
    }
}
