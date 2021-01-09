using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    /// <summary>
    /// "Случайный" игрок.
    /// Обманывает и доверяет с шансом 50/50.
    /// </summary>
    class Randomized : Player
    {
        static Randomized()
        {
            _info = new PlayerTypeInfo(
                 7,
                "Randomized",
                "Просто жульничает или сотрудничает случайным образом\nс вероятностью 50/50",
                Color.Red
            );
        }

        public override bool MakeADecision()
        {
            return RRandom.CheckChance(_chance);
        }

        public override void ResetPlayerMemory() { }

        public override Player Create()
        {
            return new Randomized();
        }

        private static double _chance = 0.5;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        public override string TypeName { get { return _info.TypeName; } }
        public override Color TypeColor { get { return _info.TypeColor; } }
    }
}
