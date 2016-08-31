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
        IJavaScriptExecutor jse;
        string filePath;
        public int timeout;
        public OpenFileDialog OPD = new OpenFileDialog();
        ChromeOptions chromeOptions;
        StreamReader urlsFiles;
        string[] urls;
        string ProductTitle;
        string ProductPrice;
        public Excel.Application excelApp;
        public Excel.Workbooks excelWB;
        public Excel.Worksheet excelWSheets;
        public Excel.Sheets excelSheets;
        public Excel.Range ceels;    


        List<string> colors = new List<string>(); // Лист с цветами 
        List<string> sizes = new List<string>(); // Лист с Размерами
        List<string> features = new List<string>(); // Лист с характеристиками

        public Form1()
        {
            InitializeComponent();            
        }

        private void OpenBrowser_Click(object sender, EventArgs e)
        {
            chromeOptions = new ChromeOptions();            
            browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            browser.Manage().Window.Maximize();
            excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelWB = excelApp.Workbooks;
            excelApp.Workbooks.Open(@"C:\Users\Админ\Downloads\products-2016-08-29_1.xlsx");
            excelWSheets = excelApp.ActiveSheet as Excel.Worksheet;
            excelSheets = excelWB[1].Worksheets;


            
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

            timeout = (int)TimeOutSetter.Value;
            jse = browser as IJavaScriptExecutor;            

            try
            {
                urlsFiles = new StreamReader(OPD.FileName);
                urls = urlsFiles.ReadToEnd().Split('\n');
                urlsFiles.Close();              
                browser.Navigate().GoToUrl("http://ru.aliexpress.com/");
                Thread.Sleep(timeout);
                foreach (string currentURL in urls) {                   
                    browser.Navigate().GoToUrl(currentURL);
                    Application.DoEvents();
                    Thread.Sleep(timeout);
                    ProductTitle = browser.Title; // Название товара

                    try {
                        ProductPrice = browser.FindElement(By.Id("j-sku-discount-price")).Text;
                        if (ProductPrice != null)
                        {
                            if (ProductPrice.Contains("-"))
                            {
                                ProductPrice = ProductPrice.Split('-')[0];
                            }
                            if (ProductPrice.Contains("."))
                            {
                                ProductPrice = ProductPrice.Split('.')[0];
                            }
                        }
                    }
                    catch {
                        ProductPrice = browser.FindElement(By.Id("j-sku-price")).Text;
                        if (ProductPrice != null)
                        {
                            if (ProductPrice.Contains("-"))
                            {
                                ProductPrice = ProductPrice.Split('-')[0];
                            }
                            if (ProductPrice.Contains("."))
                            {
                                ProductPrice = ProductPrice.Split('.')[0];
                            }
                        }
                    }

                    try
                    {
                        var colorsElements = browser.FindElements(By.CssSelector("#j-product-info-sku dl dd ul li a img"));
                        foreach (var el in colorsElements) {
                            colors.Add(el.GetAttribute("title"));
                            MessageBox.Show(el.GetAttribute("title"), "Заголовок");
                        }
                    }
                    catch {

                    }

                    MessageBox.Show(ProductPrice, "Цена");
                    var sizesObjects = browser.FindElements(By.CssSelector("#j-sku-list-2 li a span"));

                    foreach (var sizeVal in sizesObjects)
                    {
                        sizes.Add(sizeVal.Text);
                        MessageBox.Show(sizeVal.Text, "Размеры");                      
                    }


                    var featuresObjeectsTitle = browser.FindElements(By.CssSelector("span.propery-title"));
                    var featuresObjeectsDes = browser.FindElements(By.CssSelector("span.propery-des"));
                    string[,] featuresEndValues = new string[featuresObjeectsTitle.Count, 2];

                    for (int i = 0; i < featuresObjeectsTitle.Count; i++)
                    {
                        if (featuresObjeectsTitle[i].Text == "Материал:" || featuresObjeectsTitle[i].Text == "Длина платья:" || featuresObjeectsTitle[i].Text == "Сезон:" || featuresObjeectsTitle[i].Text == "Длина рукава:") {
                            featuresEndValues[i, 0] = featuresObjeectsTitle[i].Text;
                            featuresEndValues[i, 1] = featuresObjeectsDes[i].Text;
                        }
                                               
                    }                    

                    IWebElement imageToNextPage = browser.FindElement(By.CssSelector(".ui-image-viewer a.ui-magnifier-glass"));
                    imageToNextPage.Click();
                    Application.DoEvents();
                    Thread.Sleep(timeout);                    
                    
                    var LIimagesForSave = browser.FindElements(By.CssSelector("ul.new-img-border li a img"));

                    foreach (var str in LIimagesForSave) {
                        Thread.Sleep(1000);
                        jse.ExecuteScript(string.Format("var link = document.createElement('a'); link.target = '_blank'; link.download = 'img.jpg'; link.href = '{0}'; link.click();", str.GetAttribute("src")));                        
                    }

                    sizes.Clear();
                    features.Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public void PrintingToExcel(string title, string price, List<string> sizes, List<string> colors, string[,] featuresEndValues) {
            excelSheets = excelWB[0].Worksheets;
            //Получаем ссылку на лист 1
            excelWSheets = (Excel.Worksheet)excelSheets.get_Item(1);
            //Выделение группы ячеек
            excelWSheets.Rows.RowHeight = 260;
        }
    }
}
