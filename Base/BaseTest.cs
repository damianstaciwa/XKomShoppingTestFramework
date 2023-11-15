using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using XKomShoppingTestFramework.Pages;
using XKomShoppingTestFramework.Helpers;
using NUnit.Framework.Interfaces;
using NUnit.Framework;

namespace XKomShoppingTestFramework.Base
{
    public abstract class BaseTest
    {
        protected IWebDriver driver;

        protected ExtentReports extent;
        protected ExtentTest test;

        protected const string BaseUrl = "https://www.x-kom.pl/";

        protected HomePage homePage;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            extent = new ExtentReports();

            string dateTimeFormat = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string reportFileName = $"Report_{dateTimeFormat}.html";

            var reportPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Reports", reportFileName);

            var htmlReporter = new ExtentSparkReporter(reportPath);
            extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        public virtual void SetUp()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            test.Log(Status.Info, "Otwarcie strony x-kom.pl");
            driver.Navigate().GoToUrl(BaseUrl);

            WaitHelper.WaitForPageLoaded(driver);

            test.Log(Status.Info, "Akceptacja plików cookie");
            SaveAndLoadCookies();
            AcceptCookiesIfPresent();

            homePage = new HomePage(driver, test);
        }

        [TearDown]
        public virtual void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var errorMessage = TestContext.CurrentContext.Result.Message;

            switch (status)
            {
                case TestStatus.Failed:
                    test.Fail("Failure " + stackTrace + " " + errorMessage);
                    break;
                case TestStatus.Skipped:
                    test.Skip("Skipped");
                    break;
                default:
                    test.Pass("Success");
                    break;
            }

            test.Log(Status.Info, "Zamknięcie przeglądarki");
            driver.Dispose();
        }

        [OneTimeTearDown]
        public virtual void GlobalTearDown()
        {
            extent.Flush();
        }

        public void AcceptCookiesIfPresent()
        {
            var acceptCookiesButton = driver.FindElement(By.XPath("//button[contains(text(),'W porządku')]"));

            if (acceptCookiesButton.Displayed)
            {
                acceptCookiesButton.Click();
            }
        }

        public void SaveAndLoadCookies()
        {
            var cookies = driver.Manage().Cookies.AllCookies;

            foreach (var cookie in cookies)
            {
                driver.Manage().Cookies.AddCookie(cookie);
            }

            driver.Navigate().Refresh();
        }
    }
}
