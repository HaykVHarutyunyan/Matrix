using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class SingularMatrixException : Exception
    {
        public SingularMatrixException(string msg) : base(msg) { }
    }
    class NonSquareMatrixException : Exception
    {
        public NonSquareMatrixException(string msg) : base(msg) { }
    }

    class DimensionMismatchException : Exception
    {
        public DimensionMismatchException(string msg) : base(msg + "\n The dimentions differ.") { }
    }

    class NullOrEmptyMatrixException : ArgumentNullException
    {
        public NullOrEmptyMatrixException(string msg) : base(msg) { }
    }

    class InvalidRowOperationException : Exception
    {
        public InvalidRowOperationException(string msg) : base(msg) { }
    }

    class InvalidColumnOperationException : Exception
    {
        public InvalidColumnOperationException(string msg) : base(msg) { }
    }

    class NumericalInstabilityException : Exception
    {
        public NumericalInstabilityException(string msg) : base(msg) { }
    }

    class DecompositionFailedException : Exception
    {
        public DecompositionFailedException(string msg) : base(msg) { }
    }

    class InvalidPivotElementException : Exception
    {
        public InvalidPivotElementException(string msg) : base(msg) { }
    }

    class NotInvertibleException : SingularMatrixException
    {
        public NotInvertibleException(string msg) : base(msg) { }
    }


    class RankUndefinedException : Exception
    {
        public RankUndefinedException(string msg) : base(msg) { }
    }

}

