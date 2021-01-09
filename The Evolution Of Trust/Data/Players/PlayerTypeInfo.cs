using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    class PlayerTypeInfo
    {
        public PlayerTypeInfo(int id, string name, string description, Color color)
        {
            _type_color = color;
            _type_id = id;
            _type_name = name;
            _type_description = description;
        }

        private string _type_name;
        public string TypeName { get { return _type_name; } }

        private string _type_description;
        public string TypeDescription { get { return _type_description; } }

        private int _type_id;
        public int TypeId { get { return _type_id; } }

        private Color _type_color;
        public Color TypeColor { get { return _type_color; } }
    }
}
