using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanTest
{
    public partial class GameForm : Form
    {
        private readonly Pacman _pacman;
        private Keys _key;
        int delta;

        public GameForm()
        {
            InitializeComponent();
            _pacman = new Pacman();
            this.BackColor = Color.Black;
            this.ClientSize = new System.Drawing.Size(402, 402);
            _key = Keys.Right;
            delta = 2;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    _key = Keys.Right;
                    break;
                case Keys.Up:
                    _key = Keys.Up;
                    break;
                case Keys.Left:
                    _key = Keys.Left;
                    break;
                case Keys.Down:
                    _key = Keys.Down;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (_key)
            {
                case Keys.Right:
                    _pacman.X += delta;
                    break;
                case Keys.Up:
                    _pacman.Y -= delta;
                    break;
                case Keys.Left:
                    _pacman.X -= delta;
                    break;
                case Keys.Down:
                    _pacman.Y += delta;
                    break;
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(1, 1, 400, 400));
            e.Graphics.FillEllipse(Brushes.Yellow, _pacman.X, _pacman.Y, _pacman.Diameter, _pacman.Diameter);
        }
    }
}
