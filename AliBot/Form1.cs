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
using System.Web;

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
        string downloadFolderPath = @"C:\Users\Админ\Downloads\";        

        List<string> colors = new List<string>(); // Лист с цветами 
        List<string> sizes = new List<string>(); // Лист с Размерами
        List<string> features = new List<string>(); // Лист с характеристиками
        List<string> OldFileNames = new List<string>(); //Лист со старыми именами файлов
        List<string> newFileNames = new List<string>(); //Лист с новыми именами файлов
        List<string> colorImgNames = new List<string>(); // Лист с новыми именами картинок цветов  
        List<string> OldColorImgNames = new List<string>(); // Лист со старыми именами картинок цветов     

        /*Все что касается Excel*/

        string pathToEcxel; //Путь до файла Excel        
            public Excel.Application ExApp;
            public Excel.Workbook book;            
            public Excel.Worksheets workSheets;
            public Excel.Sheets sheets;
            public Excel.Range cells;
            public Excel.Worksheet workSheetPRODUCTS;
            public Excel.Worksheet workSheetIMAGES;
            public Excel.Worksheet workSheetOPTIONS;
            public Excel.Worksheet workSheetOPTIONSVALUES;
            public Excel.Worksheet workSheetATTRIBUTES;
            public int curentProductID;
            public string lastProductsRow;
            public int lastImagesRow;
            public string lastOptionsRow;
            public string lastOptionsValuesRow;
            public int lastAttributesRow;
            public int lastColorRow;
            public int lastSizeRow;

        /*Все что касается Excel*/
        public Form1()
        {
            InitializeComponent();            
        }

        private void OpenBrowser_Click(object sender, EventArgs e)
        {
            chromeOptions = new ChromeOptions();            
            browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            browser.Manage().Window.Maximize();
            pathToEcxel = @"C:\test.xlsx";
            ExApp = new Excel.Application();
            ExApp.Visible = true;
            ExApp.SheetsInNewWorkbook = 6;
            ExApp.Workbooks.Add(Type.Missing);
            book = ExApp.Workbooks[1];
            sheets = book.Worksheets;
            workSheetPRODUCTS = (Excel.Worksheet)sheets.Item[1];
            workSheetIMAGES = (Excel.Worksheet)sheets.Item[2];
            workSheetOPTIONS = (Excel.Worksheet)sheets.Item[3];
            workSheetOPTIONSVALUES = (Excel.Worksheet)sheets.Item[4];
            workSheetATTRIBUTES = (Excel.Worksheet)sheets.Item[5];
            lastColorRow = 0;
            lastSizeRow = 1;
            lastAttributesRow = 1;
            lastImagesRow = 1;
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
            curentProductID = 0;
            try
            {
                
                urlsFiles = new StreamReader(OPD.FileName);
                urls = urlsFiles.ReadToEnd().Split('\n');
                urlsFiles.Close();              
                browser.Navigate().GoToUrl("http://ru.aliexpress.com/");
                Thread.Sleep(timeout);
                foreach (string currentURL in urls) {
                    curentProductID += 1;
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

                    /*Получаем цвета*/
                    try
                    {
                        var colorsElements = browser.FindElements(By.CssSelector("#j-product-info-sku dl dd ul li a img"));
                        int colorImgCount = 0;                                         
                        foreach (var el in colorsElements) {
                            Thread.Sleep(2000);                            
                            jse.ExecuteScript(string.Format("var link = document.createElement('a'); link.target = '_blank'; link.download = 'img.jpg'; link.href = '{0}'; link.click();", el.GetAttribute("src")));
                            if (colorImgCount == 0)
                            {
                                string tempName =  HttpUtility.UrlDecode(el.GetAttribute("src").ToString().Split('/').Last().Split('.')[0] + ".jpg_50x50.jpg");
                                OldColorImgNames.Add(tempName);
                            }
                            else
                            {
                                string tempName = HttpUtility.UrlDecode(string.Format(el.GetAttribute("src").ToString().Split('/').Last().Split('.')[0] + ".jpg_50x50 ({0}).jpg", colorImgCount));
                                OldColorImgNames.Add(tempName);
                            }
                            colorImgCount += 1;                        
                        }
                        colors = renameColorImages(OldColorImgNames);

                    }
                    catch {
                        var colorsElements = browser.FindElements(By.CssSelector("#j-sku-list-1 li a"));
                        foreach (var el in colorsElements) {
                            colors.Add(el.GetAttribute("title"));                            
                        }
                    }
                    /*Получаем цвета*/

                    /*Получаем размеры*/
                    var sizesObjects = browser.FindElements(By.CssSelector("#j-sku-list-2 li a span"));

                    foreach (var sizeVal in sizesObjects)
                    {
                        sizes.Add(sizeVal.Text);                                        
                    }

                    var featuresObjeectsTitle = browser.FindElements(By.CssSelector("span.propery-title"));
                    var featuresObjeectsDes = browser.FindElements(By.CssSelector("span.propery-des"));
                    string[,] featuresEndValues = new string[featuresObjeectsTitle.Count, 2];

                    for (int i = 0; i < featuresObjeectsTitle.Count; i++)
                    {                        
                        featuresEndValues[i, 0] = featuresObjeectsTitle[i].Text;
                        featuresEndValues[i, 1] = featuresObjeectsDes[i].Text;                         
                    }
                    /*Получаем размеры*/

                    /* Качаем изображения */
                    IWebElement imageToNextPage = browser.FindElement(By.CssSelector(".ui-image-viewer a.ui-magnifier-glass"));
                    browser.Navigate().GoToUrl(string.Format(imageToNextPage.GetAttribute("href")));
                    Application.DoEvents();
                    Thread.Sleep(timeout);

                    var LIimagesForSave = browser.FindElements(By.CssSelector("ul.new-img-border li a img"));
                    //var LIimagesForSave = browser.FindElements(By.CssSelector("a.ui-image-viewer-thumb-frame img"));
                    int imageCount = 0;
                    foreach (var str in LIimagesForSave)
                    {
                        Thread.Sleep(4000);                        
                        jse.ExecuteScript(string.Format("var link = document.createElement('a'); link.target = '_blank'; link.download = 'img.jpg'; link.href = '{0}'; link.click();", str.GetAttribute("src")));
                        if (imageCount == 0)
                        {
                            OldFileNames.Add(str.GetAttribute("src").Split('/').Last());                            
                        }
                        else {
                            OldFileNames.Add(str.GetAttribute("src").Split('/').Last().Split('.')[0] + " (" + imageCount + ")" + ".jpg");
                        }                        
                        imageCount += 1;
                    }
                    /* Качаем изображения */

                    Thread.Sleep(timeout);
                    newFileNames = RenameImages(OldFileNames);                    
                    PrintingToExcel(ProductTitle, ProductPrice, sizes, colors, featuresEndValues, newFileNames);
                    sizes.Clear();
                    features.Clear();
                    OldFileNames.Clear();
                    newFileNames.Clear();
                    OldColorImgNames.Clear();
                    colors.Clear();
                }
            }
            catch (Exception ex)
            {
                StreamWriter SW = new StreamWriter(downloadFolderPath + "log.txt");
                SW.WriteLine("Ошибка основного цикла " + curentProductID.ToString() + " - " + ex.Message + Environment.NewLine);
                SW.Close();                
            }
            
        }

        public void PrintingToExcel(string title, string price, List<string> sizes, List<string> colors, string[,] featuresEndValues, List<string> newFileNames) {
            Application.DoEvents();
            /*Первая страница*/
            cells = workSheetPRODUCTS.Range["A" + curentProductID, "A" + curentProductID];
            cells.Value2 = curentProductID.ToString();
            cells = workSheetPRODUCTS.Range["B" + curentProductID, "B" + curentProductID];
            cells.Value2 = title.ToString();
            cells = workSheetPRODUCTS.Range["P" + curentProductID, "P" + curentProductID];
            cells.Value2 = price.ToString();
            /*Первая страница*/

            /*Вторая страница*/
            foreach (string str in newFileNames) {
                cells = workSheetIMAGES.Range["A" + lastImagesRow, "A" + lastImagesRow];
                cells.Value2 = curentProductID;
                cells = workSheetIMAGES.Range["B" + lastImagesRow, "B" + lastImagesRow];
                cells.Value2 = str;
                cells = workSheetIMAGES.Range["C" + lastImagesRow, "C" + lastImagesRow];
                cells.Value2 = "0";
                lastImagesRow += 1;
            }
                        
            /*Вторая страница*/

            /*Третья страница*/
            //cells = workSheetOPTIONS.Range["A" + curentProductID, "A" + curentProductID];
            //cells.Value2 = curentProductID.ToString();
            //cells = workSheetOPTIONS.Range["A" + curentProductID+1, "A" + curentProductID+1];
            //cells.Value2 = curentProductID.ToString();
            /*Третья страница*/
            
            /*Четвертая страница*/
            lastColorRow = lastSizeRow;
            for (int i = 0; i < colors.Count; i++) {
                cells = workSheetOPTIONSVALUES.Range["A" + lastColorRow, "A" + lastColorRow];
                cells.Value2 = curentProductID.ToString();
                cells = workSheetOPTIONSVALUES.Range["B" + lastColorRow, "B" + lastColorRow];
                cells.Value2 = "Цвет";
                cells = workSheetOPTIONSVALUES.Range["C" + lastColorRow, "C" + lastColorRow];
                cells.Value2 = colors[i].ToString();
                lastColorRow += 1;
            }
            lastSizeRow = lastColorRow;
            for (int i = 0; i < sizes.Count; i++)
            {
                cells = workSheetOPTIONSVALUES.Range["A" + lastSizeRow, "A" + lastSizeRow];
                cells.Value2 = curentProductID.ToString();
                cells = workSheetOPTIONSVALUES.Range["B" + lastSizeRow, "B" + lastSizeRow];
                cells.Value2 = "Размер";
                cells = workSheetOPTIONSVALUES.Range["C" + lastSizeRow, "C" + lastSizeRow];
                cells.Value2 = sizes[i].ToString();
                lastSizeRow += 1;
            }
            /*Четвертая страница*/

            /*Пятая страница*/
            for (int i = 0; i < featuresEndValues.Length/2; i++) {
                cells = workSheetATTRIBUTES.Range["A" + lastAttributesRow, "A" + lastAttributesRow];
                cells.Value2 = curentProductID.ToString();
                cells = workSheetATTRIBUTES.Range["B" + lastAttributesRow, "B" + lastAttributesRow];
                cells.Value2 = "Характеристики";
                cells = workSheetATTRIBUTES.Range["C" + lastAttributesRow, "C" + lastAttributesRow];
                cells.Value2 = featuresEndValues[i,0];
                cells = workSheetATTRIBUTES.Range["D" + lastAttributesRow, "D" + lastAttributesRow];
                cells.Value2 = featuresEndValues[i, 1];
                lastAttributesRow += 1;
            }
            /*Пятая страница*/

        }

        public List<string> RenameImages(List<string> oldImagesNames) {
            Application.DoEvents();
            char charFileName = 'A';
            try
            {
                foreach (string str in oldImagesNames)
                {
                    Thread.Sleep(2000);
                    File.Move(downloadFolderPath + str, downloadFolderPath + "img/" + curentProductID + charFileName + ".jpg");
                    newFileNames.Add((curentProductID.ToString() + (char)charFileName + ".jpg").ToString());
                    charFileName += (char)1;
                }
            }
            catch (Exception ex) {
                StreamWriter SW = new StreamWriter(downloadFolderPath+"log.txt");
                SW.WriteLine("Ошибка чтения файла " + curentProductID.ToString() + " - " + ex.Message + Environment.NewLine);
                SW.Close();                
            }
            
            return newFileNames;
        }

        public List<string> renameColorImages(List<string> OldNames)
        {
            Application.DoEvents();
            Thread.Sleep(2000);
            List<string> tempColorsNames = new List<string>();
            char newNameChar = 'A';
            try
            {
                foreach (string el in OldNames)
                {
                    Thread.Sleep(100);
                    File.Move(downloadFolderPath + el, downloadFolderPath + @"colorImg\" + "colorImg_" + curentProductID + newNameChar + ".jpg");
                    tempColorsNames.Add(@"colorImg\colorImg_" + curentProductID + newNameChar + ".jpg");
                    newNameChar = (char)(newNameChar + 1);
                }
            }
            catch (Exception ex)
            {
                StreamWriter SW = new StreamWriter(downloadFolderPath + "log.txt");
                SW.WriteLine("Ошибка чтения файла " + curentProductID.ToString() + " - " + ex.Message + Environment.NewLine);
                SW.Close();
            }
            return tempColorsNames;
        }
    }

}
