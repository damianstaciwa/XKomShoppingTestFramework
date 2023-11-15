using AventStack.ExtentReports;
using OpenQA.Selenium;
using XKomShoppingTestFramework.Base;

namespace XKomShoppingTestFramework.Pages
{
    public class HomePage : BasePage
    {
        protected ExtentTest _test;

        public HomePage(IWebDriver driver, ExtentTest test) : base(driver, test)
        {
            _test = test;
        }
    }
}
