
namespace DrawGraph
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
            this.buildGraphButton = new System.Windows.Forms.Button();
            this.sheet = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
            this.SuspendLayout();
            // 
            // buildGraphButton
            // 
            this.buildGraphButton.Location = new System.Drawing.Point(832, 12);
            this.buildGraphButton.Name = "buildGraphButton";
            this.buildGraphButton.Size = new System.Drawing.Size(181, 46);
            this.buildGraphButton.TabIndex = 0;
            this.buildGraphButton.Text = "Загрузить индивидуальное задание";
            this.buildGraphButton.UseVisualStyleBackColor = true;
            this.buildGraphButton.Click += new System.EventHandler(this.BuildGraphButton_Click);
            // 
            // sheet
            // 
            this.sheet.Location = new System.Drawing.Point(86, 78);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(611, 380);
            this.sheet.TabIndex = 4;
            this.sheet.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 536);
            this.Controls.Add(this.sheet);
            this.Controls.Add(this.buildGraphButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buildGraphButton;
        private System.Windows.Forms.PictureBox sheet;
    }
}

