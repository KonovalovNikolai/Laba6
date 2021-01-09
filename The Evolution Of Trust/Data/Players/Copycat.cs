using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    /// <summary>
    /// Игрок имитатор.
    /// На первом ходу доверяет, затем повторяет предыдущий ход противника.
    /// </summary>
    class Copycat : Player
    {
        static Copycat()
        {
            _info = new PlayerTypeInfo(
                 2,
                "Copycat",
                "\"Привет! На первом ходу я доверюсь,\nа потом просто буду копировать твой последний ход.\"",
                Color.Blue
            );
        }

        public override bool MakeADecision()
        {
            return Memory;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
        }

        public override Player Create()
        {
            return new Copycat();
        }

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        public override string TypeName { get { return _info.TypeName; } }
        public override Color TypeColor { get { return _info.TypeColor; } }
    }
}
