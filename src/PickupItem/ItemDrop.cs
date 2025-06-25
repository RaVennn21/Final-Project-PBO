using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP_PBO
{
    public class ItemDrop
    {
        private Item _item;

        private PictureBox _itemPictureBox;
        private List<Item> _inventoryItems;
        public PictureBox GetPictureBox() => _itemPictureBox;
        private Label press;

        public Label GetPressLabel() => press;

        public ItemDrop(Item item, Point Location, List<Item> InventoryItem)
        {
            _item = item;
            _inventoryItems = InventoryItem;

            _itemPictureBox = new PictureBox
            {
                Name = item.Name,
                Size = new Size(50, 50),
                Image = item._image,
                Location = Location,
                BackColor = Color.Transparent,
                Visible = true,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            press = new Label
            {
                Text = "Press F to pick up",
                Location = new Point(650, 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Visible = false
            };

        }
        public Rectangle GetBounds()
        {
            if (_itemPictureBox.Visible == true)
            {
                return _itemPictureBox.Bounds;
            }
            // Return an empty rectangle when the item is picked up (not visible)
            return Rectangle.Empty;
        }
        public void PickUp(Keys e, object sender, Point player)
        {
            if (_itemPictureBox.Visible == true && Math.Sqrt(Math.Pow(player.X - _itemPictureBox.Location.X, 2) + Math.Pow(player.Y - _itemPictureBox.Location.Y, 2)) < 100)
            {
                press.Visible = true;
                if (e == Keys.F)
                {
                    press.Visible = false;
                    _itemPictureBox.Visible = false;
                    _inventoryItems.Add(_item);
                }
            }
            else
            {
                press.Visible = false;
            }
        }
    }
}
