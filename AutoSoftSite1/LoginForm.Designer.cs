using System.Drawing;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnLogin = new Button();
            btnRegister = new Button();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(299, 9);
            label1.Name = "label1";
            label1.Size = new Size(130, 62);
            label1.TabIndex = 21;
            label1.Text = "Вход";
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnLogin.Location = new Point(417, 382);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(96, 51);
            btnLogin.TabIndex = 20;
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnRegister.Location = new Point(228, 382);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(167, 53);
            btnRegister.TabIndex = 19;
            btnRegister.Text = "Зарегистрироваться";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(342, 238);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(171, 27);
            txtPassword.TabIndex = 16;
            // 
            // lblPassword
            // 
            lblPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPassword.Location = new Point(240, 237);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(78, 25);
            lblPassword.TabIndex = 15;
            lblPassword.Text = "Пароль:";
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtEmail.Location = new Point(342, 180);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(171, 27);
            txtEmail.TabIndex = 14;
            // 
            // lblEmail
            // 
            lblEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblEmail.Location = new Point(240, 180);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(58, 25);
            lblEmail.TabIndex = 13;
            lblEmail.Text = "Email:";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(725, 450);
            Controls.Add(label1);
            Controls.Add(btnLogin);
            Controls.Add(btnRegister);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnLogin;
        private Button btnRegister;
        private TextBox txtPassword;
        private Label lblPassword;
        private TextBox txtEmail;
        private Label lblEmail;
    }
}