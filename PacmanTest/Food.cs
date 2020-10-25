using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanTest
{
    internal class Food
    {
        public int Value { get; set; }
        public int Diameter { get; set; }

        public Food()
        {
            this.Value = 0;
            this.Diameter = 5;
        }

        public Food(int value, int diameter)
        {
            this.Value = value;
            this.Diameter = diameter;
        }
    }
}
