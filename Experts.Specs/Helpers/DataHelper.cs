using System.IO;

namespace Experts.Specs.Helpers
{
    public static class DataHelper
    {
        private const string DataPath = @"\..\..\Data\";

        public static string GetPath(string filename)
        {
            var basePath = Directory.GetCurrentDirectory();
            return basePath + DataPath + filename;
        }
    }
}
