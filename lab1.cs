using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace test
{
    [TestClass]
    public class lab1 : UnitTest1
    {
        /// <inheritdoc />
        public lab1() : base("MyMVCApp") { }

        [TestMethod]
        public void IndexChromeTest()
        {
            IndexTestByDriver(ChromeDriver);
        }

        private void IndexTestByDriver(IWebDriver driver)
        {
            // Act
            driver.Navigate().GoToUrl(this.GetAbsoluteUrl("/home/index"));
            driver.FindElement(By.Id("loginLink")).Click();
            var emailTextBox = driver.FindElement(By.Id("Email"));
            emailTextBox.Click();
            emailTextBox.Clear();
            emailTextBox.SendKeys("cristina.97@gmail.com");

            var passwd = driver.FindElement(By.Id("Password"));
            passwd.Click();
            passwd.Clear();
            passwd.SendKeys("Qwer123!");

            driver.FindElement(By.ClassName("btn-default")).Click();

            // Assert
            Assert.IsTrue(driver.Url.Equals(this.GetAbsoluteUrl("")));

        }
    }
}
