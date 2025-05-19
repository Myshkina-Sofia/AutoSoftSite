using System.Drawing;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panelMenu = new Panel();
            contentPanel = new Panel();
            Profile = new ucMenu();
            MySites = new ucMenu();
            CreationSite = new ucMenu();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = SystemColors.ActiveCaption;
            panelMenu.Controls.Add(contentPanel);
            panelMenu.Controls.Add(Profile);
            panelMenu.Controls.Add(MySites);
            panelMenu.Controls.Add(CreationSite);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(968, 514);
            panelMenu.TabIndex = 0;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = SystemColors.Control;
            contentPanel.Location = new Point(94, 0);
            contentPanel.MaximumSize = new Size(874, 514);
            contentPanel.MinimumSize = new Size(874, 514);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(874, 514);
            contentPanel.TabIndex = 1;
            // 
            // Profile
            // 
            Profile.BorderColor = Color.Transparent;
            Profile.Icon = (Image)resources.GetObject("Profile.Icon");
            Profile.Location = new Point(0, 214);
            Profile.Margin = new Padding(3, 2, 3, 2);
            Profile.Menu = "Профиль";
            Profile.Name = "Profile";
            Profile.Size = new Size(95, 62);
            Profile.TabIndex = 3;
            // 
            // MySites
            // 
            MySites.BorderColor = Color.Transparent;
            MySites.Icon = (Image)resources.GetObject("MySites.Icon");
            MySites.Location = new Point(0, 146);
            MySites.Margin = new Padding(3, 2, 3, 2);
            MySites.Menu = "Мои сайты";
            MySites.Name = "MySites";
            MySites.Size = new Size(95, 62);
            MySites.TabIndex = 2;
            // 
            // CreationSite
            // 
            CreationSite.BackColor = SystemColors.ActiveCaption;
            CreationSite.BorderColor = Color.Transparent;
            CreationSite.Icon = (Image)resources.GetObject("CreationSite.Icon");
            CreationSite.Location = new Point(0, 62);
            CreationSite.Margin = new Padding(3, 2, 3, 2);
            CreationSite.Menu = "Создать сайт";
            CreationSite.Name = "CreationSite";
            CreationSite.Size = new Size(95, 78);
            CreationSite.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(967, 514);
            Controls.Add(panelMenu);
            Cursor = Cursors.Hand;
            MaximumSize = new Size(985, 561);
            MinimumSize = new Size(985, 561);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private ucMenu CreationSite;
        private ucMenu Profile;
        private ucMenu MySites;
        private Panel contentPanel;
    }
}