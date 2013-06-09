using System;
using System.Web;
using Experts.Core.Repositories;

namespace Experts.Core.Utils
{
    public static class RepositoryHelper
    {
        [ThreadStatic]
        private static RepositoryFactory _repositoryFactory;

        private const string RepositoryFactoryKey = "RepositoryFactory";

        public static RepositoryFactory GetDbRepository(this HttpContext context)
        {
            if (!HttpContext.Current.Items.Contains(RepositoryFactoryKey))
                HttpContext.Current.Items.Add(RepositoryFactoryKey, new RepositoryFactory());

            return (RepositoryFactory)HttpContext.Current.Items[RepositoryFactoryKey];
        }

        private static RepositoryFactory ThreadLocalRepository
        {
            get
            {
                if (_repositoryFactory == null)
                    _repositoryFactory = new RepositoryFactory();

                return _repositoryFactory;
            }
        }

        public static RepositoryFactory Repository
        {
            get
            {
                return HttpContext.Current == null ? ThreadLocalRepository : HttpContext.Current.GetDbRepository();
            }
        }
    }
}