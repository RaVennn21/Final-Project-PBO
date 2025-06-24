using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP_PBO
{
    public class Pointer
    {
        private const int ButtonWidth = 32;
        private const int ButtonHeight = 32;
        private int _currentFrame = 0;


        private PictureBox _pointerPictureBox;
        private Image _pointerImage;

        public PictureBox GetPictureBox() => _pointerPictureBox;
        public Pointer(Point startPosition)
        {
            using (MemoryStream ms = new MemoryStream(Resource1.Point))
            {
                _pointerImage = Image.FromStream(ms);
            }
            
            _pointerPictureBox = new PictureBox
            {
                Size = new Size(ButtonWidth, ButtonHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }
        
        public void animatePointer()
        {
            if (_pointerImage != null)
            {
                _currentFrame = (_currentFrame + 1) % 2;
                PointerSprite();
            }
        }


        public void PointerSprite()
        {
            
            int frameWidth = _pointerImage.Width / 2;
            int frameHeight = _pointerImage.Height / 1;

            Rectangle srcRect = new Rectangle(_currentFrame * frameWidth, 0, frameWidth, frameHeight);
            Bitmap currentFrameImage = new Bitmap(frameWidth, frameHeight);

            using (Graphics g = Graphics.FromImage(currentFrameImage))
            {
                g.DrawImage(_pointerImage, new Rectangle(0, 0, frameWidth, frameHeight), srcRect, GraphicsUnit.Pixel);
            }

            _pointerPictureBox.Image = currentFrameImage;
        }
    }
}
