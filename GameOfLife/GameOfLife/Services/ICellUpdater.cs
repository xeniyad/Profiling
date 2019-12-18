namespace GameOfLife.Services
{
    public interface ICellUpdater
    {
        void Update(Field nextGenerationField, Field currentField);
    }
}