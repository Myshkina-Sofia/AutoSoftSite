using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using BCrypt.Net;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace AutoSoftSite1
{
    public partial class RegisterForm : Form
    {
        private readonly TMySqlConnect db;
        public bool RegistrationSuccessful { get; private set; }
        public string RegisteredName { get; private set; }
        public string RegisteredEmail { get; private set; }
        public DateTime RegisteredCreatedAt { get; private set; }


        public RegisterForm()
        {
            InitializeComponent();
            db = new TMySqlConnect();
            RegistrationSuccessful = false;
        }



        // Обработчик кнопки "Зарегистрироваться"
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация данных
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите имя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text) || !IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Введите корректный email.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Введите пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверка, существует ли email
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE email = @email";
                object result = db.ExecuteScalar(checkQuery, "@email", txtEmail.Text);
                if (Convert.ToInt32(result) > 0)
                {
                    MessageBox.Show("Email уже зарегистрирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Хеширование пароля
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);

                // Сохранение пользователя в базе данных
                string query = "INSERT INTO Users (email, password, name, role_id) VALUES (@email, @password, @name, @roleId)";
                int rowsAffected = db.ExecuteNonQuery(query,
                    "@email", txtEmail.Text,
                    "@password", hashedPassword,
                    "@name", txtName.Text,
                    "@roleId", 2);

                if (rowsAffected > 0)
                {
                    // Поиск только что созданного пользователя по email
                    string selectQuery = "SELECT user_id, name, email, role_id, created_at FROM Users WHERE email = @email";
                    using (MySqlDataReader reader = db.ExecuteReader(selectQuery, "@email", txtEmail.Text))
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32("user_id");
                            string userName = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString("name");
                            string email = reader.GetString("email");
                            int roleId = reader.GetInt32("role_id");
                            DateTime createdAt = reader.GetDateTime("created_at");

                            MessageBox.Show($"Регистрация успешна! Добро пожаловать, {userName ?? txtEmail.Text}!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            // Открыть главную форму
                            MainForm mainForm = new MainForm(userId, userName, roleId, email);
                            mainForm.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось зарегистрировать пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик кнопки "Отмена"
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрываем форму
        }

        // Проверка корректности email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Закрытие соединения при закрытии формы
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            db.Close();
            base.OnFormClosing(e);
        }
    }
}