using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Evolution_Of_Trust
{
    class ExchangeMachine
    {
        /// <summary>
        /// Обмен между игроками.
        /// </summary>
        /// <param name="pers1">Игрок 1.</param>
        /// <param name="pers2">Игрок 2.</param>
        public void Exchange(Player pers1, Player pers2)
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

        /// <summary>
        /// Проверка шанса ошибки.
        /// Если ошибка произошла, то возвращает значение обратное decision.
        /// </summary>
        /// <param name="decision">Решение игрока.</param>
        /// <returns></returns>
        private bool CheckChance(bool decision)
        {
            return RRandom.CheckChance(_mistake_chance) ? !decision : decision;
        }

        /// <summary>
        /// Выплата при одинаковом решении довериться.
        /// </summary>
        private int _trust_trust_payoff = 2;
        public int TrustTrustPayoff
        {
            get { return _trust_trust_payoff; }
            set { _trust_trust_payoff = value; }
        }

        /// <summary>
        /// Выплаты при одинаковом решении обмануть.
        /// </summary>
        private int _cheat_cheat_payoff = 0;
        public int CheatCheatPayoff
        {
            get { return _cheat_cheat_payoff; }
            set { _cheat_cheat_payoff = value; }
        }

        /// <summary>
        /// Выплата за обман.
        /// </summary>
        private int _cheat_payoff = 3;
        public int CheatPayoff
        {
            get { return _cheat_payoff; }
            set { _cheat_payoff = value; }
        }

        /// <summary>
        /// Выплата за доверие.
        /// </summary>
        private int _trust_payoff = -1;
        public int TrustPayoff
        {
            get { return _trust_payoff; }
            set { _trust_payoff = value; }
        }

        /// <summary>
        /// Шанс ошибки.
        /// </summary>
        private double _mistake_chance = 0.05;
        public double MistakeChance
        {
            get { return _mistake_chance; }
            set { _mistake_chance = value; }
        }
    }
}
