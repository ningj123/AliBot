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
            browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            browser.Manage().Window.Maximize();
            browser.Navigate().GoToUrl("http://ru.aliexpress.com/item/16708/32640351795.html?spm=2114.03010108.3.3.rg4xNc&ws_ab_test=searchweb201556_0,searchweb201602_4_10048_10057_10039_10047_10065_10056_10055_10037_10054_301_10046_10059_10045_10058_10017_107_10060_10061_10052_414_10062_10053_10050_10051,searchweb201603_1&btsid=b7723fb6-addf-4c48-952c-ce3d8ab26850");
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
            };
            
        }
    }
}
