using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NopcommerceTesting
{
    class LoginTestCase
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
		[TestCase("123@test.com", "test@123", true)] //valid and  correct verified credentails
		[TestCase(" 123@test.com", "test@123", false)] //blankspace starting -Username
		[TestCase("123@test.com ", "test@123", false)] //blankspace endiing -Username
		[TestCase("123@test.com", " test@123", false)] //blankspace starting -Password though correct still displayed space as '*' so invalid credentials
		[TestCase("123@test.com ", "test@123 ", false)] //blankspace ending -Password though correct still displayed space as '*' so invalid credentials
		[TestCase(" 123@test.com ", " test@123 ", false)] //blank space from starting username and password 
		[TestCase(" 123@test.com ", "test@123 ", false)] //blank space from starting username
		[TestCase("test", "test", false)] //username and password invalid length 
		[TestCase("123 @test.com", "test@123", false)] //spaces between characters in username before @ sign
		[TestCase("123@ test.com", "test@123", false)] //spaces between characters in username after @ sign
		[TestCase("123@te st.com", "test@123", false)] //spaces between characters in username before @ sign and followed by some characters
		[TestCase("123@test.com", "test@123", false)] //Existing login user should not be login again in same browser either logout first user or popup message for the same.
		[TestCase("12#!$%@test.com ", "test@123", false)] // any symbolic constant not accepted in email accept '_ and .'
		[TestCase("", "", false)] //not allowed any null or Empty value because all are mandatory
		public void LoginTest(string username, string password, bool isAccepted)
		{
			//Create reference for interactions with browser
			Actions action = new Actions(driver);

			//Maximize browser window 
			driver.Manage().Window.Maximize();


			//To Delay execution for 02 sec.
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
			driver.Navigate().GoToUrl(baseURL + "/login");
			Thread.Sleep(2000);


			//LOGIN EMAIL
			driver.FindElement(By.XPath("//input[@type='email'][@name='Email']")).Clear();
			driver.FindElement(By.XPath("//input[@type='email'][@name='Email']")).SendKeys(username);
			Thread.Sleep(2000);

			//LOGIN Password
			driver.FindElement(By.XPath("//input[@type='password'][@name='Password']")).Clear();
			driver.FindElement(By.XPath("//input[@type='password'][@name='Password']")).SendKeys(password);
			Thread.Sleep(2000);


			//RememberMe: Checked - by default (unchecked)
			driver.FindElement(By.XPath("//label[text()='Remember me?']")).Click();
			Thread.Sleep(2000);


			//Login button
			driver.FindElement(By.XPath("//input[@type='submit'][@value='Log in']")).Click();
			Thread.Sleep(2000);

			if (isAccepted)
			{
				Assert.That(driver.Url, Is.EqualTo(baseURL)); //Dashboard Page
			}
			else
			{
				Assert.That(driver.FindElement(By.Name("ErrorBox")).Text, Is.EqualTo("Login was unsuccessful. Please correct the errors and try again.The credentials provided are incorrect."));
			}
		}


		[TearDown]
		public void CloseBrowser()
		{
			driver.Close();
		}
	}
}
