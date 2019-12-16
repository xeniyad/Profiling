namespace GameOfLife.Services
{
    public class NotOptimizedCellUpdater : CellUpdaterBase, ICellUpdater
    {
        public void Update(Field nextGenerationField, Field currentField)
        {
            nextGenerationField.DoForEachCell((row, column) =>
            {
                nextGenerationField.SetCell(
                    row,
                    column,
                    CalculateNextGenerationNotOptimized(row, column, nextGenerationField, currentField));  
            });
        }

        private Cell CalculateNextGenerationNotOptimized(int row, int column, Field field, Field currentField)    // UNOPTIMIZED
        {
            var cell = field.GetCell(row, column);
            var alive = cell.IsAlive;
            var count = CountNeighbors(row, column, currentField);

            if (alive && count < 2)
            {
                return new Cell(row, column, 0, false);
            }

            if (alive && (count == 2 || count == 3))
            {
                cell.Age++;
                return new Cell(row, column, cell.Age, true);
            }

            if (alive && count > 3)
            {
                return new Cell(row, column, 0, false);
            }

            if (!alive && count == 3)
            {
                return new Cell(row, column, 0, true);
            }

            return new Cell(row, column, 0, false);
        }
    }
}