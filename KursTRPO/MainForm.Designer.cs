
namespace KursTRPO
{
    partial class MainForm
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
            this.lblPath = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.PanelCheck = new System.Windows.Forms.Panel();
            this.LableNumeration = new System.Windows.Forms.Label();
            this.RdBtnBySections = new System.Windows.Forms.RadioButton();
            this.RdBtnEndToEnd = new System.Windows.Forms.RadioButton();
            this.PanelCheck.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(12, 9);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(101, 20);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "Путь к файлу:";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(16, 32);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(319, 27);
            this.tbPath.TabIndex = 1;
            this.tbPath.Text = "C:\\Users\\Егор\\Desktop\\Kurs\\ПЗ к КП Тест.docx";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(119, 154);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(106, 59);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // PanelCheck
            // 
            this.PanelCheck.Controls.Add(this.LableNumeration);
            this.PanelCheck.Controls.Add(this.RdBtnBySections);
            this.PanelCheck.Controls.Add(this.RdBtnEndToEnd);
            this.PanelCheck.Location = new System.Drawing.Point(16, 65);
            this.PanelCheck.Name = "PanelCheck";
            this.PanelCheck.Size = new System.Drawing.Size(319, 83);
            this.PanelCheck.TabIndex = 3;
            // 
            // LableNumeration
            // 
            this.LableNumeration.AutoSize = true;
            this.LableNumeration.Location = new System.Drawing.Point(3, 2);
            this.LableNumeration.Name = "LableNumeration";
            this.LableNumeration.Size = new System.Drawing.Size(92, 20);
            this.LableNumeration.TabIndex = 2;
            this.LableNumeration.Text = "Нумерация:";
            // 
            // RdBtnBySections
            // 
            this.RdBtnBySections.AutoSize = true;
            this.RdBtnBySections.Checked = true;
            this.RdBtnBySections.Location = new System.Drawing.Point(3, 55);
            this.RdBtnBySections.Name = "RdBtnBySections";
            this.RdBtnBySections.Size = new System.Drawing.Size(278, 24);
            this.RdBtnBySections.TabIndex = 1;
            this.RdBtnBySections.TabStop = true;
            this.RdBtnBySections.Text = "Отдельная внутри каждого раздела.";
            this.RdBtnBySections.UseVisualStyleBackColor = true;
            // 
            // RdBtnEndToEnd
            // 
            this.RdBtnEndToEnd.AutoSize = true;
            this.RdBtnEndToEnd.Location = new System.Drawing.Point(3, 25);
            this.RdBtnEndToEnd.Name = "RdBtnEndToEnd";
            this.RdBtnEndToEnd.Size = new System.Drawing.Size(92, 24);
            this.RdBtnEndToEnd.TabIndex = 0;
            this.RdBtnEndToEnd.TabStop = true;
            this.RdBtnEndToEnd.Text = "Сквозная";
            this.RdBtnEndToEnd.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 233);
            this.Controls.Add(this.PanelCheck);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.lblPath);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Нормоконтроль рисунков";
            this.PanelCheck.ResumeLayout(false);
            this.PanelCheck.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel PanelCheck;
        private System.Windows.Forms.Label LableNumeration;
        private System.Windows.Forms.RadioButton RdBtnBySections;
        private System.Windows.Forms.RadioButton RdBtnEndToEnd;
    }
}

