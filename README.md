# Matrix Library (C#)

A modular and extensible **linear algebra library in C#**, designed with a multi-layered architecture inspired by real-world team-based software engineering.

This project provides both **practical matrix computation tools** and a **learning-oriented environment** for understanding core concepts of linear algebra.

---

## 🧠 Design Philosophy

The system is organized into independent subsystems, each responsible for a specific aspect of matrix computation. This mirrors how complex systems are developed in collaborative environments:

* Clear separation between **data structures and algorithms**
* Modular design allowing independent extension of components
* Strong focus on **correctness, validation, and clarity**
* Suitable for both **educational purposes** and **practical use**

---

## 📚 Learning Resources (Recommended)

If you are new to linear algebra, these resources will help you understand the concepts implemented in this project:

* LU decomposition
* QR decomposition
* Determinant
* Eigenvalues and eigenvectors

These concepts are fundamental in:

* solving systems of equations
* computer graphics
* machine learning
* scientific computing

---

## 🧩 Architecture Overview

The system is divided into four main subsystems:

---

### 🧱 Core & Infrastructure

**Files:**

* `Core.cs`
* `Factory.cs`
* `Fillers.cs`
* `Utilities.cs`

**Responsibilities:**

* Defines the matrix data structure and memory representation
* Handles creation of matrices (empty, identity, custom)
* Provides utilities for initialization and display

**Explanation:**
This layer acts as the **foundation of the entire system**. Every operation depends on how matrices are stored and accessed. Efficient and clean data handling here ensures stability and performance across all other modules.

---

### ➕ Operations Engine

**Files:**

* `Operations.cs`
* `Transformations.cs`

**Responsibilities:**

* Performs arithmetic operations:

  * addition, subtraction, multiplication
* Applies transformations such as transpose

**Explanation:**
This subsystem is the **computational engine** of the library. It implements the fundamental rules of matrix algebra, enabling users to combine and manipulate matrices in meaningful ways.

---

### 🧮 Algebra & Decompositions

**Files:**

* `Algebra.cs`
* `Decompositions.cs`

**Responsibilities:**

* Computes:

  * determinant
  * inverse
  * trace
  * rank
* Performs matrix factorizations (e.g., LU decomposition)

**Explanation:**
This layer contains **advanced mathematical algorithms**. These operations are widely used in solving linear systems, optimization problems, and numerical simulations.

---

### 🛡️ Validation & Runtime Control

**Files:**

* `Validations.cs`
* `MatrixExceptions.cs`
* `LazyHandler.cs`

**Responsibilities:**

* Ensures correctness of operations (dimension checks, constraints)
* Handles errors through custom exceptions
* Optimizes performance using lazy evaluation

**Explanation:**
This subsystem guarantees that all computations are **safe, valid, and efficient**, preventing incorrect usage and improving runtime behavior.

---

## 📦 Features

This library provides a complete set of tools for working with matrices:

### Matrix Creation

* Create empty matrices of any size
* Generate identity matrices
* Initialize matrices with custom or random values

### Arithmetic Operations

* Add and subtract matrices
* Multiply matrices (core operation in linear algebra)
* Multiply matrix by scalar

### Algebraic Computations

* Compute determinant (important for invertibility)
* Compute inverse (used in solving equations)
* Calculate trace and rank

### Transformations

* Transpose matrices (switch rows and columns)

### Decompositions

* LU decomposition for factorizing matrices

### Validation & Safety

* Automatic checks for valid operations
* Custom exception handling for clear error reporting

---

## 🚀 Getting Started

### Prerequisites

* .NET SDK (6.0 or higher recommended)

```
dotnet --version
```

---

### ▶️ Build

```
dotnet build
```

---

### ▶️ Run

```
dotnet run
```

---

## 🧪 Usage Examples (Detailed)

### 1. Creating and initializing matrices

```
var A = Factory.Create(2, 2);
Fillers.Random(A);

var B = Factory.Identity(2);
```

👉 Here:

* `A` becomes a random matrix
* `B` is an identity matrix (acts like 1 in multiplication)

---

### 2. Matrix multiplication

```
var C = Operations.Multiply(A, B);
```

👉 Multiplying by identity keeps the matrix unchanged:

* This verifies correctness of implementation

---

### 3. Determinant

```
double det = Algebra.Determinant(A);
```

👉 The determinant tells:

* if the matrix is invertible
* geometric scaling factor

---

### 4. Inverse

```
var inv = Algebra.Inverse(A);
```

👉 Used to solve systems:

```
Ax = b  →  x = A⁻¹b
```

---

### 5. Transpose

```
var T = Transformations.Transpose(A);
```

👉 Useful in:

* data transformations
* machine learning preprocessing

---

## 🎯 Where This Project Can Be Used

This library is useful in multiple domains:

### 🎓 Education

* Learning linear algebra concepts
* Understanding matrix algorithms step-by-step

### 📊 Data Science

* Matrix-based computations
* Preprocessing datasets

### 🤖 Machine Learning (basic level)

* Vector/matrix transformations
* Feature manipulation

### 🎮 Computer Graphics

* Transformations (rotation, scaling, projection)

### 🧮 Scientific Computing

* Solving systems of equations
* Numerical simulations

---

## 📌 Summary

This project demonstrates a **modular, layered approach to building a linear algebra system**, combining:

* practical implementation
* educational clarity
* scalable architecture

It serves both as a **learning tool** and a **foundation for more advanced numerical software**.

---
