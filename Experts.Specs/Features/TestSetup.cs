using Experts.Specs.Helpers;
using NUnit.Framework;
using WatiN.Core;

namespace Experts.Specs.Features
{
    [SetUpFixture]
    public class TestSetup
    {
        [SetUp]
        public void OpenBrowser()
        {
            WebBrowser.Current = new IE();
            Settings.AttachToBrowserTimeOut = 20;
        }

        [TearDown]
        public void CloseBorwser()
        {
            WebBrowser.Current.Close();
        }
    }
}
