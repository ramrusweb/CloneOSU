using System;
using System.Drawing;
using System.Windows.Forms;

namespace CloneOSU
{
    public partial class CloneOSU : Form
    {
        public Bitmap HandlerTexture = Resource1.Handler,
                      TargetTexture = Resource1.Target;
        private Point _targetPosition = new Point(300, 300);
        private Point _direction = Point.Empty;

        public CloneOSU()
        {
            InitializeComponent();
            TimerTick();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);

            UpdateStyles();
        }

        // Метод перерисовки.
        void CloneOSU_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            var localPosition = PointToClient(Cursor.Position);

            _targetPosition.X += _direction.X * 10;
            _targetPosition.Y += _direction.Y * 10;

            // Запрет выхода Target за пределы окна.
            if (_targetPosition.X < 0 || _targetPosition.X > 500)
                _direction.X = 200;
            if (_targetPosition.Y < 0 || _targetPosition.Y > 500)
                _direction.Y = 200;

            var handlerRect = new Rectangle(localPosition.X - 50, localPosition.Y - 50, 100, 100);
            var targetRect = new Rectangle(_targetPosition.X - 50, _targetPosition.Y - 50, 100, 100);

            g.DrawImage(HandlerTexture, handlerRect);
            g.DrawImage(TargetTexture, targetRect);
        }

        // Таймертик.
        void TimerTick()
        {
            // Обновление.
            timer.Tick += (s, a) => { Refresh(); };
            // Случайный интервал.
            timer1.Tick += (s, a) =>
            {
                var r = new Random();
                timer1.Interval = r.Next(25, 1000);
                _direction.X = r.Next(-1, 2);
                _direction.Y = r.Next(-1, 2);
            };
        }
    }
}
