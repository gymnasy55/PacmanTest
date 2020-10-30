using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using PacmanTest.Properties;

namespace PacmanTest
{
    public partial class GameForm : Form
    {
        private Pacman _pacman;
        private readonly List<Ghost> _ghosts;
        private readonly int _delta;
        private Keys _key;
        private int _startAngle;
        private bool _isEating;
        private int _score;
        private readonly PrivateFontCollection _privateFonts;
        private readonly int _overallDots;
        private int _playerDots;
        private bool _isWin;
        private bool _isLoose;
        private readonly SoundPlayer _soundPlayer;
        //private int _lives;
        //private int _counter;

        public GameForm()
        {
            InitializeComponent();
            _pacman = new Pacman(275, 365);
            this.BackColor = Color.Black;
            this.ClientSize = new System.Drawing.Size(570, 690);
            _key = Keys.Right;
            _delta = 10;
            _startAngle = 45;
            _isEating = _isWin = _isLoose = false;
            _ghosts = new List<Ghost>
            {
                new Ghost(9 * 30, 9 * 30, GhostDirection.Down, GhostValue.Red),
                new Ghost(9 * 30, 10 * 30, GhostDirection.Up, GhostValue.Orange),
                new Ghost(8 * 30, 10 * 30, GhostDirection.Right, GhostValue.Blue),
                new Ghost(10 * 30, 10 * 30, GhostDirection.Left, GhostValue.Pink)
            };
            _score = _playerDots = 0;
            _privateFonts = new PrivateFontCollection(); ;
            _overallDots = 182;
            _soundPlayer = new SoundPlayer(Resources.game);
            //_lives = _counter = 0;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    _key = Keys.Right;
                    _startAngle = 45;
                    break;
                case Keys.Up:
                    _key = Keys.Up;
                    _startAngle = 315;
                    break;
                case Keys.Left:
                    _key = Keys.Left;
                    _startAngle = 225;
                    break;
                case Keys.Down:
                    _key = Keys.Down;
                    _startAngle = 135;
                    break;
                case Keys.N when _isLoose || _isWin:
                    Application.Restart();
                    break;
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
                _ghosts[i].MakeStep();
            bool canGo = _pacman.CanGo(_key);
            switch (_key)
            {
                case Keys.Right when canGo:
                    if (_pacman.X + 5 + _pacman.Diameter < this.ClientSize.Width - 30)
                        _pacman.X += _delta;
                    break;
                case Keys.Up when canGo:
                    if (_pacman.Y > 35)
                        _pacman.Y -= _delta;
                    break;
                case Keys.Left when canGo:
                    if (_pacman.X > 35)
                        _pacman.X -= _delta;
                    break;
                case Keys.Down when canGo:
                    if (_pacman.Y + 5 + _pacman.Diameter < this.ClientSize.Height - 30)
                        _pacman.Y += _delta;
                    break;
            }

            if (Pacman.Map[_pacman.Get_coord().Y - 1, _pacman.Get_coord().X - 1] == (int)Item.Candy)
            {
                _score += 10;
                _playerDots++;
                Pacman.Map[_pacman.Get_coord().Y - 1, _pacman.Get_coord().X - 1] = (int)Item.Empty;
            }
            else if (Pacman.Map[_pacman.Get_coord().Y - 1, _pacman.Get_coord().X - 1] == (int)Item.BigCandy)
            {
                _score += 50;
                _playerDots++;
                Pacman.Map[_pacman.Get_coord().Y - 1, _pacman.Get_coord().X - 1] = (int)Item.Empty;
            }

            if (_playerDots == _overallDots)
                _isWin = true;

            for (int i = 0; i < 4; i++)
                if (_ghosts[i].Get_coord() == _pacman.Get_coord())
                    _isLoose = true;

            _isEating = !_isEating;
            pcb.Invalidate();
        }

