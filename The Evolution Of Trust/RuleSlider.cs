﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace The_Evolution_Of_Trust
{
    class RuleSlider : TrackBar
    {
        public RuleSlider(int min, int max, string text, int value, GameMutator mutator)
        {
            _table.ColumnCount = 1;
            _table.RowCount = 2;

            _table.RowStyles.Add(new RowStyle());
            _table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _text = text;

            _mutator = mutator;

            _lable.Text = _text.Replace("{}", value.ToString());
            _lable.Font = new Font("Microsoft Sans Serif", 11);
            _lable.Dock = DockStyle.Fill;
            _table.Controls.Add(_lable, 0, 0);

            this.Minimum = min;
            this.Maximum = max;
            this.Value = value;
            this.LargeChange = 1;
            this.Dock = DockStyle.Fill;
            _table.Controls.Add(this, 0, 1);
        }

        public void UpdateValue(int value)
        {
            _lable.Text = _text.Replace("{}", value.ToString());
            _mutator(value);
        }

        private string _text;

        internal delegate void GameMutator(int value);
        private GameMutator _mutator;
        public GameMutator Mutator
        {
            get { return _mutator; }
            set { _mutator = value; }
        }

        private Label _lable = new Label();

        private TableLayoutPanel _table = new TableLayoutPanel();
        public TableLayoutPanel Table
        {
            get { return _table; }
        }
    }
}
