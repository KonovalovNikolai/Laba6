using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    /// <summary>
    /// Игрок обманщик.
    /// При принятии решения всегда обманывает.
    /// </summary>
    class Cheater : Player
    {
        static Cheater()
        {
            _info = new PlayerTypeInfo(
                 1,
                "Cheater",
                "\"Cильные должны есть слабых!\"\nВсегда обманывает.",
                Color.Black
            );
        }

        public override bool MakeADecision()
        {
            return false;
        }

        public override void ResetPlayerMemory() { }

        public override Player Create()
        {
            return new Cheater();
        }

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        public override string TypeName { get { return _info.TypeName; } }
        public override Color TypeColor { get { return _info.TypeColor; } }
    }
}
