﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Evolution_Of_Trust
{
    class Game
    {
        public Game()
        {
            Reset();
        }

        /// <summary>
        /// Вернуть количество игроков начальное состояние.
        /// </summary>
        public void Reset()
        {
            _storage.Clear();
            _count = 0;
            for (int i = 0; i < _population.Length; i++)
            {
                for (int j = 0; j < _population[i]; j++)
                {
                    _storage.Add(PlayersCreator.Create(i));
                }
            }
            _persons_number = _storage.GetPlayersNumber();
        }

        /// <summary>
        /// Именить 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="id"></param>
        public void ControlPopulation(int value, int id)
        {
            if(value == _population[id])
            {
                return;
            }
            int dif = _population[id] - value;
            _population[id] = value;
            if(dif < 0)
            {
                ChangeBiggestNumber(id, dif);
                return;
            }
            ChangeSmallestNumber(id, dif);
        }
        /// <summary>
        /// Увеличивает популяцию всех типов на dif, кроме типа по index.
        /// Начинает увеличение с наименьшего, если это не ноль.
        /// </summary>
        /// <param name="index">Индекс неизменяемого типа.</param>
        /// <param name="dif">Изменение популяции.</param>
        private void ChangeSmallestNumber(int index, int dif)
        {
            for(; dif!=0 ; dif--)
            {
                int smallest = _persons_number;
                int index_smallest = 0;
                for (int i = 0; i < _population.Length; i++)
                {
                    if (_population[i] == 0)
                    {
                        continue;
                    }
                    if (i == index)
                    {
                        continue;
                    }
                    if (smallest > _population[i])
                    {
                        index_smallest = i;
                        smallest = _population[i];
                    }
                }
                _population[index_smallest]++;
            }
        }
        /// <summary>
        /// Уменьшает популяцию всех типов на dif, кроме типа по index.
        /// Начинает уменьшение с наиюольшего.
        /// </summary>
        /// <param name="index">Индекс неизменяемого типа.</param>
        /// <param name="dif">Изменение популяции.</param>
        private void ChangeBiggestNumber(int index, int dif)
        {
            for (; dif != 0; dif++)
            {
                int biggest = 0;
                int index_biggest = 0;
                for (int i = 0; i < _population.Length; i++)
                {
                    if (_population[i] == 0)
                    {
                        continue;
                    }
                    if (i == index)
                    {
                        continue;
                    }
                    if (biggest < _population[i])
                    {
                        index_biggest = i;
                        biggest = _population[i];
                    }
                }
                _population[index_biggest]--;
            }
        }

        public void Step()
        {
            if (_count == _persons_number)
            {
                DeleteWorst();
                return;
            }
            if (_count == -1)
            {
                AddNewFromTop();
                return;
            }
            PlayMatch();
        }

        /// <summary>
        /// Удалить худших.
        /// </summary>
        private void DeleteWorst()
        {
            _storage.SortByScore();
            _storage.DeleteNumberOfWorst(_selected_number);
            _count = -1;
            _persons_number = _storage.GetPlayersNumber();
        }

        /// <summary>
        /// Скопировать лучших игроков.
        /// </summary>
        private void AddNewFromTop()
        {
            _storage.AddNewFromTop(_selected_number);
            _storage.ResetScore();
            _persons_number = _storage.GetPlayersNumber();
            _count = 0;
        }

        /// <summary>
        /// Начать матч.
        /// </summary>
        private void PlayMatch()
        {
            for (; _count < _persons_number; _count++)
            {
                for (int i = _count + 1; i < _persons_number; i++)
                {
                    PlayRounds(_storage[_count], _storage[i]);
                }
            }
        }

        /// <summary>
        /// Отыграть раунды между двумя игроками.
        /// </summary>
        /// <param name="pers1">Первый игрок.</param>
        /// <param name="pers2">Второй игрок.</param>
        private void PlayRounds(Player pers1, Player pers2)
        {
            for (int i = 0; i < _rounds_number; i++)
            {
                _exchange_machine.Exchange(pers1, pers2);
            }
            pers1.ResetPlayerMemory();
            pers2.ResetPlayerMemory();
        }

        /// <summary>
        /// Получить список всех игроков отсортированный по именам.
        /// </summary>
        /// <returns>Список игроков.</returns>
        public List<Player> GetPersonsSortedByName()
        {
            return _storage.GetPersonsSortedByName();
        }

        /// <summary>
        /// Установить шанс ошибки игрока.
        /// </summary>
        /// <param name="value">Процент вероятности.</param>
        public void SetMistakeChange(int value)
        {
            _exchange_machine.MistakeChance = (double)value / 100.0;
        }

        /// <summary>
        /// Хранилище игроков.
        /// </summary>
        private ListStorage _storage = new ListStorage();

        private ExchangeMachine _exchange_machine = new ExchangeMachine();
        public ExchangeMachine ExchangeMachine { get { return _exchange_machine; } }

        /// <summary>
        /// Массив количества типов игроков.
        /// </summary>
        private int[] _population = { 4, 4, 4, 4, 4, 4, 3, 3 };
        public int[] Population { get { return _population; } }

        /// <summary>
        /// Количество раундов.
        /// </summary>
        private int _rounds_number = 10;
        /// <summary>
        /// Установить количество раундов.
        /// </summary>
        /// <param name="value">Количество раундов.</param>
        public void SetRoundsNumber(int value)
        {
            _rounds_number = value;
        }
        public int RoundsNumber
        {
            get { return _rounds_number; }
            set { _rounds_number = value; }
        }

        private int _min_rounds_number = 1;
        public int MinRoundsNumber { get { return _min_rounds_number; } }
        private int _max_rounds_number = 50;
        public int MaxRoundsNumber { get { return _max_rounds_number; } }

        /// <summary>
        /// Количество отбираемых игроков.
        /// </summary>
        private int _selected_number = 5;
        /// <summary>
        /// Уставновить количество отбираемых игроков.
        /// </summary>
        /// <param name="value">Количество отбираемых игроков.</param>
        public void SetSelectionsNumber(int value)
        {
            _selected_number = value;
        }
        public int SelectionsNumber
        {
            get { return _selected_number; }
            set { _selected_number = value; }
        }

        private int _min_selections_number = 1;
        public int MinSelectionsNumber { get { return _min_selections_number; } }
        private int _max_selections_number = 15;
        public int MaxSelectionsNumber { get { return _max_selections_number; } }

        /// <summary>
        /// Количество игроков.
        /// </summary>
        private int _persons_number;
        public int PopulationNumber { get { return _persons_number; } }

        /// <summary>
        /// Состояние игры.
        /// </summary>
        private int _count = 0;
    }
}
