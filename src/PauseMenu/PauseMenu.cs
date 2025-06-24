using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_PBO
{
    public class PauseMenu : Form
    {

        private Button Continue;
        private Button Inventory;
        private Button Save;
        private Button exitButton;

        private Tombol _tombol;

        public PauseMenu()
        {
            InitializeLevel();
            InitializeControls();
        }

        private void InitializeLevel()
        {
            this.Text = "Pause Menu";
            this.Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview= true;
        }

        private void InitializeControls()
        {
            int buttonWidth = 170;
            int buttonHeight = 50;
            int buttonGap = 20;
            int formWidth = 800;
            int formHeight = 600;

            int startX = (formWidth - buttonWidth) / 2;
            int startY = (formHeight - (4 * buttonHeight + 3 * buttonGap)) / 2; 

            _tombol = new Tombol();

            Continue = new Button
            {
                Text = "Continue",
                Location = new Point(startX, startY),
                Size = new Size(buttonWidth, buttonHeight),
                Font = new Font("Segoe UI", 14, FontStyle.Regular)
            };
            Continue.Click += Continue_Click;
            Controls.Add(Continue);

            Inventory = new Button
            {
                Text = "Inventory",
                Location = new Point(startX, startY + (buttonHeight + buttonGap) * 1),
                Size = new Size(buttonWidth, buttonHeight),
                Font = new Font("Segoe UI", 14, FontStyle.Regular)
            };
            Inventory.Click += Inventory_Click;
            Controls.Add(Inventory);

            Save = new Button
            {
                Text = "Save",
                Location = new Point(startX, startY + (buttonHeight + buttonGap) * 2),
                Size = new Size(buttonWidth, buttonHeight),
                Font = new Font("Segoe UI", 14, FontStyle.Regular)
            };
            Save.Click += Save_Click;
            Controls.Add(Save);

            exitButton = new Button
            {
                Text = "Exit",
                Location = new Point(startX, startY + (buttonHeight + buttonGap) * 3),
                Size = new Size(buttonWidth, buttonHeight),
                Font = new Font("Segoe UI", 14, FontStyle.Regular)
            };
            exitButton.Click += ExitButton_Click;
            Controls.Add(exitButton);

            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _tombol.Press(e.KeyCode, sender);
        }
        private void Continue_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Inventory_Click(object sender, EventArgs e)
        {
            InventoryMenu inventoryMenu = new InventoryMenu();
            inventoryMenu.FormClosed += (s, args) => Show();
            inventoryMenu.Show();
            Hide();
        }
        private void Save_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Save functionality is not implemented yet.");
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
