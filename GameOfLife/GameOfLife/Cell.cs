using System.Windows.Media;

namespace GameOfLife
{
    public class Cell
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

        public Brush GetBrushColor()
        {
            return IsAlive
                ? (CheckIsFirstLife()
                    ? Brushes.White
                    : Brushes.DarkGray)
                : Brushes.Gray;
        }

        public bool CheckIsFirstLife() => Age < 2;

        public static Cell CreateNew(int i, int j)
        {
            return new Cell(i, j, 0, false);
        }
    }
}