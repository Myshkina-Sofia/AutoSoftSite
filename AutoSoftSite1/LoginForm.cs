using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using BCrypt.Net;
using AutoSoftSite1;

namespace AutoSoftSite1
{
    public partial class LoginForm : Form
    {
        private readonly TMySqlConnect db;

        public LoginForm()
        {
            InitializeComponent();
            db = new TMySqlConnect();
        }

        // Обработчик кнопки "Войти"
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация данных
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

                // Поиск пользователя по email
                string query = "SELECT user_id, name, password, role_id, email FROM Users WHERE email = @email";
                using (MySqlDataReader reader = db.ExecuteReader(query, "@email", txtEmail.Text))
                {
                    if (reader.Read())
                    {
                        string storedPassword = reader.GetString("password");
                        int userId = reader.GetInt32("user_id");
                        string userName = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString("name");
                        int roleId = reader.GetInt32("role_id");
                        string email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email");

                        // Проверка пароля
                        if (BCrypt.Net.BCrypt.Verify(txtPassword.Text, storedPassword))
                        {
                            MessageBox.Show($"Вход успешен! Добро пожаловать, {userName ?? txtEmail.Text}!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // Открыть главную форму
                            MainForm mainForm = new MainForm(userId, userName, roleId, email ?? txtEmail.Text);
                            mainForm.Show();
                            this.Hide(); // Скрываем форму входа
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким email не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка входа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (RegisterForm registerForm = new RegisterForm())
            {
                this.Hide(); // Скрываем форму входа
                registerForm.ShowDialog();
                this.Show(); // Показываем форму входа снова после закрытия формы регистрации
            }
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
