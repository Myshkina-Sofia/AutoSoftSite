using System.Drawing;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    partial class ucMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMenu));
            this.menu = new System.Windows.Forms.Label();
            this.borderPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.Transparent;
            this.menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menu.Image = ((System.Drawing.Image)(resources.GetObject("menu.Image")));
            this.menu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.menu.Size = new System.Drawing.Size(90, 46);
            this.menu.TabIndex = 0;
            this.menu.Text = "label";
            this.menu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // borderPanel
            // 
            this.borderPanel.Location = new System.Drawing.Point(0, 0);
            this.borderPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.borderPanel.Name = "borderPanel";
            this.borderPanel.Size = new System.Drawing.Size(10, 66);
            this.borderPanel.TabIndex = 1;
            // 
            // ucMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.borderPanel);
            this.Controls.Add(this.menu);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucMenu";
            this.Size = new System.Drawing.Size(90, 46);
            this.ResumeLayout(false);

        }

        #endregion

        private Label menu;
        private Panel borderPanel;
    }
}
