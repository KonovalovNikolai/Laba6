using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Evolution_Of_Trust
{
    class ExchangeMachine
    {
        public void Exchange(Person pers1, Person pers2)
        {
            bool des1 = CheckChance(pers1.MakeADecision());
            bool des2 = CheckChance(pers2.MakeADecision());

            int payoff1;
            int payoff2;

            if (des1 == des2)
            {
                if (des1)
                {

                    payoff1 = _trust_trust_payoff;
                    payoff2 = _trust_trust_payoff;
                }
                else
                {
                    payoff1 = _cheat_cheat_payoff;
                    payoff2 = _cheat_cheat_payoff;
                }
            }
            else
            {
                if (des1)
                {
                    payoff1 = _trust_payoff;
                    payoff2 = _cheat_payoff;
                }
                else
                {
                    payoff1 = _cheat_payoff;
                    payoff2 = _trust_payoff;
                }
            }

            pers1.Score += payoff1;
            pers1.Memory = des2;

            pers2.Score += payoff2;
            pers2.Memory = des1;
        }

        private bool CheckChance(bool decision)
        {
            return RRandom.CheckChance(_mistake_chance) ? !decision : decision;
        }

        private int _trust_trust_payoff = 2;
        public int TrustTrustPayoff
        {
            get { return _trust_trust_payoff; }
            set { _trust_trust_payoff = value; }
        }

        private int _cheat_cheat_payoff = 0;
        public int CheatCheatPayoff
        {
            get { return _cheat_cheat_payoff; }
            set { _cheat_cheat_payoff = value; }
        }

        private int _cheat_payoff = 3;
        public int CheatPayoff
        {
            get { return _cheat_payoff; }
            set { _cheat_payoff = value; }
        }

        private int _trust_payoff = -1;
        public int TrustPayoff
        {
            get { return _trust_payoff; }
            set { _trust_payoff = value; }
        }

        private double _mistake_chance = 0.05;
        public double MistakeChance
        {
            get { return _mistake_chance; }
            set { _mistake_chance = value; }
        }
    }
}
