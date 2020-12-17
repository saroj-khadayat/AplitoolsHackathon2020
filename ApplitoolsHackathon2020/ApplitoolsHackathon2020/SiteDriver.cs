using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitoolsHackathon2020
{
    public class SiteDriver
    {
        private IWebDriver driver;

        public  IWebDriver Driver
        {
            get { return driver; }
        }

        public  IJavaScriptExecutor JsExecutor
        {
            get { return driver as IJavaScriptExecutor; }
        }

        public void Open()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);
        }

        public void Close()
        {
            driver.Quit();
        }

        internal IWebElement FindElement(string select, How selector)
        {
            return FindElement(select, selector, driver);
        }


        internal  IWebElement FindElement(string select, How selector, ISearchContext context, int elementTimeOut = 2000)
        {
            switch (selector)
            {
                case How.ClassName:
                    return WaitandReturnElementExists(By.ClassName(select), context, elementTimeOut);
                case How.CssSelector:
                    return WaitandReturnElementExists(By.CssSelector(select), context, elementTimeOut);
                case How.Id:
                    return WaitandReturnElementExists(By.Id(select), context, elementTimeOut);
                case How.LinkText:
                    return WaitandReturnElementExists(By.LinkText(select), context, elementTimeOut);
                case How.Name:
                    return WaitandReturnElementExists(By.Name(select), context, elementTimeOut);
                case How.PartialLinkText:
                    return WaitandReturnElementExists(By.PartialLinkText(select), context, elementTimeOut);
                case How.TagName:
                    return WaitandReturnElementExists(By.TagName(select), context, elementTimeOut);
                case How.XPath:
                    return WaitandReturnElementExists(By.XPath(select), context, elementTimeOut);
            }
            throw new NotSupportedException(string.Format("Selector \"{0}\" is not supported.", selector));
        }

        public IWebElement WaitandReturnElementExists(By locator, ISearchContext context, int elementTimeOut = 2000)
        {
            if (elementTimeOut == 0)
                return context.FindElement(locator);

            var wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromMilliseconds(2000), TimeSpan.FromMilliseconds(2000));
            IWebElement webElement = null;
            wait.Until(driver =>
            {
                try
                {
                    webElement = context.FindElement(locator);
                    return webElement != null;

                }
                catch (Exception ex)
                {
                    return false;
                }
            });
            return webElement;
        }

        private const string JQueryScript = "return $('{0}').get(0)";

        public object ExecuteModern(string script)
        {
            return JsExecutor.ExecuteScript(script);
        }

        public IWebElement FindElement(string select)
        {
            return (IWebElement)ExecuteModern(string.Format(JQueryScript, select));
        }
    }
}
