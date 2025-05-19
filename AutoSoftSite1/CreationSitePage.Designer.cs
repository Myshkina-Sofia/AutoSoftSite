using System.Drawing;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    partial class CreationSitePage
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            cmbTemplate = new ComboBox();
            lblTemplate = new Label();
            btnCreateSite = new Button();
            lblSiteName = new Label();
            txtSiteName = new TextBox();
            linkSite = new LinkLabel();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28.2F);
            lblTitle.Location = new Point(270, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(303, 62);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Создать сайт";
            // 
            // cmbTemplate
            // 
            cmbTemplate.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTemplate.FormattingEnabled = true;
            cmbTemplate.Location = new Point(174, 114);
            cmbTemplate.Name = "cmbTemplate";
            cmbTemplate.Size = new Size(200, 28);
            cmbTemplate.TabIndex = 2;
            cmbTemplate.SelectedIndexChanged += cmbTemplate_SelectedIndexChanged;
            // 
            // lblTemplate
            // 
            lblTemplate.AutoSize = true;
            lblTemplate.Font = new Font("Segoe UI", 12F);
            lblTemplate.Location = new Point(20, 110);
            lblTemplate.Name = "lblTemplate";
            lblTemplate.Size = new Size(92, 28);
            lblTemplate.TabIndex = 3;
            lblTemplate.Text = "Шаблон:";
            // 
            // btnCreateSite
            // 
            btnCreateSite.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCreateSite.Location = new Point(750, 464);
            btnCreateSite.Name = "btnCreateSite";
            btnCreateSite.Size = new Size(100, 30);
            btnCreateSite.TabIndex = 4;
            btnCreateSite.Text = "Создать";
            btnCreateSite.UseVisualStyleBackColor = true;
            btnCreateSite.Click += btnCreateSite_Click;
            // 
            // lblSiteName
            // 
            lblSiteName.AutoSize = true;
            lblSiteName.Font = new Font("Segoe UI", 12F);
            lblSiteName.Location = new Point(20, 70);
            lblSiteName.Name = "lblSiteName";
            lblSiteName.Size = new Size(158, 28);
            lblSiteName.TabIndex = 5;
            lblSiteName.Text = "Название сайта:";
            // 
            // txtSiteName
            // 
            txtSiteName.Location = new Point(174, 74);
            txtSiteName.Name = "txtSiteName";
            txtSiteName.Size = new Size(200, 27);
            txtSiteName.TabIndex = 6;
            // 
            // linkSite
            // 
            linkSite.AutoSize = true;
            linkSite.Location = new Point(150, 440);
            linkSite.Name = "linkSite";
            linkSite.Size = new Size(134, 20);
            linkSite.TabIndex = 7;
            linkSite.TabStop = true;
            linkSite.Text = "Посмотреть сайт: ";
            linkSite.LinkClicked += linkSite_LinkClicked;
            // 
            // CreationSitePage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkSite);
            Controls.Add(txtSiteName);
            Controls.Add(lblSiteName);
            Controls.Add(btnCreateSite);
            Controls.Add(lblTemplate);
            Controls.Add(cmbTemplate);
            Controls.Add(lblTitle);
            Name = "CreationSitePage";
            Size = new Size(874, 514);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblTitle;
        private ComboBox cmbTemplate;
        private Label lblTemplate;
        private Button btnCreateSite;
        private Label lblSiteName;
        private TextBox txtSiteName;
        private LinkLabel linkSite;
    }
}
