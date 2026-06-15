using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public partial class Matrix : IEnumerable
    {
        public void FillRandomDouble(int min = 0, int max = 10)
        {
            Random rand = new Random();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    this[i, j] = Math.Round(min + (max - min) * rand.NextDouble(), 5);
                }
            }
        }
        public void FillRandomInt(int min = 0, int max = 10)
        {
            Random rand = new Random();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    this[i, j] = rand.Next(min, max);
                }
            }
        }
        public void Fill()
        {
            for (int i = 0; i < _rows; i++)
            {
                string[] r = Console.ReadLine().Split(' ');
                if (r.Length != _columns)
                {
                    Console.WriteLine("Input {0} doubles", _columns);
                    i--;
                    continue;
                }
                for (int j = 0; j < _columns; j++)
                {
                    double temp;
                    if (Double.TryParse(r[j], out temp))
                    {
                        this[i, j] = temp;
                    }
                    else
                    {
                        i--;
                        Console.WriteLine("NonDouble input");
                        break;
                    }
                }
            }
        }
        public void FillByArray(double[,] arr)
        {
            if (arr.GetLength(0) != _rows || arr.GetLength(1) != _columns)
            {
                throw new DimensionMismatchException("Array contains more elements then expected");
            }

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    this[i, j] = arr[i, j];
                }
            }
        }
    }
}
