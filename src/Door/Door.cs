﻿
using FP_PBO.Puzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP_PBO.Door
{
    public class Door
    {
        protected const int DoorWidth = 50;
        protected const int DoorHeight = 100;
        protected PictureBox _DoorPictureBox;
        public PictureBox GetPictureBox() => _DoorPictureBox;
        public List<Item> _inventoryItems;

        protected Label Press;
        public Label GetPressLabel() => Press;
        public Door(Point startPosition, List<Item> inventoryItems)
        {
            _DoorPictureBox = new PictureBox
            {
                Size = new Size(DoorWidth, DoorHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
            };
            _inventoryItems = inventoryItems;
        }
    }
    public class Lv1Door : Door
    {
        public Lv1Door(Point startPosition, List<Item> inventoryItems)
            : base(startPosition, inventoryItems)
        {
            _DoorPictureBox = new PictureBox
            {
                Size = new Size(DoorWidth, DoorHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
            };
            _inventoryItems = inventoryItems;
            Press = new Label
            {
                Text = "Press F to open door",
                Location = new Point(650, 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Visible = false
            };
        }
        public void openDoor(Keys e, object sender, Point player)
        {
            if (_DoorPictureBox.Visible == true && Math.Sqrt(Math.Pow(player.X - _DoorPictureBox.Location.X, 2) + Math.Pow(player.Y - _DoorPictureBox.Location.Y, 2)) < 100)
            {
                Press.Visible = true;
                if (e == Keys.F)
                {
                    bool hasKeycard = false;
                    foreach (var item in _inventoryItems)
                    {
                        if (item.Id == 1)
                        {
                            hasKeycard = true;
                            Press.Visible = false;
                            SecondLevel secondLevel = new SecondLevel(_inventoryItems,((Form)sender));
                            secondLevel.FormClosing += (s, args) => ((Form)sender).Show();
                            secondLevel.Show();
                            ((Form)sender).Hide();
                            break;
                        }
                    }
                    if (!hasKeycard)
                    {
                        MessageBox.Show("You need Keycard Lv. 1 to open this door", "Door Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                Press.Visible = false;
            }
        }
    }
    public class Lv2Door : Door
    {
        public Lv2Door(Point startPosition, List<Item> inventoryItems)
            : base(startPosition, inventoryItems)
        {
            _DoorPictureBox = new PictureBox
            {
                Size = new Size(DoorWidth, DoorHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
            };
            _inventoryItems = inventoryItems;
            Press = new Label
            {
                Text = "Press F to open door",
                Location = new Point(650, 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Visible = false
            };
        }

        public void openDoor(Keys e, object sender, Point player,Form level1)
        {
            if (_DoorPictureBox.Visible == true && Math.Sqrt(Math.Pow(player.X - _DoorPictureBox.Location.X, 2) + Math.Pow(player.Y - _DoorPictureBox.Location.Y, 2)) < 100)
            {
                Press.Visible = true;
                if (e == Keys.F)
                {
                    Press.Visible = false;
                    ((Form)sender).Hide();
                    level1.Show();
                }
            }
            else
            {
                Press.Visible = false;
            }
        }
    }
    public class PuzzleCable : Door
    {
        public PuzzleCable(Point startPosition, List<Item> inventoryItems)
            : base(startPosition, inventoryItems)
        {
            _DoorPictureBox = new PictureBox
            {
                Size = new Size(DoorWidth, DoorHeight),
                Location = startPosition,
                BackColor = Color.Transparent,
            };
            _inventoryItems = inventoryItems;
            Press = new Label
            {
                Text = "Press F to open Cable Management",
                Location = new Point(650, 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Visible = false
            };
        }
        private bool done = false;
        public void OpenCable(Keys e, object sender, Point player)
        {
            if (_DoorPictureBox.Visible == true && Math.Sqrt(Math.Pow(player.X - _DoorPictureBox.Location.X, 2) + Math.Pow(player.Y - _DoorPictureBox.Location.Y, 2)) < 100)
            {
                Press.Visible = true;
                if (e == Keys.F && !done)
                {
                    Kabel cable = new Kabel();
                    Press.Visible = false;
                    cable.Show();
                    done = true;
                }
                else if (e == Keys.F && done)
                {
                    MessageBox.Show("You have already completed this puzzle", "Puzzle Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Press.Visible = false;
            }
        }
    }
}
