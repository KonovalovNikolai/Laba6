using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Evolution_Of_Trust
{
    class Creator
    {
        public static Person Create(int id)
        {
            switch (id)
            {
                case (int)_Names.Trustful:
                    return new Trustful();

                case (int)_Names.Cheater:
                    return new Cheater();

                case (int)_Names.Copycat:
                    return new Copycat();

                case (int)_Names.Grudger:
                    return new Grudger();

                case (int)_Names.Detective:
                    return new Detective();

                case (int)_Names.Imitator:
                    return new Imitator();

                case (int)_Names.Simpleton:
                    return new Simpleton();

                case (int)_Names.Randomized:
                    return new Randomized();
                default:
                    throw new ArgumentException();
            }
        }

        private enum _Names
        {
            Trustful,
            Cheater,
            Copycat,
            Grudger,
            Detective,
            Imitator,
            Simpleton,
            Randomized
        }

    }
}
