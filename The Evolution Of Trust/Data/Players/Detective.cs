using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust.Data.Players
{
    /// <summary>
    /// Игрок детектив.
    /// Первые его ходы всегда одинаковы.
    /// Если за эти ходы его не обмануть, то он будет обманывать всегда.
    /// Иначе ведёт себя как Имитатор.
    /// </summary>
    class Detective : Player
    {
        static Detective()
        {
            _info = new PlayerTypeInfo(
                 4,
                "Detective",
                "\"Учти: я тебя анализирую.\nМои первые ходы: доверие, обман, доверие, доверие.\nЕсли обманешь, я буду действовать как Имитатор.\nЕсли ни разу не обманешь, я буду всегда жульничать,\nчтобы использовать тебя. Элементарно, мой дорогой Ватсон.\"",
                Color.Brown
            );
        }

        public override bool MakeADecision()
        {
            if (_count < _first_moves.Length)
            {
                if (!Memory)
                {
                    _was_deceived = true;
                }
                bool ret = _first_moves[_count];
                _count++;
                return ret;
            }
            if (_was_deceived)
            {
                return Memory;
            }
            return false;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
            _count = 0;
            _was_deceived = false;
        }

        public override Player Create()
        {
            return new Detective();
        }

        private static readonly bool[] _first_moves = { true, false, true, true };
        private int _count = 0;
        private bool _was_deceived = false;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        public override string TypeName { get { return _info.TypeName; } }
        public override Color TypeColor { get { return _info.TypeColor; } }
    }
}
