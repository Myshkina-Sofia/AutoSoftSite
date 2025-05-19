using AutoSoftSite1;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    public partial class ProfilePage : UserControl
    {
        private readonly TMySqlConnect db;
        private readonly int userId;
        private readonly string userName;
        private readonly string email;
        private readonly DateTime createdAt;
        private bool isEditing = false;

        // Конструктор по умолчанию для дизайнера
        public ProfilePage() : this(0, "", "", DateTime.Now) // Вызываем конструктор с параметрами
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                // Пустая инициализация для дизайнера
                return;
            }
        }

        // Оригинальный конструктор
        public ProfilePage(int userId, string userName, string email, DateTime createdAt)
        {
            InitializeComponent();
            this.userId = userId;
            this.userName = userName;
            this.email = email;
            this.createdAt = createdAt;
            db = new TMySqlConnect();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                UpdateProfileInfo();
                SetEditMode(false);
            }
        }

        private void UpdateProfileInfo()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            txtName.Text = userName;
            txtEmail.Text = email;
            txtDate.Text = createdAt.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            if (!isEditing)
            {
                SetEditMode(true);
                btnEdit.Text = "Сохранить";
                isEditing = true;
            }
            else
            {
                try
                {
                    string query = "UPDATE Users SET name = @name, email = @email WHERE user_id = @userId";
                    int rowsAffected = db.ExecuteNonQuery(query,
                        "@name", txtName.Text,
                        "@email", txtEmail.Text,
                        "@userId", userId);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetEditMode(false);
                        btnEdit.Text = "Редактировать";
                        isEditing = false;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось обновить данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetEditMode(bool enable)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            txtName.ReadOnly = !enable;
            txtEmail.ReadOnly = !enable;
            txtDate.ReadOnly = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainForm mainForm = this.FindForm() as MainForm;
                mainForm?.Logout();
            }
        }
    }
}