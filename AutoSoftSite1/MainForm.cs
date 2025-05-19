using AutoSoftSite1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    public partial class MainForm : Form
    {
        private readonly int userId;
        private readonly string userName;
        private readonly int roleId;
        private readonly string email;
        private readonly CreationSitePage creationSitePage;
        private readonly ProfilePage profilePage;
        private readonly MySitesPage mySitesPage;
        private List<ucMenu> menuButtons;

        public MainForm(int userId, string userName, int roleId, string email)
        {
            InitializeComponent();
            this.userId = userId;
            this.userName = userName;
            this.roleId = roleId;
            this.email = email;

            contentPanel.Location = new Point(92, 0);
            contentPanel.Size = new Size(985, 561);
            contentPanel.Visible = true;
            contentPanel.BringToFront();
            System.Diagnostics.Debug.WriteLine($"contentPanel: Location={contentPanel.Location}, Size={contentPanel.Size}, Visible={contentPanel.Visible}");

            creationSitePage = new CreationSitePage(userId);
            creationSitePage.SiteCreated += (s, e) => mySitesPage.LoadUserSites();
            var db = new TMySqlConnect();
            string query = "SELECT created_at FROM Users WHERE user_id = @userId";
            object result = db.ExecuteScalar(query, "@userId", userId);
            DateTime createdAt = result != null ? Convert.ToDateTime(result) : DateTime.Now;
            profilePage = new ProfilePage(userId, userName, email, createdAt);
            mySitesPage = new MySitesPage(userId);

            contentPanel.Controls.Add(creationSitePage);
            System.Diagnostics.Debug.WriteLine($"Added {creationSitePage.Name} to contentPanel");
            contentPanel.Controls.Add(profilePage);
            System.Diagnostics.Debug.WriteLine($"Added {profilePage.Name} to contentPanel");
            contentPanel.Controls.Add(mySitesPage);
            System.Diagnostics.Debug.WriteLine($"Added {mySitesPage.Name} to contentPanel");

            menuButtons = new List<ucMenu> { CreationSite, MySites, Profile };
            ShowPage(creationSitePage); // Показываем страницу напрямую

            ClickMenu(menuButtons);

            foreach (var menu in menuButtons)
            {
                System.Diagnostics.Debug.WriteLine($"Menu: {menu?.Name}, Text: {menu?.Menu}, Icon: {(menu?.Icon != null ? "Set" : "Null")}");
            }

            activMenu(CreationSite, MySites, Profile);
        }

        private void ClickMenu(List<ucMenu> menu1)
        {
            foreach (var menu in menu1)
            {
                if (menu != null)
                {
                    menu.menuClick += Menu_menuClick;
                    System.Diagnostics.Debug.WriteLine($"Subscribed menuClick for {menu.Name}");
                }
            }
        }

        private void Menu_menuClick(object? sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Menu_menuClick triggered");
            if (sender is ucMenu menuButton1)
            {
                System.Diagnostics.Debug.WriteLine($"Clicked menu: {menuButton1.Name}");
                switch (menuButton1.Name)
                {
                    case "CreationSite":
                        activMenu(CreationSite, MySites, Profile);
                        ShowPage(creationSitePage);
                        System.Diagnostics.Debug.WriteLine("Showing CreationSitePage");
                        break;
                    case "MySites":
                        activMenu(MySites, CreationSite, Profile);
                        ShowPage(mySitesPage);
                        System.Diagnostics.Debug.WriteLine("Showing MySitesPage");
                        break;
                    case "Profile":
                        activMenu(Profile, MySites, CreationSite);
                        ShowPage(profilePage);
                        System.Diagnostics.Debug.WriteLine("Showing ProfilePage");
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine($"Unknown menu name: {menuButton1.Name}");
                        break;
                }
            }
        }

        private void activMenu(ucMenu activ, params ucMenu[] inactive)
        {
            if (activ != null)
            {
                activ.BorderColor = Color.White;
                activ.Controls.OfType<Label>().First().BackColor = SystemColors.GradientActiveCaption;
            }
            foreach (ucMenu inactive1 in inactive)
            {
                if (inactive1 != null)
                {
                    inactive1.BorderColor = Color.Transparent;
                    inactive1.Controls.OfType<Label>().First().BackColor = Color.Transparent;
                }
            }
        }

        private void ShowPage(UserControl page)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            if (page == null)
            {
                System.Diagnostics.Debug.WriteLine("ShowPage: Page parameter is null");
                return;
            }

            if (!contentPanel.Controls.Contains(page))
            {
                System.Diagnostics.Debug.WriteLine($"ShowPage: Page {page.Name} is not in contentPanel, adding it");
                contentPanel.Controls.Add(page);
            }

            page.Location = new Point(0, 0);
            page.Size = contentPanel.Size;

            contentPanel.Visible = true;
            System.Diagnostics.Debug.WriteLine($"ShowPage: contentPanel.Visible set to {contentPanel.Visible}");

            foreach (UserControl control in contentPanel.Controls.OfType<UserControl>())
            {
                if (control != page)
                {
                    control.Visible = false;
                    System.Diagnostics.Debug.WriteLine($"ShowPage: Hiding page {control.Name}");
                }
            }

            page.Visible = true;
            page.BringToFront();
            System.Diagnostics.Debug.WriteLine($"ShowPage: Showing page {page.Name}, Visible: {page.Visible}, Location: {page.Location}, Size: {page.Size}");
        }

        public void Logout()
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

    }
}