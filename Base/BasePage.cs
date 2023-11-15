using AventStack.ExtentReports;
using OpenQA.Selenium;
using XKomShoppingTestFramework.Helpers;
using XKomShoppingTestFramework.Pages;

namespace XKomShoppingTestFramework.Base
{
    public class BasePage
    {
        protected IWebDriver Driver;
        private ExtentTest _test;

        public BasePage(IWebDriver driver, ExtentTest test)
        {
            Driver = driver;
            _test = test;
        }

        public ProductsPage ClickMenuItem(string menuItemText)
        {
            _test.Log(Status.Info, "Wybranie kategorii z menu głównego");
            var menuItemPath = $"//a[@role='menuitem']//div[contains(text(), '{menuItemText}')]";
            var menuItem = Driver.FindElement(By.XPath(menuItemPath));

            menuItem.Click();
            WaitHelper.WaitForPageLoaded(Driver);

            return new ProductsPage(Driver, _test);
        }

        public ProductsPage ClickSubMenuItem(string subMenuItemText)
        {
            _test.Log(Status.Info, "Wybranie podkategorii z menu");
            var subMenuItemPath = $"//a[contains(text(), '{subMenuItemText}')]";
            var subMenuItem = Driver.FindElement(By.XPath(subMenuItemPath));

            subMenuItem.Click();
            WaitHelper.WaitForPageLoaded(Driver);

            return new ProductsPage(Driver, _test);
        }

        public ProductsPage ChooseProductType(string productType)
        {
            _test.Log(Status.Info, "Wybranie typu produktu");
            var subMenuItemPath = $"//span[contains(text(),'{productType}')]";
            var subMenuItem = Driver.FindElement(By.XPath(subMenuItemPath));

            subMenuItem.Click();
            WaitHelper.WaitForPageLoaded(Driver);

            return new ProductsPage(Driver, _test);
        }

        public CartPage GoToCart()
        {
            _test.Log(Status.Info, "Przejście do koszyka");
            var cartLinkItemPath = "//div[@data-name = 'miniBasketTab']//a";
            var cartLink = Driver.FindElement(By.XPath(cartLinkItemPath));

            cartLink.Click();
            WaitHelper.WaitForPageLoaded(Driver);

            return new CartPage(Driver, _test);
        }
    }
}
