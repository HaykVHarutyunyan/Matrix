using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public partial class Matrix : IEnumerable
    {
        public static Matrix operator -(Matrix unary)
        {
            return -1 * unary;
        }
        public static Matrix operator +(Matrix left, Matrix right)
        {
            if (left._rows != right._rows || left._columns != right._columns)
            {
                throw new DimensionMismatchException("You cannot run addition operation.");
            }

            Matrix Sum;
            if (left is Square && right is Square)
            {
                Sum = new Square(left._rows);
            }
            else
            {
                Sum = new Matrix(left._rows, left._columns);
            }

            for (int i = 0; i < left._rows; i++)
            {
                for (int j = 0; j < left._columns; j++)
                {
                    Sum[i, j] = Math.Round(left[i, j] + right[i, j], 5);
                }
            }
            return Sum;
        }
        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left._rows != right._rows || left._columns != right._columns)
            {
                throw new DimensionMismatchException("You cannot run subtraction operation.");
            }

            Matrix Sub;

            if (left is Square && right is Square)
            {
                Sub = new Square(left._rows);
            }
            else
            {
                Sub = new Matrix(left._rows, left._columns);
            }

            for (int i = 0; i < left._rows; i++)
            {
                for (int j = 0; j < left._columns; j++)
                {
                    Sub[i, j] = Math.Round(left[i, j] - right[i, j], 5);
                }
            }
            return Sub;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left._rows != right._rows || left._columns != right._columns)
            {
                throw new DimensionMismatchException("You cannot run element-multiplication operation.");
            }

            Matrix Mult = new Matrix(left._rows, left._columns);
            if (left is Square && right is Square)
            {
                Mult = new Square(left._rows);
            }
            else
            {
                Mult = new Matrix(left._rows, left._columns);
            }

            for (int i = 0; i < left._rows; i++)
            {
                for (int j = 0; j < left._columns; j++)
                {
                    Mult[i, j] = Math.Round(left[i, j] * right[i, j], 5);
                }
            }
            return Mult;
        }
        public static Matrix operator *(Matrix left, double right)
        {
            Matrix mult;
            if (left is Square)
            {
                mult = new Square(left._rows);
            }
            else
            {
                mult = new Matrix(left._rows, left._columns);
            }

            for (int i = 0; i < left._rows; i++)
            {
                for (int j = 0; j < left._columns; j++)
                {
                    mult[i, j] = Math.Round(left[i, j] * right, 5);
                }
            }

            return mult;
        }
        public static Matrix operator *(double left, Matrix right)
        {
            return right * left;
        }
        public static Matrix operator ^(Matrix left, Matrix right)
        {
            if (left._columns != right._rows)
            {
                throw new DimensionMismatchException($"Cannot multiply a {left._rows}×{left._columns} to a " +
                    $"{right._rows}×{right._columns} Matrix<T_rows, TElements> — inner dimensions differ.");
            }

            Matrix mult;
            if (left._rows != right._columns)
            {
                mult = new Matrix(left._rows, right._columns);
            }
            else
            {
                mult = new Square(left._rows);
            }

            for (int i = 0; i < left._rows; i++)
            {
                for (int j = 0; j < right._columns; j++)
                {
                    for (int k = 0; k < right._rows; k++)
                    {
                        mult[i, j] += left[i, k] * right[k, j];
                    }
                    mult[i, j] = Math.Round(mult[i, j], 5);
                }
            }

            return mult;
        }

        public static Matrix operator /(Matrix left, Matrix right)
        {
            if (left._rows != right._rows || left._columns != right._columns)
            {
                throw new DimensionMismatchException("You cannot run division operation.");
            }

            Matrix Dev;
            if (left is Square && right is Square)
            {
                Dev = new Square(left._rows);
            }
            else
            {
                Dev = new Matrix(left._rows, left._columns);
            }

            for (int i = 0; i < left._rows; i++)
            {
                for (int j = 0; j < left._columns; j++)
                {
                    try
                    {
                        Dev[i, j] = Math.Round(left[i, j] / right[i, j], 5);
                    }
                    catch (DivideByZeroException exp)
                    {
                        Dev = null;
                        Console.WriteLine(exp.Message);
                    }
                }
            }
            return Dev;
        }

        public static Matrix operator /(Matrix left, double right)
        {
            if (right == 0)
            {
                throw new DivideByZeroException();
            }

            Matrix dev;
            if (left is Square)
            {
                dev = new Square(left._rows);
            }
            else
            {
                dev = new Matrix(left._rows, left._columns);
            }


            for (int i = 0; i < left._rows; i++)
            {
                for (int j = 0; j < left._columns; j++)
                {
                    dev[i, j] = Math.Round(left[i, j] / right, 5);
                }
            }

            return dev;
        }

        public static bool operator ==(Matrix left, Matrix right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Matrix left, Matrix right)
        {

            return !left.Equals(right);
        }

        public bool Equals(Matrix eq)
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if (this[i, j] != eq[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj as Matrix);
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + _rows;
            hash = hash * 31 + _columns;

            for (int i = 0; i < _rows; ++i)
                for (int j = 0; j < _columns; ++j)
                    hash = hash * 31 + this[i, j].GetHashCode();

            return hash;
        }

        public Matrix Multiply(Matrix other) => this ^ other;
        public Matrix Add(Matrix other) => this + other;
        public Matrix Subtract(Matrix other) => this - other;
        public Matrix Divide(Matrix other) => this / other;
        public Matrix Divide(double a) => this / a;
    }
}
