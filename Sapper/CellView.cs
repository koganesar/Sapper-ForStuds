namespace Sapper;
public enum StateType
{
    Closed, Opened, Marked
}

public delegate void ChangeStateDelegate(CellView cell);
public delegate void StartDelegate();

public class CellView : Control
{
    private Field _f;
    private int _row;
    private int _col;
    private static bool firstClick = true;

    public int Row => _row;
    public int Col => _col;
    private bool _content => _f[_row, _col];
    private StateType _state = StateType.Closed;

    public StateType State
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                if (value != StateType.Opened || _state != StateType.Marked)
                {
                    if (_state != StateType.Opened)
                    {
                        _state = value;
                        Refresh();
                    }

                    StateChanged(this);
                }
            }
        }
    }

    public event ChangeStateDelegate StateChanged;
    public event StartDelegate Start;
    public CellView(Field f, int row, int col)
    {
        _f = f;
        _row = row;
        _col = col;
        firstClick = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.Clear(Color.LightGray);
        switch (_state)
        {
            case StateType.Closed:
            {
                DrawClosed(e.Graphics);
                break;
            }
            case StateType.Opened:
            {
                DrawOpened(e.Graphics);
                break;
            }
            case StateType.Marked:
            {
                DrawClosed(e.Graphics);
                DrawMark(e.Graphics);
                break;
            }
        }
    }

    private void DrawMark(Graphics g)
    {
        Brush b = new SolidBrush(Color.Blue);
        g.FillEllipse(b, 3, 3, Width-7, Height-7);
    }
    private void DrawOpened(Graphics g)
    {
        var p = new Pen(Color.Black);
        g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
        if (!_content)
        {
            DrawNeghbours(g);
        }
        else
        {
            DrawDeath(g);
        }
    }

    private void DrawClosed(Graphics g)
    {
        var p1 = new Pen(Color.WhiteSmoke, 4);
        var p2 = new Pen(Color.SlateGray, 4);
        g.DrawLine(p1, 1, 1, 1, Height - 1);
        g.DrawLine(p1, 1, 1, Width - 1, 1);
        g.DrawLine(p2, Width - 1, Height - 1, 1, Height - 1);
        g.DrawLine(p2, Width - 1, Height - 1, Width - 1, 1);
    }

    private void DrawDeath(Graphics g)
    {
        var p = new Pen(Color.Red, 3);
        g.DrawLine(p, 1, 1, Width-1, Height-1);
        g.DrawLine(p, 1, Height-1, Width-1, 1);
    }

    private void DrawNeghbours(Graphics g)
    {
        var fnt = new Font(FontFamily.GenericMonospace,
            14, FontStyle.Bold);
        var brush = new SolidBrush(Color.BlueViolet);
        var val = _f.GetNeighboursMineCount(_row, _col);
        if (val > 0)
        {
            g.DrawString(
                val.ToString(),
                fnt,
                brush,
                0,
                0);
        }
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        if (firstClick)
        {
            firstClick = false;
            Start();
        }
        if (e.Button == MouseButtons.Left)
            State = StateType.Opened;
        if (e.Button == MouseButtons.Right)
        {
            if (State == StateType.Marked)
                State = StateType.Closed;
            else if (State == StateType.Closed)
                State = StateType.Marked;
        }
    }
}