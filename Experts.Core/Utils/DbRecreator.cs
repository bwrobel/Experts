using System;
using System.Configuration;
using DataFresh;
using System.IO;

namespace Experts.Core.Utils
{
    public static class DatabaseUtil
    {
        public static void PrepareDataFresh()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;

            CheckDbConnection(connectionString);

            var dataFresh = new SqlDataFresh(connectionString);
            dataFresh.SnapshotPath = new DirectoryInfo(@"C:\experts_snapshots\");
            dataFresh.PrepareDatabaseforDataFresh(true);
        }

        public static void RefreshDataFresh()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;

            CheckDbConnection(connectionString);

            var dataFresh = new SqlDataFresh(connectionString);
            dataFresh.SnapshotPath = new DirectoryInfo(@"C:\experts_snapshots\");
            dataFresh.RefreshTheDatabase();
        }

        private static void CheckDbConnection(string connectionString)
        {
            //if (!connectionString.Contains(@".\SQLEXPRESS"))
            //    throw new Exception("Cannot execute on non local database");
        }
    }
}
