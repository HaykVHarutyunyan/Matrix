using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrices
{
    public partial class Matrix : IEnumerable
    {
        protected double[][] _matrix;
        protected int _rows;
        protected int _columns;
        protected MatrixFactory _factory = new MatrixFactory();
        public int Rows => _rows;
        public int Columns => _columns;

        public double[] this[int i]
        {
            get => _matrix[i];
            set => _matrix[i] = value;
        }
        public double this[int i, int j]
        {
            get => _matrix[i][j];
            set => _matrix[i][j] = value;
        }
        internal Matrix(int rows, int columns)
        {
            _matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                _matrix[i] = new double[columns];
            }
            _rows = rows;
            _columns = columns;

            u = new Lazy<Matrix>(() => GetU());
        }
        internal Matrix(double[][] matrix)
        {
            _matrix = matrix.Select(r => r.ToArray()).ToArray();
            _rows = matrix.Length;
            _columns = matrix[0].Length;
            u = new Lazy<Matrix>(() => GetU());
        }
        internal Matrix(double[,] matrix)
        {
            _rows = matrix.GetLength(0);
            _columns = matrix.GetLength(1);
            _matrix = new double[_rows][];
            for (int i = 0; i < _rows; i++)
            {
                _matrix[i] = new double[_columns];
                for (int j = 0; j < _columns; j++)
                {
                    _matrix[i][j] = matrix[i, j];
                }
            }
            u = new Lazy<Matrix>(() => GetU());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (double[] i in _matrix)
            {
                foreach (double j in i)
                {
                    sb.Append($"{j, 2} ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public void Print() => Console.WriteLine(this);

        public IEnumerator<double[]> GetEnumerator()
        {
            return ((IEnumerable<double[]>)_matrix).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _matrix.GetEnumerator();
        }
    }

    public partial class Square : Matrix
    {
        internal Square(int size) : base(size, size)
        {
            LazyHandler();
        }
        internal Square(double[][] matrix) : base(matrix)
        {
            LazyHandler();
        }
        internal Square(double[,] matrix) : base(matrix)
        {
            LazyHandler();
        }
    }
}
