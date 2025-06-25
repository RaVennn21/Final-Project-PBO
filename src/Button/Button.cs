using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP_PBO
{
    public class Tombol
    {
        protected const int ButtonWidth = 30;
        protected const int ButtonHeight = 30;
        int buttonGap = 20;
        int formWidth = 800;
        int formHeight = 600;

        protected PictureBox _ButtonPictureBox;
        protected Image _ButtonImage;
        public List<Item> _inventoryItems;

        public PictureBox GetPictureBox() => _ButtonPictureBox;
        public Tombol()
        {

        }


        public virtual void Press(Keys e, object sender)
        {
            if (e == Keys.Escape)
            {
                ((Form)sender).Close();
            }
        }

        public virtual void Click(object sender, EventArgs e)
        {
            ((Form)sender).Close();
        }

    }
    public class StartButton : Tombol
    {
        private const int ButtonWidth = 190;
        private const int ButtonHeight = 60;
        public StartButton(Point startPosition)
        {
            using (MemoryStream ms = new MemoryStream(Resource1.Start))
            {
                _ButtonImage = Image.FromStream(ms);
            }
            _ButtonPictureBox = new PictureBox
            {
                Size = new Size(ButtonWidth, ButtonHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
                Image = _ButtonImage,
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }
    }

    public class ExitButton : Tombol
    {
        private const int ButtonWidth = 190;
        private const int ButtonHeight = 60;

        public ExitButton(Point startPosition)
        {
            using (MemoryStream ms = new MemoryStream(Resource1.Exit))
            {
                _ButtonImage = Image.FromStream(ms);
            }
            _ButtonPictureBox = new PictureBox
            {
                Size = new Size(ButtonWidth, ButtonHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
                Image = _ButtonImage,
                
            };
        }
    }

    public class PauseButton : Tombol
    {
        public PauseButton(Point startPosition, List<Item> InventoryItems)
        {
            using (MemoryStream ms = new MemoryStream(Resource1.Pause))
            {
                _ButtonImage = Image.FromStream(ms);
            }
            _ButtonPictureBox = new PictureBox
            {
                Size = new Size(ButtonWidth, ButtonHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
                Image = _ButtonImage,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            _inventoryItems = InventoryItems;
        }
        public override void Press(Keys e, object sender)
        {
            if (e == Keys.Escape)
            {
                PauseMenu pauseMenu = new PauseMenu(_inventoryItems);
                pauseMenu.FormClosed += (s, args) => ((Form)sender).Show();
                pauseMenu.Show();
                ((Form)sender).Hide();
            }
        }
        

    }

    public class InventoryButton : Tombol
    {
        private const int ButtonWidth = 125;
        private const int ButtonHeight = 85;
        public List<Item> _inventoryItems;
        public InventoryButton(Point startPosition,List<Item> Inven)
        {
            using (MemoryStream ms = new MemoryStream(Resource1.Inven))
            {
                _ButtonImage = Image.FromStream(ms);
            }
            _ButtonPictureBox = new PictureBox
            {
                Size = new Size(ButtonWidth, ButtonHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
                Image = _ButtonImage,
            };

            _inventoryItems = Inven;
        }

        public override void Press(Keys e, object sender)
        {
            if (e == Keys.E)
            {
                InventoryMenu inventoryMenu = new InventoryMenu(_inventoryItems);
                inventoryMenu.FormClosed += (s, args) => ((Form)sender).Show();
                inventoryMenu.Show();
                ((Form)sender).Hide();
            }
        }
        
    }

    public class BackButton : Tombol
    {
        public BackButton(Point startPosition)
        {
            using (MemoryStream ms = new MemoryStream(Resource1.Back))
            {
                _ButtonImage = Image.FromStream(ms);
            }
            _ButtonPictureBox = new PictureBox
            {
                Size = new Size(ButtonWidth, ButtonHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
                Image = _ButtonImage,
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }

        public override void Press(Keys e, object sender)
        {
            if (e == Keys.Escape)
            {
                ((Form)sender).Close();
            }
        }
    }



}
