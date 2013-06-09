using Experts.Web.Models.Shared;

namespace Experts.Web.Helpers
{
    public static class PagerHelper
    {
        public static Pagination Pagination(int? currentPage, int quantity)
        {
            return new Pagination
            {
                CurrentPageNumber = currentPage ?? 1,
                TotalPages = (quantity / PageSize) + 1,
            };
        }

        public const int PageSize = 10;

        public const int KeywordTilesSize = 50;

        public const int SimiliarExpertsListSize = 5;

        public const int SimiliarThreadsListSize = 12;

        public const int ExpertOverviewFeedbackLimit = 50;
        public const int FeedbackPageSize = 5;

        public const int ExpertOverviewAnsweredQuestionsLimit = 100;
        public const int AnsweredQuestionsPageSize = 10;

        public const int SeoSimiliarThreadsLimit = 5;
        public const int SeoSimiliarExpertsLimit = 3;
    }
}