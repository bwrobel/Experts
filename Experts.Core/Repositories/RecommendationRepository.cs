using Experts.Core.Data;
using Experts.Core.Entities;
using Experts.Core.Exceptions;

namespace Experts.Core.Repositories
{
    public class RecommendationRepository:EntityRepository<Recommendation>
    {
        public RecommendationRepository(DataContext db)
            : base(db)
        {
        }

        public Recommendation FindByRecommenderEmail(string email)
        {
            var recommendation = Find(r => r.RecommenderEmail == email);
            return recommendation;
        }

        public Recommendation ConfirmExpertRegistration(string recommendationKey)
        {
            var recommendation = CheckRecommendationKey(recommendationKey);

            recommendation.DidExpertRegister = true;
            Update(recommendation);

            return recommendation;
        }

        public Recommendation CheckRecommendationKey(string recommendationKey)
        {
            var recommendation = FindByRecommendationKey(recommendationKey);

            if (recommendation.DidExpertRegister)
                throw new ExpertAlreadyRegisteredException();

            return recommendation;
        }

        private Recommendation FindByRecommendationKey(string recommendationKey)
        {
            var recommendation = Find(r => r.RecommendationKey == recommendationKey);

            if (recommendation == null)
                throw new RecommendationNotFoundException();

            return recommendation;
        }
    }
}
