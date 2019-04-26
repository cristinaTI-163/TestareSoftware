using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace test
{
    [TestClass]
    public class Lab2 :UnitTest1
    {
        public Lab2() : base("MyMVCApp") { }

        [TestMethod]
        public void IndexChromeTest()
        {
            IndexTestByDriver(this.ChromeDriver);
        }

        private void IndexTestByDriver(IWebDriver driver)
        {
            var result = new List<Card>();
            // Act
            driver.Navigate().GoToUrl("http://ebay.com/");
            Actions builder = new Actions(driver);
            driver.FindElement(By.XPath("/html/body/header/nav/table/tbody/tr/td[1]/span")).Click();
            int count = 0;
            //var nextElement = driver.FindElement(By.XPath("/html/body/div[1]/section/footer/div[2]/a[1]"));
            while(!HasClass(driver.FindElement(By.XPath("/html/body/div[1]/section/footer/div[2]/a[2]")), "disabled"))
            {
                if(count == 2)
                {
                    break;
                }
                var itemList = driver.FindElements(By.ClassName("card-item"));
                foreach (var item in itemList)
                {
                    var parent = item.FindElement(By.TagName("figcaption"));
                    var name = parent.FindElement(By.XPath(".//h3/a")).Text;
                    var price = parent.FindElement(By.XPath(".//button/span")).Text;
                    var description = parent.FindElement(By.TagName("em")).Text;


                    var intList = Regex.Split(price, @"\D+");
                    var value = string.Join(string.Empty, intList);
                    if (!value.Equals(string.Empty))
                    {
                        var moneyCount = int.Parse(value);
                        var card = new Card
                        {
                            Name = name,
                            Price = moneyCount,
                            Description = description
                        };
                        result.Add(card);
                    }
                    else
                    {
                        continue;
                    }


                }
                count++;
                driver.FindElement(By.XPath("/html/body/div[1]/section/footer/div[2]/a[2]")).Click();
            }
            
            System.Console.WriteLine("Done");

            var minPrice = result.FirstOrDefault(x => x.Price.Equals(result.Min(m => m.Price)));
            for (int i = 0; i < result.Count; i++)
            {
                //Tests.WriteLine($"#{i+1}: {result[i].Name}   {result[i].Price} lei");
            }
            Debug.WriteLine($"Minmal Price: {minPrice.Name}   {minPrice.Price} lei");

            Assert.IsTrue(true);

        }

        public static bool HasClass(IWebElement el, string className)
        {
            return el.GetAttribute("class").Split(' ').Contains(className);
        }

    }
    
}
