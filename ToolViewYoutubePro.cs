using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ToolViewYoutubePro
{
    
    class ToolViewYoutubePro
    {
        IWebDriver driver;
        public ToolViewYoutubePro()
        {
            // can them profile cho that
            try
            {
                FirefoxOptions options = new FirefoxOptions();
                FirefoxProfile profile = new FirefoxProfile();
                string userAgent = randomUserAgent();
                profile.SetPreference("general.useragent.override", userAgent);
                if(ToolConfig.ui != true)
                {
                    options.AddArguments("--headless");
                }
                options.Profile = profile;
                driver = new FirefoxDriver(options);
                ToolLog.writeLog("User Agent:"+ userAgent);
            } catch(Exception ex)
            {
                ToolLog.showError(ex);
            }
        }
        public void gotoGoogle()
        {
            try
            {
                waitBrowser(3);
                driver.Navigate().GoToUrl(ToolConfig.urlGoogle);
                ToolLog.writeLog("tao user moi");
                waitBrowser(3);
                // nhap vao khung tim kiem
                driver.FindElement(By.Name(ToolConfig.elementSearch)).SendKeys(ToolConfig.keySearchGoogle);
                Thread.Sleep(ToolConfig.timeWait);
                driver.FindElement(By.Name(ToolConfig.elementSearch)).SendKeys(Keys.Enter);
                ToolLog.writeLog("tao thong tin trinh duyet moi");
                waitBrowser(ToolConfig.timeWait);
            }
            catch (Exception ex)
            {
                ToolLog.showError(ex);
            }
            
        }

        public void gotoYoutube()
        {
            try
            {
                Thread.Sleep(ToolConfig.timeWait);
                driver.FindElement(By.CssSelector("[href*='" + ToolConfig.urlYoutube + "']")).SendKeys(Keys.Enter);
                waitBrowser(ToolConfig.timeWait);
                ToolLog.writeLog("da vao youtube");
                waitBrowser(ToolConfig.timeWait);
                driver.FindElement(By.Name(ToolConfig.elementSearchYoutube)).SendKeys(ToolConfig.keySearchYoutube);
                waitBrowser(ToolConfig.timeWait);
                driver.FindElement(By.Name(ToolConfig.elementSearchYoutube)).SendKeys(Keys.Enter);
                ToolLog.writeLog("tim kiem" + ToolConfig.keySearchYoutube);
                waitBrowser(ToolConfig.timeWait);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                var element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[href*='" + ToolConfig.idVideo + "']")));
                Actions action = new Actions(driver);
                ToolLog.writeLog("hover chuot" + " vào video chứa url video");
                action.MoveToElement(element).Perform();
                waitBrowser(ToolConfig.timeWait);

                driver.FindElement(By.CssSelector("[href*='" + ToolConfig.idVideo + "']")).Click();
                ToolLog.writeLog("click vao video");
                ToolLog.writeLog("đang xem: " + ToolConfig.idVideo);
                Thread.Sleep(ToolConfig.timeVideo * 1000);
                ToolLog.writeLog("đã xem " + ToolConfig.timeVideo + "s");
            }
            catch (Exception ex)
            {
                ToolLog.showError(ex);
            }
            
        }

        public void waitBrowser(int second)
        {
            ToolLog.writeLog("dang cho "+second+"s");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(second);
            Thread.Sleep(second * 1000);
        }
        public void openNewTab()
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
                ToolLog.writeLog("mở tab mới");
                waitBrowser(2);
            }
            catch (Exception ex)
            {
                ToolLog.showError(ex);
            }
        }

        public void closeNewTab(int index=0)
        {
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles[index]);
                driver.Close();
                ToolLog.writeLog("close tab " + index);
                //((IJavaScriptExecutor)driver).ExecuteScript("window.close();");
                driver.SwitchTo().Window(driver.WindowHandles[index]);
                ToolLog.writeLog("swich tab " + index);
                waitBrowser(3);
            }
            catch (Exception ex)
            {
                ToolLog.showError(ex);
            }
            
        }
        public void closeBrowser()
        {
            try
            {
                waitBrowser(2);
                driver.Quit();
                waitBrowser(2);
            }
            catch (Exception ex)
            {
                ToolLog.showError(ex);
            }
            
        }
        public string randomUserAgent()
        {
            ArrayList usetAgents = new ArrayList() {
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36",
                "Mozilla/5.0 (X11; Ubuntu; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2919.83 Safari/537.36",
                //"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2866.71 Safari/537.36",
                "Mozilla/5.0 (X11; Ubuntu; Linux i686 on x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2820.59 Safari/537.36",
                //"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2762.73 Safari/537.36",
                //"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2656.18 Safari/537.36",
                //"Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML like Gecko) Chrome/44.0.2403.155 Safari/537.36",
                "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36",
                //"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2227.1 Safari/537.36",
                //"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:77.0) Gecko/20190101 Firefox/77.0",
                //"Mozilla/5.0 (Windows NT 10.0; WOW64; rv:77.0) Gecko/20100101 Firefox/77.0",
                "Mozilla/5.0 (X11; Linux ppc64le; rv:75.0) Gecko/20100101 Firefox/75.0",
                //"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:39.0) Gecko/20100101 Firefox/75.0",
                //"Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.10; rv:75.0) Gecko/20100101 Firefox/75.0",
                "Mozilla/5.0 (X11; Linux; rv:74.0) Gecko/20100101 Firefox/74.0",
                //"Mozilla/5.0 (Macintosh; Intel Mac OS X 10.13; rv:61.0) Gecko/20100101 Firefox/73.0",
                //"Mozilla/5.0 (X11; OpenBSD i386; rv:72.0) Gecko/20100101 Firefox/72.0",
                //"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:71.0) Gecko/20100101 Firefox/71.0",
                //"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:70.0) Gecko/20191022 Firefox/70.0",
                //"Mozilla/5.0 (Windows NT 6.1; WOW64; rv: 70.0) Gecko / 20190101 Firefox / 70.0",
                "Mozilla/ 5.0(Windows; U; Windows NT 9.1; en - US; rv: 12.9.1.11) Gecko / 20100821 Firefox / 70",
                //"Mozilla/5.0 (X11; Linux i686; rv:64.0) Gecko/20100101 Firefox/64.0",
                //"Mozilla/5.0 (Windows NT 6.1; WOW64; rv: 64.0) Gecko / 20100101 Firefox / 64.0",
                //"Mozilla/5.0 (X11; Linux i586; rv:63.0) Gecko/20100101 Firefox/63.0",
                //"Mozilla/5.0 (Windows NT 6.2; WOW64; rv: 63.0) Gecko / 20100101 Firefox / 63.0",
                //"Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.10; rv:62.0) Gecko/20100101 Firefox/62.0",
                //"Mozilla/5.0 (Macintosh; Intel Mac OS X 10.14; rv: 10.0) Gecko / 20100101 Firefox / 62.0",
                //"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A",
                //"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/537.13+ (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2",
                //"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_3) AppleWebKit/534.55.3 (KHTML, like Gecko) Version/5.1.3 Safari/534.53.10",
                //"Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_8; de-at) AppleWebKit/533.21.1 (KHTML, like Gecko) Version/5.0.5 Safari/533.21.1",
                //"Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_7; da-dk) AppleWebKit/533.21.1 (KHTML, like Gecko) Version/5.0.5 Safari/533.21.1",
                //"Mozilla/5.0 (Windows; U; Windows NT 6.1; tr-TR) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.4 Safari/533.20.27",
                //"Mozilla/5.0 (Windows; U; Windows NT 6.1; ko - KR) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Windows; U; Windows NT 6.1; fr - FR) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Windows; U; Windows NT 6.1; en - US) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Windows; U; Windows NT 6.1; cs - CZ) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Windows; U; Windows NT 6.0; ja - JP) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Windows; U; Windows NT 6.0; en - US) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; PPC Mac OS X 10_5_8; zh - cn) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; PPC Mac OS X 10_5_8; ja - jp) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_7; ja - jp) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; zh - cn) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; sv - se) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; ko - kr) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; ja - jp) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; it - it) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; fr - fr) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; es - es) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; en - us) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; en - gb) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; de - de) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.4 Safari / 533.20.27",
                //"Mozilla/5.0 (Windows; U; Windows NT 6.1; sv-SE) AppleWebKit/533.19.4 (KHTML, like Gecko) Version/5.0.3 Safari/533.19.4",
                //"Mozilla/5.0 (Windows; U; Windows NT 6.1; ja - JP) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Windows; U; Windows NT 6.1; de - DE) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Windows; U; Windows NT 6.0; hu - HU) AppleWebKit / 533.19.4(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Windows; U; Windows NT 6.0; en - US) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Windows; U; Windows NT 6.0; de - DE) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Windows; U; Windows NT 5.1; ru - RU) AppleWebKit / 533.19.4(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Windows; U; Windows NT 5.1; ja - JP) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Windows; U; Windows NT 5.1; it - IT) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Windows; U; Windows NT 5.1; en - US) AppleWebKit / 533.20.25(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_7; en - us) AppleWebKit / 534.16 + (KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_6; fr - ch) AppleWebKit / 533.19.4(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_5; de - de) AppleWebKit / 534.15 + (KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",
                //"Mozilla / 5.0(Macintosh; U; Intel Mac OS X 10_6_5; ar) AppleWebKit / 533.19.4(KHTML, like Gecko) Version / 5.0.3 Safari / 533.19.4",

            };
            int idAgent = new Random().Next(usetAgents.Count);
            return usetAgents[idAgent].ToString();
        }
    }
    class ToolLog
    {
        public static void writeLog(string mess = "")
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n" + DateTime.Now + " " + mess);
            File.AppendAllText(ToolConfig.filePath + "log.txt", sb.ToString());
            Console.WriteLine(sb.ToString());
        }
        public static void showError(Exception ex)
        {
            //Console.WriteLine(ex.Message);
            writeLog(ex.Message);
        }
    }
    class ToolConfig
    {
        public static int timeWait = Int32.Parse(ConfigurationManager.AppSettings["timeWait"]);//seconds
        public static string filePath = string.Empty;
        public static string fileLogName = "logs.txt";
        public static string urlGoogle = @"https://www.google.com/";
        public static string urlYoutube = @"https://www.youtube.com/";
        public static string keySearchYoutube = ConfigurationManager.AppSettings["keySearchYoutube"];
        public static string keySearchGoogle = "youtube";
        public static string elementSearch = "q";
        public static string elementSearchYoutube = "search_query";
        public static string idVideo = ConfigurationManager.AppSettings["idVideo"];
        public static int timeVideo = Int32.Parse(ConfigurationManager.AppSettings["timeVideo"]);//seconds
        public static bool ui = ConfigurationManager.AppSettings["ui"].Contains("true");
    }
}
