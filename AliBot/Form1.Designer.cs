﻿namespace AliBot
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
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TimeOutSetter = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.TimeOutSetter)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenBrowser
            // 
            this.OpenBrowser.Location = new System.Drawing.Point(44, 21);
            this.OpenBrowser.Name = "OpenBrowser";
            this.OpenBrowser.Size = new System.Drawing.Size(107, 23);
            this.OpenBrowser.TabIndex = 0;
            this.OpenBrowser.Text = "Открыть Chrome";
            this.OpenBrowser.UseVisualStyleBackColor = true;
            this.OpenBrowser.Click += new System.EventHandler(this.OpenBrowser_Click);
            // 
            // CloseBrowser
            // 
            this.CloseBrowser.BackColor = System.Drawing.Color.DimGray;
            this.CloseBrowser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CloseBrowser.ForeColor = System.Drawing.Color.Maroon;
            this.CloseBrowser.Location = new System.Drawing.Point(44, 221);
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
            this.FileSelectorURLs.Location = new System.Drawing.Point(44, 50);
            this.FileSelectorURLs.Name = "FileSelectorURLs";
            this.FileSelectorURLs.Size = new System.Drawing.Size(107, 23);
            this.FileSelectorURLs.TabIndex = 2;
            this.FileSelectorURLs.Text = "Выбрать файл\r\n";
            this.FileSelectorURLs.UseVisualStyleBackColor = false;
            this.FileSelectorURLs.Click += new System.EventHandler(this.FileSelectorURLs_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выбрать файл со списком товаров";
            // 
            // StartWork
            // 
            this.StartWork.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.StartWork.Location = new System.Drawing.Point(44, 168);
            this.StartWork.Name = "StartWork";
            this.StartWork.Size = new System.Drawing.Size(107, 47);
            this.StartWork.TabIndex = 4;
            this.StartWork.Text = "Сбор информации";
            this.StartWork.UseVisualStyleBackColor = false;
            this.StartWork.Click += new System.EventHandler(this.StartWork_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Выбрать файл для сохранения результатов";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DimGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.ForeColor = System.Drawing.Color.Maroon;
            this.button1.Location = new System.Drawing.Point(44, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Выбрать файл\r\n";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // TimeOutSetter
            // 
            this.TimeOutSetter.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.TimeOutSetter.Location = new System.Drawing.Point(44, 119);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 287);
            this.Controls.Add(this.TimeOutSetter);
            this.Controls.Add(this.label2);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenBrowser;
        private System.Windows.Forms.Button CloseBrowser;
        private System.Windows.Forms.Button FileSelectorURLs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartWork;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown TimeOutSetter;
    }
}

