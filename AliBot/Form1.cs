using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.IO;
using Microsoft.Office;
using Excel = Microsoft.Office.Interop.Excel;

namespace AliBot
{
    public partial class Form1 : Form
    {
        IWebDriver browser;
        string filePath;
        public int timeout;
        public OpenFileDialog OPD = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
            
        }

        private void OpenBrowser_Click(object sender, EventArgs e)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            var prefs = new Dictionary<string, object> {
                { "download.default_directory", @"C:\code" },
                { "download.prompt_for_download", false }
                };
            chromeOptions.AddAdditionalCapability("chrome.prefs", prefs);
            browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            browser.Manage().Window.Maximize();   
        }

        private void CloseBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                browser.Quit();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void FileSelectorURLs_Click(object sender, EventArgs e)
        {            
            if (OPD.ShowDialog() == DialogResult.OK) {
                filePath = OPD.FileName;
                label1.Text = filePath;
            };            
        }

        private void StartWork_Click(object sender, EventArgs e)
        {
            StreamReader urlsFiles;
            string[] urls;
            timeout = (int)TimeOutSetter.Value;

            try
            {
                urlsFiles = new StreamReader(OPD.FileName);
                urls = urlsFiles.ReadToEnd().Split('\n');                
                browser.Navigate().GoToUrl("http://ru.aliexpress.com/");
                Thread.Sleep(timeout);
                foreach (string currentURL in urls) {                     
                    List<string> values = new List<string>();
                    List<string> sizes = new List<string>();
                    List<string> features = new List<string>(); 
                    browser.Navigate().GoToUrl(currentURL);
                    Thread.Sleep(timeout);
                    values.Add(browser.Title);
                    values.Add(browser.FindElement(By.Id("j-sku-discount-price")).Text);
                    var sizesObjects = browser.FindElements(By.CssSelector("#j-sku-list-2 li a span"));

                    foreach (var sizeVal in sizesObjects)
                    {
                        sizes.Add(sizeVal.Text);//Лист с размерами
                    }

                    var featuresObjeectsTitle = browser.FindElements(By.CssSelector("span.propery-title"));
                    var featuresObjeectsDes = browser.FindElements(By.CssSelector("span.propery-des"));
                    string[,] featuresEndValues = new string[featuresObjeectsTitle.Count, 2];

                    for (int i = 0; i < featuresObjeectsTitle.Count; i++)
                    {
                        featuresEndValues[i, 0] = featuresObjeectsTitle[i].Text;
                        featuresEndValues[i, 1] = featuresObjeectsDes[i].Text;
                        MessageBox.Show(featuresEndValues[i, 0] + " - " + featuresEndValues[i, 1]);
                    }

                    IWebElement imageToNextPage = browser.FindElement(By.CssSelector("a .ui-magnifier-glass"));
                    imageToNextPage.Click();
                    Thread.Sleep(timeout);        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
