using WatiN.Core;

namespace Experts.Specs.Helpers
{
    public static class ElementExtensions
    {
        public static bool IsDisplayed(this Element element)
        {
            if (element.Style.Display == "none")
                return false;

            if (element.Parent != null)
                return IsDisplayed(element.Parent);

            return true;
        }
    }
}
