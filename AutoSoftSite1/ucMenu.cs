using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoSoftSite1
{
    public partial class ucMenu : UserControl
    {
        private string menuTitle = "";
        private Image icon;
        private Color borderColor = Color.Transparent;

        public event EventHandler menuClick;

        public string Menu
        {
            get => menuTitle;
            set
            {
                menuTitle = value;
                menu.Text = menuTitle; // Немедленно обновляем текст
                this.Invalidate(); // Перерисовываем для надежности
            }
        }

        public Image Icon
        {
            get => icon;
            set
            {
                icon = value;
                menu.Image = icon; // Немедленно обновляем иконку
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                borderPanel.BackColor = borderColor;
                this.Invalidate();
            }
        }

        public ucMenu()
        {
            InitializeComponent();
            this.Click += menu_Click; // Привязываем событие клика ко всему контролу
            menu.Click += menu_Click; // Также к Label для надежности
        }

        private void menu_Click(object sender, EventArgs e)
        {
            menuClick?.Invoke(this, e);
        }
    }
}