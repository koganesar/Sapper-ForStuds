namespace Sapper
{
    public partial class MainForm : Form
    {
        private Field _f;
        private FieldController _fController;
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            if (_fController is not null)
                _fController.Dispose();
            _f = new Field(10, 15);
            _fController = new FieldController(panel1, _f);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            _fController.ResizeField();
        }
    }
}