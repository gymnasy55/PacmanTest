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
        private readonly int _delta;
        private Keys _key;
        private int _start_angle;

        public GameForm()
        {
            InitializeComponent();
            _pacman = new Pacman(2, 3);
            this.BackColor = Color.Black;
            this.ClientSize = new System.Drawing.Size(502, 502);
            _key = Keys.Right;
            _delta = 2;
            _start_angle = 45;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    _key = Keys.Right;
                    _start_angle = 45;
                    break;
                case Keys.Up:
                    _key = Keys.Up;
                    _start_angle = 315;
                    break;
                case Keys.Left:
                    _key = Keys.Left;
                    _start_angle = 225;
                    break;
                case Keys.Down:
                    _key = Keys.Down;
                    _start_angle = 135;
                    break;
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            switch (_key)
            {
                case Keys.Right:
                    //if(((Bitmap)pcb.Image).GetPixel(_pacman.X+_delta, _pacman.Y) != Color.Blue)
                    if (_pacman.X + _pacman.Diameter < this.ClientSize.Width - 2)
                        _pacman.X += _delta;
                    break;
                case Keys.Up:
                    if(_pacman.Y > 3)
                        _pacman.Y -= _delta;
                    break;
                case Keys.Left:
                    if(_pacman.X > 2)
                        _pacman.X -= _delta;
                    break;
                case Keys.Down:
                    if(_pacman.Y +  _pacman.Diameter < this.ClientSize.Height - 3)
                        _pacman.Y += _delta;
                    break;
            }
            pcb.Invalidate();
        }

        private void pcb_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(0, 1, this.ClientSize.Width - 1, this.ClientSize.Height - 2));
            e.Graphics.FillPie(Brushes.Yellow, new Rectangle(_pacman.X, _pacman.Y, _pacman.Diameter, _pacman.Diameter), _start_angle, 270);
        }
    }
}
