using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace The_Evolution_Of_Trust
{
    public partial class MainForm : Form
    {
        private List<Slider> _sliders = new List<Slider>();

        private bool _thread_is_running = false;
        private Graphics _g;
        private SolidBrush _brush = new SolidBrush(Color.Black);
        private Pen _pen = new Pen(Color.Red, 1F);
        private float _centerX;
        private float _centerY;
        private float _radius;
        private float _radius_outside;
        private float _small_radius = 15F;

        private Game _game = new Game();

        public MainForm()
        {
            InitializeComponent();

            _g = DrawDeskPanel.CreateGraphics();

            DrawPopulationSliders();
            DrawRuleSliders();
            DrawPayoffs();
            SetCoords();
        }

        private void DrawDeskPanel_Paint(object sender, PaintEventArgs e)
        {
            StringFormat format = new StringFormat();
            Font font = new Font("Arial", 11);

            if(_game.PopulationNumber == 0)
            {
                return;
            }
            if(_game.PopulationNumber == 1)
            {
                Player player = _game.GetPersonsSortedByName()[0];
                _brush.Color = player.TypeColor;
                FillCicle(_brush, _centerX, _centerY, _small_radius);
                _brush.Color = Color.Black;
                _g.DrawString(player.Score.ToString(), font, _brush, _centerX + _small_radius, _centerY, format);
                return;
            }

            double angle = 360 / _game.PopulationNumber;
            double current_angle = 0;
            foreach(var player in _game.GetPersonsSortedByName())
            {
                double radians = current_angle * Math.PI / 180;

                float x = _centerX + _radius * (float)Math.Cos(radians);
                float y = _centerY + _radius * (float)Math.Sin(radians);

                _brush.Color = player.TypeColor;
                FillCicle(_brush, x, y, _small_radius);

                x = _centerX + _radius_outside * (float)Math.Cos(radians);
                y = _centerY + _radius_outside * (float)Math.Sin(radians);

                _brush.Color = Color.Black;
                _g.DrawString(player.Score.ToString(), font, _brush, x, y, format);

                current_angle += angle;
            }
        }
        
        private void DrawPopulationSliders()
        {
            PlayerTypeInfo[] PlayerTypeInfos = {
                Trustful.Info,
                Cheater.Info,
                Copycat.Info,
                Grudger.Info,
                Detective.Info,
                Imitator.Info,
                Simpleton.Info,
                Randomized.Info
            };
            int i = 0;
            for (int y = 0; y < PopulatioTableLayoutPanel.ColumnCount; y++)
            {
                for (int x = 0; x < PopulatioTableLayoutPanel.RowCount; x++)
                {
                    Slider slider = new Slider(PlayerTypeInfos[i], _game.Population[i], _game.PopulationNumber);
                    _sliders.Add(slider);
                    slider.Scroll += Slider_Scroll;
                    PopulatioTableLayoutPanel.Controls.Add(slider.Table, x, y);
                    i++;
                }
            }
        }
        private void Slider_Scroll(object sender, System.EventArgs e)
        {
            ShutDownThread();
            Slider slider = sender as Slider;
            slider.Counter.Text = (sender as Slider).Value.ToString();
            _game.ControlPopulation(slider.Value, slider.TypeId);
            _game.Reset();
            UpdatePopulationSliders();
            DrawDeskPanel.Refresh();
        }
        private void UpdatePopulationSliders()
        {
            foreach (var slider in _sliders)
            {
                slider.UpdateValue(_game.Population[slider.TypeId]);
            }
        }

        private void DrawRuleSliders()
        {
            RuleSlider[] sliders =
            {
                new RuleSlider(_game.MinRoundsNumber, _game.MaxRoundsNumber, "Играть {} раундов за матч", _game.RoundsNumber, _game.SetRoundsNumber),
                new RuleSlider(_game.MinSelectionsNumber, _game.MaxSelectionsNumber, "После каждого турнира удалять {} худших игроков и копировать n лучших", _game.SelectionsNumber, _game.SetSelectionsNumber),
                new RuleSlider(_game.ExchangeMachine.MinMistakeChance, _game.ExchangeMachine.MaxMistakeChance, "В каждом раунде игрок делает ошибку с вероятностью {}%", (int)(_game.ExchangeMachine.MistakeChance*100), _game.SetMistakeChange),
            };

            for(int i = 0; i < RulesTableLayoutPanel.RowCount; i++)
            {
                sliders[i].Scroll += RulesSlider_Scroll;
                RulesTableLayoutPanel.Controls.Add(sliders[i].Table, 0, i);
            }
        }
        private void RulesSlider_Scroll(object sender, System.EventArgs e)
        {
            (sender as RuleSlider).UpdateValue();
        }

        private void FillCicle(Brush brush, float centerX, float centerY, float radius)
        {
            _g.FillEllipse(brush, centerX - radius, centerY - radius, radius * 2, radius * 2);
        }

        private void SetCoords()
        {
            _centerX = DrawDeskPanel.Width / 2;
            _centerY = DrawDeskPanel.Height / 2;
            _radius = DrawDeskPanel.Width / 2 - 80;
            _radius_outside = _radius + _small_radius;
        }

        private void DrawPayoffs()
        {
            PayoffUpDown[] _payoffs = {
                new PayoffUpDown(
                   _game.ExchangeMachine.MinPayoff,
                   _game.ExchangeMachine.MaxPayoff,
                   _game.ExchangeMachine.TrustTrustPayoff,
                   _game.ExchangeMachine.SetTrustTrustPayoff),
                new PayoffUpDown(
                   _game.ExchangeMachine.MinPayoff,
                   _game.ExchangeMachine.MaxPayoff,
                   _game.ExchangeMachine.TrustPayoff,
                   _game.ExchangeMachine.SetTrustPayoff),
                new PayoffUpDown(
                   _game.ExchangeMachine.MinPayoff,
                   _game.ExchangeMachine.MaxPayoff,
                   _game.ExchangeMachine.CheatPayoff,
                   _game.ExchangeMachine.SetCheatPayoff),
                new PayoffUpDown(
                   _game.ExchangeMachine.MinPayoff,
                   _game.ExchangeMachine.MaxPayoff,
                   _game.ExchangeMachine.CheatCheatPayoff,
                   _game.ExchangeMachine.SetCheatCheatPayoff)
            };

            foreach(var payoff in _payoffs)
            {
                payoff.ValueChanged += PayoffUpDown_ValueChanged;
            }

            _payoffs[1].Dock = DockStyle.Bottom;
            _payoffs[2].Dock = DockStyle.Top;

            TrustTrustLayoutPanel.Controls.Add(_payoffs[0], 0, 1);

            TrustCheatTableLayoutPanel.Controls.Add(_payoffs[1], 0, 0);
            TrustCheatTableLayoutPanel.Controls.Add(_payoffs[2], 0, 1);

            CheatCheatTableLayoutPanel.Controls.Add(_payoffs[3], 0, 1);
        }
        private void PayoffUpDown_ValueChanged(object sender, EventArgs e)
        {
            (sender as PayoffUpDown).UpdateValue();
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            ShutDownThread();
            _game.Step();
            DrawDeskPanel.Refresh();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ShutDownThread();
            _game.Reset();
            DrawDeskPanel.Refresh();
        }

        private void ShutDownThread()
        {
            if (!_thread_is_running)
                return;
            _thread_is_running = false;
            ResetStartButton();
        }
        private void ResetStartButton()
        {
            StartButton.Text = "Start";
            StartButton.Click += StartButton_Click;
            StartButton.Click -= StopButton_Click;
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            Thread loop_game = new Thread(LoopGame);
            _thread_is_running = true;
            loop_game.Start();

            StartButton.Text = "Stop";
            StartButton.Click -= StartButton_Click;
            StartButton.Click += StopButton_Click;
        }
        private void StopButton_Click(object sender, EventArgs e)
        {
            ShutDownThread();
        }

        private void LoopGame()
        {
           while(_thread_is_running)
            {
                _game.Step();
                DrawDeskPanel.Refresh();
                Thread.Sleep(200);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDownThread();
        }
    }
}
