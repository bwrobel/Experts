using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class DbRepository
    {
        protected readonly DataContext Db;

        public DbRepository(DataContext db)
        {
            Db = db;
        }
    }

    public abstract class EntityRepository<T> : DbRepository
        where T : class, IEntity
    {
        protected EntityRepository(DataContext db) : base(db)
        {
        }

        public virtual void Add(T entity)
        {
            Db.Set<T>().Add(entity);
            Db.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Db.Set<T>().Remove(entity);
            Db.SaveChanges();
        }

        public virtual T Get(int id)
        {
            return Db.Set<T>().Find(id);
        }

        protected virtual IQueryable<T> OrderedRows
        {
            get { return Db.Set<T>(); }
        }

        public virtual IEnumerable<T> All()
        {
            return OrderedRows.ToList();
        }

        public virtual int Count(Expression<Func<T, bool>> query)
        {
            return Db.Set<T>().Count(query);
        }

        public virtual int Count(Func<IQueryable<T>, IQueryable<T>> query = null)
        {
            var results = Db.Set<T>().AsQueryable();
            if (query != null)
                results = query(results);

            return results.Count();
        }

        public virtual T Find(Expression<Func<T, bool>> query)
        {
            return OrderedRows.SingleOrDefault(query);
        }

        public virtual IEnumerable<T> Find(int itemsPerPage = int.MaxValue, int page = 1, Func<IQueryable<T>, IQueryable<T>> query = null, Func<T, object> order = null, bool ascending = true, Func<T, IComparable> orderThen = null, bool thenAscending = true)
        {
            var queryResult = OrderedRows;
            if (query != null)
                queryResult = query(queryResult);

            if (order == null)
                queryResult = queryResult.OrderBy(q => q.Id);
            else
            {
                queryResult = (ascending ? queryResult.OrderBy(order) : queryResult.OrderByDescending(order)).AsQueryable();
                if (orderThen != null)
                    queryResult = (thenAscending ? queryResult.OrderBy(orderThen) : queryResult.OrderByDescending(orderThen)).AsQueryable();
            }

            return queryResult
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
        }
    }

    public class RepositoryFactory : IDisposable
    {
        private readonly DataContext _db;
        private UserRepository _userRepository;
        private ThreadRepository _threadRepository;
        private CategoryRepository _categoryRepository;
        private ExpertRepository _expertRepository;
        private PaymentRepository _paymentRepository;
        private OpinionRepository _opinionRepository;
        private PartnerRepository _partnerRepository;
        private SEOKeywordRepository _seoKeywordRepository;
        private QueuedEmailRepository _queuedEmailRepository;
        private EventRepository _eventRepository;
        private ChatRepository _chatRepository;
        private TransferRepository _transferRepository;
        private ModeratorRepository _moderatorRepository;
        private SubscriptionRepository _subscriptionRepository;
        private RecommendationRepository _recommendationRepository;
        private ProvisionRepository _provisionRepository;
        private FeedbackRepository _feedbackRepository;
        private ConsultantRepository _consultantRepository;
        private AdCampaignLandingPageRepository _adCampaignLandingPageRepository;

        public RepositoryFactory()
        {
            _db = new DataContext();
        }

        public UserRepository User
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_db)); }
        }

        public ThreadRepository Thread
        {
            get { return _threadRepository ?? (_threadRepository = new ThreadRepository(_db)); }
        }

        public CategoryRepository Category
        {
            get { return _categoryRepository ?? (_categoryRepository = new CategoryRepository(_db)); }
        }

        public ExpertRepository Expert
        {
            get { return _expertRepository ?? (_expertRepository = new ExpertRepository(_db)); }
        }

        public OpinionRepository Opinion
        {
            get { return _opinionRepository ?? (_opinionRepository = new OpinionRepository(_db)); }
        }

        public PaymentRepository Payment
        {
            get { return _paymentRepository ?? (_paymentRepository = new PaymentRepository(_db)); }
        }

        public PartnerRepository Partner
        {
            get { return _partnerRepository ?? (_partnerRepository = new PartnerRepository(_db)); }
        }

        public SEOKeywordRepository SEOKeyword
        {
            get { return _seoKeywordRepository ?? (_seoKeywordRepository = new SEOKeywordRepository(_db)); }
        }

        public QueuedEmailRepository QueuedEmail
        {
            get { return _queuedEmailRepository ?? (_queuedEmailRepository = new QueuedEmailRepository(_db)); }
        }

        public EventRepository Event
        {
            get { return _eventRepository ?? (_eventRepository = new EventRepository(_db)); }
        }

        public ChatRepository Chat
        {
            get { return _chatRepository ?? (_chatRepository = new ChatRepository(_db)); }
        }

        public TransferRepository Transfer
        {
            get { return _transferRepository ?? (_transferRepository = new TransferRepository(_db)); }
        }

        public ModeratorRepository Moderator
        {
            get { return _moderatorRepository ?? (_moderatorRepository = new ModeratorRepository(_db)); }
        }

        public SubscriptionRepository Subscription
        {
            get { return _subscriptionRepository ?? (_subscriptionRepository = new SubscriptionRepository(_db)); }
        }

        public RecommendationRepository Recommendation
        {
            get { return _recommendationRepository ?? (_recommendationRepository = new RecommendationRepository(_db)); }
        }

        public ProvisionRepository Provision
        {
            get { return _provisionRepository ?? (_provisionRepository = new ProvisionRepository(_db)); }
        }

        public FeedbackRepository Feedback
        {
            get { return _feedbackRepository ?? (_feedbackRepository = new FeedbackRepository(_db)); }
        }

        public ConsultantRepository Consultant
        {
            get { return _consultantRepository ?? (_consultantRepository = new ConsultantRepository(_db)); }
        }

        public AdCampaignLandingPageRepository AdCampaignLandingPage
        {
            get { return _adCampaignLandingPageRepository ?? (_adCampaignLandingPageRepository = new AdCampaignLandingPageRepository(_db)); }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
