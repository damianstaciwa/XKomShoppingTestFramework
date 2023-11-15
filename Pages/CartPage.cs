using AventStack.ExtentReports;
using OpenQA.Selenium;
using System.Globalization;
using XKomShoppingTestFramework.Base;

namespace XKomShoppingTestFramework.Pages
{
    public class CartPage : BasePage
    {
        private ExtentTest _test;

        public CartPage(IWebDriver driver, ExtentTest test) : base(driver, test)
        {
            _test = test;
        }

        public decimal GetProductsCostSummary()
        {
            _test.Log(Status.Info, "Pobranie całkowitej wartości produktów w koszyku");
            var productsSummaryCostPath = $"(//button[contains(text(), 'Oblicz ratę lub leasing')]//ancestor::div[2]//span)[4]";
            var productsSummaryCostElement = Driver.FindElement(By.XPath(productsSummaryCostPath));

            var productSummaryCost = productsSummaryCostElement.Text;
            string processedText = productSummaryCost.Replace(" ", "").Replace("zł", "").Replace(",", ".");

            return decimal.Parse(processedText, CultureInfo.InvariantCulture);
        }
    }
}
