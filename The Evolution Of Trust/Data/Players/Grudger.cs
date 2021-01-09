using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    /// <summary>
    /// Злопамятный игрок.
    /// Пока его не обманут, всегда доверяет.
    /// Но если обмануть, то всегда обманывает.
    /// </summary>
    class Grudger : Player
    {
        static Grudger()
        {
            _info = new PlayerTypeInfo(
                 3,
                "Grudger",
                "\"Я начну с доверия и буду продолжать доверять,\nно если ты хоть раз меня обманешь,\nЯ БУДУ ЖУЛЬНИЧАТЬ ДО ПОСЛЕДНЕГО.\"",
                Color.Yellow
            );
        }

        public override bool MakeADecision()
        {
            if (!Memory)
            {
                _was_deceived = true;
            }
            return !_was_deceived;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
            _was_deceived = false;
        }

        public override Player Create()
        {
            return new Grudger();
        }

        private bool _was_deceived = false;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        public override string TypeName { get { return _info.TypeName; } }
        public override Color TypeColor { get { return _info.TypeColor; } }
    }
}
