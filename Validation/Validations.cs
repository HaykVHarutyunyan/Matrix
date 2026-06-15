using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    partial class Matrix : IEnumerable
    {
        public virtual bool IsSquare() => _rows == _columns;
        public virtual bool IsSymmetric() => false;
        public virtual bool IsDiagonal() => false;
        public virtual bool IsIdentity() => false;
        public virtual bool IsUpperTriangular() => false;
        public virtual bool IsLowerTriangular() => false;
        public bool IsTriangular() => IsUpperTriangular() || IsLowerTriangular();
        public virtual bool IsSingular() => false;
        public bool IsNonSingular() => !IsSingular();
        public bool IsInvertible() => IsNonSingular();
        public virtual bool IsOrthogonal() => false;
    }

    partial class Square : Matrix
    {
        public override bool IsSymmetric()
        {
            Matrix transposed = Transpose();
            return this == transposed;
        }
        public override bool IsDiagonal()
        {
            for (int i = 0; i < Rows; i++)
            {
                if (this[i, i] == 0)
                {
                    return false;
                }

                for (int j = 0; j < Columns; j++)
                {
                    if (i != j && this[i, j] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public override bool IsIdentity()
        {
            for (int i = 0; i < Rows; i++)
            {

                for (int j = 0; j < Columns; j++)
                {
                    if ((i != j && this[i, j] != 0) || (i == j && this[i, j] != 1))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override bool IsUpperTriangular()
        {
            for (int i = 1; i < Rows; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Math.Round(this[i, j], 4) != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override bool IsLowerTriangular()
        {
            for (int i = 0; i < Rows - 1; i++)
            {
                for (int j = i + 1; j < Columns; j++)
                {
                    if (Math.Round(this[i, j], 4) != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool IsSingular() => Det == 0;

        public override bool IsOrthogonal()
        {
            Square transposed = (Square)Transpose();
            return Multiply(transposed).IsIdentity();
        }

    }
}
