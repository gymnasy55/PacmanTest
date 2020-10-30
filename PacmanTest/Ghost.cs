using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PacmanTest
{
    enum GhostDirection
    {
        Left = -1, Right = 1, Up = -2, Down = 2
    }

    enum GhostValue
    {
        Red, Orange, Blue, Pink, Chasedb //ILLIApidor
    }

    class Ghost
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Diameter { get; } = 20;
        public GhostDirection Direction;
        public GhostValue Value;

        public Ghost()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Ghost(int x, int y, GhostDirection direction, GhostValue value)
        {
            this.X = x;
            this.Y = y;
            this.Direction = direction;
            this.Value = value;
        }

        public Point Get_coord() => new Point((int)Math.Ceiling((this.X + 1) / 30.0), (int)Math.Ceiling((this.Y + 1) / 30.0));

        private bool isWall(int X, int Y) => Pacman.Map[Y - 1, X - 1] == (int)Item.Wall;

        public GhostDirection ChangeDirection()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks + (int)this.Value);
            List<GhostDirection> list = new List<GhostDirection>();
            Point coord = Get_coord();
            if ((this.X + 15) % 15 == 0 && (this.X + 15) % 30 != 0 && (this.Y + 15) % 15 == 0 && (this.Y + 15) % 30 != 0)
            {
                if (!isWall(coord.X - 1, coord.Y) && -(int)this.Direction != (int)GhostDirection.Left)
                    list.Add(GhostDirection.Left);
                if (!isWall(coord.X + 1, coord.Y) && -(int)this.Direction != (int)GhostDirection.Right)
                    list.Add(GhostDirection.Right);
                if (!isWall(coord.X, coord.Y - 1) && -(int)this.Direction != (int)GhostDirection.Up)
                    list.Add(GhostDirection.Up);
                if (!isWall(coord.X, coord.Y + 1) && -(int)this.Direction != (int)GhostDirection.Down)
                    list.Add(GhostDirection.Down);
                if (list.Count == 0)
                    return (GhostDirection)(-(int)this.Direction);
                return list[rnd.Next(0, list.Count)];
            }
            else
            {
                return this.Direction;
            }
        }

        public bool CanGo()
        {
            Point coord = this.Get_coord();
            switch (this.Direction)
            {
                case GhostDirection.Right:
                    if (!isWall(coord.X + 1, coord.Y))
                    {
                        this.Y = (coord.Y - 1) * 30 + 15 - 10;
                        return true;
                    }
                    else if ((this.X + 10) % 15 == 0 && (this.X + 10) % 30 != 0)
                        return false;
                    else
                        return true;
                case GhostDirection.Left:
                    if (!isWall(coord.X - 1, coord.Y))
                    {
                        this.Y = (coord.Y - 1) * 30 + 15 - 10;
                        return true;
                    }
                    else if ((this.X + 10) % 15 == 0 && (this.X + 10) % 30 != 0)
                        return false;
                    else
                        return true;
                case GhostDirection.Up:
                    if (!isWall(coord.X, coord.Y - 1))
                    {
                        this.X = (coord.X - 1) * 30 + 15 - 10;
                        return true;
                    }
                    else if ((this.Y + 10) % 15 == 0 && (this.Y + 10) % 30 != 0)
                        return false;
                    else
                        return true;
                case GhostDirection.Down:
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

        public void MakeStep()
        {
            this.Direction = ChangeDirection();
            switch (this.Direction)
            {
                case GhostDirection.Right:
                    this.X += 10;
                    break;
                case GhostDirection.Left:
                    this.X -= 10;
                    break;
                case GhostDirection.Up:
                    this.Y -= 10;
                    break;
                case GhostDirection.Down:
                    this.Y += 10;
                    break;

            }
        }
    }
}
