using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace The_Evolution_Of_Trust
{
    class PayoffUpDown : NumericUpDown
    {
        public PayoffUpDown(int min, int max, int value, GameMutator mutator)
        {
            this.Dock = DockStyle.Fill;
            this.Minimum = min;
            this.Maximum = max;
            this.Value = (decimal)value;
            this.Margin = new Padding(0);
            _mutator = mutator;
        }

        public void UpdateValue()
        {
            if (this.Value > this.Maximum)
            {
                this.Value = this.Maximum;
            }
            if (this.Value < this.Minimum)
            {
                this.Value = this.Minimum;
            }
            _mutator((int)this.Value);
        }

        internal delegate void GameMutator(int value);
        private GameMutator _mutator;
        public GameMutator Mutator
        {
            get { return _mutator; }
            set { _mutator = value; }
        }
    }
}
