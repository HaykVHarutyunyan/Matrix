using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vectors;

namespace Matrices
{
    public partial class Matrix : IEnumerable
    {
        public int Rank()
        {
            int rank = 0;

            for (int i = 0; i < _U.Rows; i++)
            {
                if (_U[i, i] != 0)
                {
                    rank++;
                }
            }
            return rank;
        }

    }

    public partial class Square : Matrix
    {
        public Vector EigenValues => (Vector)_EigenResult.Value["EigenValue"];
        public Matrix EigenVectors => (Matrix)_EigenResult.Value["EigenVector"];

        public double Det => _det.Value;

        private double Determinant()
        {
            double det = Math.Pow(-1, swapCount);

            for (int i = 0; i < Rows; i++)
            {
                det *= _U[i, i];
            }
            return det;
        }

        public Square Inverse()
        {
            if (!IsInvertible())
            {
                throw new NotInvertibleException("This matrix cannot be inverted.");
            }

            Matrix inverse = Clone();
            for (int i = 0; i < inverse.Rows; i++)
            {
                for (int j = 0; j < inverse.Columns; j++)
                {
                    Square temp = (Square)Clone();
                    temp = (Square)temp.RemoveRow(i).RemoveColumn(j);
                    inverse[j, i] = Math.Pow(-1, i + j) * temp.Det;
                }
            }
            inverse /= Det;

            return (Square)inverse;
        }

        public double Trace()
        {
            double[] diagonal = GetDiagonal();
            return diagonal.Sum();
        }

        private Square DMatrix()
        {
            Square D = _factory.IdentityMatrix(EigenValues.Size);
            for (int i = 0; i < EigenValues.Size; i++)
            {
                D[i, i] = EigenValues[i];
            }
            return D;
        }


        private Hashtable GetEigenvaluesEigenVectors()
        {
            Hashtable valueVector = new Hashtable();
            Square A = (Square)Clone();
            Matrix[] qr;
            Square eigenvectors = _factory.IdentityMatrix(Rows);

            while (!A.IsTriangular())
            {
                qr = A.QR();
                eigenvectors = (Square)eigenvectors.Multiply(qr[0]);
                A = (Square)qr[1].Multiply(qr[0]);
            }

            valueVector.Add("EigenValue", new Vector(A.GetDiagonal()));
            valueVector.Add("EigenVector", eigenvectors);

            return valueVector;
        }

        private Square _ExponentiationBySquare(int power)
        {
            Square pow = _factory.IdentityMatrix(Rows);
            Square temp = (Square)Clone();
            while (power != 0)
            {
                if (power % 2 == 1)
                {
                    pow = (Square)pow.Multiply(temp);
                }
                temp = (Square)temp.Multiply(temp);
                power /= 2;
            }
            return pow;
        }

        public Square Pow(int power)
        {
            if (power < 0)
            {
                if (IsSingular())
                {
                    throw new InvalidOperationException("This matrix does not have an Inverse matrix.");
                }
                return Inverse().Pow(-power);
            }
            if (power == 0)
            {
                return _factory.IdentityMatrix(Rows);
            }
            if (power <= 10)
            {
                return _ExponentiationBySquare(power);
            }

            Matrix P = EigenVectors;
            Matrix D = DMatrix();
            Square PInverse = Inverse();

            for (int i = 0; i < D.Rows; i++)
            {
                D[i, i] = Math.Pow(D[i, i], power);
            }

            return (Square)P.Multiply(D.Multiply(PInverse));
        }


    }
}
