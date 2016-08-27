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
            this.SuspendLayout();
            // 
            // OpenBrowser
            // 
            this.OpenBrowser.Location = new System.Drawing.Point(44, 12);
            this.OpenBrowser.Name = "OpenBrowser";
            this.OpenBrowser.Size = new System.Drawing.Size(107, 23);
            this.OpenBrowser.TabIndex = 0;
            this.OpenBrowser.Text = "Открыть Browser";
            this.OpenBrowser.UseVisualStyleBackColor = true;
            this.OpenBrowser.Click += new System.EventHandler(this.OpenBrowser_Click);
            // 
            // CloseBrowser
            // 
            this.CloseBrowser.BackColor = System.Drawing.Color.DimGray;
            this.CloseBrowser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CloseBrowser.ForeColor = System.Drawing.Color.Maroon;
            this.CloseBrowser.Location = new System.Drawing.Point(44, 177);
            this.CloseBrowser.Name = "CloseBrowser";
            this.CloseBrowser.Size = new System.Drawing.Size(107, 23);
            this.CloseBrowser.TabIndex = 1;
            this.CloseBrowser.Text = "Закрыть Browser";
            this.CloseBrowser.UseVisualStyleBackColor = false;
            this.CloseBrowser.Click += new System.EventHandler(this.CloseBrowser_Click);
            // 
            // FileSelectorURLs
            // 
            this.FileSelectorURLs.BackColor = System.Drawing.Color.DimGray;
            this.FileSelectorURLs.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.FileSelectorURLs.ForeColor = System.Drawing.Color.Maroon;
            this.FileSelectorURLs.Location = new System.Drawing.Point(44, 229);
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
            this.label1.Location = new System.Drawing.Point(157, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выбрать файл со списком товаров";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 287);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileSelectorURLs);
            this.Controls.Add(this.CloseBrowser);
            this.Controls.Add(this.OpenBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AliBot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenBrowser;
        private System.Windows.Forms.Button CloseBrowser;
        private System.Windows.Forms.Button FileSelectorURLs;
        private System.Windows.Forms.Label label1;
    }
}

