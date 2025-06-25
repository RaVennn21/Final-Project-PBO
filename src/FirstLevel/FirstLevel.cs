using FP_PBO.Door;
using System.Drawing;
using System.Windows.Forms;

namespace FP_PBO
{
    public class FirstLevel : Form
    {
        private const int PlayerInitialPositionX = 50;
        private const int PlayerInitialPositionY = 450;
        private const int AnimationInterval = 100;

        public FirstLevel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
            InitializeLevel();
            SpwanItem();
        }

        private PauseButton _pause;
        private Player _player;
        private InventoryButton _inventoryButton;
        private System.Windows.Forms.Timer _animationTimer;
        private ItemDrop ItemDrop;
        private PictureBox _playerPictureBox;
        private Pointer DoorPointer;
        private Lv1Door Lv1Door;

        public List<Item> _inventoryItems = new List<Item>();


        private void InitializeLevel()
        {
            // Main form 
            Text = "Level 1";
            Size = new Size(800, 600);
            BackColor = Color.LightGray;
            StartPosition = FormStartPosition.CenterScreen;
            using (MemoryStream ms = new MemoryStream(Resource1.background))
            {
                BackgroundImage = Image.FromStream(ms);
            }
            BackgroundImageLayout = ImageLayout.Center;

            // Player
            _player = new Player(new Point(PlayerInitialPositionX, PlayerInitialPositionY),80,106,506,0,ClientSize.Width,ClientSize.Height);
            _playerPictureBox = _player.GetPictureBox();
            typeof(PictureBox).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(_playerPictureBox, true, null);
            Controls.Add(_playerPictureBox);

            // player animation timer
            _animationTimer = new System.Windows.Forms.Timer { Interval = AnimationInterval };
            _animationTimer.Tick += (sender, e) => Render();
            _animationTimer.Start();

            // Pause Button
            _pause = new PauseButton(new Point(10, 10),_inventoryItems);
            Controls.Add(_pause.GetPictureBox());
            _pause.GetPictureBox().Click += PauseButton_Click;

            // Inventory Button
            _inventoryButton = new InventoryButton(new Point(670, 0),_inventoryItems);
            Controls.Add(_inventoryButton.GetPictureBox());
            _inventoryButton.GetPictureBox().Click += InventoryButton_Click;

            // Door Pointer
            DoorPointer = new Pointer(new Point(140, 395));
            Controls.Add(DoorPointer.GetPictureBox());
            DoorPointer.GetPictureBox().BringToFront();

            // Level 1 Door
            Lv1Door = new Lv1Door(new Point(140, 475), _inventoryItems);
            Controls.Add(Lv1Door.GetPictureBox());
            Controls.Add(Lv1Door.GetPressLabel());

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
        }
        private void SpwanItem()
        {
            Image keycard;
            using (MemoryStream ms = new MemoryStream(Resource1.Keycard))
            {
                keycard = Image.FromStream(ms);
            }
            Item Keycard = new Item("Keycard Lv. 1", "can use to poen level 1 door", keycard,001);

            ItemDrop = new ItemDrop(Keycard, new Point(450, 500), _inventoryItems);
            Controls.Add(ItemDrop.GetPictureBox());
            Controls.Add(ItemDrop.GetPressLabel());
        }
        private void Render()
        {
            _player.Animate();
            DoorPointer.animatePointer();
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var itemDropBounds = new List<Rectangle> { ItemDrop.GetBounds() };
            _player.Walk(e.KeyCode, ClientSize, itemDropBounds);

            _pause.Press(e.KeyCode, sender);
            _inventoryButton.Press(e.KeyCode, sender);
            ItemDrop.PickUp(e.KeyCode, sender, _player.WorldPosition);
            Lv1Door.openDoor(e.KeyCode, sender,_player.WorldPosition);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            _player.StopWalking();
        }

        public void PauseButton_Click(object sender, EventArgs e)
        {
            PauseMenu pauseMenu = new PauseMenu(_inventoryItems);
            pauseMenu.FormClosed += (s, args) => Show();
            pauseMenu.Show();
            Hide();
        }

        public void InventoryButton_Click(object sender, EventArgs e)
        {
            InventoryMenu inventoryMenu = new InventoryMenu(_inventoryItems);
            inventoryMenu.FormClosed += (s, args) => Show();
            inventoryMenu.Show();
            Hide();
        }
    }
}
