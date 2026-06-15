using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Vectors;

namespace Matrices
{
    public class MatrixFactory
    {
        private bool HasMatrix<T_rows, TElements>(T_rows[] matrix) where T_rows : IList<TElements>
        {
            switch (Type.GetTypeCode(typeof(TElements)))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public Matrix Create<T_rows, TElements>(T_rows[] matrix) where T_rows : IList<TElements>
        {
            if (!HasMatrix<T_rows, TElements>(matrix))
            {
                throw new NotSupportedException("Only numeric row types are allowed.");
            }

            int a = matrix[0].Count;

            double[][] mat = new double[matrix.Count()][];
            for (int i = 0; i < matrix.Count(); i++)
            {
                mat[i] = new double[a];
                for (int j = 0; j < a; j++)
                {
                    mat[i][j] = (double)(object)matrix[i][j];
                }
            }

            return Create(mat);
        }
        public Matrix Create(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Dimentions must be posivie numbers");
            }

            return new Square(size);
        }
        public Matrix Create(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentException("Dimentions must be posivie numbers");
            }

            if (rows == columns)
            {
                return new Square(rows);
            }
            return new Matrix(rows, columns);
        }
        public Matrix Create(double[][] matrix)
        {
            if (matrix is null || matrix.Length == 0)
            {
                throw new ArgumentException("Matrix cannot be null or empty.", nameof(matrix));
            }

            int cols = matrix[0].Length;
            if (matrix.Any(r => r.Length != cols))
            {
                throw new ArgumentException("All rows must have the same length.", nameof(matrix));
            }

            if (matrix.Count() == cols)
            {
                return new Square(matrix);
            }
            return new Matrix(matrix);
        }
        public Matrix Create(double[,] matrix)
        {
            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                return new Square(matrix);
            }
            return new Matrix(matrix);
        }

        public Matrix Zero(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentException("Dimentions must be posivie numbers");
            }

            if (rows == columns)
            {
                return new Square(rows);
            }
            return new Matrix(rows, columns);
        }
        public Square Zero(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Dimentions must be posivie numbers");
            }

            return new Square(size);
        }
        public Matrix One(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentException("Dimentions must be posivie numbers");
            }

            if (rows == columns)
            {
                return One(rows);
            }

            Matrix oneMatrix = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    oneMatrix[i, j] = 1;
                }
            }
            return oneMatrix;
        }
        public Square One(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Dimentions must be posivie numbers");
            }
            Square oneMatrix = new Square(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    oneMatrix[i, j] = 1;
                }
            }
            return oneMatrix;
        }
        public Square IdentityMatrix(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("Dimentions must be posivie numbers");
            }

            Square identityMatrix = new Square(n);

            for (int i = 0; i < n; i++)
            {
                identityMatrix[i, i] = 1;
            }

            return identityMatrix;
        }
        public Matrix FromVector(Vector v)
        {
            Matrix vectorMatrix = Create<Vector, double>(new Vector[] { v });
            vectorMatrix = vectorMatrix.Transpose();
            return vectorMatrix;
        }
    }
}
