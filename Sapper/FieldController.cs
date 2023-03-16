using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace Sapper
{
    public class FieldController : IDisposable
    {
        private Control _container;
        private Field _field;
        private readonly Timer _timer;
        private readonly Label _clockLabel;
        private readonly Label _minesLabel;

        private int _ticks = 0;
        private int _unopenedMinesCount;

        public int CellSize
        {
            get
            {
                var w = _container.Width;
                var h = _container.Height;
                var cellW = (int)(w / _field.Cols);
                var cellH = (int)(h / _field.Rows);
                return Math.Min(cellW, cellH);
            }
        }

        public FieldController(Control container, Field f, Timer timer, Label clockLabel, Label minesLabel)
        {
            _field = f;
            _timer = timer;
            _clockLabel = clockLabel;
            _minesLabel = minesLabel;
            _container = container;
            _unopenedMinesCount = _field.MineCount;
            _minesLabel.Text = _unopenedMinesCount.ToString();
            Create();
        }

        private void Create()
        {
            Rectangle r = GetRectangle();
            for (int i = 0; i < _field.Rows; i++)
            {
                for (int j = 0; j < _field.Cols; j++)
                {
                    CellView cv = new CellView(_field, i, j, this);
                    cv.Location = new Point(r.X + j * CellSize, r.Y + i * CellSize);
                    cv.Size = new Size(CellSize, CellSize);
                    cv.StateChanged += CvOnStateChanged;
                    cv.Start += CvOnStart;
                    cv.Name = $"c{i},{j}";
                    _container.Controls.Add(cv);
                }
            }
        }

        public void ResizeField()
        {
            Rectangle r = GetRectangle();
            for (int i = 0; i < _field.Rows; i++)
            {
                for (int j = 0; j < _field.Cols; j++)
                {
                    if (_container.Controls[i * _field.Cols + j] is CellView cv)
                    {
                        cv.Location = new Point(r.X + j * CellSize, r.Y + i * CellSize);
                        cv.Size = new Size(CellSize, CellSize);
                        cv.Invalidate();
                    }
                }
            }
        }

        private void CvOnStart()
        {
            _field.Mine();
            _timer.Interval = 1000;
            _ticks = 0;
            _timer.Tick += (sender, args) =>
            {
                ++_ticks;
                var minutes = _ticks / 60;
                var seconds = _ticks % 60;
                _clockLabel.Text = $"{minutes} : {seconds}";
            };
            _timer.Start();
        }

        private void CvOnStateChanged(StateType prevValue, CellView cell)
        {
            if (prevValue == StateType.Marked)
            {
                ++_unopenedMinesCount;
                _minesLabel.Text = _unopenedMinesCount.ToString();
            }
            if (cell.State == StateType.Marked)
            {
                --_unopenedMinesCount;
                _minesLabel.Text = _unopenedMinesCount.ToString();
            }

            if (cell.State == StateType.Opened)
            {
                if (_field[cell.Row, cell.Col])
                    EndGame(false);
                else
                {
                    if (_field.GetNeighboursMineCount(cell.Row, cell.Col) == 0)
                    {
                        var nbrs = GetNeghbours(cell);
                        nbrs.ForEach(c => c.State = StateType.Opened);
                    }

                    var endWithWin = true;
                    foreach (var containerControl in _container.Controls)
                        if (containerControl is CellView cv)
                        {
                            if (cv.State == StateType.Opened && _field[cv.Row, cv.Col])
                            {
                                EndGame(false);
                                return;
                            }

                            if (cv.State != StateType.Opened && !_field[cv.Row, cv.Col]) 
                                endWithWin = false;
                        }

                    if(endWithWin)
                        EndGame(true);
                }
            }
        }

        private void EndGame(bool win)
        {
            foreach (Control containerControl in _container.Controls)
            {
                if (containerControl is CellView cv)
                {
                    cv.State = StateType.Opened;
                }
            }

            _timer.Stop();
            
            if(!win) return;

            const string recordFilePath = "records.txt";
            var record = _ticks;
            if (File.Exists(recordFilePath))
            {
                var split = File.ReadAllText(recordFilePath).Split(':');
                var currentRecordsTicks = int.Parse(split[0]) * 60 + int.Parse(split[1]);
                if (record > currentRecordsTicks)
                    record = currentRecordsTicks;
            }

            File.WriteAllText(recordFilePath, $"{record / 60}:{record % 60}");
        }

        private Rectangle GetRectangle()
        {
            var r = new Rectangle();
            r.Width = CellSize * _field.Cols;
            r.Height = CellSize * _field.Rows;
            r.X = (_container.Width - r.Width) / 2;
            r.Y = (_container.Height - r.Height) / 2;
            return r;
        }

        public List<CellView> GetNeghbours(CellView cell)
        {
            var nbrs = new List<CellView>();
            for (int i = cell.Row - 1; i <= cell.Row + 1; i++)
            {
                if (i < 0 || i >= _field.Rows) continue;
                for (int j = cell.Col - 1; j <= cell.Col + 1; j++)
                {
                    if (j < 0 || j >= _field.Cols) continue;
                    if (i == cell.Row && j == cell.Col) continue;
                    var ctrl = _container.Controls.Find($"c{i},{j}", false)[0];
                    if (ctrl is CellView cvCtrl)
                    {
                        nbrs.Add(cvCtrl);
                    }
                }
            }
            return nbrs;
        }

        public void Dispose()
        {
            int cc = _container.Controls.Count;
            for (int i = 0; i < cc; i++)
                _container.Controls.RemoveAt(0);
        }
    }
}
