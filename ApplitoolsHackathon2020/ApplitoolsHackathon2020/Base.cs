using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Applitools;
using Applitools.Selenium;
using Applitools.VisualGrid;
using Newtonsoft.Json.Bson;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Configuration = Applitools.Selenium.Configuration;
using Applitools.Utils.Geometry;

namespace ApplitoolsHackathon2020
{
    public class Base
    {
        public Eyes _eyes;
        public VisualGridRunner runner;
        public Configuration config;
        SiteDriver _siteDriver = new SiteDriver();
        
        
        [SetUp]
        public void Initialize()
        {
            //Initialize the Runner the test.
            runner = new VisualGridRunner(10);

            // Initialize the eyes SDK 
            _eyes = new Eyes(runner);

            // Initialize eyes Configuration
            config = new Configuration();
            config.SetApiKey("80t00102Dxj9FBzQ7UWU1031n4MRH109Vvc5JoceMwbA79Q110 ");
            config.SetBatch(new BatchInfo("Holiday Shopping"));
            config.AddBrowser(1200, 800, BrowserType.CHROME);

            //Adding configuration for cross-browsers testing
            //Uncomment the following to run across multiple browsers
            //config.AddBrowser(1200, 800, BrowserType.FIREFOX);
            //config.AddBrowser(1200, 800, BrowserType.EDGE_CHROMIUM);
            //config.AddBrowser(1200, 800, BrowserType.SAFARI);
            //config.AddDeviceEmulation(DeviceName.iPhone_X);

            _eyes.SetConfiguration(config);
        }

        [TearDown]
        public void TestCleanUp()
        {
            _eyes.CloseAsync();
            _siteDriver.Close();
            TestResultsSummary allTestResults = runner.GetAllTestResults();
            Console.WriteLine(allTestResults);
        }

        public void Start(string url, string appName, string testName)
        {
            _siteDriver.Open();
            _eyes.Open(_siteDriver.Driver, appName, testName);
            _siteDriver.Driver.Navigate().GoToUrl(url);
            _siteDriver.Driver.Manage().Window.Maximize();
            
        }
        public void ClickBlackColor() =>
            _siteDriver.FindElement(PageObjects.BlackColorCheckboxCssLocator).Click();

        public void ClickFilterButton() =>
            _siteDriver.FindElement(PageObjects.FilterButtonCssLocator, How.CssSelector).Click();

        public void ClickAppliAirXNight() =>
            _siteDriver.FindElement(PageObjects.AppliAirxNightCssLocator, How.CssSelector).Click();
    }
}
