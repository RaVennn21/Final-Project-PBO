using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace FP_PBO
{
    internal class InventoryMenu : Form
    {
        public List<Item> _inventoryItems;

        private Label _descriptionLabel;

        public InventoryMenu(List<Item> InventoryItems)
        {
            _inventoryItems = InventoryItems ?? new List<Item>();
            KeyPreview = true;
            InitializeInventory();
            DisplayItems();
        }

        private BackButton _backButton;

        private void InitializeInventory()
        {
            Text = "Inventory";
            Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            _backButton = new BackButton(new Point(10, 10));
            Controls.Add(_backButton.GetPictureBox());
            _backButton.GetPictureBox().Click += Button_Click;

            KeyDown += OnKeyDown;

            _descriptionLabel = new Label
            {
                Location = new Point(400, 80),
                Size = new Size(350, 400),
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = false
            };
            Controls.Add(_descriptionLabel);

        }

        

        public void AddItem(Item item)
        {
            _inventoryItems.Add(item);
        }

        public void DisplayItems()
        {
            int buttonWidth = 90;
            int buttonHeight = 90;
            int buttonGap = 20;
            int startX = 50;
            int startY = 80;
            int itemRow = 3;

            for (int i = 0; i < _inventoryItems.Count; i++)
            {

                var item = _inventoryItems[i];
                int row = i / itemRow;
                int col = i % itemRow;



                Button ItemButton = new Button
                {
                    Size = new Size(buttonWidth, buttonHeight),
                    Location = new Point(startX + col * (buttonWidth + buttonGap), startY + row * (buttonHeight + buttonGap)),
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    Image = item._image,
                    Tag = item
                };
                Controls.Add(ItemButton);
                ItemButton.Click += DisplayItemDescription;

            }
            KeyDown += OnKeyDown;
        }

        public void DisplayItemDescription(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var item = btn.Tag as Item;

            _descriptionLabel.Text = $"\n\n\n\n\n{item.Name}\n\n{item.Description}";

            _descriptionLabel.Image = item._image; 
            int imgWidth = 100;
            int imgHeight = 100;

            Bitmap resizedImage = new Bitmap(_descriptionLabel.Image, new Size(imgWidth, imgHeight));

            _descriptionLabel.Image = resizedImage;

            _descriptionLabel.ImageAlign = ContentAlignment.TopCenter;
        }
        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            _backButton.Press(e.KeyCode, sender);
        }
        public void Button_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
