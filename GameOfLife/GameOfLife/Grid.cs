using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife
{
    internal class Grid
    {
        public int SizeX { get; }

        private int SizeY { get; }

        private readonly Field _currentField;

        private readonly Field _nextGenerationField;

        private static Random _rnd;

        private readonly Canvas _drawCanvas;

        private readonly Ellipse[,] _cellsVisuals;

        
        public Grid(Canvas canvas)
        {
            _drawCanvas = canvas;
            _rnd = new Random();
            SizeX = (int) (_drawCanvas.Width / 5);
            SizeY = (int)(_drawCanvas.Height / 5);

            _currentField = new Field(SizeX, SizeY);
            _nextGenerationField = new Field(SizeX, SizeY);
            _cellsVisuals = new Ellipse[SizeX, SizeY];

            SetRandomPattern();
            InitCellsVisuals();
            UpdateGraphics();
        }


        public void Clear()
        {
            _currentField.Clear();
            _nextGenerationField.Clear();

            DoForEachCell((i, j) =>
            {
                _cellsVisuals[i, j].Fill = Brushes.Gray;
            });
        }


        private void MouseMove(object sender, MouseEventArgs e)
        {
            var cellVisual = sender as Ellipse ?? throw new InvalidOperationException("Sender is not Ellipse");
            
            int i = (int) cellVisual.Margin.Left / 5;
            int j = (int) cellVisual.Margin.Top / 5;
            

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Cell cell = _currentField.GetCell(i, j);
                if (!cell.IsAlive)
                {
                    cell.IsAlive = true;
                    cell.Age = 0;
                    cellVisual.Fill = Brushes.White;
                }
            }
        }

        public void UpdateGraphics()
        {
            DoForEachCell((i, j) =>
            {
                _cellsVisuals[i, j].Fill = _currentField.GetCell(i, j).GetBrushColor();
            });
        }

        public void InitCellsVisuals()
        {
            DoForEachCell((i, j) =>
            {
                var ellipse = new Ellipse();
                ellipse.Width = ellipse.Height = 5;

                Cell cell = _currentField.GetCell(i, j);
                double left = cell.PositionX;
                double top = cell.PositionY;

                ellipse.Margin = new Thickness(left, top, 0, 0);
                ellipse.Fill = Brushes.Gray;
                _drawCanvas.Children.Add(ellipse);

                ellipse.MouseMove += MouseMove;
                ellipse.MouseLeftButtonDown += MouseMove;

                _cellsVisuals[i, j] = ellipse;
            });

            UpdateGraphics();
        }

        private void DoForEachCell(Action<int, int> action)
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    action(i, j);
                }
            }
        }

        public static bool GetRandomBoolean()
        {
            return _rnd.NextDouble() > 0.8;
        }

        public void SetRandomPattern()
        {
            _currentField.DoForEachCell((i, j, cell) =>
            {
                cell.IsAlive = GetRandomBoolean();
            });
        }
        
        public void UpdateToNextGeneration()
        {
            _currentField.DoForEachCell((i, j, cell) =>
            {
                Cell nextGenCell = _nextGenerationField.GetCell(i, j);
                cell.IsAlive = nextGenCell.IsAlive;
                cell.Age = nextGenCell.Age;
            });

            UpdateGraphics();
        }
        

        public void Update()
        {
            _nextGenerationField.DoForEachCell((row, column, cell) =>
            {
                // UNOPTIMIZED
                _nextGenerationField.SetCell(row, column, CalculateNextGenerationNotOptimized(row, column, cell));

                // OPTIMIZED
                // CalculateNextGenerationOptimized(row, column, cell);   
            });

            UpdateToNextGeneration();
        }

        public Cell CalculateNextGenerationNotOptimized(int row, int column, Cell cell)    // UNOPTIMIZED
        {
            var alive = cell.IsAlive;
            var count = CountNeighbors(row, column);

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

        public void CalculateNextGenerationOptimized(int row, int column, Cell cell)     // OPTIMIZED
        {
            int count = CountNeighbors(row, column);
            
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

        public int CountNeighbors(int row, int column)
        {
            int count = 0;

            if (row != SizeX - 1 && _currentField.GetCell(row + 1, column).IsAlive)
            {
                count++;
            }

            if (row != SizeX - 1 && column != SizeY - 1 && _currentField.GetCell(row + 1, column + 1).IsAlive)
            {
                count++;
            }

            if (column != SizeY - 1 && _currentField.GetCell(row, column + 1).IsAlive)
            {
                count++;
            }

            if (row != 0 && column != SizeY - 1 && _currentField.GetCell(row - 1, column + 1).IsAlive)
            {
                count++;
            }

            if (row != 0 && _currentField.GetCell(row - 1, column).IsAlive)
            {
                count++;
            }

            if (row != 0 && column != 0 && _currentField.GetCell(row - 1, column - 1).IsAlive)
            {
                count++;
            }

            if (column != 0 && _currentField.GetCell(row, column - 1).IsAlive)
            {
                count++;
            }

            if (row != SizeX - 1 && column != 0 && _currentField.GetCell(row + 1, column - 1).IsAlive)
            {
                count++;
            }

            return count;
        }
    }
}