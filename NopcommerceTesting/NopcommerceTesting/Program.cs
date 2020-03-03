using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace NopcommerceTesting
{
	class Program
	{
		static void Main(string[] args)
		{
			//Create reference of browser
			IWebDriver driver = new ChromeDriver();

			//Create reference for interactions with browser
			Actions action = new Actions(driver);

			//Maximize browser window 
			driver.Manage().Window.Maximize();

			//To Delay execution for 02 sec. as to view the resize browser
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));

			try
			{

				#region REGISTER 
				//------------------Navigate to the Nopcommerce Registration Page-------------------------------
				driver.Navigate().GoToUrl("http://nop1.ysoftsolution.com/register");
				Thread.Sleep(2000);

				//gender: By.Class = 'gender';
				driver.FindElement(By.XPath("//input[@type='radio'][@name='Gender']")).Click();
				Thread.Sleep(2000);

				//FirstName element
				IWebElement elementfirstName = driver.FindElement(By.Id("FirstName"));
				elementfirstName.SendKeys("Test First Name");
				Thread.Sleep(2000);

				//LastName element
				IWebElement elementlastName = driver.FindElement(By.Id("LastName"));
				elementlastName.SendKeys("Test Last Name");
				Thread.Sleep(2000);

				//Date of birth element
				var txtBirDay = driver.FindElement(By.Name("DateOfBirthDay"));
				action.ClickAndHold(txtBirDay).SendKeys("03").Perform();
				Thread.Sleep(2000);

				var txtBirMonth = driver.FindElement(By.Name("DateOfBirthMonth"));
				action.ClickAndHold(txtBirMonth).SendKeys("March").Perform();
				Thread.Sleep(2000);

				var txtBirYear = driver.FindElement(By.Name("DateOfBirthYear"));
				action.ClickAndHold(txtBirYear).SendKeys("2020").Perform();
				Thread.Sleep(2000);

				//email element
				IWebElement elementEmail = driver.FindElement(By.Id("Email"));
				elementEmail.SendKeys("123@test.com");
				Thread.Sleep(2000);

				//Comapny element
				IWebElement elementCompany = driver.FindElement(By.Id("Company"));
				elementCompany.SendKeys("TESTING COMPANY");
				Thread.Sleep(2000);

				//NewsLetter element- uncheck as by Default : Checked
				driver.FindElement(By.Id("Newsletter")).Click();
				Thread.Sleep(2000);


				//Password element
				IWebElement elementPassword = driver.FindElement(By.Id("Password"));
				elementPassword.SendKeys("test@123");
				Thread.Sleep(2000);

				//ConfirmPassword element
				IWebElement elementConfirmPassword = driver.FindElement(By.Id("ConfirmPassword"));
				elementConfirmPassword.SendKeys("test@123");
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
				#endregion


				//-------------------------Navigate to the Nopcommerce LOGIN Page-----------------------------

				#region LOGIN 

				//wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
				driver.Navigate().GoToUrl("http://nop1.ysoftsolution.com/login");
				Thread.Sleep(2000);

				//LOGIN EMAIL
				driver.FindElement(By.XPath("//input[@type='email'][@name='Email']")).SendKeys("123@test.com");
				Thread.Sleep(2000);

				//LOGIN Password
				driver.FindElement(By.XPath("//input[@type='password'][@name='Password']")).SendKeys("test@123");
				Thread.Sleep(2000);


				//RememberMe
				driver.FindElement(By.XPath("//label[text()='Remember me?']")).Click();
				Thread.Sleep(2000);


				//Login
				driver.FindElement(By.XPath("//input[@type='submit'][@value='Log in']")).Click();
				Thread.Sleep(2000);

				#endregion


				//------------Forgot Password------------------

				#region RECOVERY / Forgot Pasword
				//Navigate to Recovery Page
				driver.Navigate().GoToUrl("http://nop1.ysoftsolution.com/passwordrecovery");
				Thread.Sleep(2000);

				//Email
				driver.FindElement(By.Id("Email")).SendKeys("123@test.com");
				Thread.Sleep(2000);

				//Recover button
				driver.FindElement(By.XPath("//input[@type='submit'][@name='send-email']")).Click();
				Thread.Sleep(2000);
				#endregion

				
				//----------Search------------- 

				#region SEARCH PRODUCT
				//Navigate to home Page
				driver.Navigate().GoToUrl("http://nop1.ysoftsolution.com/");
				Thread.Sleep(2000);

				//Search element with keyword searching -'Apple MacBook Pro 13-inch'
				driver.FindElement(By.XPath("//input[@type='text'][@name='q']")).SendKeys("Apple MacBook Pro 13-inch");
				wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
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
				#endregion


			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
			}
			finally
			{
				driver.Dispose();
				//Close the browser
				driver.Quit();
			}
		}
	}
}
