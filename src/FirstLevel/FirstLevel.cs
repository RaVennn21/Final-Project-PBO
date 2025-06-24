using System.Drawing;
using System.Windows.Forms;

namespace FP_PBO
{
    public class FirstLevel : Form
    {
        private const int PlayerInitialPositionX = 50;
        private const int PlayerInitialPositionY = 50;
        private const int AnimationInterval = 100;

        public FirstLevel()
        {
            InitializeLevel();
        }

        private PauseButton _pause;
        private Player _player;
        private InventoryButton _inventoryButton;
        private System.Windows.Forms.Timer _animationTimer;
        private void InitializeLevel()
        {
            // Main form 
            Text = "Level 1";
            Size = new Size(800, 600);
            BackColor = Color.LightGray;
            StartPosition = FormStartPosition.CenterScreen;

            // Player
            _player = new Player(new Point(PlayerInitialPositionX, PlayerInitialPositionY));
            Controls.Add(_player.GetPictureBox());

            // player animation timer
            _animationTimer = new System.Windows.Forms.Timer { Interval = AnimationInterval };
            _animationTimer.Tick += (sender, e) => Render();
            _animationTimer.Start();

            // Pause Button
            _pause = new PauseButton(new Point(10, 10));
            Controls.Add(_pause.GetPictureBox());
            _pause.GetPictureBox().Click += PauseButton_Click;

            // Inventory Button
            _inventoryButton = new InventoryButton(new Point(-5, 475));
            Controls.Add(_inventoryButton.GetPictureBox());
            _inventoryButton.GetPictureBox().Click += InventoryButton_Click;

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
        }
        private void Render()
        {
            _player.Animate();
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _player.Walk(e.KeyCode, ClientSize);
            _pause.Press(e.KeyCode, sender);
            _inventoryButton.Press(e.KeyCode, sender);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            _player.StopWalking();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            PauseMenu pauseMenu = new PauseMenu();
            pauseMenu.FormClosed += (s, args) => Show();
            pauseMenu.Show();
            Hide();
        }

        private void InventoryButton_Click(object sender, EventArgs e)
        {
            InventoryMenu inventoryMenu = new InventoryMenu();
            inventoryMenu.FormClosed += (s, args) => Show();
            inventoryMenu.Show();
            Hide();
        }
    }
}
