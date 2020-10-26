﻿using System;
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
using PacmanTest.Properties;

namespace PacmanTest
{
    public partial class GameForm : Form
    {
        private readonly Pacman _pacman;
        private readonly int _delta;
        private Keys _key;
        private int _startAngle;
        private bool _isEating;

        public GameForm()
        {
            InitializeComponent();
            _pacman = new Pacman(35, 35);
            this.BackColor = Color.Black;
            this.ClientSize = new System.Drawing.Size(570, 660);
            _key = Keys.Right;
            _delta = 10;
            _startAngle = 45;
            _isEating = false;
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
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            bool cango = _pacman.CanGo(_key);
            switch (_key)
            {
                case Keys.Right when cango:
                    if (_pacman.X + 5 + _pacman.Diameter < this.ClientSize.Width - 30)
                        _pacman.X += _delta;
                    break;
                case Keys.Up when cango:
                    if (_pacman.Y > 35)
                        _pacman.Y -= _delta;
                    break;
                case Keys.Left when cango:
                    if (_pacman.X > 35)
                        _pacman.X -= _delta;
                    break;
                case Keys.Down when cango:
                    if (_pacman.Y + 5 + _pacman.Diameter < this.ClientSize.Height - 30)
                        _pacman.Y += _delta;
                    break;
            }
            _isEating = !_isEating;
            pcb.Invalidate();
        }

        private void pcb_Paint(object sender, PaintEventArgs e)
        {
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
                    else if (Pacman.Map[j, i] == 3)
                    {
                        Image image = Resources.pixel_ghost_red_128x128;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 2)
                    {
                        Image image = Resources.pixel_ghost_orange_128x128;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 5)
                    {
                        Image image = Resources.pixel_ghost_blue_128x128;
                        e.Graphics.DrawImage(image, rect);
                    }
                    else if (Pacman.Map[j, i] == 6)
                    {
                        Image image = Resources.pixel_ghost_pink_128x128;
                        e.Graphics.DrawImage(image, rect);
                    }
                }
            }
            if (_isEating)
                e.Graphics.FillPie(Brushes.Yellow, new Rectangle(_pacman.X, _pacman.Y, _pacman.Diameter, _pacman.Diameter), _startAngle, 270);
            else
                e.Graphics.FillEllipse(Brushes.Yellow, new Rectangle(_pacman.X, _pacman.Y, _pacman.Diameter, _pacman.Diameter));
        }
    }
}
