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
        // Игрок принимает решение довериться или обмануть другого игрока.
        // Разные типы игроков принимают решение по-разному.
        public abstract bool MakeADecision();

        // Вернуть игрока в начальное состояние.
        public abstract void ResetPlayerMemory();

        // Создать игрока своего типа.
        public abstract Player Create();

        // Счёт игрока.
        private int _score = 0;
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        // Предыдущий ход противника.
        // Многие типы игроков принимают решения
        // операясь на этот поле.
        private bool _memory = true;
        public bool Memory
        {
            get { return _memory; }
            set { _memory = value; }
        }

        // Имя типа
        public abstract string TypeName { get; }
        // Цвет типа
        public abstract Color TypeColor { get; }
    }

    class Cheater : Player
    {
        static Cheater()
        {
            _info = new PlayerTypeInfo(_type_id, _type_name, _type_description, _type_color);
        }

        public override bool MakeADecision()
        {
            return false;
        }

        public override void ResetPlayerMemory() { }

        public override Player Create()
        {
            return new Cheater();
        }

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        private static int _type_id = 1;
        private static string _type_name = "Cheater";
        private static Color _type_color = Color.Black;
        private static string _type_description = 
            "\"Cильные должны есть слабых!\"\nВсегда обманывает.";

        public override string TypeName { get { return _type_name; } }
        //public override int TypeId { get { return _type_id; } }
        public override Color TypeColor { get { return _type_color; } }
    }

    class Copycat : Player
    {
        static Copycat()
        {
            _info = new PlayerTypeInfo(_type_id, _type_name, _type_description, _type_color);
        }

        public override bool MakeADecision()
        {
            return Memory;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
        }

        public override Player Create()
        {
            return new Copycat();
        }

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        private static int _type_id = 2;
        private static string _type_name = "Copycat";
        private static Color _type_color = Color.Blue;
        private static string _type_description = 
            "\"Привет! На первом ходу я доверюсь,\nа потом просто буду копировать твой последний ход.\"";
        //public override int TypeId { get { return _type_id; } }
        public override string TypeName { get { return _type_name; } }
        public override Color TypeColor { get { return _type_color; } }
    }

    class Grudger : Player
    {
        static Grudger()
        {
            _info = new PlayerTypeInfo(_type_id, _type_name, _type_description, _type_color);
        }

        public override bool MakeADecision()
        {
            if (!Memory)
            {
                _was_deceived = true;
            }
            return !_was_deceived;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
            _was_deceived = false;
        }

        public override Player Create()
        {
            return new Grudger();
        }

        private bool _was_deceived = false;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        private static int _type_id = 3;
        private static string _type_name = "Grudger";
        private static Color _type_color = Color.Yellow;
        private static string _type_description =
            "\"Я начну с доверия и буду продолжать доверять,\nно если ты хоть раз меня обманешь,\nЯ БУДУ ЖУЛЬНИЧАТЬ ДО ПОСЛЕДНЕГО.\"";
        public override string TypeName { get { return _type_name; } }
        //public override int TypeId { get { return _type_id; } }
        public override Color TypeColor { get { return _type_color; } }
    }

    class Detective : Player
    {
        static Detective()
        {
            _info = new PlayerTypeInfo(_type_id, _type_name, _type_description, _type_color);
        }

        public override bool MakeADecision()
        {
            if (_count < _first_moves.Length)
            {
                if (!Memory)
                {
                    _was_deceived = true;
                }
                bool ret = _first_moves[_count];
                _count++;
                return ret;
            }
            if (_was_deceived)
            {
                return Memory;
            }
            return false;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
            _count = 0;
            _was_deceived = false;
        }

        public override Player Create()
        {
            return new Detective();
        }

        private static readonly bool[] _first_moves = { true, false, true, true };
        private int _count = 0;
        private bool _was_deceived = false;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        private static int _type_id = 4;
        private static string _type_name = "Detective";
        private static Color _type_color = Color.Brown;
        private static string _type_description =
            "\"Учти: я тебя анализирую.\nМои первые ходы: доверие, обман, доверие, доверие.\nЕсли обманешь, я буду действовать как Имитатор.\nЕсли ни разу не обманешь, я буду всегда жульничать,\nчтобы использовать тебя. Элементарно, мой дорогой Ватсон.\"";

        public override string TypeName { get { return _type_name; } }
        //public override int TypeId { get { return _type_id; } }
        public override Color TypeColor { get { return _type_color; } }
    }

    class Imitator : Player
    {
        static Imitator()
        {
            _info = new PlayerTypeInfo(_type_id, _type_name, _type_description, _type_color);
        }

        public override bool MakeADecision()
        {
            if (_was_deceived_twice)
                return Memory;

            if (!Memory) _count++;
            else _count = 0;

            if (_count == 2)
            {
                _was_deceived_twice = true;
            }

            return true;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
            _was_deceived_twice = false;
            _count = 0;
        }

        public override Player Create()
        {
            return new Imitator();
        }

        private bool _was_deceived_twice = false;
        private int _count = 0;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        private static int _type_id = 5;
        private static string _type_name = "Imitator";
        private static Color _type_color = Color.Purple;
        private static string _type_description =
             "\"Привет!\nЯ почти как Имитатор, но жульничаю только если обмануть меня два раза подряд.\nВ конце концов, первый раз мог быть ошибкой!\"";

        public override string TypeName { get { return _type_name; } }
        //public override int TypeId { get { return _type_id; } }
        public override Color TypeColor { get { return _type_color; } }
    }

    class Simpleton : Player
    {
        static Simpleton()
        {
            _info = new PlayerTypeInfo(_type_id, _type_name, _type_description, _type_color);
        }

        public override bool MakeADecision()
        {
            if (!Memory)
            {
                _last_move = !_last_move;
            }
            return _last_move;
        }

        public override void ResetPlayerMemory()
        {
            Memory = true;
            _last_move = true;
        }

        public override Player Create()
        {
            return new Simpleton();
        }

        private bool _last_move = true;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        private static int _type_id = 6;
        private static string _type_name = "Simpleton";
        private static Color _type_color = Color.Green;
        private static string _type_description =
            "\"превет\nсначала я доверять тебе.\nесли ты тоже доверять мене, я повторять свой ход, даже если это ошибка.\nесли ты обмануть меня, я делать свой ход наоборот, даже если это ошибка.\"";

        public override string TypeName { get { return _type_name; } }
        //public override int TypeId { get { return _type_id; } }
        public override Color TypeColor { get { return _type_color; } }
    }

    class Randomized : Player
    {
        static Randomized()
        {
            _info = new PlayerTypeInfo(_type_id, _type_name, _type_description, _type_color);
        }

        public override bool MakeADecision()
        {
            return RRandom.CheckChance(_chance);
        }

        public override void ResetPlayerMemory() { }

        public override Player Create()
        {
            return new Randomized();
        }

        private static double _chance = 0.5;

        private static PlayerTypeInfo _info;
        public static PlayerTypeInfo Info { get { return _info; } }

        private static int _type_id = 7;
        private static Color _type_color = Color.Red;
        private static string _type_name = "Randomized";
        private static string _type_description =
            "Просто жульничает или сотрудничает случайным образом\nс вероятностью 50/50";

        public override string TypeName { get { return _type_name; } }
        //public override int TypeId { get { return _type_id; } }
        public override Color TypeColor { get { return _type_color; } }
    }
}
