using FP_PBO;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FP_PBO
{
    public class Player
    {
        private const int PlayerWidth = 80;
        private const int PlayerHeight = 106;
        private const int TotalFrames = 6;

        private PictureBox _playerPictureBox;
        private Image _spriteSheet;
        private Image _idleSpriteSheet;
        private int _currentFrame;
        private int _currentRow;
        private bool _isMoving;

        public Player(Point startPosition)
        {
            using (MemoryStream ms = new MemoryStream(Resource1.Player))
            {
                _spriteSheet = Image.FromStream(ms);
            }
            using (MemoryStream ms = new MemoryStream(Resource1.IdlePlayer))
            {
                _idleSpriteSheet = Image.FromStream(ms);
            }

            _currentFrame = 0;
            _currentRow = 0;

            _playerPictureBox = new PictureBox
            {
                Size = new Size(PlayerWidth, PlayerHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
            };

            UpdateSprite();
        }

        public PictureBox GetPictureBox() => _playerPictureBox;

        public Point WorldPosition => _playerPictureBox.Location;

        public void SetScreenPosition(Point screenPos)
        {
            _playerPictureBox.Location = screenPos;
        }

        public void Walk(Keys key, Size boundary, List<Rectangle> itemDropBounds)
        {
            int speed = 10;
            _isMoving = true;

            // Store the original position
            Point originalPosition = _playerPictureBox.Location;

            // Calculate the new position based on the key
            switch (key)
            {
                case Keys.S:
                    if (_playerPictureBox.Bottom < boundary.Height)
                        _playerPictureBox.Top += speed;
                    break;
                case Keys.W:
                    if (_playerPictureBox.Bottom > 530 )
                        _playerPictureBox.Top -= speed;
                    break;
                case Keys.A:
                    _currentRow = 1;
                    if (_playerPictureBox.Left > 0)
                        _playerPictureBox.Left -= speed;
                    break;
                case Keys.D:
                    _currentRow = 0;
                    if (_playerPictureBox.Right < boundary.Width)
                        _playerPictureBox.Left += speed;
                    break;
                default:
                    _isMoving = false;
                    break;
            }

            // Check for collision with any item drop
            foreach (var bounds in itemDropBounds)
            {
                if (_playerPictureBox.Bounds.IntersectsWith(bounds))
                {
                    // Collision detected, revert to original position
                    _playerPictureBox.Location = originalPosition;
                    break;
                }
            }
        }

        public void StopWalking()
        {
            _isMoving = false;
            
            UpdateSpriteIdle();
        }

        public void Animate()
        {
            if (_isMoving)
            {
                _currentFrame = (_currentFrame + 1) % TotalFrames;
                UpdateSprite();
            }
            else
            {
                _currentFrame = (_currentFrame + 1) % 4;
                UpdateSpriteIdle();
            }
        }
        public Rectangle GetBounds()
        {
            return _playerPictureBox.Bounds;
        }

        private void UpdateSprite()
        {
            int frameWidth = _spriteSheet.Width / TotalFrames;
            int frameHeight = _spriteSheet.Height / 2;

            Rectangle srcRect = new Rectangle(_currentFrame * frameWidth, _currentRow * frameHeight, frameWidth, frameHeight);
            Bitmap currentFrameImage = new Bitmap(frameWidth, frameHeight);

            using (Graphics g = Graphics.FromImage(currentFrameImage))
            {
                g.DrawImage(_spriteSheet, new Rectangle(0, 0, frameWidth, frameHeight), srcRect, GraphicsUnit.Pixel);
            }

            _playerPictureBox.Image = currentFrameImage;
        }
        private void UpdateSpriteIdle()
        {
            int frameWidth = _idleSpriteSheet.Width / 4;
            int frameHeight = _idleSpriteSheet.Height / 1;

            Rectangle srcRect = new Rectangle(_currentFrame * frameWidth, 0, frameWidth, frameHeight);
            Bitmap currentFrameImage = new Bitmap(frameWidth, frameHeight);

            using (Graphics g = Graphics.FromImage(currentFrameImage))
            {
                g.DrawImage(_idleSpriteSheet, new Rectangle(0, 0, frameWidth, frameHeight), srcRect, GraphicsUnit.Pixel);
            }

            _playerPictureBox.Image = currentFrameImage;
        }
    }
}
