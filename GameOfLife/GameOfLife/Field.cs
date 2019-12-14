using System;

namespace GameOfLife
{
    public class Field
    {
        private readonly Cell[,] _cells;

        public int SizeX { get; }

        public int SizeY { get; }

        public Field(int sizeX, int sizeY)
        {
            if (sizeX == default || sizeY == default)
            {
                throw new ArgumentException("You should not pass zero as a size value");
            }

            SizeX = sizeX;
            SizeY = sizeY;

            _cells = new Cell[sizeX, sizeY];

            Clear();
        }

        public void Clear()
        {
            DoForEachCell((i, j) =>
            {
                _cells[i, j] = Cell.CreateNew(i, j);
            });
        }

        public Cell GetCell(int row, int column)
        {
            ValidateSizes(row, column);

            return _cells[row, column];
        }

        public void SetCell(int row, int column, Cell cell)
        {
            ValidateSizes(row, column);

            _cells[row, column] = cell ?? throw new ArgumentNullException(nameof(cell));
        }

        public void DoForEachCell(Action<int, int> action)
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    action(i, j);
                }
            }
        }

        public void DoForEachCell(Action<int, int, Cell> action)
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    action(i, j, _cells[i, j]);
                }
            }
        }

        private void ValidateSizes(int row, int column)
        {
            if (row > SizeX || column > SizeY)
            {
                throw new IndexOutOfRangeException("You have tried to get item out of range");
            }
        }
    }
}