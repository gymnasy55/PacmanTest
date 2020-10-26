using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanTest
{
    enum GhostDirection
    {
        Left, Right, Up, Down
    }

    enum GhostValue
    {
        Red, Orange, Blue, Pink
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
    }
}
