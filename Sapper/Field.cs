using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapper
{
    public class Field
    {
        private int _cols;
        private int _rows;

        public int Cols
        {
            get => _cols;
            set => _cols = Math.Min(30, Math.Max(5, value));
            
        }

        public int Rows
        {
            get => _rows;
            set
            {
                _rows = Math.Min(30, Math.Max(5, value));
            }
        }

        public int MineCount => (int)Math.Ceiling(Rows * Cols * 0.2);

        private bool[,] _mineField;
        private Random _r = new Random((int)DateTime.Now.Ticks);

        public bool this[int row, int col]
        {
            get
            {
                if (row < 0 || col < 0 || row >= Rows || col >= Cols) return false;
                return _mineField[row, col];
            }
        }
        public Field(int w, int h)
        {
            Cols = w;
            Rows = h;
            _mineField = new bool[Rows, Cols];
        }

        public void Mine()
        {
            for (int i = 0; i < MineCount; i++)
            {
                int mr;
                int mc;
                do
                {
                    mr = _r.Next(Rows);
                    mc = _r.Next(Cols);
                } while (_mineField[mr, mc]);

                _mineField[mr, mc] = true;
            }
        }

        public int GetNeighboursMineCount(int row, int col)
        {
            int cnt = 0;
            for (int i = row-1; i <= row+1; i++)
            {
                if (i < 0 || i >= Rows) continue;
                for (int j = col-1; j <= col+1; j++)
                {
                    if (j < 0 || j >= Cols || (i == row && j == col)) continue;
                    if (_mineField[i, j]) cnt++;
                }
            }
            return cnt;
        }
    }
}
