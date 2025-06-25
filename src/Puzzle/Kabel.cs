using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace FP_PBO.Puzzle
{
    public class Kabel : Form
    {
        private PictureBox _kabelA;
        private PictureBox _kabelB;
        private PictureBox _kabelC;
        private int currenrtKabelIndex = 0;
        private Image _spriteSheet;
        bool Done = false;
        public Kabel()
        {
            InitializeKabel();
            KeyPreview = true;
        }

        private void InitializeKabel()
        {
            Text = "puzlle";
            Size = new Size(300, 300);
            StartPosition = FormStartPosition.CenterScreen;
            BackgroundImageLayout = ImageLayout.Center;

            using (var ms = new System.IO.MemoryStream(Resource1.Kabel))
            {
                _spriteSheet = Image.FromStream(ms);
            }
            UpdateSprite();

            _kabelA = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(100, 70),
                Size = new Size(120, 20),
                BackColor = Color.Transparent
            };
            Controls.Add(_kabelA);
            _kabelA.Click += KabelAGetClick;

            _kabelB = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(100, 120),
                Size = new Size(120, 20),
                BackColor = Color.Transparent
            };
            Controls.Add(_kabelB);
            _kabelB.Click += KabelBGetClick;

            _kabelC = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(100, 170),
                Size = new Size(120, 20),
                BackColor = Color.Transparent
            };
            Controls.Add(_kabelC);
            _kabelC.Click += KabelCGetClick;
        }
        private void KabelAGetClick(object sender, EventArgs e)
        {
            if (currenrtKabelIndex == 0)
            {
                currenrtKabelIndex = 7;
                UpdateSprite();
            }
            else
            {
                MessageBox.Show("Cabel A is alredy Cut", "Cable Cut", MessageBoxButtons.OK);
            }
        }
        private void KabelBGetClick(object sender, EventArgs e)
        {
            if (currenrtKabelIndex == 0)
            {
                currenrtKabelIndex = 3;
                UpdateSprite();
                MessageBox.Show("You cut The wrong Cable");
                Application.Exit();
            }
            else if (currenrtKabelIndex == 7)
            {
                currenrtKabelIndex = 2;
                UpdateSprite();
            }
            else if(currenrtKabelIndex == 2)
            {
                MessageBox.Show("Cabel B is alredy Cut", "Cable Cut", MessageBoxButtons.OK);
            }
        }
        private void KabelCGetClick(object sender, EventArgs e)
        {
            if (currenrtKabelIndex == 0)
            {
                currenrtKabelIndex = 5;
                UpdateSprite();
                MessageBox.Show("You cut The wrong Cable");
                Application.Exit();
            }
            else if (currenrtKabelIndex == 2)
            {
                currenrtKabelIndex = 6;
                UpdateSprite();
                MessageBox.Show("You solve the puzzle", "Sucsess", MessageBoxButtons.OK);
                
                Close();
            }
            else if (currenrtKabelIndex == 7)
            {
                currenrtKabelIndex = 1;
                UpdateSprite();
                MessageBox.Show("You cut The wrong Cable");
                Application.Exit();
            }
        }
        private void UpdateSprite()
        {
            int frameWidth = _spriteSheet.Width / 8;
            int frameHeight = _spriteSheet.Height;

            Rectangle srcRect = new Rectangle(currenrtKabelIndex * frameWidth, 0, frameWidth, frameHeight);
            Bitmap currentFrameImage = new Bitmap(frameWidth, frameHeight);

            using (Graphics g = Graphics.FromImage(currentFrameImage))
            {
                g.DrawImage(_spriteSheet, new Rectangle(0, 0, frameWidth, frameHeight), srcRect, GraphicsUnit.Pixel);
            }

            BackgroundImage = currentFrameImage;
        }
    }
}
