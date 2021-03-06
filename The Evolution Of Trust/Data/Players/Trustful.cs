﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    /// <summary>
    /// Доверчивый игрок.
    /// При принятии решения всегда доверяет.
    /// </summary>
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

        public override string TypeName { get { return _info.TypeName; } }
        public override Color TypeColor { get { return _info.TypeColor; } }
    }
}
