using FP_PBO.Door;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_PBO
{
    public class SecondLevel : Form
    {
        private const int PlayerInitialPositionX = 715;
        private const int PlayerInitialPositionY = 400;
        private const int AnimationInterval = 100;
        List<Item> _inventoryItems;
        private Form _firstLavel;
        public SecondLevel(List<Item> inventoryItems, Form firstLavel)
        {
            _inventoryItems = inventoryItems;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
            InitializeLevel();
            SpwanItem();
            _firstLavel = firstLavel;
        }
        private PauseButton _pause;
        private Player _player;
        private InventoryButton _inventoryButton;
        private System.Windows.Forms.Timer _animationTimer;
        private ItemDrop ItemDrop;
        private PictureBox _playerPictureBox;
        private Pointer DoorPointer;
        private Pointer PuzzlePointer;
        private Lv2Door Lv2Door;
        private PuzzleCable puzzleCable;
        private void InitializeLevel()
        {
            //Main form
            Text = "Level 2";
            Size = new Size(800, 600);
            BackColor = Color.LightGray;
            StartPosition = FormStartPosition.CenterScreen;
            using (MemoryStream ms = new MemoryStream(Resource1.SecondLavel))
            {
                BackgroundImage = Image.FromStream(ms);
            }
            BackgroundImageLayout = ImageLayout.Center;

            // Player
            _player = new Player(new Point(PlayerInitialPositionX, PlayerInitialPositionY), 120, 136,160,0,ClientSize.Width,ClientSize.Height);
            _playerPictureBox = _player.GetPictureBox();
            typeof(PictureBox).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(_playerPictureBox, true, null);
            Controls.Add(_playerPictureBox);

            // player animation timer
            _animationTimer = new System.Windows.Forms.Timer { Interval = AnimationInterval };
            _animationTimer.Tick += (sender, e) => Render();
            _animationTimer.Start();

            //Pause Button
            _pause = new PauseButton(new Point(10, 10), _inventoryItems);
            Controls.Add(_pause.GetPictureBox());
            _pause.GetPictureBox().Click += PauseButton_Click;

            // Inventory Button
            _inventoryButton = new InventoryButton(new Point(670, 0), _inventoryItems);
            Controls.Add(_inventoryButton.GetPictureBox());
            _inventoryButton.GetPictureBox().BringToFront();
            _inventoryButton.GetPictureBox().Click += InventoryButton_Click;

            // Door Pointer
            DoorPointer = new Pointer(new Point(750, 380));
            Controls.Add(DoorPointer.GetPictureBox());
            DoorPointer.GetPictureBox().BringToFront();

            //Lv2Door
            Lv2Door = new Lv2Door(new Point(750, 410), _inventoryItems);
            Controls.Add(Lv2Door.GetPictureBox());
            Controls.Add(Lv2Door.GetPressLabel());

            // Puzzle Cable
            puzzleCable = new PuzzleCable(new Point(80,125), _inventoryItems);
            Controls.Add(puzzleCable.GetPictureBox());
            Controls.Add(puzzleCable.GetPressLabel());

            PuzzlePointer = new Pointer(new Point(80, 95));
            Controls.Add(PuzzlePointer.GetPictureBox());
            PuzzlePointer.GetPictureBox().BringToFront();


            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
        }
        private void SpwanItem()
        {
            bool _hasFlashlight = false;
            foreach (var item in _inventoryItems)
            {
                if (item.Id == 002)
                {
                    _hasFlashlight = true;
                }

            }
            if (!_hasFlashlight)
            {
                Image Flashlight;
                using (MemoryStream ms = new MemoryStream(Resource1.Flashlight))
                {
                    Flashlight = Image.FromStream(ms);
                }
                Item Keycard = new Item("Flashlight", "can use see in the dark", Flashlight, 002);

                ItemDrop = new ItemDrop(Keycard, new Point(450, 500), _inventoryItems);
                Controls.Add(ItemDrop.GetPictureBox());
                Controls.Add(ItemDrop.GetPressLabel());
            }
        }
        private void Render()
        {
            _player.Animate();
            DoorPointer.animatePointer();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            var itemDropBounds = ItemDrop != null ? new List<Rectangle> { ItemDrop.GetBounds() }: new List<Rectangle>();

            _player.Walk(e.KeyCode, ClientSize, itemDropBounds);

            if (ItemDrop != null)
            {
                ItemDrop.PickUp(e.KeyCode, sender, _player.WorldPosition);
            }
            _pause.Press(e.KeyCode, sender);
            _inventoryButton.Press(e.KeyCode, sender);
            Lv2Door.openDoor(e.KeyCode, sender, _player.WorldPosition, _firstLavel);

            puzzleCable.OpenCable(e.KeyCode, sender,_player.WorldPosition);
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
