using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RateAmLib.Utils.Abtract;



namespace RateAmLib.Utils
{
    public class SeleniumWebClient : IWebClient
    {
        public Task<string> GetPageAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string[]> GetPagesAsync()
        {

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            IWebDriver driver = new ChromeDriver(chromeOptions);
            driver.Navigate().GoToUrl("https://rate.am/en");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            //IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            //jsExecutor.ExecuteScript("window.localStorage.clear();");
            //jsExecutor.ExecuteScript("window.sessionStorage.clear();");

            var pages = new List<string>
            {
                driver.PageSource
            };

            for (int j = 5; j < 15; j += 4)
            {

                for (int i = 0; i < 4; i++)
                {
                    var optionNum = j + i;
                    var curId = i + 1;
                    wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                    if (optionNum == 16) break;

                    string xpath = "//*[@id=\"ctl00_Content_RB_dlCurrency" + curId + "\"]/option[" + optionNum + "]";
                    IWebElement optionToSelect = driver.FindElement(By.XPath(xpath));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
                    optionToSelect.Click();
                }
                pages.Add(driver.PageSource);

            }

            return Task.FromResult(pages.ToArray());
        }
    }
}
