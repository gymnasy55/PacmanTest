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

        public Pacman(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
