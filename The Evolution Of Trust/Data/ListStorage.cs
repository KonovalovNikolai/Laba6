using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Evolution_Of_Trust
{
    class ListStorage
    {
        // Добавление игрока в хранилище.
        public void Add(Player pers)
        {
            _persons_list.Add(pers);
        }

        // Отсортировать хранилище по счёту
        public void SortByScore()
        {
            _persons_list.Sort((x, y) => x.Score.CompareTo(y.Score));
        }

        // Обнуление счёта игроков.
        public void ResetScore()
        {
            foreach (var item in _persons_list)
            {
                item.Score = 0;
            }
        }

        // Очищение хранилища.
        public void Clear()
        {
            _persons_list.Clear();
        }

        // Получить список всех игроков.
        public List<Player> GetAllPersons()
        {
            return new List<Player>(_persons_list);
        }

        // Получить список всех игроков отсортированный по именам.
        public List<Player> GetPersonsSortedByName()
        {
            return _persons_list.OrderBy(u => u.TypeName).ToList();
        }

        // Получить список всех игроков отсортированный по счёту.
        public List<Player> GetPersonsSortedByScore()
        {
            return _persons_list.OrderBy(u => u.Score).ToList();
        }

        // Получение игркоа по индексу.
        public Player this[int index]
        {
            get { return _persons_list[index]; }
        }

        // Удаление указанного количества игроков с наименьшим счётом.
        // Если у игроков одинаковые счета, то удаляются случайные из них.
        // - int number - количество удаляемых игроков
        public void DeleteNumberOfWorst(int number)
        {
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

        // Удаление игроков из указанного промежутка.
        // Если промежуток больше числа удаляемых игроков,
        // то выбираются случайные игроки из этого промежутка. 
        // Начало промежутка всегда 0.
        // Игроки удаляются из промежутка [0, end).
        // - int end - конец промежутка.
        // - ref int number - количество удаляемых игроков.
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

        // Добавление указанного количества игроков.
        // Новые игроки создаются из игроков с лучшим счётом.
        // Если у игроков одинаковые счета, то создаются по случайным из них.
        // - int number - количество удаляемых игроков
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

        // Добавление игроков из указанного промежутка.
        // Если промежуток больше числа добавляемых игроков,
        // то выбираются случайные игроки из этого промежутка. 
        // Игроки добавляются из промежутка [start, end].
        // - ref int start - начало промежутка
        // - ref int end - конец промежутка.
        // - ref int number - количество удаляемых игроков.
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

        //Хранилище игроков
        private List<Player> _persons_list = new List<Player>();
    }
}
