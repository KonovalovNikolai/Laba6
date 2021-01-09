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
            _persons_list.Add(pers);
        }

        /// <summary>
        /// Отсортировать хранилище по счёту.
        /// </summary>
        public void SortByScore()
        {
            _persons_list.Sort((x, y) => x.Score.CompareTo(y.Score));
        }

        /// <summary>
        /// Обнуление счёта игроков.
        /// </summary>
        public void ResetScore()
        {
            foreach (var item in _persons_list)
            {
                item.Score = 0;
            }
        }

        /// <summary>
        /// Очищение хранилища.
        /// </summary>
        public void Clear()
        {
            _persons_list.Clear();
        }

        /// <summary>
        /// Получить количество игроков.
        /// </summary>
        /// <returns>Количество игроков.</returns>
        public int GetPlayersNumber()
        {
            return _persons_list.Count();
        }

        /// <summary>
        /// Получить список всех игроков.
        /// </summary>
        /// <returns>Список игроков.</returns>
        public List<Player> GetAllPersons()
        {
            return new List<Player>(_persons_list);
        }

        /// <summary>
        /// Получить список всех игроков отсортированный по именам.
        /// </summary>
        /// <returns>Список игроков.</returns>
        public List<Player> GetPersonsSortedByName()
        {
            return _persons_list.OrderBy(u => u.TypeName).ToList();
        }

        /// <summary>
        /// Получить список всех игроков отсортированный по счёту.
        /// </summary>
        /// <returns>Список игроков.</returns>
        public List<Player> GetPersonsSortedByScore()
        {
            return _persons_list.OrderBy(u => u.Score).ToList();
        }

        /// <summary>
        /// Получение игркоа по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Игрок.</returns>
        public Player this[int index]
        {
            get { return _persons_list[index]; }
        }

        /// <summary>
        /// Удаление указанного количества игроков с наименьшим счётом.
        /// Если у игроков одинаковые счета, то удаляются случайные из них.
        /// </summary>
        /// <param name="number">Количество удаляемых игроков.</param>
        public void DeleteNumberOfWorst(int number)
        {
            if(_persons_list.Count - number < 0)
            {

            }
            int lowest_score = _persons_list[0].Score;
            int i = 0;
            for (; number > 0 && i < _persons_list.Count; i++)
            {
                if (lowest_score == _persons_list[i].Score)
                {
                    continue;
                }
                DeleteRange(i, ref number);
                lowest_score = _persons_list[i].Score;
                i = 0;
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
                _persons_list.RemoveRange(0, end);
                number -= end;
                end = 0;
                return;
            }
            while (number != 0)
            {
                int index = RRandom.random.Next(end);
                _persons_list.RemoveAt(index);
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
            int best_score = _persons_list[_persons_list.Count - 1].Score;
            int i = _persons_list.Count - 2;
            int start = i + 1;
            for (; number > 0 && i >= 0; i--)
            {
                if (best_score == _persons_list[i].Score)
                {
                    continue;
                }
                AddRange(ref start, ref i, ref number);
                best_score = _persons_list[i].Score;
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
                    Add(_persons_list[j].Create());
                }
                number -= start - end;
                return;
            }
            while (number != 0)
            {
                int index = RRandom.random.Next(end + 1, start + 1);
                Add(_persons_list[index]);
                Add(_persons_list[index].Create());
                _persons_list.RemoveAt(index);
                start--;
                number--;
            }
        }

        /// <summary>
        /// Хранилище игроков.
        /// </summary>
        private List<Player> _persons_list = new List<Player>();
    }
}
