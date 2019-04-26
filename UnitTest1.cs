using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace test
{
    [TestClass]
    public class UnitTest1
    {
        private const int iisPort = 49869;
        private string _applicationName;
        private Process _iisProcess;

        protected UnitTest1(string applicationName)
        {
            _applicationName = applicationName;
        }
        public ChromeDriver ChromeDriver { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            // Start IISExpress
            StartIIS();
            // Start Selenium drivers
            this.ChromeDriver = new ChromeDriver(@"D:\Users\testare");
        }


        [TestCleanup]
        public void TestCleanup()
        {
            // Ensure IISExpress is stopped
            if (_iisProcess.HasExited == false)
            {
                _iisProcess.Kill();
            }
            // Stop all Selenium drivers
            //this.FirefoxDriver.Quit();
            this.ChromeDriver.Quit();
            //this.InternetExplorerDriver.Quit();
        }

        private void StartIIS()
        {
            var applicationPath = GetApplicationPath(_applicationName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new Process();
            _iisProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            _iisProcess.StartInfo.ErrorDialog = true;

            _iisProcess.StartInfo.LoadUserProfile = true;
            _iisProcess.StartInfo.CreateNoWindow = false;
            _iisProcess.StartInfo.UseShellExecute = false;
            _iisProcess.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            _iisProcess.StartInfo.Arguments = string.Format($"/path:{applicationPath}/port:{iisPort}");
            _iisProcess.Start();
        }
        protected virtual string GetApplicationPath(string applicationName)
        {
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            return Path.Combine(solutionFolder, applicationName);
        }
        public string GetAbsoluteUrl(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
            {
                relativeUrl = "/" + relativeUrl;
            }
            return String.Format($"http://localhost:{iisPort}{relativeUrl}");
        }
    }
}
