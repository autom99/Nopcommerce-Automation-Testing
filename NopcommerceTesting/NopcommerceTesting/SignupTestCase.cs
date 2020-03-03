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

	class SignupTestCase
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
		[TestCase("TestFirstName", "TestLastName", " 123@test.com", " test@123", " test@123", false)] //blankspace not allowed in starting end
		[TestCase("TestFirstName", "TestLastName"," 123@test.com ", "test@123 ", "test@123 ", false)] //blankspace not allowed in end
		[TestCase("TestFirstName", "TestLastName", "123@test.com", "test@123", "test@123", true)] //valid input
		[TestCase("TestFirstName", "TestLastName", "123", "test@123", "test@123", false)] //invalid email/username
		[TestCase("TestFirstName", "TestLastName", "123 test@gmail.com", "test@123", "test@123", false)] //Space between characters/alphanumerics in email/username
		[TestCase("TestFirstName", "TestLastName", "test@test.com", "tes", "tes", false)] //password length validation
		[TestCase("TestFirstName", "TestLastName", "test@test.com", "test", " test1", false)] //password and confirm password not matched
		[TestCase("TestFirstName", "TestLastName", "test@test.com", " test", "test", false)] //password-blank space from starting 
		[TestCase("TestFirstName", "TestLastName", "test@test.com", "test ", "test", false)] //password-blank space from ending 
		[TestCase("TestFirstName", "TestLastName", "test@test.com", "test", "test ", false)] //confirm password-blank space from starting
		[TestCase("TestFirstName", "TestLastName", "test@test.com", "test@123", "test@123", false)] //valid credential but already registered-Notification like 'Already registered'
		[TestCase("TestFirstName", "TestLastName", "test@ test.com", "test@123", "test@123", false)] //Invalid email 
		[TestCase("TestFirstName", "TestLastName", "12#!$%@test.com ", "test@123", false)] // any symbolic constant not accepted in email accept '_ and .'
		[TestCase(" TestFirstName", "TestLastName", "test@ test.com", "test@123", false)] //First Name -starting space not allowed
		[TestCase("TestFirstName ", "TestLastName", "test@ test.com", "test@123", false)] //First Name -ending space not allowed
		[TestCase("TestFirstName", " TestLastName", "test@ test.com", "test@123", false)] //last Name -starting space not allowed
		[TestCase("TestFirstName", "TestLastName ", "test@ test.com", "test@123", false)] //last Name -ending space not allowed
		[TestCase("Test First Name", "Test Last Name ", "test@ test.com", "test@123", false)] //last Name -space is not allowed between in characters
		[TestCase("","",null,null,null,false)] //not allowed any null or Empty value because all are mandatory
		[TestCase("Test First Name", "Test Last Name ", "test@ test.com", null, false)]
		[TestCase("Test First Name", "Test Last Name ", null, "test@123", false)]
		[TestCase("Test First Name", null, "test@ test.com", "test@123", false)]
		[TestCase("Test First Name", "Test", null,null, false)]
		public void SignupTest(string firstname,string lastname, string username, string password, string confirmPassword,bool isAccepted)
		{

			//Create reference for interactions with browser
			Actions action = new Actions(driver);

			//Maximize browser window 
			driver.Manage().Window.Maximize();


			//To Delay execution for 02 sec. as to view the resize browser
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
			driver.Navigate().GoToUrl(baseURL + "/register");
			Thread.Sleep(2000);

			//gender: By.Class = 'gender';
			driver.FindElement(By.XPath("//input[@type='radio'][@name='Gender']")).Click();
			Thread.Sleep(2000);

			//FirstName element
			driver.FindElement(By.Id("FirstName")).Clear();
		    driver.FindElement(By.Id("FirstName")).SendKeys(firstname);
			Thread.Sleep(2000);

			//LastName element
			driver.FindElement(By.Id("LastName")).Clear();
			driver.FindElement(By.Id("LastName")).SendKeys(lastname);
			Thread.Sleep(2000);

			//Date of birth element
			var txtBirDay = driver.FindElement(By.Name("DateOfBirthDay"));
			action.ClickAndHold(txtBirDay).SendKeys("3").Perform();
			Thread.Sleep(2000);

			var txtBirMonth = driver.FindElement(By.Name("DateOfBirthMonth"));
			action.ClickAndHold(txtBirMonth).SendKeys("March").Perform();
			Thread.Sleep(2000);

			var txtBirYear = driver.FindElement(By.Name("DateOfBirthYear"));
			action.ClickAndHold(txtBirYear).SendKeys("2020").Perform();
			Thread.Sleep(2000);

			//email element
			driver.FindElement(By.Id("Email")).Clear();
			driver.FindElement(By.Id("Email")).SendKeys(username);
			Thread.Sleep(2000);

			//Comapny element
			driver.FindElement(By.Id("Company")).Clear();
			driver.FindElement(By.Id("Company")).SendKeys("TESTING COMPANY");
			Thread.Sleep(2000);

			//NewsLetter element- uncheck as by Default : Checked
			driver.FindElement(By.Id("Newsletter")).Click();
			Thread.Sleep(2000);


			//Password element
			driver.FindElement(By.Id("Password")).Clear();
			driver.FindElement(By.Id("Password")).SendKeys(password);
			Thread.Sleep(2000);

			//ConfirmPassword element
			driver.FindElement(By.Id("confirmPassword")).Clear();
			driver.FindElement(By.Id("confirmPassword")).SendKeys(confirmPassword);
			Thread.Sleep(2000);


			//Click on Submit button : id 'register-button'
			driver.FindElement(By.Id("register-button")).Click();
			//To Delay execution for 02 sec. as to view the resize browser
			wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
			Thread.Sleep(2000);


			//After successfully registered click on Continue button.
			driver.FindElement(By.Id("register-continue")).Click();
			Thread.Sleep(2000);

			//Logout
			driver.FindElement(By.XPath("//a[text()='Log out']")).Click();
			Thread.Sleep(2000);

			if (isAccepted)
			{
				Assert.That(driver.Url, Is.EqualTo(baseURL + "/registerresult/1"));
			}
			else
			{
				Assert.That(driver.FindElement(By.Name("ErrorBox")).Text, Is.EqualTo("Login failed"));
			}
		}

		[TearDown]
		public void CloseBrowser()
		{
			driver.Close();
		}
	}
}
