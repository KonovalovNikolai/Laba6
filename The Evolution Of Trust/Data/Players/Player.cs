using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Evolution_Of_Trust
{
    abstract class Player
    {
        /// <summary>
        /// Игрок принимает решение довериться или обмануть другого игрока.
        /// Разные типы игроков принимают решение по-разному.
        /// </summary>
        public abstract bool MakeADecision();

        /// <summary>
        /// Вернуть игрока в начальное состояние.
        /// </summary>
        public abstract void ResetPlayerMemory();

        /// <summary>
        /// Создать игрока своего типа.
        /// </summary>
        /// <returns></returns>
        public abstract Player Create();

        /// <summary>
        /// Счёт игрока
        /// </summary>
        private int _score = 0;
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// Предыдущий ход противника.
        /// Многие типы игроков принимают решения
        /// операясь на этот поле.
        /// </summary>
        private bool _memory = true;
        public bool Memory
        {
            get { return _memory; }
            set { _memory = value; }
        }


        public abstract string TypeName { get; }
        public abstract Color TypeColor { get; }
    }
}
