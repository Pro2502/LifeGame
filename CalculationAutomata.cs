using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheGameOfLIFE
{
    class CalculationAutomata
    {
        public int CurrentGeneration { get; private set; }
        private bool[,] _field;
        private readonly int _rows;
        private readonly int _cols;
        private Random _random = new Random();
        public CalculationAutomata(int rows, int cols, int density)
        {
            this._rows = rows;
            this._cols = cols;
            _field = new bool[cols, rows];
            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    _field[x, y] = random.Next(density) == 0;
                }
            }
        }
        public void CalculateGeneration()
        {
            var newField = new bool[_cols, _rows];

            using (StreamWriter sw = new StreamWriter(@"C: \Users\Анна\source\repos\TheGameOfLIFE\contdition.txt"))
            {
                for (int x = 0; x < _cols; x++)
                {
                    for (int y = 0; y < _rows; y++)
                    {
                        var neighboursCount = CountNeighbours(x, y);
                        var hasLife = _field[x, y];
                        if (!hasLife && neighboursCount == 3)
                        {
                            newField[x, y] = true;
                            
                        }
                        else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))
                        {
                            newField[x, y] = false;
                            
                        }
                        else
                        {
                            newField[x, y] = _field[x, y];
                        }
                    sw.WriteLine(String.Format("{0}|{1}|{2}|", x, y, newField[x, y]));
                }
            }
            sw.Close();
            if (File.Exists(@"C: \Users\Анна\source\repos\TheGameOfLIFE\contdition.txt"))
                {
                    StreamReader  sr = File.OpenText(@"C: \Users\Анна\source\repos\TheGameOfLIFE\contdition.txt");
                    while (!sr.EndOfStream)
                    {
                        string data_string = sr.ReadLine();
                        string[] fields = data_string.Split('|');
                        int x = Convert.ToInt32(fields[0]);
                        int y = Convert.ToInt32(fields[1]);
                        _field[x, y] = Convert.ToBoolean(fields[2]);
                    }
                    sr.Close();
                }
            }
            _field = newField;
            CurrentGeneration++;
        }
        public bool[,] CountingCurrentGeneration()
        {
            var result = new bool[_cols, _rows];
            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    result[x, y] = _field[x, y];
                }
            }
            return _field;
        }
        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + _cols) % _cols;//если заходим за левый край - он является правым
                    var row = (y + j + _rows) % _rows;
                    var isSelfChecking = col == x && row == y;// самопроверка
                    var hasLife = _field[col, row];
                    if (hasLife && !isSelfChecking)
                        count++;
                }
            }
            return count;
        }
    }
}

