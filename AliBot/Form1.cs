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

namespace AliBot
{
    public partial class Form1 : Form
    {
        IWebDriver browser;
        string filePath;

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
            OpenFileDialog OPD = new OpenFileDialog();
            if (OPD.ShowDialog() == DialogResult.OK) {
                filePath = OPD.FileName;
                label1.Text = filePath;
            };            
        }

        private void StartWork_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(1000);
                List<string> values = new List<string>();
                List<string> sizes = new List<string>();
                List<string> features = new List<string>();
                browser.Navigate().GoToUrl("http://ru.aliexpress.com/");
                Thread.Sleep(6000);
                browser.Navigate().GoToUrl("http://ru.aliexpress.com/item/COLROVE-Work-Summer-Style-Women-Bodycon-Dresses-Sexy-2016-New-Arrival-Casual-Green-Crew-Neck-Half/32655446818.html?spm=2114.03010108.3.12.1ZdDIh&ws_ab_test=searchweb201556_0,searchweb201602_4_10048_10039_10057_10047_10065_10056_10055_10037_10054_10046_10059_10045_10032_10058_10017_107_10060_10061_10052_414_10062_10053_10050_10051,searchweb201603_1&btsid=3c650fc1-9996-4108-ba8f-9024881c6155");
                Thread.Sleep(6000);
                values.Add(browser.Title);
                values.Add(browser.FindElement(By.Id("j-sku-discount-price")).Text);                

                var sizesObjects = browser.FindElements(By.CssSelector("#j-sku-list-2 li a span"));

                MessageBox.Show(sizesObjects[0].Text);

                foreach (var sizeVal in sizesObjects)
                {
                    sizes.Add(sizeVal.Text);//Лист с размерами
                }

                var featuresObjeectsTitle = browser.FindElements(By.ClassName(""));
                var featuresObjeectsDes = browser.FindElements(By.ClassName(""));
                string[,] featuresEndValues = new string[featuresObjeectsTitle.Count, 2];

                for (int i = 0; i < featuresObjeectsTitle.Count; i++)
                {
                    featuresEndValues[i, 0] = featuresObjeectsTitle[i].Text;
                    featuresEndValues[i, 1] = featuresObjeectsDes[i].Text;
                }

                //IWebElement imageToNextPage = browser.FindElement(By.ClassName("a .ui-magnifier-glass"));
                //imageToNextPage.Click();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