        private void pcb_Paint(object sender, PaintEventArgs e)
        {
            void ShowEnd(string message)
            {
                tmrGame.Enabled = false;
                Rectangle rect = new Rectangle(1, this.ClientSize.Height / 2 - 50, this.ClientSize.Width - 2, 100);
                e.Graphics.DrawRectangle(new Pen(Color.White, 2), rect);
                rect.Inflate(-2, -2);
                e.Graphics.FillRectangle(Brushes.Black, rect);
                e.Graphics.DrawString(message, new Font(_privateFonts.Families[0], 30), Brushes.Yellow, this.ClientSize.Width / 16 * 5, this.ClientSize.Height / 2 - 40);
                e.Graphics.DrawString("Press N to restart", new Font(_privateFonts.Families[0], 22), Brushes.Yellow, this.ClientSize.Width / 4, this.ClientSize.Height / 2);
                _soundPlayer.Stop();
            }

            //void NewRound()
            //{
            //    tmrGame.Enabled = false;
            //    tmrRestart.Enabled = true;
            //    if (_counter >= 10)
            //    {
            //        SoundPlayer soundPlayer = new SoundPlayer(Resources.death);
            //        soundPlayer.PlaySync();
            //        _pacman = new Pacman(275, 690);
            //        _ghosts[0] = new Ghost(9 * 30, 9 * 30, GhostDirection.Down, GhostValue.Red);
            //        _ghosts[1] = new Ghost(9 * 30, 10 * 30, GhostDirection.Down, GhostValue.Red);
            //        _ghosts[2] = new Ghost(8 * 30, 10 * 30, GhostDirection.Down, GhostValue.Red);
            //        _ghosts[3] = new Ghost(10 * 30, 10 * 30, GhostDirection.Down, GhostValue.Red);
            //        _counter = 0;
            //        _lives--;
            //    }
            //    tmrRestart.Enabled = false;
            //    tmrGame.Enabled = true;
            //}

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 22; j++)
                {
                    Rectangle rect = new Rectangle(i * 30, j * 30, 30, 30);
                    if (Pacman.Map[j, i] == 8)
                    {
                        e.Graphics.DrawRectangle(Pens.DarkSlateGray, rect);
                        rect.Inflate(-1, -1);
                        e.Graphics.FillRectangle(Brushes.Blue, rect);
                    }
                    else if (Pacman.Map[j, i] == 1)
                    {
                        rect.Inflate(-13, -13);
                        e.Graphics.FillEllipse(Brushes.White, rect);
                    }
                    else if (Pacman.Map[j, i] == 4)
                    {
                        rect.Inflate(-10, -10);
                        e.Graphics.FillEllipse(Brushes.White, rect);
                    }
                    else if (Pacman.Map[j, i] == 3 && _ghosts[0].Direction == GhostDirection.Left)
                    {
                        Image image = Resources.pixel_ghost_red_left_128x128;
                        rect.X = _ghosts[0].X; rect.Y = _ghosts[0].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 3 && _ghosts[0].Direction == GhostDirection.Right)
                    {
                        Image image = Resources.pixel_ghost_red_right_128x128;
                        rect.X = _ghosts[0].X; rect.Y = _ghosts[0].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 3 && _ghosts[0].Direction == GhostDirection.Up)
                    {
                        Image image = Resources.pixel_ghost_red_up_128x128;
                        rect.X = _ghosts[0].X; rect.Y = _ghosts[0].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 3 && _ghosts[0].Direction == GhostDirection.Down)
                    {
                        Image image = Resources.pixel_ghost_red_down_128x128;
                        rect.X = _ghosts[0].X; rect.Y = _ghosts[0].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 2 && _ghosts[1].Direction == GhostDirection.Left)
                    {
                        Image image = Resources.pixel_ghost_orange_left_128x128;
                        rect.X = _ghosts[1].X; rect.Y = _ghosts[1].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 2 && _ghosts[1].Direction == GhostDirection.Right)
                    {
                        Image image = Resources.pixel_ghost_orange_right_128x128;
                        rect.X = _ghosts[1].X; rect.Y = _ghosts[1].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 2 && _ghosts[1].Direction == GhostDirection.Up)
                    {
                        Image image = Resources.pixel_ghost_orange_up_128x128;
                        rect.X = _ghosts[1].X; rect.Y = _ghosts[1].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 2 && _ghosts[1].Direction == GhostDirection.Down)
                    {
                        Image image = Resources.pixel_ghost_orange_down_128x128;
                        rect.X = _ghosts[1].X; rect.Y = _ghosts[1].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 5 && _ghosts[2].Direction == GhostDirection.Left)
                    {
                        Image image = Resources.pixel_ghost_blue_left_128x128;
                        rect.X = _ghosts[2].X; rect.Y = _ghosts[2].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 5 && _ghosts[2].Direction == GhostDirection.Right)
                    {
                        Image image = Resources.pixel_ghost_blue_right_128x128;
                        rect.X = _ghosts[2].X; rect.Y = _ghosts[2].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 5 && _ghosts[2].Direction == GhostDirection.Up)
                    {
                        Image image = Resources.pixel_ghost_blue_up_128x128;
                        rect.X = _ghosts[2].X; rect.Y = _ghosts[2].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 5 && _ghosts[2].Direction == GhostDirection.Down)
                    {
                        Image image = Resources.pixel_ghost_blue_down_128x128;
                        rect.X = _ghosts[2].X; rect.Y = _ghosts[2].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 6 && _ghosts[3].Direction == GhostDirection.Left)
                    {
                        Image image = Resources.pixel_ghost_pink_left_128x128;
                        rect.X = _ghosts[3].X; rect.Y = _ghosts[3].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 6 && _ghosts[3].Direction == GhostDirection.Right)
                    {
                        Image image = Resources.pixel_ghost_pink_right_128x128;
                        rect.X = _ghosts[3].X; rect.Y = _ghosts[3].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 6 && _ghosts[3].Direction == GhostDirection.Up)
                    {
                        Image image = Resources.pixel_ghost_pink_up_128x128;
                        rect.X = _ghosts[3].X; rect.Y = _ghosts[3].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 6 && _ghosts[3].Direction == GhostDirection.Down)
                    {
                        Image image = Resources.pixel_ghost_pink_down_128x128;
                        rect.X = _ghosts[3].X; rect.Y = _ghosts[3].Y;
                        e.Graphics.DrawImage(image, rect);
                    }
                }
            }

