using AutoSoftSite1;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    public partial class MySitesPage : UserControl
    {
        private readonly int userId;
        private readonly TMySqlConnect db;

        public MySitesPage() : this(0) { }

        public MySitesPage(int userId)
        {
            try
            {
                InitializeComponent();
                this.userId = userId;
                db = new TMySqlConnect();
                LoadUserSites();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"MySitesPage: Exception during initialization - {ex.Message}\nStackTrace: {ex.StackTrace}");
                MessageBox.Show($"Ошибка при инициализации страницы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadUserSites()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            // Очистка предыдущих элементов
            Controls.Clear();

            // Запрос для получения сайтов пользователя
            string query = "SELECT site_id, title, created_at FROM Sites WHERE user_id = @userId ORDER BY created_at DESC";
            try
            {
                using (var reader = db.ExecuteReader(query, "@userId", userId))
                {
                    bool hasSites = false;
                    int yOffset = 20;

                    while (reader.Read())
                    {
                        hasSites = true;
                        int siteId = reader.GetInt32("site_id");
                        string title = reader.GetString("title");
                        DateTime createdAt = reader.GetDateTime("created_at");

                        // Создание метки для заголовка сайта
                        Label lblTitle = new Label
                        {
                            Text = $"Сайт: {title}",
                            Location = new Point(20, yOffset),
                            Size = new Size(400, 30),
                            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                            AutoSize = true
                        };
                        Controls.Add(lblTitle);

                        // Создание метки для даты создания
                        Label lblDate = new Label
                        {
                            Text = $"Создан: {createdAt.ToString("dd.MM.yyyy HH:mm:ss")}",
                            Location = new Point(20, yOffset + 30),
                            Size = new Size(400, 20),
                            Font = new Font("Segoe UI", 10F),
                            AutoSize = true
                        };
                        Controls.Add(lblDate);

                        // Добавление ссылки на просмотр (если файл существует)
                        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{title.Replace(" ", "_")}_{siteId}.html");
                        LinkLabel linkSite = new LinkLabel
                        {
                            Text = "Посмотреть сайт",
                            Location = new Point(20, yOffset + 50),
                            Size = new Size(100, 20),
                            Tag = filePath,
                            Enabled = File.Exists(filePath)
                        };
                        linkSite.LinkClicked += (s, e) =>
                        {
                            if (File.Exists(filePath))
                            {
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                            }
                            else
                            {
                                MessageBox.Show("Файл сайта не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        };
                        Controls.Add(linkSite);

                        yOffset += 80; // Увеличиваем отступ для следующего сайта
                    }

                    reader.Close();

                    // Если сайтов нет
                    if (!hasSites)
                    {
                        Label noSitesLabel = new Label
                        {
                            Text = "Сайтов нет",
                            Location = new Point(20, 20),
                            Size = new Size(200, 30),
                            Font = new Font("Segoe UI", 14F, FontStyle.Italic),
                            ForeColor = Color.Gray
                        };
                        Controls.Add(noSitesLabel);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"MySitesPage: Error in LoadUserSites - {ex.Message}");
                MessageBox.Show($"Ошибка загрузки сайтов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
