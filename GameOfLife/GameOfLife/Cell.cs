namespace GameOfLife
{
    internal class Cell
    {
        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public int Age { get; set; }

        public bool IsAlive { get; set; }

        public Cell(int row, int column, int age, bool alive)
        {
            const int positionRatio = 5;

            PositionX = row * positionRatio;
            PositionY = column * positionRatio;
            Age = age;
            IsAlive = alive;
        }
    }
}