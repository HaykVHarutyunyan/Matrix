using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vectors;

namespace Matrices
{
    public partial class Matrix : IEnumerable
    {
        private Lazy<Matrix> u;
        protected int swapCount = 0;
        public virtual Matrix _U => u.Value;

        protected virtual void Swapper(int index)
        {
            int max = index;
            if (this[index, index] == 0)
            {
                for (int i = index + 1; i < _rows; i++)
                {
                    max = Math.Abs(this[max, index]) < Math.Abs(this[i, index]) ? i : max;
                }
            }
            if (max != index)
            {
                Swap_rows(index, max);
            }
        }
        protected virtual Matrix GetU()
        {
            Matrix U = Clone();

            for (int i = 0; i < U._rows - 1; i++)
            {
                U.Swapper(i);

                for (int j = i + 1; j < U._rows; j++)
                {
                    if (U[j, i] == 0)
                    {
                        continue;
                    }
                    double coef = Math.Round(U[j, i] / U[i, i], 5);
                    for (int k = i; k < U._columns; k++)
                    {
                        U[j, k] -= U[i, k] * coef;
                    }
                }
            }
            return U;
        }

        public Matrix REF()
        {
            Matrix echelon = _U.Clone();

            for (int i = 0; i < echelon._rows && i < echelon._columns; i++)
            {
                double coef = echelon[i, i];
                if (coef != 0)
                {
                    for (int j = i; j < echelon._columns; j++)
                    {
                        echelon[i, j] /= coef;
                    }
                }
            }

            return echelon;
        }
        public Matrix RREF()
        {
            Matrix rref = REF();

            for (int i = rref._rows - 1; i > 0; i--)
            {
                for (int j = i - 1; j > -1; j--)
                {
                    double coef = rref[j, i];
                    for (int k = i; k < rref._columns; k++)
                    {
                        rref[j, k] -= rref[i, k] * coef;
                    }
                }
            }

            return rref;
        }
    }

    public partial class Square : Matrix
    {
        private Square _Q => _decompositions[(int)DecompositionMatrices.Q].Value;
        private Square _R => _decompositions[(int)DecompositionMatrices.R].Value;
        private Square _L => _decompositions[(int)DecompositionMatrices.L].Value;
        private Square _P;
        protected override void Swapper(int index)
        {
            int max = index;
            if (this[index, index] == 0)
            {
                for (int i = index + 1; i < _rows; i++)
                {
                    max = Math.Abs(this[max, index]) < Math.Abs(this[i, index]) ? i : max;
                }
            }
            if (max != index)
            {
                Swap_rows(index, max);
                swapCount++;
            }
            GetP(index, max);
        }

        private Square GetL()
        {
            Square uInverse = ((Square)_U).Inverse();
            return (Square)Multiply(uInverse);
        }
        protected override Matrix GetU()
        {
            Square U = new Square(_matrix);

            for (int i = 0; i < U._rows - 1; i++)
            {
                U.Swapper(i);

                for (int j = i + 1; j < U._rows; j++)
                {
                    if (U[j, i] == 0)
                    {
                        continue;
                    }
                    double coef = Math.Round(U[j, i] / U[i, i], 5);
                    for (int k = i; k < U._columns; k++)
                    {
                        U[j, k] -= U[i, k] * coef;
                    }
                }
            }
            return U;
        }
        private void GetP(int first, int second)
        {
            if (_P is null)
            {
                _P = _factory.IdentityMatrix(_rows);
            }
            _P.Swap_rows(first, second);
        }

        private Square GetQ()
        {
            if (!IsInvertible())
            {
                throw new NotInvertibleException("This matrix does not have orthogonal matrix due to it is not invertible.");
            }
            Matrix Q = new Matrix(_rows, 0);
            Q = Q.AddColumn(GetColumn(0).Unit(), 0);
            for (int i = 1; i < _columns; i++)
            {
                Vector col = GetColumn(i);
                for (int j = 0; j < i; j++)
                {
                    col = (Q.GetColumn(j)).GramShmidt(col);
                }
                Q = Q.AddColumn(col, i);
            }

            return (Square)Q;
        }
        private Square GetR()
        {
            Matrix qInverse = _Q.Transpose();
            return (Square)qInverse.Multiply(this);
        }

        public Matrix[] LU()
        {
            if (_L is null || _U is null)
            {
                throw new DecompositionFailedException("The matrix does not have a unique LU representation.");
            }
            return new Matrix[] { _L, _U };
        }
        public Matrix[] LUP()
        {
            if (_L is null || _U is null)
            {
                throw new DecompositionFailedException("The matrix does not have a unique LUP representation.");
            }
            return new Matrix[] { _L, _U, _P };
        }
        public Square[] QR()
        {
            return new Square[] { _Q, _R };
        }
    }
}
