﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Evolution_Of_Trust
{
    static class RRandom
    {
        public static bool CheckChance(double chance)
        {
            return chance >= _random.NextDouble(); ;
        }

        private static Random _random = new Random();
        public static Random Random
        {
            get { return _random; }
        }
    }
}
