using System.ComponentModel.DataAnnotations;
using Experts.Core.Repositories;
using Experts.Core.Entities;

namespace Experts.Web.Validation
{
    public class IsKeywordUniqueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var keyPhrase = value as string;

            using (var rf = new RepositoryFactory())
                return rf.SEOKeyword.Find(sq => sq.Phrase == keyPhrase && sq.IntStatus != (int)SEOKeywordStatus.New) == null;
        }
    }
}