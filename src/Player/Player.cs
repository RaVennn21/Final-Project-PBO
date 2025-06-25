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
        private int PlayerWidth = 80;
        private int PlayerHeight = 106;
        private const int TotalFrames = 6;

        private PictureBox _playerPictureBox;
        private Image _spriteSheet;
        private Image _idleSpriteSheet;
        private int _currentFrame;
        private int _currentRow;
        private bool _isMoving;
        private int Top,Left,Right,Bottom;

        public Player(Point startPosition,int width, int hight,int top,int left, int right, int bottom)
        {
            PlayerHeight = hight;
            PlayerWidth = width;
            using (MemoryStream ms = new MemoryStream(Resource1.Player))
            {
                _spriteSheet = Image.FromStream(ms);
            }
            using (MemoryStream ms = new MemoryStream(Resource1.IdlePlayer))
            {
                _idleSpriteSheet = Image.FromStream(ms);
            }
            Top = top;
            Left = left;
            Right = right;
            Bottom = bottom;

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

            Point originalPosition = _playerPictureBox.Location;

            switch (key)
            {
                case Keys.S:
                    if (_playerPictureBox.Bottom < Bottom)
                        _playerPictureBox.Top += speed;
                    break;
                case Keys.W:
                    if (_playerPictureBox.Bottom > Top )
                        _playerPictureBox.Top -= speed;
                    break;
                case Keys.A:
                    _currentRow = 1;
                    if (_playerPictureBox.Left > Left)
                        _playerPictureBox.Left -= speed;
                    break;
                case Keys.D:
                    _currentRow = 0;
                    if (_playerPictureBox.Right < Right)
                        _playerPictureBox.Left += speed;
                    break;
                default:
                    _isMoving = false;
                    break;
            }

          
            foreach (var bounds in itemDropBounds)
            {
                if (_playerPictureBox.Bounds.IntersectsWith(bounds))
                {
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
