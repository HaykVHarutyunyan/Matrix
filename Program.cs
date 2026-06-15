using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Vectors;

namespace Matrices
{
    class Program
    {
        // ─── Helpers ──────────────────────────────────────────────────────────────
        static void Header(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"  {title}");
            Console.WriteLine(new string('═', 60));
        }

        static void SubHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine($"  ── {title} ──");
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("  Press any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
        }

        // ─── Main ─────────────────────────────────────────────────────────────────
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           Matrix Library — Interactive Demo              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.WriteLine("  A comprehensive walkthrough of every major feature.");
            Pause();

            Demo_Creation();
            Demo_ArithmeticOperations();
            Demo_Transformations();
            Demo_Determinant();
            Demo_Inverse();
            Demo_LU_Decomposition();
            Demo_QR_Decomposition();
            Demo_EigenvaluesEigenvectors();
            Demo_MatrixPow();
            Demo_RankAndTrace();
            Demo_Validations();
            Demo_RandomMatrices();

            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    Demo Complete!                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 1. MATRIX CREATION
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_Creation()
        {
            Header("1. Matrix Creation");

            var factory = new MatrixFactory();

            // --- Identity matrix
            SubHeader("3×3 Identity Matrix");
            Square identity = factory.IdentityMatrix(3);
            identity.Print();

            // --- Zero matrix
            SubHeader("2×3 Zero Matrix");
            Matrix zero = factory.Zero(2, 3);
            zero.Print();

            // --- All-ones matrix
            SubHeader("3×3 All-Ones Matrix");
            Square ones = factory.One(3);
            ones.Print();

            // --- From 2-D array
            SubHeader("3×3 Matrix from double[,]");
            double[,] data = {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            Square A = (Square)factory.Create(data);
            A.Print();

            // --- From jagged array
            SubHeader("Non-Square 2×4 Matrix from double[][]");
            double[][] jagged = {
                new double[] { 1, 0, -1, 2 },
                new double[] { 3, 4,  0, 1 }
            };
            Matrix B = factory.Create(jagged);
            B.Print();

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 2. ARITHMETIC OPERATIONS
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_ArithmeticOperations()
        {
            Header("2. Arithmetic Operations");

            var factory = new MatrixFactory();

            double[,] d1 = { { 1, 2 }, { 3, 4 } };
            double[,] d2 = { { 5, 6 }, { 7, 8 } };

            Square A = (Square)factory.Create(d1);
            Square B = (Square)factory.Create(d2);

            SubHeader("Matrix A");
            A.Print();

            SubHeader("Matrix B");
            B.Print();

            // --- Addition
            SubHeader("A + B  (element-wise addition)");
            Matrix sum = A + B;
            sum.Print();

            // --- Subtraction
            SubHeader("A - B  (element-wise subtraction)");
            Matrix diff = A - B;
            diff.Print();

            // --- Element-wise multiplication (Hadamard product)
            SubHeader("A * B  (Hadamard / element-wise product)");
            Matrix hadamard = A * B;
            hadamard.Print();

            // --- Scalar multiplication
            SubHeader("A × 3  (scalar multiplication)");
            Matrix scaled = A * 3;
            scaled.Print();

            // --- Negation
            SubHeader("-A  (negation)");
            Matrix neg = -A;
            neg.Print();

            // --- Matrix dot-product multiplication
            SubHeader("A ^ B  (dot-product / standard matrix multiplication)");
            Console.WriteLine("  A ^ B = A × B  where C[i,j] = Σk A[i,k]*B[k,j]");
            Matrix product = A ^ B;
            product.Print();

            // --- Multiply non-square: 2×3 × 3×2
            SubHeader("Non-square multiplication: (2×3) × (3×2)");
            double[,] m23 = { { 1, 2, 3 }, { 4, 5, 6 } };
            double[,] m32 = { { 7, 8 }, { 9, 10 }, { 11, 12 } };
            Matrix M1 = factory.Create(m23);
            Matrix M2 = factory.Create(m32);
            Console.WriteLine("  Left (2×3):");
            M1.Print();
            Console.WriteLine("  Right (3×2):");
            M2.Print();
            Console.WriteLine("  Result (2×2):");
            Matrix dotResult = M1.Multiply(M2);
            dotResult.Print();

            // --- Scalar division
            SubHeader("B / 2  (scalar division)");
            Matrix divided = B / 2.0;
            divided.Print();

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 3. TRANSFORMATIONS
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_Transformations()
        {
            Header("3. Transformations");

            var factory = new MatrixFactory();

            double[,] data = {
                { 1,  2,  3 },
                { 4,  5,  6 },
                { 7,  8,  9 }
            };
            Square A = (Square)factory.Create(data);

            SubHeader("Original Matrix A");
            A.Print();

            // --- Transpose
            SubHeader("Transpose of A  (rows ↔ columns)");
            Matrix T = A.Transpose();
            T.Print();

            // --- Remove row
            SubHeader("Remove Row 1 from A");
            Matrix noRow = A.RemoveRow(1);
            noRow.Print();

            // --- Remove column
            SubHeader("Remove Column 2 from A");
            Matrix noCol = A.RemoveColumn(2);
            noCol.Print();

            // --- Swap rows
            SubHeader("Swap Rows 0 and 2 of A");
            Matrix swapped = (Square)factory.Create(data); // fresh copy
            ((Square)swapped).Swap_rows(0, 2);
            swapped.Print();

            // --- Get column as Vector
            SubHeader("Column 1 of A as a Vector");
            Vector col1 = A.GetColumn(1);
            Console.WriteLine("  " + col1);

            // --- Diagonal of A
            SubHeader("Main Diagonal of A");
            double[] diag = A.GetDiagonal();
            Console.Write("  [");
            foreach (var d in diag) Console.Write($" {d}");
            Console.WriteLine(" ]");

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 4. DETERMINANT
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_Determinant()
        {
            Header("4. Determinant");

            var factory = new MatrixFactory();

            // --- 2×2 example (manual check: 1*4 - 2*3 = -2)
            double[,] d2 = { { 1, 2 }, { 3, 4 } };
            Square A = (Square)factory.Create(d2);
            SubHeader("2×2 Matrix");
            A.Print();
            Console.WriteLine($"  det(A) = {A.Det}  (expected: 1×4 − 2×3 = −2)");

            // --- 3×3 example
            double[,] d3 = {
                {  6,  1,  1 },
                {  4, -2,  5 },
                {  2,  8,  7 }
            };
            Square B = (Square)factory.Create(d3);
            SubHeader("3×3 Matrix");
            B.Print();
            Console.WriteLine($"  det(B) = {B.Det}  (expected: −306)");

            // --- Singular matrix (det = 0)
            double[,] singular = {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            Square S = (Square)factory.Create(singular);
            SubHeader("Singular Matrix (rows are linearly dependent)");
            S.Print();
            Console.WriteLine($"  det(S) = {Math.Round(S.Det, 5)}  (expected: 0)");
            Console.WriteLine($"  IsSingular = {S.IsSingular()}");

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 5. MATRIX INVERSE
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_Inverse()
        {
            Header("5. Matrix Inverse");

            var factory = new MatrixFactory();

            double[,] data = {
                { 1, 2 },
                { 3, 4 }
            };
            Square A = (Square)factory.Create(data);

            SubHeader("Original Matrix A");
            A.Print();

            Square inv = A.Inverse();
            SubHeader("A⁻¹ (Inverse of A)");
            inv.Print();

            SubHeader("Verification: A × A⁻¹ should equal I");
            Matrix check = A.Multiply(inv);
            check.Print();
            Console.WriteLine($"  IsIdentity = {((Square)check).IsIdentity()}");

            // --- 3×3 inverse
            double[,] d3 = {
                { 2, 1, 0 },
                { 1, 3, 1 },
                { 0, 1, 2 }
            };
            Square B = (Square)factory.Create(d3);
            SubHeader("3×3 Matrix B");
            B.Print();
            Square invB = B.Inverse();
            SubHeader("B⁻¹");
            invB.Print();
            SubHeader("Verification: B × B⁻¹");
            B.Multiply(invB).Print();

            // --- Attempting to invert a singular matrix
            SubHeader("Attempting to Invert a Singular Matrix");
            double[,] singularData = { { 1, 2 }, { 2, 4 } };
            Square sing = (Square)factory.Create(singularData);
            try
            {
                Square failedInv = sing.Inverse();
            }
            catch (NotInvertibleException ex)
            {
                Console.WriteLine($"  ✓ Caught expected exception: {ex.Message}");
            }

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 6. LU DECOMPOSITION
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_LU_Decomposition()
        {
            Header("6. LU Decomposition  (A = L · U)");

            var factory = new MatrixFactory();

            double[,] data = {
                { 2,  1, -1 },
                {-3, -1,  2 },
                {-2,  1,  2 }
            };
            Square A = (Square)factory.Create(data);

            SubHeader("Original Matrix A");
            A.Print();

            Matrix[] lu = A.LU();
            Matrix L = lu[0];
            Matrix U = lu[1];

            SubHeader("L  (lower-triangular factor)");
            L.Print();

            SubHeader("U  (upper-triangular factor)");
            U.Print();

            SubHeader("Verification: L × U");
            L.Multiply(U).Print();

            SubHeader("Row Echelon Form (REF) of A");
            A.REF().Print();

            SubHeader("Reduced Row Echelon Form (RREF) of A");
            A.RREF().Print();

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 7. QR DECOMPOSITION
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_QR_Decomposition()
        {
            Header("7. QR Decomposition  (A = Q · R)");

            var factory = new MatrixFactory();

            double[,] data = {
                { 12, -51,   4 },
                {  6, 167, -68 },
                { -4,  24, -41 }
            };
            Square A = (Square)factory.Create(data);

            SubHeader("Original Matrix A");
            A.Print();

            Square[] qr = A.QR();
            Square Q = qr[0];
            Square R = qr[1];

            SubHeader("Q  (orthogonal matrix — columns are orthonormal)");
            Q.Print();

            SubHeader("R  (upper-triangular factor)");
            R.Print();

            SubHeader("Verification: Q × R should equal A");
            Q.Multiply(R).Print();

            SubHeader("Q is orthogonal?  Q · Qᵀ = I");
            Matrix qtq = Q.Multiply(Q.Transpose());
            qtq.Print();
            Console.WriteLine($"  IsOrthogonal = {Q.IsOrthogonal()}");

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 8. EIGENVALUES & EIGENVECTORS
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_EigenvaluesEigenvectors()
        {
            Header("8. Eigenvalues and Eigenvectors");

            var factory = new MatrixFactory();

            // Symmetric matrix — eigenvalues are real and easy to verify
            double[,] data = {
                { 4, 1 },
                { 2, 3 }
            };
            Square A = (Square)factory.Create(data);

            SubHeader("Matrix A");
            A.Print();

            SubHeader("Eigenvalues  (λ such that Av = λv)");
            Vector eigenVals = A.EigenValues;
            Console.WriteLine("  λ = " + eigenVals);

            SubHeader("Eigenvectors  (one per column)");
            Matrix eigenVecs = A.EigenVectors;
            eigenVecs.Print();

            SubHeader("Verification: A·v₀  vs  λ₀·v₀");
            Vector v0 = eigenVecs.GetColumn(0);
            double lambda0 = eigenVals[0];
            Console.WriteLine($"  v₀ = {v0}");
            Console.WriteLine($"  λ₀ = {lambda0}");
            Matrix Av0 = A.Multiply(factory.FromVector(v0));
            Console.WriteLine("  A·v₀:");
            Av0.Print();
            Console.WriteLine($"  λ₀·v₀: [{Math.Round(lambda0 * v0[0], 4)}, {Math.Round(lambda0 * v0[1], 4)}]");

            // 3×3 diagonal — eigenvalues trivially the diagonal
            SubHeader("3×3 Diagonal Matrix  (eigenvalues = diagonal entries)");
            double[,] diag3 = { { 5, 0, 0 }, { 0, 3, 0 }, { 0, 0, 1 } };
            Square D = (Square)factory.Create(diag3);
            D.Print();
            Console.WriteLine("  Eigenvalues: " + D.EigenValues);

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 9. MATRIX POWER
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_MatrixPow()
        {
            Header("9. Matrix Power  (A^n)");

            var factory = new MatrixFactory();

            double[,] data = {
                { 1, 1 },
                { 0, 1 }
            };
            Square A = (Square)factory.Create(data);

            SubHeader("Base Matrix A  (upper-triangular Jordan block)");
            A.Print();

            for (int p = 0; p <= 5; p++)
            {
                Console.WriteLine($"  A^{p}:");
                A.Pow(p).Print();
            }

            SubHeader("Negative Power  A^(-1)  (inverse via Pow)");
            double[,] d2 = { { 2, 1 }, { 5, 3 } };
            Square B = (Square)factory.Create(d2);
            Console.WriteLine("  B:");
            B.Print();
            Console.WriteLine("  B^(-1):");
            B.Pow(-1).Print();
            Console.WriteLine("  B^(-2):");
            B.Pow(-2).Print();

            SubHeader("Large Power  A^20 using eigendecomposition");
            double[,] rot = { { 2, 1 }, { 1, 3 } };
            Square C = (Square)factory.Create(rot);
            Console.WriteLine("  C:");
            C.Print();
            Console.WriteLine("  C^20:");
            C.Pow(20).Print();

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 10. RANK AND TRACE
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_RankAndTrace()
        {
            Header("10. Rank and Trace");

            var factory = new MatrixFactory();

            // --- Full-rank 3×3
            double[,] fullRank = {
                { 1, 0, 0 },
                { 0, 2, 0 },
                { 0, 0, 3 }
            };
            Square A = (Square)factory.Create(fullRank);
            SubHeader("Full-Rank Diagonal Matrix");
            A.Print();
            Console.WriteLine($"  Rank  = {A.Rank()}  (expected: 3)");
            Console.WriteLine($"  Trace = {A.Trace()}  (sum of diagonal: 1+2+3 = 6)");

            // --- Rank-2 matrix
            double[,] rank2 = {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            Square B = (Square)factory.Create(rank2);
            SubHeader("Rank-Deficient Matrix (rows linearly dependent)");
            B.Print();
            Console.WriteLine($"  Rank  = {B.Rank()}  (expected: 2)");
            Console.WriteLine($"  Trace = {B.Trace()}  (1+5+9 = 15)");

            // --- Non-square matrix rank
            SubHeader("Non-Square Matrix Rank");
            double[,] nonSq = {
                { 1, 0, 2 },
                { 0, 1, 3 }
            };
            Matrix C = factory.Create(nonSq);
            C.Print();
            Console.WriteLine($"  Rank = {C.Rank()}  (expected: 2)");

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 11. VALIDATION PREDICATES
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_Validations()
        {
            Header("11. Validation Predicates");

            var factory = new MatrixFactory();

            // --- Identity
            Square I = factory.IdentityMatrix(3);
            SubHeader("Identity Matrix Checks");
            I.Print();
            Console.WriteLine($"  IsIdentity     = {I.IsIdentity()}");
            Console.WriteLine($"  IsDiagonal     = {I.IsDiagonal()}");
            Console.WriteLine($"  IsSymmetric    = {I.IsSymmetric()}");
            Console.WriteLine($"  IsOrthogonal   = {I.IsOrthogonal()}");
            Console.WriteLine($"  IsUpperTriangular = {I.IsUpperTriangular()}");
            Console.WriteLine($"  IsLowerTriangular = {I.IsLowerTriangular()}");
            Console.WriteLine($"  IsTriangular   = {I.IsTriangular()}");
            Console.WriteLine($"  IsSingular     = {I.IsSingular()}");
            Console.WriteLine($"  IsInvertible   = {I.IsInvertible()}");

            // --- Symmetric
            double[,] sym = { { 1, 2, 3 }, { 2, 5, 6 }, { 3, 6, 9 } };
            Square S = (Square)factory.Create(sym);
            SubHeader("Symmetric Matrix");
            S.Print();
            Console.WriteLine($"  IsSymmetric = {S.IsSymmetric()}  (A = Aᵀ)");

            // --- Upper triangular
            double[,] upper = { { 1, 2, 3 }, { 0, 4, 5 }, { 0, 0, 6 } };
            Square U = (Square)factory.Create(upper);
            SubHeader("Upper Triangular Matrix");
            U.Print();
            Console.WriteLine($"  IsUpperTriangular = {U.IsUpperTriangular()}");
            Console.WriteLine($"  IsLowerTriangular = {U.IsLowerTriangular()}");

            // --- Lower triangular
            double[,] lower = { { 1, 0, 0 }, { 2, 3, 0 }, { 4, 5, 6 } };
            Square L = (Square)factory.Create(lower);
            SubHeader("Lower Triangular Matrix");
            L.Print();
            Console.WriteLine($"  IsLowerTriangular = {L.IsLowerTriangular()}");
            Console.WriteLine($"  IsUpperTriangular = {L.IsUpperTriangular()}");

            // --- Singular
            double[,] singData = { { 1, 2 }, { 2, 4 } };
            Square sing = (Square)factory.Create(singData);
            SubHeader("Singular Matrix (det = 0)");
            sing.Print();
            Console.WriteLine($"  IsSingular   = {sing.IsSingular()}");
            Console.WriteLine($"  IsInvertible = {sing.IsInvertible()}");

            Pause();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 12. RANDOM MATRICES
        // ─────────────────────────────────────────────────────────────────────────
        static void Demo_RandomMatrices()
        {
            Header("12. Random Matrix Generation");

            var factory = new MatrixFactory();

            SubHeader("4×4 Random Integer Matrix  (range 0–9)");
            Square randInt = (Square)factory.Create(4);
            randInt.FillRandomInt(0, 10);
            randInt.Print();
            Console.WriteLine($"  Trace = {randInt.Trace()}");
            Console.WriteLine($"  Rank  = {randInt.Rank()}");

            SubHeader("3×3 Random Double Matrix  (range 1.0–5.0)");
            Square randDbl = (Square)factory.Create(3);
            randDbl.FillRandomDouble(1, 5);
            randDbl.Print();
            Console.WriteLine($"  Determinant = {Math.Round(randDbl.Det, 4)}");

            SubHeader("3×4 Random Non-Square Matrix");
            Matrix rect = factory.Create(3, 4);
            rect.FillRandomInt(0, 20);
            rect.Print();
            Console.WriteLine($"  Rank = {rect.Rank()}");

            SubHeader("QR Decomposition on a 3×3 Random Matrix");
            Square rndSq = (Square)factory.Create(3);
            rndSq.FillRandomInt(1, 9);
            Console.WriteLine("  Random A:");
            rndSq.Print();
            try
            {
                Square[] qr = rndSq.QR();
                Console.WriteLine("  Q:");
                qr[0].Print();
                Console.WriteLine("  R:");
                qr[1].Print();
                Console.WriteLine("  Q·R:");
                qr[0].Multiply(qr[1]).Print();
            }
            catch (NotInvertibleException ex)
            {
                Console.WriteLine($"  (Singular random matrix — retrying) {ex.Message}");
            }

            Pause();
        }
    }
}
