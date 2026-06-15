using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vectors;

namespace Matrices
{
    public partial class Matrix : IEnumerable
    {
        public virtual Matrix Clone() => _factory.Create(_matrix);

        public Matrix SubMatrix(int startRow, int startCol, int nRow, int nCol)
        {
            if (startRow < 0 || startCol < 0 ||
                startRow >= _rows || startCol >= _columns ||
                nRow > _rows - startRow || nCol > _columns - startCol)
            {
                throw new ArgumentOutOfRangeException("Indices are out of range");
            }

            Matrix sub = _factory.Create(nRow, nCol);

            for (int i = startRow; i < nRow; i++)
            {
                for (int j = startCol; j < nCol; j++)
                {
                    sub[i, j] = this[i, j];
                }
            }

            return sub;
        }

        public Vector GetColumn(int index)
        {
            if (index < 0 || index >= _columns)
            {
                throw new ArgumentOutOfRangeException();
            }

            Matrix cloned = Transpose();
            return new Vector(cloned[index]);
        }
    }

    public partial class Square : Matrix
    {
        public double[] GetDiagonal(bool main = true)
        {
            double[] diagonal = new double[_rows];
            for (int i = 0; i < _rows; i++)
            {
                diagonal[i] = main ? this[i, i] : this[_rows - i - 1, _columns - i - 1];
            }
            return diagonal;
        }
    }
}
