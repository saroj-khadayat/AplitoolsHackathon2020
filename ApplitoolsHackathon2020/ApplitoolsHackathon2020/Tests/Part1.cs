using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ApplitoolsHackathon2020.Tests
{
    public class Part1:Base
    {
        [Test]
        [Order(1)]
        public void MainPageTest()
        {
            Start(Urls.V1ProductionVersion, "AppliFashion","Test 1");
            _eyes.CheckWindow("main page", true);
        }

        [Test]
        public void FilterProductFunctionTest()
        {
            Start(Urls.V1ProductionVersion, "AppliFashion", "Test 2");
            ClickBlackColor();
            ClickFilterButton();
            _eyes.CheckRegion(By.Id("product_grid"), "filter by color");
        }

        [Test]
        public void ProductDetailsTest()
        {
            Start(Urls.V1ProductionVersion, "AppliFashion", "Test 3");
            ClickAppliAirXNight();
            _eyes.CheckWindow("product details", true);
        }
    }
}
