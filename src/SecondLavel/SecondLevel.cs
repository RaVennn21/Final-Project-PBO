using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_PBO
{
    public class SecondLevel : Form
    {
        List<Item> _inventoryItems;
        public SecondLevel(List<Item> inventoryItems)
        {
            _inventoryItems = inventoryItems;
            InitializeLevel();
        }
        private void InitializeLevel()
        {
            Text = "Level 2";
            Size = new Size(800, 600);
            BackColor = Color.LightGray;
            StartPosition = FormStartPosition.CenterScreen;
            using (MemoryStream ms = new MemoryStream(Resource1.SecondLavel))
            {
                BackgroundImage = Image.FromStream(ms);
            }
            BackgroundImageLayout = ImageLayout.Center;
        }
    }
}
