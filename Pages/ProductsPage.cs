using AventStack.ExtentReports;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Globalization;
using XKomShoppingTestFramework.Base;
using XKomShoppingTestFramework.Helpers;

namespace XKomShoppingTestFramework.Pages
{
    public class ProductsPage : BasePage
    {
        protected ExtentTest _test;

        public ProductsPage(IWebDriver driver, ExtentTest test) : base(driver, test)
        {
            _test = test;
        }

        public ProductsPage OpenFirstProduct()
        {
            _test.Log(Status.Info, "Otwarcie kontekstu pierwszego produktu z listy");
            var firstProducyPath = $"(//div[@data-name = 'productCard'])[1]";
            var firstProduct = Driver.FindElement(By.XPath(firstProducyPath));

            firstProduct.Click();
            WaitHelper.WaitForPageLoaded(Driver);
            CheckAndClickDismissInfoButton();

            return this;
        }

        public decimal GetProductCost()
        {
            _test.Log(Status.Info, "Pobranie wartości ceny otwartego produktu");
            var productCostPath = "(//div//h1[@data-name = 'productTitle']//ancestor::div//div[contains(text(), ' zł')])[2]";
            var productCostElement = Driver.FindElement(By.XPath($"{productCostPath}"));

            var productCost = productCostElement.Text;
            string processedText = productCost.Replace(" ", "").Replace("zł", "").Replace(",", ".");

            return decimal.Parse(processedText, CultureInfo.InvariantCulture);
        }

        public ProductsPage AddProductToTheCart()
        {
            _test.Log(Status.Info, "Dodanie otwartego produktu do koszyka");
            var cartIconPath = $"//button[@title = 'Dodaj do koszyka']//span[contains(text(), 'Dodaj do koszyka')]";
            var cartIcon = Driver.FindElement(By.XPath(cartIconPath));

            WaitHelper.WaitForPageLoaded(Driver);
            cartIcon.Click();

            var closePopupIconPath = "//button[@title = 'Zamknij']//span";
            var closePopupIcon = Driver.FindElement(By.XPath(closePopupIconPath));

            WaitHelper.WaitForPageLoaded(Driver);
            closePopupIcon.Click();

            return this;
        }

        public bool IsElementPresent(By by)
        {
            ReadOnlyCollection<IWebElement> elements = Driver.FindElements(by);
            return elements.Count > 0;
        }

        public void CheckAndClickDismissInfoButton()
        {
            _test.Log(Status.Info, "Zamknięcie informacji o promocjach");
            By dismissInfoButton = By.XPath("//div[@data-name = 'infobarDismissal']//button");

            if (IsElementPresent(dismissInfoButton))
            {
                Driver.FindElement(dismissInfoButton).Click();
            }
        }
    }
}
