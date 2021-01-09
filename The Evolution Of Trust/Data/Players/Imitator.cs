using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    /// <summary>
    /// Игрок подражатель.
    /// Начинает вести себя как Имитатор, если его обмануть дважды подряд.
    /// Иначе всегда доверяет.
    /// </summary>
    class Imitator : Player
    {
        static Imitator()
        {
            _info = new PlayerTypeInfo(
                 5,
                "Imitator",
                "\"Привет!\nЯ почти как Имитатор, но жульничаю только если обмануть меня два раза подряд.\nВ конце концов, первый раз мог быть ошибкой!\"",
                Color.Purple
            );
        }

        public override bool MakeADecision()
        {
            if (_was_deceived_twice)
                return Memory;

            if (!Memory) _count++;
            else _count = 0;

            if (_count == 2)
            {
                _was_deceived_twice = true;
            }

            return true;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
            _was_deceived_twice = false;
            _count = 0;
        }

        public override Player Create()
        {
            return new Imitator();
        }

        private bool _was_deceived_twice = false;
        private int _count = 0;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        public override string TypeName { get { return _info.TypeName; } }
        public override Color TypeColor { get { return _info.TypeColor; } }
    }
}
