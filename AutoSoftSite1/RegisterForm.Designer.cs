using System.Drawing;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    partial class RegisterForm
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
            lblName = new Label();
            txtName = new TextBox();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtConfirmPassword = new TextBox();
            lblConfirmPassword = new Label();
            btnRegister = new Button();
            btnCancel = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblName.Location = new Point(221, 107);
            lblName.Name = "lblName";
            lblName.Size = new Size(51, 25);
            lblName.TabIndex = 0;
            lblName.Text = "Имя:";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new Point(382, 108);
            txtName.Name = "txtName";
            txtName.Size = new Size(171, 27);
            txtName.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtEmail.Location = new Point(382, 161);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(171, 27);
            txtEmail.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblEmail.Location = new Point(221, 161);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(58, 25);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email:";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(382, 219);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(171, 27);
            txtPassword.TabIndex = 5;
            // 
            // lblPassword
            // 
            lblPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPassword.Location = new Point(221, 218);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(78, 25);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Пароль:";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtConfirmPassword.Location = new Point(382, 302);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new Size(171, 27);
            txtConfirmPassword.TabIndex = 7;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblConfirmPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblConfirmPassword.Location = new Point(221, 274);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(130, 70);
            lblConfirmPassword.TabIndex = 6;
            lblConfirmPassword.Text = "Подтверждение пароля:";
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnRegister.Location = new Point(209, 363);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(159, 53);
            btnRegister.TabIndex = 8;
            btnRegister.Text = "Зарегистрироваться";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnCancel.Location = new Point(382, 363);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(171, 51);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(241, 9);
            label1.Name = "label1";
            label1.Size = new Size(295, 62);
            label1.TabIndex = 10;
            label1.Text = "Регистрация";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(742, 450);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnRegister);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RegisterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private TextBox txtName;
        private TextBox txtEmail;
        private Label lblEmail;
        private TextBox txtPassword;
        private Label lblPassword;
        private TextBox txtConfirmPassword;
        private Label lblConfirmPassword;
        private Button btnRegister;
        private Button btnCancel;
        private Label label1;
    }
}

