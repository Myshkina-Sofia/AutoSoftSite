using AutoSoftSite1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    public partial class CreationSitePage : UserControl
    {
        private readonly int userId;
        private readonly TMySqlConnect db;
        private Dictionary<int, string> blockTypes;
        private Dictionary<string, List<(string typeName, int position)>> Blocks;
        private int siteId;
        private Dictionary<string, string> templateDescriptions = new Dictionary<string, string>();
        private Label lblTemplateDescription = new Label();

        public event EventHandler SiteCreated;

        public CreationSitePage() : this(0)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }
        }

        public CreationSitePage(int userId)
        {
            InitializeComponent();
            linkSite.Visible = false;
            System.Diagnostics.Debug.WriteLine($"CreationSitePage: Initialized, Visible={this.Visible}");
            this.userId = userId;
            db = new TMySqlConnect();
            blockTypes = new Dictionary<int, string>();
            Blocks = new Dictionary<string, List<(string, int)>>();
            InitializeTemplateBlocks();
            LoadBlockTypes();
            LoadTemplates();
            //this.Controls.Add(debugLabel);
        }

        private void LoadBlockTypes()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            blockTypes = new Dictionary<int, string>();
            string query = "SELECT block_type_id, type_name FROM BlockTypes";
            System.Diagnostics.Debug.WriteLine($"Выполняется запрос LoadBlockTypes: {query}");
            using (var reader = db.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    int blockTypeId = reader.GetInt32("block_type_id");
                    string typeName = reader.GetString("type_name");
                    blockTypes[blockTypeId] = typeName;
                    System.Diagnostics.Debug.WriteLine($"Загружен тип блока: ID={blockTypeId}, Название={typeName}");
                }
                reader.Close();
            }
        }

        private void LoadTemplates()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            cmbTemplate.Items.Clear();
            templateDescriptions.Clear();
            string query = "SELECT name, description FROM Templates";
            System.Diagnostics.Debug.WriteLine($"Выполняется запрос LoadTemplates: {query}");
            using (var reader = db.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    string templateName = reader.GetString("name");
                    string description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description");
                    cmbTemplate.Items.Add(templateName);
                    templateDescriptions[templateName] = description;
                    System.Diagnostics.Debug.WriteLine($"Загружен шаблон: {templateName}");
                }
                reader.Close();
            }
            if (cmbTemplate.Items.Count > 0)
                cmbTemplate.SelectedIndex = 0;
            UpdateBlockFields();
        }

        private void InitializeTemplateBlocks()
        {
            Blocks["Простая визитка"] = new List<(string, int)> { ("Название", 1), ("Текст", 2), ("Документ", 3) };
            Blocks["Блог"] = new List<(string, int)> { ("Название", 1), ("Текст", 2), ("Текст", 3), ("Документ", 4) };
            Blocks["Портфолио"] = new List<(string, int)> { ("Название", 1), ("Текст", 2), ("Документ", 3), ("Документ", 4), ("Документ", 5) };
            System.Diagnostics.Debug.WriteLine("Инициализированы шаблоны блоков.");
        }

        private void cmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBlockFields();
            // Обновляем описание шаблона
            if (cmbTemplate.SelectedItem != null && templateDescriptions.ContainsKey(cmbTemplate.SelectedItem.ToString()))
                lblTemplateDescription.Text = templateDescriptions[cmbTemplate.SelectedItem.ToString()];
            else
                lblTemplateDescription.Text = "";
            System.Diagnostics.Debug.WriteLine($"Выбран шаблон: {cmbTemplate.SelectedItem}");
        }

        private async void UpdateBlockFields()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            // Полностью очищаем все элементы управления
            this.Controls.Clear();

            // Добавляем заголовок
            Label lblTitle = new Label
            {
                Text = "Создать сайт",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 28.2F)
            };
            this.Controls.Add(lblTitle);
            // Центрируем заголовок по ширине страницы
            lblTitle.Left = (this.Width - lblTitle.Width) / 2;

            // Добавляем выпадающий список шаблонов и его метку
            Label lblTemplate = new Label
            {
                Text = "Шаблон:",
                Location = new Point(20, 110),
                Size = new Size(92, 28),
                Font = new Font("Segoe UI", 12F)
            };
            this.Controls.Add(lblTemplate);
            cmbTemplate.Location = new Point(150, 110);
            cmbTemplate.Size = new Size(200, 28);
            this.Controls.Add(cmbTemplate);
            cmbTemplate.BringToFront();

            // Добавляем метку и поле для заголовка сайта
            Label lblSiteName = new Label
            {
                Text = "Название сайта:",
                Location = new Point(20, 70),
                Size = new Size(158, 28),
                Font = new Font("Segoe UI", 12F)
            };
            this.Controls.Add(lblSiteName);
            txtSiteName.Location = new Point(150, 70);
            txtSiteName.Size = new Size(200, 27);
            this.Controls.Add(txtSiteName);

            // Добавляем описание шаблона справа
            lblTemplateDescription.Location = new Point(400, 90);
            lblTemplateDescription.Size = new Size(250, 100);
            lblTemplateDescription.Font = new Font("Segoe UI", 8F);
            lblTemplateDescription.TextAlign = ContentAlignment.TopLeft;
            lblTemplateDescription.AutoSize = false;
            lblTemplateDescription.BorderStyle = BorderStyle.FixedSingle;
            lblTemplateDescription.BackColor = Color.WhiteSmoke;
            lblTemplateDescription.Padding = new Padding(6);
            if (cmbTemplate.SelectedItem != null && templateDescriptions.ContainsKey(cmbTemplate.SelectedItem.ToString()))
                lblTemplateDescription.Text = templateDescriptions[cmbTemplate.SelectedItem.ToString()];
            else
                lblTemplateDescription.Text = "";
            this.Controls.Add(lblTemplateDescription);

            if (cmbTemplate.SelectedItem == null) return;

            string selectedTemplate = cmbTemplate.SelectedItem.ToString();
            if (!Blocks.ContainsKey(selectedTemplate))
            {
                MessageBox.Show($"Шаблон '{selectedTemplate}' не найден в списке поддерживаемых шаблонов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var blocks = Blocks[selectedTemplate];
            int yOffset = 150;

            foreach (var (typeName, position) in blocks)
            {
                string labelText = typeName == "Название" ? "Заголовок:" : typeName + ":";
                Label lbl = new Label
                {
                    Text = labelText,
                    Location = new Point(20, yOffset),
                    Size = new Size(120, 28),
                    Font = new Font("Segoe UI", 12F),
                    Tag = "dynamic"
                };
                this.Controls.Add(lbl);

                if (typeName == "Документ")
                {
                    int textBoxWidth = 150;
                    int buttonWidth = 100;
                    int controlHeight = 27;
                    int gap = 10;

                    TextBox txt = new TextBox
                    {
                        Location = new Point(150, yOffset),
                        Size = new Size(textBoxWidth, controlHeight),
                        Tag = "dynamic",
                        ReadOnly = true
                    };
                    Button btnSelectFile = new Button
                    {
                        Text = "Выбрать файл",
                        Location = new Point(150 + textBoxWidth + gap, yOffset),
                        Size = new Size(buttonWidth, controlHeight),
                        Tag = "dynamic"
                    };
                    btnSelectFile.Click += async (s, e) =>
                    {
                        System.Diagnostics.Debug.WriteLine($"Начало выбора файла для типа {typeName} на позиции {position}");
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            openFileDialog.Filter = "Документы|*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx|Изображения|*.png;*.jpg;*.jpeg;*.gif;*.bmp|Видео|*.mp4;*.webm;*.ogg|Все файлы|*.*";
                            openFileDialog.Title = "Выберите документ, изображение или видео";
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                string filePath = openFileDialog.FileName;
                                System.Diagnostics.Debug.WriteLine($"Выбран файл: {filePath}");
                                string relativePath = await UploadFileToServer(filePath);
                                if (!string.IsNullOrEmpty(relativePath))
                                {
                                    txt.Text = relativePath;
                                    System.Diagnostics.Debug.WriteLine($"Файл загружен, относительный путь: {relativePath}");
                                }
                                else
                                {
                                    MessageBox.Show("Не удалось загрузить файл на сервер.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    System.Diagnostics.Debug.WriteLine("Не удалось загрузить файл на сервер.");
                                }
                            }
                        }
                    };
                    this.Controls.Add(txt);
                    this.Controls.Add(btnSelectFile);
                    txt.Tag = new { TypeName = typeName, Position = position };
                    yOffset += controlHeight + 13;
                }
                else
                {
                    TextBox txt = new TextBox
                    {
                        Location = new Point(150, yOffset),
                        Size = new Size(200, 27),
                        Tag = "dynamic"
                    };
                    this.Controls.Add(txt);
                    txt.Tag = new { TypeName = typeName, Position = position };
                    yOffset += txt.Height + 13;
                }
            }

            // После динамических элементов добавляем кнопку и ссылку
            btnCreateSite.Location = new Point(150, yOffset + 20);
            this.Controls.Add(btnCreateSite);
            linkSite.Location = new Point(150, yOffset + 60);
            this.Controls.Add(linkSite);
            System.Diagnostics.Debug.WriteLine($"Обновлены поля для шаблона: {selectedTemplate}");
        }

        private async Task<string> UploadFileToServer(string filePath)
        {
            System.Diagnostics.Debug.WriteLine($"Начало загрузки файла: {filePath}");

            try
            {
                // Проверка доступности на HTTP
                using (var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5175/api/") })
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    System.Diagnostics.Debug.WriteLine($"Проверка доступности сервера на http://localhost:5175/api/upload...");
                    var checkResponse = await httpClient.GetAsync("upload");
                    if (checkResponse.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine("Сервер доступен на http://localhost:5175/api/upload.");
                        // Используем тот же клиент для POST
                        return await PerformUpload(httpClient, filePath);
                    }
                    System.Diagnostics.Debug.WriteLine($"Сервер не отвечает на 5175/api/upload. Статус: {checkResponse.StatusCode}");
                }

                // Проверка доступности на HTTPS
                using (var httpsClient = new HttpClient { BaseAddress = new Uri("https://localhost:7171/api/") })
                {
                    httpsClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    System.Diagnostics.Debug.WriteLine("Проверка доступности сервера на https://localhost:7171/api/upload...");
                    var checkResponse = await httpsClient.GetAsync("upload");
                    if (checkResponse.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine("Сервер доступен на https://localhost:7171/api/upload.");
                        return await PerformUpload(httpsClient, filePath);
                    }
                    System.Diagnostics.Debug.WriteLine($"Сервер не отвечает на 7171/api/upload. Статус: {checkResponse.StatusCode}");
                    MessageBox.Show("Сервер не отвечает ни на http://localhost:5175/api/upload, ни на https://localhost:7171/api/upload. Убедитесь, что он запущен и эндпоинт доступен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                string errorMessage = $"Ошибка подключения к серверу: {ex.Message} (Статус: {ex.StatusCode})";
                System.Diagnostics.Debug.WriteLine(errorMessage);
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                string errorMessage = $"Ошибка загрузки файла: {ex.Message}";
                System.Diagnostics.Debug.WriteLine(errorMessage);
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private async Task<string> PerformUpload(HttpClient client, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                System.Diagnostics.Debug.WriteLine("Создание MultipartFormDataContent...");
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(fileStream), "file", Path.GetFileName(filePath));
                System.Diagnostics.Debug.WriteLine($"Добавлен файл: {Path.GetFileName(filePath)}");

                System.Diagnostics.Debug.WriteLine("Отправка POST-запроса на /api/upload...");
                var response = await client.PostAsync("upload", content);
                response.EnsureSuccessStatusCode();
                System.Diagnostics.Debug.WriteLine($"Ответ получен. Статус: {response.StatusCode}");

                string responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Успешная загрузка. Ответ сервера: {responseBody}");
                return responseBody.Trim('"');
            }
        }

        private void btnCreateSite_Click(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            try
            {
                if (string.IsNullOrWhiteSpace(txtSiteName.Text))
                {
                    MessageBox.Show("Введите название сайта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string insertSiteQuery = "INSERT INTO Sites (user_id, title) VALUES (@userId, @title)";
                int rowsAffected = db.ExecuteNonQuery(insertSiteQuery,
                    "@userId", userId,
                    "@title", txtSiteName.Text);
                System.Diagnostics.Debug.WriteLine($"Добавлен сайт. Строк затронуто: {rowsAffected}");

                if (rowsAffected > 0)
                {
                    string selectSiteQuery = "SELECT site_id FROM Sites WHERE user_id = @userId AND title = @title ORDER BY created_at DESC LIMIT 1";
                    object result = db.ExecuteScalar(selectSiteQuery, "@userId", userId, "@title", txtSiteName.Text);
                    if (result == null)
                    {
                        MessageBox.Show("Не удалось получить ID созданного сайта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    siteId = Convert.ToInt32(result);
                    System.Diagnostics.Debug.WriteLine($"Получен siteId: {siteId}");

                    foreach (Control control in this.Controls)
                    {
                        if (control is TextBox txt && txt.Tag != null)
                        {
                            var tag = (dynamic)txt.Tag;
                            string typeName = tag.TypeName;
                            int position = tag.Position;
                            string content = txt.Text;
                            AddBlock(siteId, typeName, content, position);
                            System.Diagnostics.Debug.WriteLine($"Добавлен блок: Type={typeName}, Content={content}, Position={position}");
                        }
                    }

                    GenerateSite();
                    MessageBox.Show("Сайт успешно создан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSiteName.Clear();
                    UpdateBlockFields();
                    linkSite.Visible = true;
                    SiteCreated?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания сайта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Ошибка создания сайта: {ex.Message}");
            }
        }

        private void AddBlock(int siteId, string typeName, string content, int position)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            int blockTypeId = blockTypes.First(x => x.Value == typeName).Key;
            string insertBlockQuery = "INSERT INTO Blocks (site_id, block_type_id, content, position) VALUES (@siteId, @blockTypeId, @content, @position)";
            db.ExecuteNonQuery(insertBlockQuery,
                "@siteId", siteId,
                "@blockTypeId", blockTypeId,
                "@content", content,
                "@position", position);
            System.Diagnostics.Debug.WriteLine($"Добавлен блок в БД: siteId={siteId}, blockTypeId={blockTypeId}, content={content}, position={position}");
        }

        private void GenerateSite()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            string template = cmbTemplate.SelectedItem.ToString();
            string htmlContent = GetHtmlTemplate(template);
            string safeSiteName = string.Join("_", txtSiteName.Text.Split(Path.GetInvalidFileNameChars()));
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{safeSiteName}_{siteId}.html");
            File.WriteAllText(filePath, htmlContent);
            linkSite.Text = $"Посмотреть сайт: {filePath}";
            System.Diagnostics.Debug.WriteLine($"Сгенерирован сайт: {filePath}");
        }

        private string GetHtmlTemplate(string templateName)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return "";
            string blocksHtml = "";
            foreach (Control control in this.Controls)
            {
                if (control is TextBox txt && txt.Tag != null)
                {
                    var tag = (dynamic)txt.Tag;
                    string typeName = tag.TypeName;
                    string content = txt.Text;
                    string cssClass = typeName.ToLower() switch
                    {
                        "название" => "title",
                        "текст" => "text",
                        "документ" => "document",
                        _ => typeName.ToLower()
                    };

                    if (typeName == "Документ")
                    {
                        string extension = Path.GetExtension(content)?.ToLower();
                        if (new[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" }.Contains(extension))
                        {
                            blocksHtml += $"<div class=\"{cssClass}\"><img src=\"http://localhost:5175{content}\" alt=\"Image\" style=\"max-width: 100%; height: auto;\"></div>\n";
                        }
                        else if (new[] { ".mp4", ".webm", ".ogg" }.Contains(extension))
                        {
                            blocksHtml += $"<div class=\"{cssClass}\"><video controls style=\"max-width: 100%; height: auto;\"><source src=\"http://localhost:5175{content}\" type=\"video/{extension.Replace(".", "")}\">Ваш браузер не поддерживает видео.</video></div>\n";
                        }
                        else if (new[] { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" }.Contains(extension))
                        {
                            string docType = extension switch
                            {
                                ".doc" or ".docx" => "Word",
                                ".xls" or ".xlsx" => "Excel",
                                ".ppt" or ".pptx" => "PowerPoint",
                                _ => "Документ"
                            };
                            blocksHtml += $"<div class=\"{cssClass}\"><a href=\"http://localhost:5175{content}\" target=\"_blank\" class=\"office-doc\">Открыть {docType} документ</a></div>\n";
                        }
                    }
                    else
                    {
                        blocksHtml += $"<div class=\"{cssClass}\">{content}</div>\n";
                    }
                    System.Diagnostics.Debug.WriteLine($"Добавлен блок в HTML: Type={typeName}, Content={content}");
                }
            }

            return templateName switch
            {
                "Простая визитка" => $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial; text-align: center; background-color: white; }}
                        .title {{ font-size: 24px; margin: 20px; }}
                        .text {{ font-size: 16px; margin: 10px; }}
                        .document {{ margin: 20px; }}
                        .office-doc {{ 
                            display: inline-block;
                            padding: 10px 20px;
                            background-color: #0078d4;
                            color: white;
                            text-decoration: none;
                            border-radius: 5px;
                            margin: 10px;
                        }}
                        .office-doc:hover {{
                            background-color: #005a9e;
                        }}
                    </style>
                </head>
                <body>
                    {blocksHtml}
                </body>
                </html>",
                "Блог" => $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Times New Roman; background-color: #f0f0f0; }}
                        .title {{ font-size: 24px; margin: 20px; }}
                        .text {{ font-size: 16px; margin: 10px; background-color: white; padding: 10px; }}
                        .document {{ margin: 20px; }}
                        .office-doc {{ 
                            display: inline-block;
                            padding: 10px 20px;
                            background-color: #0078d4;
                            color: white;
                            text-decoration: none;
                            border-radius: 5px;
                            margin: 10px;
                        }}
                        .office-doc:hover {{
                            background-color: #005a9e;
                        }}
                    </style>
                </head>
                <body>
                    {blocksHtml}
                </body>
                </html>",
                "Портфолио" => $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Roboto; background-color: #333; color: white; display: flex; flex-wrap: wrap; }}
                        .title {{ font-size: 24px; width: 100%; text-align: center; margin: 20px; }}
                        .text {{ font-size: 16px; width: 30%; margin: 10px; }}
                        .document {{ width: 30%; margin: 10px; border: 1px solid white; padding: 10px; }}
                        .office-doc {{ 
                            display: inline-block;
                            padding: 10px 20px;
                            background-color: #0078d4;
                            color: white;
                            text-decoration: none;
                            border-radius: 5px;
                            margin: 10px;
                        }}
                        .office-doc:hover {{
                            background-color: #005a9e;
                        }}
                    </style>
                </head>
                <body>
                    {blocksHtml}
                </body>
                </html>",
                _ => "<html><body>Шаблон не поддерживается</body></html>"
            };
        }

        private void linkSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            try
            {
                string filePath = linkSite.Text.Replace("Посмотреть сайт: ", "").Trim();
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                System.Diagnostics.Debug.WriteLine($"Открыт файл: {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Ошибка открытия файла: {ex.Message}");
            }
        }
    }
}