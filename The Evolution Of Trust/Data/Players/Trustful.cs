using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    class Trustful : Player
    {
        static Trustful()
        {
            _info = new PlayerTypeInfo(
                0,
                "Trustful",
                "\"Давай будем лучшими друзьями!\"\nВсегда доверяется.",
                Color.Pink
            );
        }

        public override bool MakeADecision()
        {
            return true;
        }

        public override void ResetPlayerMemory() { }

        public override Player Create()
        {
            return new Trustful();
        }

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        private static int _type_id = 0;
        private static string _type_name = "Trustful";
        private static Color _type_color = Color.Pink;
        private static string _type_description =
            "\"Давай будем лучшими друзьями!\"\nВсегда доверяется.";
        public override string TypeName { get { return _type_name; } }
        //public override int TypeId { get {return _type_id; } }
        public override Color TypeColor { get { return _type_color; } }
    }
}
