using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_PBO
{
    public class MainForm : Form
    {

        private Tombol _tombol;
        private ExitButton _exitButton;
        private StartButton _startButton;
        private Label _titleLabel;

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

            _titleLabel = new Label
            {
                Text = "A Toxic Environment",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(250, 75),
                AutoSize = true,
                ForeColor = Color.Black
            };
            Controls.Add(_titleLabel);
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

            _startButton = new StartButton(new Point(startX, startY));
            Controls.Add(_startButton.GetPictureBox());
            _startButton.GetPictureBox().Click += StartGameButton_Click;

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
