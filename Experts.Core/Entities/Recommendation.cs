using System.Text.RegularExpressions;
using Experts.Core.Utils;

namespace Experts.Core.Entities
{
    public class Recommendation : IEntity
    {
        public int Id { get; set; }

        public bool DidExpertRegister { get; set; }

        public bool DidRecommenderReceiveBonus { get; set; }

        public string RecommendationKey { get; set; }

        public virtual User Recommender { get; set; }

        public string RecommenderName { get; set; }
        public string RecommenderSurname { get; set; }
        public string RecommendedExpertComment { get; set; }
        public string RecommenderEmail { get; set; }

        public string RecommendedExpertEmail { get; set; }
        public string RecommendedExpertName { get; set; }
        public string RecommendedExpertSurname { get; set; }
        public virtual Category RecommendedCategory { get; set; }

        public void GenerateRecommendationKey()
        {
            RecommendationKey = GenerateKey();
        }

        private string GenerateKey()
        {
            var key = CryptoHelper.CreateSalt();
            return Regex.Replace(key, @"\W", string.Empty);
        }
    }
}
