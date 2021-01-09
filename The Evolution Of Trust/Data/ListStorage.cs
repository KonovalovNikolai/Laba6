using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Evolution_Of_Trust
{
    class ListStorage
    {
        /// <summary>
        /// Добавление игрока в хранилище.
        /// </summary>
        /// <param name="pers">Добавляемый игрок.</param>
        public void Add(Player pers)
        {
            _players_list.Add(pers);
        }

        /// <summary>
        /// Отсортировать хранилище по счёту.
        /// </summary>
        public void SortByScore()
        {
            _players_list.Sort((x, y) => x.Score.CompareTo(y.Score));
        }

        /// <summary>
        /// Обнуление счёта игроков.
        /// </summary>
        public void ResetScore()
        {
            foreach (var item in _players_list)
            {
                item.Score = 0;
            }
        }

        /// <summary>
        /// Очищение хранилища.
        /// </summary>
        public void Clear()
        {
            _players_list.Clear();
        }

        /// <summary>
        /// Получить количество игроков.
        /// </summary>
        /// <returns>Количество игроков.</returns>
        public int GetPlayersNumber()
        {
            return _players_list.Count();
        }

        /// <summary>
        /// Получить список всех игроков.
        /// </summary>
        /// <returns>Список игроков.</returns>
        public List<Player> GetAllPersons()
        {
            return new List<Player>(_players_list);
        }

        /// <summary>
        /// Получить список всех игроков отсортированный по именам.
        /// </summary>
        /// <returns>Список игроков.</returns>
        public List<Player> GetPersonsSortedByName()
        {
            return _players_list.OrderBy(u => u.TypeName).ToList();
        }

        /// <summary>
        /// Получить список всех игроков отсортированный по счёту.
        /// </summary>
        /// <returns>Список игроков.</returns>
        public List<Player> GetPersonsSortedByScore()
        {
            return _players_list.OrderBy(u => u.Score).ToList();
        }

        /// <summary>
        /// Получение игркоа по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Игрок.</returns>
        public Player this[int index]
        {
            get { return _players_list[index]; }
        }

        /// <summary>
        /// Удаление указанного количества игроков с наименьшим счётом.
        /// Если у игроков одинаковые счета, то удаляются случайные из них.
        /// </summary>
        /// <param name="number">Количество удаляемых игроков.</param>
        public void DeleteNumberOfWorst(int number)
        {
            if(_players_list.Count < number)
            {
                Clear();
                return;
            }
            int lowest_score = _players_list[0].Score;
            int i = 0;
            for (; number > 0 && i < _players_list.Count; i++)
            {
                if (lowest_score == _players_list[i].Score)
                {
                    continue;
                }
                DeleteRange(i, ref number);
                i = 0;
                lowest_score = _players_list[i].Score;
            }
            if (number > 0)
            {
                DeleteRange(i, ref number);
            }
        }

        /// <summary>
        /// Удаление игроков из указанного промежутка.
        /// Если промежуток больше числа удаляемых игроков, то выбираются случайные игроки из этого промежутка.
        /// Начало промежутка всегда 0.
        /// Игроки удаляются из промежутка [0, end).
        /// </summary>
        /// <param name="end">Конец промежутка.</param>
        /// <param name="number">Количество удаляемых игроков.</param>
        private void DeleteRange(int end, ref int number)
        {
            if (end <= number)
            {
                _players_list.RemoveRange(0, end);
                number -= end;
                end = 0;
                return;
            }
            while (number != 0)
            {
                int index = RRandom.random.Next(end);
                _players_list.RemoveAt(index);
                number--;
                end--;
            }
        }

        /// <summary>
        /// Добавление указанного количества игроков.
        /// Новые игроки создаются из игроков с лучшим счётом.
        /// Если у игроков одинаковые счета, то создаются по случайным из них.
        /// </summary>
        /// <param name="number">Количество удаляемых игроков.</param>
        public void AddNewFromTop(int number)
        {
            if(_players_list.Count == 0)
            {
                return;
            }
            if (_players_list.Count < number)
            {
                number = _players_list.Count;
            }
            int best_score = _players_list[_players_list.Count - 1].Score;
            int i = _players_list.Count - 2;
            int start = i + 1;
            for (; number > 0 && i >= 0; i--)
            {
                if (best_score == _players_list[i].Score)
                {
                    continue;
                }
                AddRange(ref start, ref i, ref number);
                best_score = _players_list[i].Score;
                start = i;
            }
            AddRange(ref start, ref i, ref number);
        }

        /// <summary>
        /// Добавление игроков из указанного промежутка.
        /// Если промежуток больше числа добавляемых игроков, то выбираются случайные игроки из этого промежутка.
        /// Игроки добавляются из промежутка [start, end].
        /// </summary>
        /// <param name="start">Начало промежутка.</param>
        /// <param name="end">Конец промежутка.</param>
        /// <param name="number">Количество удаляемых игроков.</param>
        private void AddRange(ref int start, ref int end, ref int number)
        {
            if (start - end <= number)
            {
                for (int j = end + 1; j <= start; j++)
                {
                    Add(_players_list[j].Create());
                }
                number -= start - end;
                return;
            }
            while (number != 0)
            {
                int index = RRandom.random.Next(end + 1, start + 1);
                Add(_players_list[index]);
                Add(_players_list[index].Create());
                _players_list.RemoveAt(index);
                start--;
                number--;
            }
        }

        /// <summary>
        /// Хранилище игроков.
        /// </summary>
        private List<Player> _players_list = new List<Player>();
    }
}
