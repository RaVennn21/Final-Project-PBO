using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_PBO
{
    public class MainForm : Form
    {
        private Button startGameButton;
        private Tombol _tombol;
        private ExitButton _exitButton;

        public MainForm()
        {
            InitializeForm();
            InitializeControls();
        }

        private void InitializeForm()
        {
            Text = "Main Menu";
            Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
        }

        private void InitializeControls()
        {

            int buttonWidth = 190;
            int buttonHeight = 60;
            int buttonGap = 20;
            int formWidth = 800;
            int formHeight = 600;

            int startX = (formWidth - buttonWidth) / 2;
            int startY = (formHeight - (2 * buttonHeight + 1 * buttonGap)) / 2;

            startGameButton = new Button
            {
                Text = "Start Game",
                Location = new Point(startX, startY),
                Size = new Size(buttonWidth, buttonHeight),
                Font = new Font("Segoe UI", 14, FontStyle.Regular)
            };
            startGameButton.Click += StartGameButton_Click;
            Controls.Add(startGameButton);

            _exitButton = new ExitButton(new Point(startX, startY + (buttonHeight + buttonGap) * 1));
            Controls.Add(_exitButton.GetPictureBox());
            _exitButton.GetPictureBox().Click += ExitButton_Click;


            _tombol = new Tombol();

            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _tombol.Press(e.KeyCode, sender);
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            FirstLevel firstForm = new FirstLevel();
            firstForm.FormClosed += (s, args) => Show();
            firstForm.Show();
            Hide();
        }



        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