            if (_isEating)
                e.Graphics.FillPie(Brushes.Yellow, new Rectangle(_pacman.X, _pacman.Y, _pacman.Diameter, _pacman.Diameter), _startAngle, 270);
            else
                e.Graphics.FillEllipse(Brushes.Yellow, new Rectangle(_pacman.X, _pacman.Y, _pacman.Diameter, _pacman.Diameter));

            e.Graphics.DrawString($"Score:{_score}", new Font(_privateFonts.Families[0], 22), Brushes.Yellow, 1, 22 * 30);

            if (_isWin)
                ShowEnd("YOU WIN!");
            else if(_isLoose)
                ShowEnd("YOU LOOSE!");
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            using (MemoryStream fontStream = new MemoryStream(Resources.crackman))
            {
                IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
                byte[] fontData = new byte[fontStream.Length];
                fontStream.Read(fontData, 0, (int)fontStream.Length);
                Marshal.Copy(fontData, 0, data, (int)fontStream.Length);
                _privateFonts.AddMemoryFont(data, (int)fontStream.Length);
                fontStream.Close();
                Marshal.FreeCoTaskMem(data);
            }
            using (MemoryStream fontStream = new MemoryStream(Resources.crackman_back))
            {
                IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
                byte[] fontData = new byte[fontStream.Length];
                fontStream.Read(fontData, 0, (int)fontStream.Length);
                Marshal.Copy(fontData, 0, data, (int)fontStream.Length);
                _privateFonts.AddMemoryFont(data, (int)fontStream.Length);
                fontStream.Close();
                Marshal.FreeCoTaskMem(data);
            }
            _soundPlayer.PlayLooping();
        }

        //private void tmrRestart_Tick(object sender, EventArgs e)
        //{
        //    _counter++;
        //}
    }
}