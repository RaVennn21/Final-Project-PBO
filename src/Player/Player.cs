using FP_PBO;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace FP_PBO
{
    public class Player
    {
        private const int PlayerWidth = 32;
        private const int PlayerHeight = 48;
        private const int TotalFrames = 8;

        private PictureBox _playerPictureBox;
        private Image _spriteSheet;
        private int _currentFrame;
        private int _currentRow;
        private bool _isMoving;

        public Player(Point startPosition)
        {
            using (MemoryStream ms = new MemoryStream(Resource1.rpg_sprite_walk))
            {
                _spriteSheet = Image.FromStream(ms);
            }

            _currentFrame = 0;
            _currentRow = 0;

            _playerPictureBox = new PictureBox
            {
                Size = new Size(PlayerWidth, PlayerHeight),
                Location = startPosition,
                BackColor = Color.Transparent
            };

            UpdateSprite();
        }

        public PictureBox GetPictureBox() => _playerPictureBox;

        public Point WorldPosition => _playerPictureBox.Location;

        public void SetScreenPosition(Point screenPos)
        {
            _playerPictureBox.Location = screenPos;
        }

        public void Walk(Keys key, Size boundary)
        {
            int speed = 10;
            _isMoving = true;

            switch (key)
            {
                case Keys.S:
                    _currentRow = 0;
                    if (_playerPictureBox.Bottom < boundary.Height)
                        _playerPictureBox.Top += speed;
                    break;
                case Keys.W:
                    _currentRow = 1;
                    if (_playerPictureBox.Top > 0)
                        _playerPictureBox.Top -= speed;
                    break;
                case Keys.A:
                    _currentRow = 2;
                    if (_playerPictureBox.Left > 0)
                        _playerPictureBox.Left -= speed;
                    break;
                case Keys.D:
                    _currentRow = 3;
                    if (_playerPictureBox.Right < boundary.Width)
                        _playerPictureBox.Left += speed;
                    break;
                default:
                    _isMoving = false;
                    break;
            }
        }

        public void StopWalking()
        {
            _isMoving = false;
            _currentFrame = 0;
            UpdateSprite();
        }

        public void Animate()
        {
            if (_isMoving)
            {
                _currentFrame = (_currentFrame + 1) % TotalFrames;
                UpdateSprite();
            }
        }

        private void UpdateSprite()
        {
            int frameWidth = _spriteSheet.Width / TotalFrames;
            int frameHeight = _spriteSheet.Height / 4;

            Rectangle srcRect = new Rectangle(_currentFrame * frameWidth, _currentRow * frameHeight, frameWidth, frameHeight);
            Bitmap currentFrameImage = new Bitmap(frameWidth, frameHeight);

            using (Graphics g = Graphics.FromImage(currentFrameImage))
            {
                g.DrawImage(_spriteSheet, new Rectangle(0, 0, frameWidth, frameHeight), srcRect, GraphicsUnit.Pixel);
            }

            _playerPictureBox.Image = currentFrameImage;
        }
    }
}
