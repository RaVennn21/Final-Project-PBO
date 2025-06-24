using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP_PBO
{
    public class Item
    {
        protected string _name;
        protected string _description;
        protected bool _pickup ;
        public Image _image;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public bool Pickup
        {
            get { return _pickup; }
            set { _pickup = value; }
        }
        public Item(string name, string description, Image image)
        {
            _name = name;
            _description = description;
            _pickup = false;
            _image = image;
        }


    }
}
