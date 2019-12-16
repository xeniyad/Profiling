namespace GameOfLife.Services
{
    public class OptimizedWayCellUpdater : CellUpdaterBase, ICellUpdater
    {
        public void Update(Field nextGenerationField, Field currentField)
        {
            nextGenerationField.DoForEachCell((row, column) =>
            {
                CalculateNextGenerationOptimized(row, column, nextGenerationField, currentField);
            });
        }

        private void CalculateNextGenerationOptimized(int row, int column, Field nextGenerationField, Field currentField)     // OPTIMIZED
        {
            Cell cell = nextGenerationField.GetCell(row, column);
            int count = CountNeighbors(row, column, currentField);

            if (cell.IsAlive && count < 2)
            {
                cell.IsAlive = false;
                cell.Age = 0;
            }

            if (cell.IsAlive && (count == 2 || count == 3))
            {
                cell.Age++;
                cell.IsAlive = true;
            }

            if (cell.IsAlive && count > 3)
            {
                cell.IsAlive = false;
                cell.Age = 0;
            }

            if (!cell.IsAlive && count == 3)
            {
                cell.IsAlive = true;
                cell.Age = 0;
            }
        }
    }
}