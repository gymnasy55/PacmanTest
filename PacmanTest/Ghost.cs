using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanTest
{
    class Ghost
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Diameter { get; } = 20;

        public Ghost()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Ghost(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
