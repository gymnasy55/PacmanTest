using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PacmanTest
{
    internal class Pacman
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Diameter { get; } = 20; 

        public Pacman()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Pacman(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
