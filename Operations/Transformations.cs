using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{

    public partial class Matrix : IEnumerable
    {
        public Matrix Transpose()
        {
            Matrix transposed;
            if (IsSquare())
            {
                transposed = new Square(_rows);
            }
            else
            {
                transposed = new Matrix(_columns, _rows);
            }

            for (int i = 0; i < transposed._rows; i++)
            {
                for (int j = 0; j < transposed._columns; j++)
                {
                    transposed[i, j] = this[j, i];
                }
            }
            return transposed;
        }
        public void Swap_rows(int first, int second)
        {
            if (first == second)
            {
                return;
            }

            if (first > _rows || first < 0 || second > _rows || second < 0)
            {
                throw new InvalidRowOperationException("The mentioned index is out of range.");
            }

            for (int i = 0; i < _columns; i++)
            {
                double temp = this[first, i];
                this[first, i] = this[second, i];
                this[second, i] = temp;
            }
        }

        public Matrix RemoveRow(int rowIndex)
        {
            if (rowIndex >= _rows || rowIndex < 0)
            {
                throw new InvalidRowOperationException("The mentioned index is out of range.");
            }

            Matrix rowRemoved;
            if (_rows - 1 == _columns)
            {
                rowRemoved = new Square(_columns);
            }
            else
            {
                rowRemoved = new Matrix(_rows - 1, _columns); ;
            }

            int checker = 0;
            for (int i = 0; i < _rows; i++)
            {
                if (i == rowIndex)
                {
                    checker = 1;
                    continue;
                }
                for (int j = 0; j < _columns; j++)
                {
                    rowRemoved[i - checker, j] = this[i, j];
                }
            }

            return rowRemoved;
        }
        public Matrix RemoveColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= _columns)
            {
                throw new InvalidRowOperationException("The mentioned index is out of range.");
            }

            Matrix columnRemoved;
            if (_rows == _columns - 1)
            {
                columnRemoved = new Square(_rows);
            }
            else
            {
                columnRemoved = new Matrix(_rows, _columns - 1);
            }

            for (int i = 0; i < columnRemoved._rows; i++)
            {
                int counter = 0;
                for (int j = 0; j < _columns; j++)
                {
                    if (j == columnIndex)
                    {
                        counter = 1;
                        continue;
                    }
                    columnRemoved[i, j - counter] = this[i, j];
                }
            }

            return columnRemoved;
        }
        public Matrix AddRow<TRow>(IList<TRow> row, int index)
        {
            if (_columns != row.Count || index < 0 || index > _rows)
            {
                throw new InvalidRowOperationException("The lengths of matrix row and given row do not match.");
            }

            List<double[]> matrix = new List<double[]>();

            foreach (double[] _rows in _matrix)
            {
                matrix.Add(_rows);
            }

            matrix.Insert(index, (double[])(object)row);
            double[][] arr = matrix.ToArray();

            Matrix newMatrix = _factory.Create<double[], double>(arr);
            return newMatrix;
        }
        public Matrix AddColumn<TElements>(IList<TElements> column, int index)
        {
            if (_rows != column.Count || index < 0 || index > _rows)
            {
                throw new InvalidColumnOperationException("The lengths of matrix column and given column do not match.");
            }

            Matrix newMatrix;
            if (_rows == _columns + 1)
            {
                newMatrix = new Square(_rows);
            }
            else
            {
                newMatrix = new Matrix(_rows, _columns + 1);
            }

            for (int i = 0; i < newMatrix._rows; i++)
            {
                for (int j = 0; j < newMatrix._columns; j++)
                {
                    if (j == index)
                    {
                        newMatrix[i, j] = (double)(object)column[i];
                        continue;
                    }
                    if (j > index)
                    {
                        newMatrix[i, j] = this[i, j - 1];
                        continue;
                    }
                    newMatrix[i, j] = this[i, j];
                }
            }

            return newMatrix;
        }
    }
}
