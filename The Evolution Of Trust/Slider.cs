using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace The_Evolution_Of_Trust
{
    class Slider : TrackBar
    {
        public Slider(PlayerTypeInfo info, int value, int max)
        {
            _name = info.TypeName;
            _type_id = info.TypeId;

            _table.ColumnCount = 1;
            _table.RowCount = 3;

            _table.RowStyles.Add(new RowStyle());
            _table.RowStyles.Add(new RowStyle());
            _table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _name_lable.Text = _name;
            _name_lable.ForeColor = info.TypeColor;
            _name_lable.Font = new Font("Arial", 12, FontStyle.Bold);
            _name_lable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            _name_lable.Dock = DockStyle.Fill;
            _table.Controls.Add(_name_lable, 0, 0);

            this.Minimum = 0;
            this.Maximum = max;
            this.Value = value;
            this.LargeChange = 1;
            this.Dock = DockStyle.Fill;
            _table.Controls.Add(this, 0, 2);

            _count_lable.Text = this.Value.ToString();
            _count_lable.Dock = DockStyle.Fill;
            _count_lable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            _table.Controls.Add(_count_lable, 0, 1);

            _toolTip.AutoPopDelay = 5000;
            _toolTip.InitialDelay = 100;
            _toolTip.ReshowDelay = 500;
            _toolTip.ShowAlways = true;
            _toolTip.ToolTipTitle = info.TypeName;
            _toolTip.UseFading = true;

            _toolTip.SetToolTip(_name_lable, info.TypeDescription);
        }

        public void UpdateValue(int value)
        {
            this.Value = value;
            _count_lable.Text = this.Value.ToString();
        }

        private int _type_id;
        public int TypeId { get { return _type_id; } }

        private string _name;
        private Label _name_lable = new Label();
        private Label _count_lable = new Label();
        private ToolTip _toolTip = new ToolTip();
        public Label Counter
        {
            get { return _count_lable; }
        }

        private TableLayoutPanel _table = new TableLayoutPanel();
        public TableLayoutPanel Table
        {
            get { return _table; }
        }
    }
}
