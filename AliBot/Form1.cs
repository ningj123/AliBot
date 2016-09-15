using System;
using System.Collections.Generic;
using System.Linq;
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
        string filePath;
        public int timeout;
        public OpenFileDialog OPD = new OpenFileDialog();
        ChromeOptions chromeOptions;
        StreamReader urlsFiles;
        string[] urls;
        string ProductTitle;
        string ProductPrice;
        string DiscountProductPrice;
        string downloadFolderPath;
        int productId;    

        List<string> features = new List<string>(); // Лист с характеристиками
        string[,] featuresEndListVal;
        string[][,] infoStringArr;     
        List<string> newFileNames = new List<string>(); //Лист с новыми именами файлов  

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

        /// <summary>
        /// Инициализирует все необходимые инструменты для сбора
        /// информации. Запускает Chrome, Excel и заполняет
        /// необходимые переменные.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenBrowser_Click(object sender, EventArgs e)
        {
            try
            {
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
                productId = (int)StartId.Value - 1;
            }
            catch (Exception ex) {
                writeLog(ex.Message);
            }
        }

        /// <summary>
        /// Закрывает открытые приложения Chrome, Excel и данное приложение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseBrowser_Click(object sender, EventArgs e)
        {            
            try
            {
                browser.Quit();
                ExApp.Quit();
                this.Close();
            }
            catch (Exception ex){
                writeLog(ex.Message);
                MessageBox.Show("Ошибка", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// Приступить к работе(Начать сбор информации). Инструменты должны быть инициализированы,
        /// и выбран файл со списком URL товаров которые нужно спарсить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartWork_Click(object sender, EventArgs e)
        {
            timeout = (int)TimeOutSetter.Value;
            try
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

                    if (titleCheckBox.Checked)
                    {
                        ProductTitle = getProductTitle();
                    }
                    else ProductTitle = null;
                    if (PriceCheckBox.Checked)
                    {
                        ProductPrice = getProductPrice();
                    }
                    else ProductPrice = null;
                    if (discountPriceCheckBox.Checked)
                    {
                        DiscountProductPrice = getDiscountProductPrice();
                    }
                    else DiscountProductPrice = null;
                    if (FeaturesCheckbox.Checked)
                    {
                        featuresEndListVal = getFeatures();
                    }
                    else featuresEndListVal = null;
                    if (InfoCheckbox.Checked)
                    {
                        infoStringArr = getAllInfo();
                    }
                    else infoStringArr = null;
                    if (ImagesCheckBox.Checked)
                    {
                        newFileNames = getProductImages();
                    }
                    else newFileNames = null;
                    Application.DoEvents();
                    PrintingToExcel(ProductTitle, ProductPrice, DiscountProductPrice, featuresEndListVal, infoStringArr, newFileNames);
                }
            }
            catch (Exception ex)
            {
                writeLog(ex.Message);
                MessageBox.Show(ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Запись данных в таблицы Excel.
        /// </summary>
        /// <param name="productTitle"></param>
        /// <param name="productPrice"></param>
        /// <param name="discountProductPrice"></param>
        /// <param name="features"></param>
        /// <param name="allInfo"></param>
        /// <param name="productImg"></param>
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
                        if (strArr[0, 0] != null & strArr[1, d] != null) {
                            cells = workSheetOPTIONSVALUES.Range["A" + lastInfoRow, "A" + lastInfoRow];
                            cells.Value2 = productId.ToString();
                            cells = workSheetOPTIONSVALUES.Range["B" + lastInfoRow, "B" + lastInfoRow];
                            cells.Value2 = strArr[0, 0].ToString();
                            cells = workSheetOPTIONSVALUES.Range["C" + lastInfoRow, "C" + lastInfoRow];
                            cells.Value2 = strArr[1, d].ToString();
                            lastInfoRow += 1;
                        }                        
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

        /// <summary>
        /// Переименовать загруженные картинки и после этого переместить их
        /// в папку для скаченных картинок.
        /// </summary>
        /// <param name="oldImagesNames"></param>
        /// <returns></returns>
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
                return newFileNames;
            }
            catch (Exception ex) {                
                writeLog(ex.Message);
                return null;
            }          
            
        }

        /// <summary>
        /// Показывает всплывающее окно "О нас".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Click(object sender, EventArgs e)
        {
            AboutUs AU = new AboutUs();
            AU.ShowDialog();              
        }

        /// <summary>
        /// Получить заголовок продукта.
        /// </summary>
        /// <returns></returns>
        string getProductTitle() {
            return browser.Title.ToString();
        }

        /// <summary>
        /// Получить цену продукта.
        /// </summary>
        /// <returns></returns>
        string getProductPrice() {
            try
            {
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
                else
                {
                    return null;
                }
            }
            catch (Exception ex) {
                writeLog(ex.Message);
                return null;                
            }
        }

        /// <summary>
        /// Получить цену продукта со скидкой.
        /// </summary>
        /// <returns></returns>
        string getDiscountProductPrice()
        {
            try
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
            catch (Exception ex) {
                writeLog(ex.Message);
                return "Нет скидки";                
            }
        }

        /// <summary>
        /// Получить все характеристики товара.
        /// </summary>
        /// <returns></returns>
        string[,] getFeatures() {
            try
            {
                List<IWebElement> featuresObjeectsTitle = browser.FindElements(By.CssSelector("span.propery-title")).ToList();
                List<IWebElement> featuresObjeectsDes = browser.FindElements(By.CssSelector("span.propery-des")).ToList();
                string[,] featuresTempValues = new string[featuresObjeectsTitle.Count, 2];
                if (featuresObjeectsTitle.Count > 0 & featuresObjeectsDes.Count > 0)
                {
                    for (int i = 0; i < featuresObjeectsTitle.Count; i++)
                    {
                        featuresTempValues[i, 0] = featuresObjeectsTitle[i].Text;
                        featuresTempValues[i, 1] = featuresObjeectsDes[i].Text;
                    }
                }
                return featuresTempValues;
            }
            catch (Exception ex) {
                writeLog(ex.Message);
                return null;                
            }
        }

        /// <summary>
        /// Получить основные изображения товара. И после вызывается метод RenameImages();
        /// </summary>
        /// <returns></returns>
        List<string> getProductImages() {
            try
            {
                Thread.Sleep(timeout);
                IWebElement imageToNextPage = browser.FindElement(By.CssSelector(".ui-image-viewer a.ui-magnifier-glass"));
                browser.Navigate().GoToUrl(string.Format(imageToNextPage.GetAttribute("href")));
                Application.DoEvents();
                IJavaScriptExecutor newJSE = browser as IJavaScriptExecutor;
                List<string> tempFileNames = new List<string>();
                List<string> OldFileNames = new List<string>(); //Лист со старыми именами файлов
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
                OldFileNames.Clear();
                return tempFileNames;
            }
            catch (Exception ex) {
                writeLog(ex.Message);
                return null;                
            }
        }        

        /// <summary>
        /// Получить всю информацию о продукте: Цвета, размеры и т.д.
        /// </summary>
        /// <returns></returns>
        string[][,] getAllInfo() {
            Application.DoEvents();
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
                                Thread.Sleep(timeout);
                                JSEcolorUmg.ExecuteScript(string.Format("var link = document.createElement('a'); link.target = '_blank'; link.download = 'img.jpg'; link.href = '{0}'; link.click();", ImgEl.GetAttribute("src")));
                                Thread.Sleep(timeout);
                                if (colorImgCount == 0)
                                {
                                    string tempName = HttpUtility.UrlDecode(ImgEl.GetAttribute("src").ToString().Split('/').Last().Split('.')[0] + ".jpg_50x50.jpg");
                                    oldFileNames.Add(tempName);
                                }
                                else
                                {
                                    string tempName = HttpUtility.UrlDecode(string.Format(ImgEl.GetAttribute("src").ToString().Split('/').Last().Split('.')[0] + ".jpg_50x50 ({0}).jpg", colorImgCount));
                                    oldFileNames.Add(tempName);
                                }
                                Thread.Sleep(timeout);
                                colorImgCount += 1;
                            }
                            tempNewFiles = renameShareImages(oldFileNames);
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

        /// <summary>
        /// Переименовать изображения из информации о продукте. т.е. если в информации о продукте вместо Цвета
        /// изображения, то после загрузки этих изображений на компьютер вызывается данный метод, для переименования
        /// и перемещения товара в соответствующую папку.
        /// </summary>
        /// <param name="OldNames"></param>
        /// <returns></returns>
        public List<string> renameShareImages(List<string> OldNames)
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

        /// <summary>
        /// Записать данные в лог. При ошибках.
        /// </summary>
        /// <param name="message"></param>
        public void writeLog(string message) {
            try
            {
                using (StreamWriter logWriter = new StreamWriter("/Log.txt", true))
                {
                    logWriter.WriteLine(string.Format("{0} ID товара: {1}; Ошибка: {2}", DateTime.Now, productId, message));
                    logWriter.Close();
                }
            }
            catch {
                MessageBox.Show("Ошибка записи файла Log.txt", "Ошибка");
            }
        }

        /// <summary>
        /// Открывает диалог выбора файла со списком URL адресов товаров,
        /// которые нужно спарсить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSelectorURLs_Click(object sender, EventArgs e)
        {
            if (OPD.ShowDialog() == DialogResult.OK)
            {
                filePath = OPD.FileName;
                label1.Text = filePath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK) {
                downloadFolderPath = FBD.SelectedPath;
            }
        }
    }
}
