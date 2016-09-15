namespace AliBot
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.OpenBrowser = new System.Windows.Forms.Button();
            this.CloseBrowser = new System.Windows.Forms.Button();
            this.FileSelectorURLs = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.StartWork = new System.Windows.Forms.Button();
            this.downFolderPathLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TimeOutSetter = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.TEST = new System.Windows.Forms.Button();
            this.ImagesCheckBox = new System.Windows.Forms.CheckBox();
            this.PriceCheckBox = new System.Windows.Forms.CheckBox();
            this.titleCheckBox = new System.Windows.Forms.CheckBox();
            this.FeaturesCheckbox = new System.Windows.Forms.CheckBox();
            this.discountPriceCheckBox = new System.Windows.Forms.CheckBox();
            this.InfoCheckbox = new System.Windows.Forms.CheckBox();
            this.StartId = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.URLSimgStatus = new System.Windows.Forms.PictureBox();
            this.DownloadFolderStatus = new System.Windows.Forms.PictureBox();
            this.ToolsStatus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.TimeOutSetter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.URLSimgStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadFolderStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenBrowser
            // 
            this.OpenBrowser.Enabled = false;
            this.OpenBrowser.Location = new System.Drawing.Point(44, 79);
            this.OpenBrowser.Name = "OpenBrowser";
            this.OpenBrowser.Size = new System.Drawing.Size(132, 23);
            this.OpenBrowser.TabIndex = 0;
            this.OpenBrowser.Text = "Открыть Инструменты";
            this.OpenBrowser.UseVisualStyleBackColor = true;
            this.OpenBrowser.Click += new System.EventHandler(this.OpenBrowser_Click);
            // 
            // CloseBrowser
            // 
            this.CloseBrowser.BackColor = System.Drawing.Color.DimGray;
            this.CloseBrowser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CloseBrowser.ForeColor = System.Drawing.Color.Maroon;
            this.CloseBrowser.Location = new System.Drawing.Point(44, 283);
            this.CloseBrowser.Name = "CloseBrowser";
            this.CloseBrowser.Size = new System.Drawing.Size(132, 54);
            this.CloseBrowser.TabIndex = 1;
            this.CloseBrowser.Text = "Закрыть Chrome";
            this.CloseBrowser.UseVisualStyleBackColor = false;
            this.CloseBrowser.Click += new System.EventHandler(this.CloseBrowser_Click);
            // 
            // FileSelectorURLs
            // 
            this.FileSelectorURLs.BackColor = System.Drawing.Color.DimGray;
            this.FileSelectorURLs.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.FileSelectorURLs.ForeColor = System.Drawing.Color.Maroon;
            this.FileSelectorURLs.Location = new System.Drawing.Point(44, 21);
            this.FileSelectorURLs.Name = "FileSelectorURLs";
            this.FileSelectorURLs.Size = new System.Drawing.Size(132, 23);
            this.FileSelectorURLs.TabIndex = 2;
            this.FileSelectorURLs.Text = "Выбрать файл\r\n";
            this.FileSelectorURLs.UseVisualStyleBackColor = false;
            this.FileSelectorURLs.Click += new System.EventHandler(this.FileSelectorURLs_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выбрать файл со списком товаров";
            // 
            // StartWork
            // 
            this.StartWork.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.StartWork.Enabled = false;
            this.StartWork.Location = new System.Drawing.Point(44, 195);
            this.StartWork.Name = "StartWork";
            this.StartWork.Size = new System.Drawing.Size(132, 67);
            this.StartWork.TabIndex = 4;
            this.StartWork.Text = "Начать сбор информации";
            this.StartWork.UseVisualStyleBackColor = false;
            this.StartWork.Click += new System.EventHandler(this.StartWork_Click);
            // 
            // downFolderPathLabel
            // 
            this.downFolderPathLabel.AutoSize = true;
            this.downFolderPathLabel.Location = new System.Drawing.Point(185, 55);
            this.downFolderPathLabel.Name = "downFolderPathLabel";
            this.downFolderPathLabel.Size = new System.Drawing.Size(277, 13);
            this.downFolderPathLabel.TabIndex = 6;
            this.downFolderPathLabel.Text = "Выбрать папку в которую сохраняются изображения";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DimGray;
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.ForeColor = System.Drawing.Color.Maroon;
            this.button1.Location = new System.Drawing.Point(44, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Выбрать файл\r\n";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TimeOutSetter
            // 
            this.TimeOutSetter.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.TimeOutSetter.Location = new System.Drawing.Point(44, 114);
            this.TimeOutSetter.Maximum = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.TimeOutSetter.Name = "TimeOutSetter";
            this.TimeOutSetter.Size = new System.Drawing.Size(120, 20);
            this.TimeOutSetter.TabIndex = 7;
            this.TimeOutSetter.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Задержка в мс";
            // 
            // TEST
            // 
            this.TEST.Location = new System.Drawing.Point(449, 277);
            this.TEST.Name = "TEST";
            this.TEST.Size = new System.Drawing.Size(132, 60);
            this.TEST.TabIndex = 9;
            this.TEST.Text = "О программе";
            this.TEST.UseVisualStyleBackColor = true;
            this.TEST.Click += new System.EventHandler(this.About_Click);
            // 
            // ImagesCheckBox
            // 
            this.ImagesCheckBox.AutoSize = true;
            this.ImagesCheckBox.Location = new System.Drawing.Point(485, 96);
            this.ImagesCheckBox.Name = "ImagesCheckBox";
            this.ImagesCheckBox.Size = new System.Drawing.Size(96, 17);
            this.ImagesCheckBox.TabIndex = 12;
            this.ImagesCheckBox.Text = "Изображения";
            this.ImagesCheckBox.UseVisualStyleBackColor = true;
            // 
            // PriceCheckBox
            // 
            this.PriceCheckBox.AutoSize = true;
            this.PriceCheckBox.Location = new System.Drawing.Point(484, 50);
            this.PriceCheckBox.Name = "PriceCheckBox";
            this.PriceCheckBox.Size = new System.Drawing.Size(52, 17);
            this.PriceCheckBox.TabIndex = 11;
            this.PriceCheckBox.Text = "Цена";
            this.PriceCheckBox.UseVisualStyleBackColor = true;
            // 
            // titleCheckBox
            // 
            this.titleCheckBox.AutoSize = true;
            this.titleCheckBox.Location = new System.Drawing.Point(484, 27);
            this.titleCheckBox.Name = "titleCheckBox";
            this.titleCheckBox.Size = new System.Drawing.Size(80, 17);
            this.titleCheckBox.TabIndex = 10;
            this.titleCheckBox.Text = "Заголовок";
            this.titleCheckBox.UseVisualStyleBackColor = true;
            // 
            // FeaturesCheckbox
            // 
            this.FeaturesCheckbox.AutoSize = true;
            this.FeaturesCheckbox.Location = new System.Drawing.Point(484, 119);
            this.FeaturesCheckbox.Name = "FeaturesCheckbox";
            this.FeaturesCheckbox.Size = new System.Drawing.Size(109, 17);
            this.FeaturesCheckbox.TabIndex = 14;
            this.FeaturesCheckbox.Text = "Характеристики";
            this.FeaturesCheckbox.UseVisualStyleBackColor = true;
            // 
            // discountPriceCheckBox
            // 
            this.discountPriceCheckBox.AutoSize = true;
            this.discountPriceCheckBox.Location = new System.Drawing.Point(484, 73);
            this.discountPriceCheckBox.Name = "discountPriceCheckBox";
            this.discountPriceCheckBox.Size = new System.Drawing.Size(112, 17);
            this.discountPriceCheckBox.TabIndex = 15;
            this.discountPriceCheckBox.Text = "Цена со скидкой";
            this.discountPriceCheckBox.UseVisualStyleBackColor = true;
            // 
            // InfoCheckbox
            // 
            this.InfoCheckbox.AutoSize = true;
            this.InfoCheckbox.Location = new System.Drawing.Point(484, 142);
            this.InfoCheckbox.Name = "InfoCheckbox";
            this.InfoCheckbox.Size = new System.Drawing.Size(44, 17);
            this.InfoCheckbox.TabIndex = 16;
            this.InfoCheckbox.Text = "Info";
            this.InfoCheckbox.UseVisualStyleBackColor = true;
            // 
            // StartId
            // 
            this.StartId.Location = new System.Drawing.Point(44, 155);
            this.StartId.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.StartId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StartId.Name = "StartId";
            this.StartId.Size = new System.Drawing.Size(120, 20);
            this.StartId.TabIndex = 18;
            this.StartId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Начальный ID";
            // 
            // URLSimgStatus
            // 
            this.URLSimgStatus.Image = global::AliBot.Properties.Resources.quit;
            this.URLSimgStatus.Location = new System.Drawing.Point(12, 18);
            this.URLSimgStatus.Name = "URLSimgStatus";
            this.URLSimgStatus.Size = new System.Drawing.Size(25, 26);
            this.URLSimgStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.URLSimgStatus.TabIndex = 20;
            this.URLSimgStatus.TabStop = false;
            // 
            // DownloadFolderStatus
            // 
            this.DownloadFolderStatus.Image = global::AliBot.Properties.Resources.quit;
            this.DownloadFolderStatus.Location = new System.Drawing.Point(13, 47);
            this.DownloadFolderStatus.Name = "DownloadFolderStatus";
            this.DownloadFolderStatus.Size = new System.Drawing.Size(25, 26);
            this.DownloadFolderStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DownloadFolderStatus.TabIndex = 21;
            this.DownloadFolderStatus.TabStop = false;
            // 
            // ToolsStatus
            // 
            this.ToolsStatus.Image = global::AliBot.Properties.Resources.quit;
            this.ToolsStatus.Location = new System.Drawing.Point(13, 76);
            this.ToolsStatus.Name = "ToolsStatus";
            this.ToolsStatus.Size = new System.Drawing.Size(25, 26);
            this.ToolsStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ToolsStatus.TabIndex = 22;
            this.ToolsStatus.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(619, 361);
            this.Controls.Add(this.ToolsStatus);
            this.Controls.Add(this.DownloadFolderStatus);
            this.Controls.Add(this.URLSimgStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.StartId);
            this.Controls.Add(this.InfoCheckbox);
            this.Controls.Add(this.discountPriceCheckBox);
            this.Controls.Add(this.FeaturesCheckbox);
            this.Controls.Add(this.ImagesCheckBox);
            this.Controls.Add(this.PriceCheckBox);
            this.Controls.Add(this.titleCheckBox);
            this.Controls.Add(this.TEST);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TimeOutSetter);
            this.Controls.Add(this.downFolderPathLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.StartWork);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileSelectorURLs);
            this.Controls.Add(this.CloseBrowser);
            this.Controls.Add(this.OpenBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AliBot";
            ((System.ComponentModel.ISupportInitialize)(this.TimeOutSetter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.URLSimgStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadFolderStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenBrowser;
        private System.Windows.Forms.Button CloseBrowser;
        private System.Windows.Forms.Button FileSelectorURLs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartWork;
        private System.Windows.Forms.Label downFolderPathLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown TimeOutSetter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button TEST;
        private System.Windows.Forms.CheckBox ImagesCheckBox;
        private System.Windows.Forms.CheckBox PriceCheckBox;
        private System.Windows.Forms.CheckBox titleCheckBox;
        private System.Windows.Forms.CheckBox FeaturesCheckbox;
        private System.Windows.Forms.CheckBox discountPriceCheckBox;
        private System.Windows.Forms.CheckBox InfoCheckbox;
        private System.Windows.Forms.NumericUpDown StartId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox URLSimgStatus;
        private System.Windows.Forms.PictureBox DownloadFolderStatus;
        private System.Windows.Forms.PictureBox ToolsStatus;
    }
}

