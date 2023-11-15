using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace XKomShoppingTestFramework.Helpers
{
    public static class WaitHelper
    {
        public static void WaitForPageLoaded(IWebDriver driver)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(
                d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
