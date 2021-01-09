using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    /// <summary>
    /// Игрок простак.
    /// Начинает с доверия.
    /// Но если его обмануть, то меняет своё действие на обратное.
    /// </summary>
    class Simpleton : Player
    {
        static Simpleton()
        {
            _info = new PlayerTypeInfo(
                 6,
                "Simpleton",
                "\"превет\nсначала я доверять тебе.\nесли ты тоже доверять мене, я повторять свой ход, даже если это ошибка.\nесли ты обмануть меня, я делать свой ход наоборот, даже если это ошибка.\"",
                Color.Green
            );
        }

        public override bool MakeADecision()
        {
            if (!Memory)
            {
                _last_move = !_last_move;
            }
            return _last_move;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
            _last_move = true;
        }

        public override Player Create()
        {
            return new Simpleton();
        }

        private bool _last_move = true;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        public override string TypeName { get { return _info.TypeName; } }
        public override Color TypeColor { get { return _info.TypeColor; } }
    }
}
