using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PacmanTest
{
    enum Item
    {
        Empty = 0, Candy = 1, OrangeGhost = 2, RedGhost = 3, BigCandy = 4, PacMan = 7, Wall = 8, BlueGhost = 5, PinkGhost = 6, ChaseGhost = 9,
    }

    internal class Pacman
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Diameter { get; } = 20;

        public static int[,] Map { get; set; } =
        {
            {8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8},
            {8, 1, 1, 1, 1, 1, 1, 1, 1, 8, 1, 1, 1, 1, 1, 1, 1, 1, 8},
            {8, 4, 8, 8, 1, 8, 8, 8, 1, 8, 1, 8, 8, 8, 1, 8, 8, 4, 8},
            {8, 1, 8, 8, 1, 8, 8, 8, 1, 8, 1, 8, 8, 8, 1, 8, 8, 1, 8},
            {8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 8},
            {8, 1, 8, 8, 1, 8, 1, 8, 8, 8, 8, 8, 1, 8, 1, 8, 8, 1, 8},
            {8, 1, 1, 1, 1, 8, 1, 1, 1, 8, 1, 1, 1, 8, 1, 1, 1, 1, 8},
            {8, 8, 8, 8, 1, 8, 8, 8, 1, 8, 1, 8, 8, 8, 1, 8, 8, 8, 8},
            {0, 0, 0, 8, 1, 8, 1, 1, 1, 1, 1, 1, 1, 8, 1, 8, 0, 0, 0},
            {8, 8, 8, 8, 1, 8, 1, 8, 8, 3, 8, 8, 1, 8, 1, 8, 8, 8, 8},
            {0, 0, 0, 0, 1, 1, 1, 8, 5, 2, 6, 8, 1, 1, 1, 0, 0, 0, 0},
            {8, 8, 8, 8, 1, 8, 1, 8, 8, 8, 8, 8, 1, 8, 1, 8, 8, 8, 8},
            {0, 0, 0, 8, 1, 8, 1, 1, 1, 7, 1, 1, 1, 8, 1, 8, 0, 0, 0},
            {8, 8, 8, 8, 1, 8, 1, 8, 8, 8, 8, 8, 1, 8, 1, 8, 8, 8, 8},
            {8, 1, 1, 1, 1, 1, 1, 1, 1, 8, 1, 1, 1, 1, 1, 1, 1, 1, 8},
            {8, 1, 8, 8, 1, 8, 8, 8, 1, 8, 1, 8, 8, 8, 1, 8, 8, 1, 8},
            {8, 4, 1, 8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 8, 1, 4, 8},
            {8, 8, 1, 8, 1, 8, 1, 8, 8, 8, 8, 8, 1, 8, 1, 8, 1, 8, 8},
            {8, 1, 1, 1, 1, 8, 1, 1, 1, 8, 1, 1, 1, 8, 1, 1, 1, 1, 8},
            {8, 1, 8, 8, 8, 8, 8, 8, 1, 8, 1, 8, 8, 8, 8, 8, 8, 1, 8},
            {8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 8},
            {8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8}
        };

		public Pacman()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Pacman(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point Get_coord() => new Point((int)Math.Ceiling((this.X + 10) / 30.0), (int)Math.Ceiling((this.Y + 10) / 30.0));

        private bool isWall(int X, int Y) => Map[Y - 1, X - 1] == (int)Item.Wall;

        public bool CanGo(Keys key)
        {
            Point coord = this.Get_coord();
            switch (key)
            {
                case Keys.Right:
                    if (!isWall(coord.X + 1, coord.Y))
                    {
                        this.Y = (coord.Y - 1) * 30 + 15 - 10;
                        return true;
                    }
                    else if ((this.X + 10) % 15 == 0 && (this.X + 10) % 30 != 0)
                        return false;
                    else
                        return true;
                case Keys.Left:
                    if (!isWall(coord.X - 1, coord.Y))
                    {
                        this.Y = (coord.Y - 1) * 30 + 15 - 10;
                        return true;
                    }
                    else if ((this.X + 10) % 15 == 0 && (this.X + 10) % 30 != 0)
                        return false;
                    else
                        return true;
                case Keys.Up:
                    if (!isWall(coord.X, coord.Y - 1))
                    {
                        this.X = (coord.X - 1) * 30 + 15 - 10;
                        return true;
                    }
                    else if ((this.Y + 10) % 15 == 0 && (this.Y + 10) % 30 != 0)
                        return false;
                    else
                        return true;
                case Keys.Down:
                    if (!isWall(coord.X, coord.Y + 1))
                    {
                        this.X = (coord.X - 1) * 30 + 15 - 10;
                        return true;
                    }
                    else if ((this.Y + 10) % 15 == 0 && (this.Y + 10) % 30 != 0)
                        return false;
                    else
                        return true;
            }
            return false;

        }
    }
}
