using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NopcommerceTesting
{
    class SearchTestCase
    {
        //Create reference of browser
        IWebDriver driver = new ChromeDriver();
        string baseURL = "http://nop1.ysoftsolution.com/";

        [SetUp]
        public void StartBrowser()
        {
            driver.Navigate().GoToUrl(baseURL);
        }

        [Test]
        [TestCase("Apple MacBook Pro 13 - inch",true)] //specific text product name searching functionality
        [TestCase("",false)] //Null or empty invalid search text. 'Search term minimum length is 3 characters'.
        [TestCase("test", true)] //wild search using any character or keyword
        [TestCase("Apple MacBook Pro 13 - inch ", true)] //spacing from end of search text
        [TestCase(" Apple MacBook Pro 13 - inch", true)] //spacing from begin of search text
        [TestCase("Pro", true)] //3 characters search properly working
        [TestCase("!@#", true)]  //invalid Product name/not found: Criteria for search text does not match at that time: 'No products were found that matched your criteria'.
        public void SearchText(string searchText,bool isAccepted)
        {
            //Navigate to home Page
            driver.Navigate().GoToUrl("http://nop1.ysoftsolution.com/");
            Thread.Sleep(2000);

            //Search element with keyword searching -'Apple MacBook Pro 13-inch'
            driver.FindElement(By.XPath("//input[@type='text'][@name='q']")).SendKeys(searchText);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
            Thread.Sleep(2000);

            //Click on Search Button
            driver.FindElement(By.XPath("//text()[.='Search']/ancestor::a[1]")).Click();
            Thread.Sleep(2000);

            //Click on searched Product
            driver.FindElement(By.XPath("//a[text()='Apple MacBook Pro 13-inch']")).Click();
            //driver.Navigate().GoToUrl("http://nop1.ysoftsolution.com/apple-macbook-pro-13-inch");
            Thread.Sleep(2000);

            //Product Images
            driver.FindElement(By.XPath("/html[1]/body[1]/div[6]/div[3]/div[2]/div[1]/div[1]/div[1]/form[1]/div[1]/div[1]/div[1]/div[2]/div[3]/img[1]")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("/html[1]/body[1]/div[6]/div[3]/div[2]/div[1]/div[1]/div[1]/form[1]/div[1]/div[1]/div[1]/div[2]/div[2]/img[1]")).Click();
            Thread.Sleep(2000);

            if (isAccepted)
            {
                Assert.That(driver.Url, Is.EqualTo(baseURL + "/search?q=test&adv=true&sid=true"));
            }
            else
            {
                Assert.That(driver.FindElement(By.Name("ErrorBox")).Text, Is.EqualTo("No products were found that matched your criteria"));
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
