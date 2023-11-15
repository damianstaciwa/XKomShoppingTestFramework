using FluentAssertions;
using NUnit.Framework;
using XKomShoppingTestFramework.Base;
using XKomShoppingTestFramework.Constants;

namespace XKomShoppingTestFramework.Tests
{
    public class Tests : BaseTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void AddingProductsToTheCartTests()
        {
            homePage.ClickMenuItem(MenuItems.LaptopyIKomputery);
            var productsPage = homePage.ClickSubMenuItem(SubMenuItems.Laptopy);
            productsPage.OpenFirstProduct();
            var productsCost = productsPage.GetProductCost();
            productsPage.AddProductToTheCart();

            homePage.ClickMenuItem(MenuItems.UrzadzeniaPeryferyjne);
            productsPage = homePage.ClickSubMenuItem(SubMenuItems.Sluchawki);
            productsPage.OpenFirstProduct();
            productsCost += productsPage.GetProductCost();
            productsPage.AddProductToTheCart();

            homePage.ClickMenuItem(MenuItems.PodzespolyKomputerowe);
            homePage.ClickSubMenuItem(SubMenuItems.KartyGraficzne);
            productsPage = homePage.ChooseProductType(SubMenuItems.KartyAMD);
            productsPage.OpenFirstProduct();
            productsCost += productsPage.GetProductCost();
            productsPage.AddProductToTheCart();

            var cartPage = homePage.GoToCart();
            var productsCostSummary = cartPage.GetProductsCostSummary();

            productsCost.Should().Be(productsCostSummary);
        }
    }
}