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
        string DiscountProductPrice;
        string downloadFolderPath = @"C:\Users\Админ\Downloads\";
        int productId;    

        List<string> colors = new List<string>(); // Лист с цветами 
        List<string> sizes = new List<string>(); // Лист с Размерами
        List<string> features = new List<string>(); // Лист с характеристиками
        string[,] featuresEndListVal;
        string[][,] infoStringArr;
        List<string> listOfType = new List<string>(); //Список Типов данных товара (Размер, цвет, и т.д.)        
        List<string> OldFileNames = new List<string>(); //Лист со старыми именами файлов
        List<string> newFileNames = new List<string>(); //Лист с новыми именами файлов
        List<string> colorImgNames = new List<string>(); // Лист с новыми именами картинок цветов  
        List<string> OldColorImgNames = new List<string>(); // Лист со старыми именами картинок цветов     

        /*Все что касается Excel*/

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
            public string lastProductsRow;
            public int lastImagesRow;
            public int lastOptionsRow;
            public string lastOptionsValuesRow;
            public int lastAttributesRow;
            public int lastColorRow;
            public int lastSizeRow;
            public int lastMainRow;
            public int lastInfoRow;

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
            lastMainRow = 1;
            lastOptionsRow = 1;
            lastInfoRow = 1;
            productId = (int)StartId.Value;
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
                    /*Получаем размеры*/

                    /*Получаем характеристики*/
                    var featuresObjeectsTitle = browser.FindElements(By.CssSelector("span.propery-title"));
                    var featuresObjeectsDes = browser.FindElements(By.CssSelector("span.propery-des"));
                    string[,] featuresEndValues = new string[featuresObjeectsTitle.Count, 2];

                    for (int i = 0; i < featuresObjeectsTitle.Count; i++)
                    {                        
                        featuresEndValues[i, 0] = featuresObjeectsTitle[i].Text;
                        featuresEndValues[i, 1] = featuresObjeectsDes[i].Text;                         
                    }
                    /*Получаем характеристики*/

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

                    //Thread.Sleep(timeout);
                    //newFileNames = RenameImages(OldFileNames);                    
                    //PrintingToExcel(ProductTitle, ProductPrice, sizes, colors, featuresEndValues, newFileNames);
                    //sizes.Clear();
                    //features.Clear();
                    //OldFileNames.Clear();
                    //newFileNames.Clear();
                    //OldColorImgNames.Clear();
                    //colors.Clear();
                }
            }
            catch (Exception ex)
            {
                StreamWriter SW = new StreamWriter(downloadFolderPath + "log.txt");
                SW.WriteLine("Ошибка основного цикла " + " - " + ex.Message + Environment.NewLine);
                SW.Close();                
            }
            
        }

        public void PrintingToExcel(string productTitle, string productPrice, string discountProductPrice, string[,] features, string[][,] allInfo, List<string> productImg) {
            Application.DoEvents();
            /*Первая страница*/            
            cells = workSheetPRODUCTS.Range["A" + lastMainRow, "A" + lastMainRow];
            cells.Value2 = productId.ToString();
            if (productTitle != null) {
                cells = workSheetPRODUCTS.Range["B" + lastMainRow, "B" + lastMainRow];
                cells.Value2 = productTitle.ToString();
            }
            if (productPrice != null) {
                cells = workSheetPRODUCTS.Range["C" + lastMainRow, "C" + lastMainRow];
                cells.Value2 = productPrice.ToString();
            }
            if (discountProductPrice != null) {
                cells = workSheetPRODUCTS.Range["D" + lastMainRow, "D" + lastMainRow];
                cells.Value2 = discountProductPrice.ToString();
            }
            /*Первая страница*/

            /*Вторая страница*/
            if (newFileNames != null) {
                if (newFileNames.Count > 0) { 
                    foreach (string str in newFileNames)
                    {
                        cells = workSheetIMAGES.Range["A" + lastImagesRow, "A" + lastImagesRow];
                        cells.Value2 = productId;
                        cells = workSheetIMAGES.Range["B" + lastImagesRow, "B" + lastImagesRow];
                        cells.Value2 = str;
                        cells = workSheetIMAGES.Range["C" + lastImagesRow, "C" + lastImagesRow];
                        cells.Value2 = "0";
                        lastImagesRow += 1;
                    }
                }
            }
            /*Вторая страница*/

            /*Третья страница*/
            if (allInfo != null) {
                foreach (string[,] strArr in allInfo) {                    
                    cells = workSheetOPTIONS.Range["A" + lastOptionsRow, "A" + lastOptionsRow];
                    cells.Value2 = productId.ToString();
                    cells = workSheetOPTIONS.Range["B" + lastOptionsRow, "B" + lastOptionsRow];
                    cells.Value2 = strArr[0,0].ToString();
                    lastOptionsRow += 1;
                }
            }
            /*Третья страница*/

            /*Четвертая страница*/
            if (allInfo != null) {
                foreach (string[,] strArr in allInfo) {
                    for (int d = 0; d < strArr.GetLength(1); d++) {
                        cells = workSheetOPTIONSVALUES.Range["A" + lastInfoRow, "A" + lastInfoRow];
                        cells.Value2 = productId.ToString();
                        cells = workSheetOPTIONSVALUES.Range["B" + lastInfoRow, "B" + lastInfoRow];
                        cells.Value2 = strArr[0,0].ToString();
                        cells = workSheetOPTIONSVALUES.Range["C" + lastInfoRow, "C" + lastInfoRow];
                        cells.Value2 = strArr[1,d].ToString();
                        lastInfoRow += 1;
                    }
                }
            }
            /*Четвертая страница*/

            /*Пятая страница*/
            for (int i = 0; i < features.Length/2; i++) {
                cells = workSheetATTRIBUTES.Range["A" + lastAttributesRow, "A" + lastAttributesRow];
                cells.Value2 = productId.ToString();
                cells = workSheetATTRIBUTES.Range["B" + lastAttributesRow, "B" + lastAttributesRow];
                cells.Value2 = "Характеристики";
                cells = workSheetATTRIBUTES.Range["C" + lastAttributesRow, "C" + lastAttributesRow];
                cells.Value2 = features[i,0];
                cells = workSheetATTRIBUTES.Range["D" + lastAttributesRow, "D" + lastAttributesRow];
                cells.Value2 = features[i, 1];
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
                    File.Move(downloadFolderPath + str, downloadFolderPath + "img/" + productId + charFileName + ".jpg");
                    newFileNames.Add((productId.ToString() + (char)charFileName + ".jpg").ToString());
                    charFileName += (char)1;
                }
            }
            catch (Exception ex) {
                StreamWriter SW = new StreamWriter(downloadFolderPath+"log.txt");
                SW.WriteLine("Ошибка чтения файла " + productId.ToString() + " - " + ex.Message + Environment.NewLine);
                SW.Close();                
            }
            
            return newFileNames;
        }

        private void TEST_Click(object sender, EventArgs e)
        {                
                urlsFiles = new StreamReader(OPD.FileName);
                urls = urlsFiles.ReadToEnd().Split('\n');
                urlsFiles.Close();
                browser.Navigate().GoToUrl("http://ru.aliexpress.com/");
                Thread.Sleep(timeout);
                foreach (string currentURL in urls)
                {
                    productId += 1;
                    browser.Navigate().GoToUrl(currentURL);
                    Application.DoEvents();

                if (titleCheckBox.Checked) {
                    ProductTitle = getProductTitle();
                }
                else ProductTitle = null;
                if (PriceCheckBox.Checked) {
                    ProductPrice = getProductPrice();
                }
                else ProductPrice = null;
                if (discountPriceCheckBox.Checked) {
                    DiscountProductPrice = getDiscountProductPrice();
                }
                else DiscountProductPrice = null;
                if (FeaturesCheckbox.Checked) {
                    featuresEndListVal = getFeatures();
                }
                else featuresEndListVal = null;
                if (InfoCheckbox.Checked) {
                    infoStringArr = getAllInfo();
                }
                else infoStringArr = null;
                if (ImagesCheckBox.Checked) {
                    newFileNames = getProductImages();
                }
                else newFileNames = null;
                PrintingToExcel(ProductTitle, ProductPrice, DiscountProductPrice, featuresEndListVal, infoStringArr, newFileNames);
            }
         }

        string getProductTitle() {
            return browser.Title.ToString();
        }

        string getProductPrice() {
            List<IWebElement> prices = browser.FindElements(By.ClassName("p-price")).ToList();            
            if (prices.Count > 0)
            {
                string price = prices[0].Text;
                if (price.Contains('-'))
                {
                    price = price.Split('-')[0];
                }
                if (price.Contains('.'))
                {
                    price = price.Split('.')[0];
                }
                return price;
            }
            else {
                return null;
            }

        }

        string getDiscountProductPrice()
        {
            List<IWebElement> discPrices = browser.FindElements(By.ClassName("p-price")).ToList();
            if (discPrices.Count > 1)
            {
                string price = discPrices[1].Text;
                if (price.Contains('-'))
                {
                    price = price.Split('-')[0];
                }
                if (price.Contains('.'))
                {
                    price = price.Split('.')[0];
                }
                return price;
            }
            else
            {
                return "Нет скидки";
            }
        }

        string[,] getFeatures() {

            List<IWebElement> featuresObjeectsTitle = browser.FindElements(By.CssSelector("span.propery-title")).ToList();
            List<IWebElement> featuresObjeectsDes = browser.FindElements(By.CssSelector("span.propery-des")).ToList();
            string[,] featuresTempValues = new string[featuresObjeectsTitle.Count, 2];
            if (featuresObjeectsTitle.Count > 0 & featuresObjeectsDes.Count > 0) { 
                for (int i = 0; i < featuresObjeectsTitle.Count; i++)
                {
                    featuresTempValues[i, 0] = featuresObjeectsTitle[i].Text;
                    featuresTempValues[i, 1] = featuresObjeectsDes[i].Text;
                }
            }
            return featuresTempValues;
        }

        List<string> getProductImages() {
            Thread.Sleep(2500);
            IWebElement imageToNextPage = browser.FindElement(By.CssSelector(".ui-image-viewer a.ui-magnifier-glass"));
            browser.Navigate().GoToUrl(string.Format(imageToNextPage.GetAttribute("href")));
            Application.DoEvents();
            IJavaScriptExecutor newJSE = browser as IJavaScriptExecutor;
            List<string> tempFileNames = new List<string>();
            List<IWebElement> LIimagesForSave = browser.FindElements(By.CssSelector("ul.new-img-border li a img")).ToList();            
            int imageCount = 0;            
            if (LIimagesForSave.Count > 0)
            {
                foreach (var str in LIimagesForSave)
                {                    
                    Thread.Sleep(timeout);
                    newJSE.ExecuteScript(string.Format("var link = document.createElement('a'); link.target = '_blank'; link.download = 'img.jpg'; link.href = '{0}'; link.click();", str.GetAttribute("src")));
                    if (imageCount == 0)
                    {
                        OldFileNames.Add(str.GetAttribute("src").Split('/').Last());
                    }
                    else
                    {
                        OldFileNames.Add(str.GetAttribute("src").Split('/').Last().Split('.')[0] + " (" + imageCount + ")" + ".jpg");
                    }
                    imageCount += 1;
                }
                Thread.Sleep(timeout);
                Application.DoEvents();
                char charFileName = 'A';
                
                foreach (string str in OldFileNames)
                {                    
                    File.Move(downloadFolderPath + str, downloadFolderPath + "img/" + productId + charFileName + ".jpg");
                    tempFileNames.Add((productId.ToString() + (char)charFileName + ".jpg").ToString());
                    charFileName += (char)1;
                }    
            }
            else
            {
                tempFileNames = null;
            }
            return tempFileNames;
        }

        List<string> getColors() {
            List<string> tempColorsNames = new List<string>();
            List<IWebElement> colorsElementsIMG = browser.FindElements(By.CssSelector("#j-product-info-sku dl dd ul li a img")).ToList();
            List<IWebElement> colorsElementsSpan = browser.FindElements(By.CssSelector("#j-product-info-sku dl dd ul li a span")).ToList();
            IJavaScriptExecutor newjse = browser as IJavaScriptExecutor;
            int colorImgCount = 0;
            if (colorsElementsIMG.Count > 0)
            {
                foreach (var el in colorsElementsIMG)
                {
                    Thread.Sleep(1000);
                    newjse.ExecuteScript(string.Format("var link = document.createElement('a'); link.target = '_blank'; link.download = 'img.jpg'; link.href = '{0}'; link.click();", el.GetAttribute("src")));
                    if (colorImgCount == 0)
                    {
                        string tempName = HttpUtility.UrlDecode(el.GetAttribute("src").ToString().Split('/').Last().Split('.')[0] + ".jpg_50x50.jpg");
                        OldColorImgNames.Add(tempName);
                    }
                    else
                    {
                        string tempName = HttpUtility.UrlDecode(string.Format(el.GetAttribute("src").ToString().Split('/').Last().Split('.')[0] + ".jpg_50x50 ({0}).jpg", colorImgCount));
                        OldColorImgNames.Add(tempName);
                    }
                    colorImgCount += 1;
                }
                Thread.Sleep(timeout);
                char newNameChar = 'A';
                foreach (string el in OldColorImgNames)
                {                    
                    File.Move(downloadFolderPath + el, downloadFolderPath + @"colorImg\" + "colorImg_" + productId + newNameChar + ".jpg");
                    tempColorsNames.Add(@"colorImg\colorImg_" + productId + newNameChar + ".jpg");
                    newNameChar = (char)(newNameChar + 1);
                }
            }
            else if (colorsElementsSpan.Count > 0) {
                foreach (IWebElement el in colorsElementsSpan) {
                    tempColorsNames.Add(el.GetAttribute("title"));
                }                
            }
            return tempColorsNames;       
        }

        List<string> getSizes() {
            List<string> tempSizesList = new List<string>();
            List<IWebElement> sizesObjects = browser.FindElements(By.CssSelector("#j-sku-list-2 li a span")).ToList();
            if (sizesObjects.Count > 0)
            {
                foreach (var sizeVal in sizesObjects)
                {
                    tempSizesList.Add(sizeVal.Text);
                }
            }
            else tempSizesList = null;
            return tempSizesList;
        }

        string[][,] getAllInfo() {
            List<IWebElement> infoParent = browser.FindElements(By.Id("j-product-info-sku")).ToList();
            List<string> infoTitles = new List<string>();
            List<string> infoValues = new List<string>();
            string[][,] endValuesResult;
            if (infoParent.Count > 0) {
                List<IWebElement> infoElParent = infoParent[0].FindElements(By.CssSelector("dl")).ToList();
                if (infoElParent.Count > 0)
                {
                    endValuesResult = new string[infoElParent.Count][,];
                    int counter = 0;                  
                    foreach (IWebElement el in infoElParent)
                    {
                        List<IWebElement> infoSpan = el.FindElements(By.CssSelector("dd ul li a span")).ToList();
                        List<IWebElement> infoImg = el.FindElements(By.CssSelector("dd ul li a img")).ToList();
                        if (infoSpan.Count > 0)
                        {
                            endValuesResult[counter] = new string[2, infoSpan.Count];
                            endValuesResult[counter][0, 0] = el.FindElement(By.CssSelector("dt")).Text;
                            for (int y = 0; y < infoSpan.Count; y++)
                            {
                                endValuesResult[counter][1, y] = infoSpan[y].Text;
                            }
                        }
                        else if (infoImg.Count > 0) {
                            endValuesResult[counter] = new string[2, infoImg.Count];
                            endValuesResult[counter][0, 0] = el.FindElement(By.CssSelector("dt")).Text;
                            IJavaScriptExecutor JSEcolorUmg = browser as IJavaScriptExecutor;
                            List<string> oldFileNames = new List<string>();
                            List<string> tempNewFiles = new List<string>();
                            int colorImgCount = 0;
                            foreach (IWebElement ImgEl in infoImg) {                                
                                JSEcolorUmg.ExecuteScript(string.Format("var link = document.createElement('a'); link.target = '_blank'; link.download = 'img.jpg'; link.href = '{0}'; link.click();", ImgEl.GetAttribute("src")));
                                Thread.Sleep(2000);
                                if (colorImgCount == 0)
                                {
                                    string tempName = HttpUtility.UrlDecode(el.GetAttribute("src").ToString().Split('/').Last().Split('.')[0] + ".jpg_50x50.jpg");
                                    oldFileNames.Add(tempName);
                                }
                                else
                                {
                                    string tempName = HttpUtility.UrlDecode(string.Format(el.GetAttribute("src").ToString().Split('/').Last().Split('.')[0] + ".jpg_50x50 ({0}).jpg", colorImgCount));
                                    oldFileNames.Add(tempName);
                                }
                                Thread.Sleep(1000);
                                colorImgCount += 1;
                            }
                            tempNewFiles = renameColorImages(OldColorImgNames);
                            for (int h = 0; h < tempNewFiles.Count; h++) {
                                endValuesResult[counter][1, h] = tempNewFiles[h];
                            }
                        }                        
                        counter += 1;
                    } 
                }                
                else {
                    return null;
                }
            }
            else {
                return null;
            }
            return endValuesResult;
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
                    File.Move(downloadFolderPath + el, downloadFolderPath + @"colorImg\" + "colorImg_" + productId + newNameChar + ".jpg");
                    tempColorsNames.Add(@"colorImg\colorImg_" + productId + newNameChar + ".jpg");
                    newNameChar = (char)(newNameChar + 1);
                }
            }
            catch (Exception ex)
            {
                StreamWriter SW = new StreamWriter(downloadFolderPath + "log.txt");
                SW.WriteLine("Ошибка чтения файла " + productId.ToString() + " - " + ex.Message + Environment.NewLine);
                SW.Close();
            }
            return tempColorsNames;
        }
    }
}
