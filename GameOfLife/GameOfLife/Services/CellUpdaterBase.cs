namespace GameOfLife.Services
{
    public abstract class CellUpdaterBase
    {
        protected int CountNeighbors(int row, int column, Field currentField)
        {
            int count = 0;

            if (row != currentField.SizeX - 1 && currentField.GetCell(row + 1, column).IsAlive)
            {
                count++;
            }

            if (row != currentField.SizeX - 1 && column != currentField.SizeY - 1 && currentField.GetCell(row + 1, column + 1).IsAlive)
            {
                count++;
            }

            if (column != currentField.SizeY - 1 && currentField.GetCell(row, column + 1).IsAlive)
            {
                count++;
            }

            if (row != 0 && column != currentField.SizeY - 1 && currentField.GetCell(row - 1, column + 1).IsAlive)
            {
                count++;
            }

            if (row != 0 && currentField.GetCell(row - 1, column).IsAlive)
            {
                count++;
            }

            if (row != 0 && column != 0 && currentField.GetCell(row - 1, column - 1).IsAlive)
            {
                count++;
            }

            if (column != 0 && currentField.GetCell(row, column - 1).IsAlive)
            {
                count++;
            }

            if (row != currentField.SizeX - 1 && column != 0 && currentField.GetCell(row + 1, column - 1).IsAlive)
            {
                count++;
            }

            return count;
        }
    }
}