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
        public void Add(Person pers)
        {
            _persons_list.Add(pers);
        }

        ////Удаление и добавление указаного числа игроков
        ////Для корректной работы число должно быть не больше
        ////половины количества игроков
        //public void Selection(int number)
        //{
        //    _persons_list.Sort((x, y) => x.Score.CompareTo(y.Score));

        //    DeleteNumberOfWorst(number);
        //    AddNewFromTop(number);
        //}

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
        public List<Person> GetAllPersons()
        {
            return new List<Person>(_persons_list);
        }

        // Получить список всех игроков отсортированный по именам.
        public List<Person> GetPersonsSortedByName()
        {
            return _persons_list.OrderBy(u => u.TypeName).ToList();
        }

        // Получить список всех игроков отсортированный по счёту.
        public List<Person> GetPersonsSortedByScore()
        {
            return _persons_list.OrderBy(u => u.Score).ToList();
        }

        // Получение игркоа по индексу.
        public Person this[int index]
        {
            get { return _persons_list[index]; }
        }


        // Удаление указанное количество игроков с наименьшим счётом.
        // Если у игроков одинаковые счета, то удаляются случайные из них.
        // - int number - количество удаляемых игроков
        private void DeleteNumberOfWorst(int number)
        {
            //_persons_list.Sort((x, y) => x.Score.CompareTo(y.Score));
            int lowest_score = _persons_list[0].Score;
            int i = 0;
            for (; number > 0 && i < _persons_list.Count; i++)
            {
                if (lowest_score == _persons_list[i].Score)
                {
                    continue;
                }
                DeleteRange(ref i, ref number);
                lowest_score = _persons_list[i].Score;
            }
            if (number > 0)
            {
                DeleteRange(ref i, ref number);
            }
        }
        private void DeleteRange(ref int end, ref int number)
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

        private void AddNewFromTop(int number)
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

        private List<Person> _persons_list = new List<Person>();
    }
}
