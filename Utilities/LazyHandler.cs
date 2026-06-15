using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vectors;

namespace Matrices
{
    public partial class Square : Matrix
    {
        private Lazy<Square>[] _decompositions;
        private Lazy<Hashtable> _EigenResult;
        private Lazy<double> _det;
        private Lazy<Matrix> _d;
        private void LazyHandler()
        {
            _decompositions = new Lazy<Square>[4];
            _decompositions[(int)DecompositionMatrices.L] = new Lazy<Square>(() => GetL());
            _decompositions[(int)DecompositionMatrices.U] = new Lazy<Square>(() => (Square)GetU());
            _decompositions[(int)DecompositionMatrices.Q] = new Lazy<Square>(() => GetQ());
            _decompositions[(int)DecompositionMatrices.R] = new Lazy<Square>(() => GetR());
            _EigenResult = new Lazy<Hashtable>(() => GetEigenvaluesEigenVectors());
            _det = new Lazy<double>(() => Determinant());
            _d = new Lazy<Matrix>(() => DMatrix());
        }
        private enum DecompositionMatrices
        {
            L = 0,
            U = 1,
            Q = 2,
            R = 3
        }
    }
}
