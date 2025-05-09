using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        ExtentReports extent = new ExtentReports();

        string dateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string reportFilePath = $@"D:\{dateTime}.html";
        ExtentSparkReporter htmlreporter = new ExtentSparkReporter(reportFilePath);

        extent.AttachReporter(htmlreporter);
        ExtentTest test = extent.CreateTest("Test Case", "Positive Login Test");

        try
        {
            OpenUrl(driver, test, "https://parabank.parasoft.com/parabank/index.htm");

            using (var reader = new StreamReader(@"D:\User_Data.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<UserData>();
                foreach (var record in records)
                {
                    string dynamicUsername = record.Username + DateTime.Now.ToString("HHmmss");
                    RegisterUser(driver, test, record, dynamicUsername);
                    Logout(driver, test);
                    PerformLogin(driver, test, dynamicUsername, record.Password);
                    ValidateLogin(driver, test);
                }
            }
        }
        catch (Exception ex)
        {
            test.Log(Status.Fail, $"Test failed with exception: {ex.Message}\n{ex.StackTrace}");
        }
        finally
        {
            extent.Flush();
            driver.Quit();
        }
    }

    static void OpenUrl(IWebDriver driver, ExtentTest test, string url)
    {
        test.Log(Status.Info, "Tahmidul Islam\nDept of CSE\nJagannath University");
        driver.Navigate().GoToUrl(url);
        Thread.Sleep(3000);
        test.Log(Status.Info, $"Open url: {url}");
        Console.WriteLine($"Open url: {url}");
        driver.Manage().Window.Maximize();
    }

    static void RegisterUser(IWebDriver driver, ExtentTest test, UserData data, string username)
    {
        driver.FindElement(By.LinkText("Register")).Click();
        Thread.Sleep(2000);

        driver.FindElement(By.Id("customer.firstName")).SendKeys(data.FirstName);
        driver.FindElement(By.Id("customer.lastName")).SendKeys(data.LastName);
        driver.FindElement(By.Id("customer.address.street")).SendKeys(data.Address);
        driver.FindElement(By.Id("customer.address.city")).SendKeys(data.City);
        driver.FindElement(By.Id("customer.address.state")).SendKeys(data.State);
        driver.FindElement(By.Id("customer.address.zipCode")).SendKeys(data.ZipCode);
        driver.FindElement(By.Id("customer.phoneNumber")).SendKeys(data.Phone);
        driver.FindElement(By.Id("customer.ssn")).SendKeys(data.SSN);
        driver.FindElement(By.Id("customer.username")).SendKeys(username);
        driver.FindElement(By.Id("customer.password")).SendKeys(data.Password);
        driver.FindElement(By.Id("repeatedPassword")).SendKeys(data.ConfirmPassword);

        driver.FindElement(By.CssSelector("#customerForm input[type='submit']")).Click();
        test.Log(Status.Info, $"User {username} registered successfully");
        Thread.Sleep(2000);
    }

    static void Logout(IWebDriver driver, ExtentTest test)
    {
        driver.FindElement(By.LinkText("Log Out")).Click();
        test.Log(Status.Pass, "Logged out successfully");
        Thread.Sleep(2000);
    }

    static void PerformLogin(IWebDriver driver, ExtentTest test, string username, string password)
    {
        driver.FindElement(By.Name("username")).SendKeys(username);
        driver.FindElement(By.Name("password")).SendKeys(password);
        driver.FindElement(By.CssSelector("input.button")).Click();
        test.Log(Status.Info, $"Login attempt with username: {username}");
        Thread.Sleep(2000);
    }

    static void ValidateLogin(IWebDriver driver, ExtentTest test)
    {
        try
        {
            bool isLoggedIn = driver.FindElement(By.LinkText("Log Out")).Displayed;
            test.Log(Status.Pass, "Login successful");
        }
        catch (NoSuchElementException)
        {
            test.Log(Status.Fail, "Login failed");
        }
    }
}

public class UserData
{
    [Name("First Name")]
    public string FirstName { get; set; }

    [Name("Last Name")]
    public string LastName { get; set; }

    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    [Name("Zip Code")]
    public string ZipCode { get; set; }

    [Name("Phone Number")]
    public string Phone { get; set; }

    public string SSN { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    [Name("Confirm Password")]
    public string ConfirmPassword { get; set; }
}
