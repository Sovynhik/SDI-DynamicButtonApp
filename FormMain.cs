using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DynamicButton
{
    public partial class FormMain : Form
    {
        private List<Button> lstButtons = new List<Button>();
        private int buttonCount = 0;
        private Random random = new Random();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            timer.Interval = 1000; // 1 секунда
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Button btn in lstButtons)
            {
                int value = int.Parse(btn.Text);
                btn.Text = (value + 1).ToString();
            }
        }

        private void btnCreateButton_Click(object sender, EventArgs e)
        {
            Button newButton = new Button
            {
                Text = "0",
                Name = "dynamicBtn" + buttonCount,
                Size = new Size(60, 30),
                BackColor = Color.FromArgb(
                    random.Next(256),
                    random.Next(256),
                    random.Next(256))
            };

            int maxX = this.ClientSize.Width - newButton.Width - 20;
            int maxY = this.ClientSize.Height - newButton.Height - 80;
            int x = random.Next(20, maxX > 20 ? maxX : 20);
            int y = random.Next(80, maxY > 80 ? maxY : 80);

            newButton.Location = new Point(x, y);

            // Контекстное меню с мнемоникой
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("&Удалить кнопку");
            deleteItem.Click += (s, ev) =>
            {
                lstButtons.Remove(newButton);
                this.Controls.Remove(newButton);
                newButton.Dispose();
            };
            cms.Items.Add(deleteItem);
            newButton.ContextMenuStrip = cms;

            lstButtons.Add(newButton);
            this.Controls.Add(newButton);
            buttonCount++;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить все динамические кнопки?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (Button btn in lstButtons.ToList()) // ToList() чтобы избежать ошибки при удалении
                {
                    this.Controls.Remove(btn);
                    btn.Dispose();
                }
                lstButtons.Clear();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}