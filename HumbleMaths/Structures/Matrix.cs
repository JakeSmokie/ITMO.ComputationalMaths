using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HumbleMaths.Structures {
    public class Matrix<T> : IEnumerable<T>, IEquatable<Matrix<T>>, ICloneable {
        public const int MaxWidth = 21;
        public const int MaxHeight = 20;

        private readonly T[,] _matrix;

        /// <exception cref="ArgumentException">Wrong values of width or height</exception>
        public Matrix(int width, int height)
        {
            if (IsSizeIncorrect(width, height)) {
                throw new ArgumentException("Wrong values of width or height");
            }

            _matrix = new T[height, width];
        }

        /// <exception cref="ArgumentException">Array cannot be null</exception>
        public Matrix(T[,] array)
        {
            _matrix = array?.Clone() as T[,] ??
                      throw new ArgumentException("Array cannot be null");
        }

        /// <summary>
        ///     Gets element from matrix by row and column numbers
        /// </summary>
        /// <param name="i">Row number (starts from 0)</param>
        /// <param name="j">Column number (starts from 0)</param>
        /// <returns>Seeked element</returns>
        public T this[int i, int j] {
            get { return _matrix[i, j]; }
            set { _matrix[i, j] = value; }
        }

        public int Width {
            get { return _matrix.GetLength(1); }
        }

        public int Height {
            get { return _matrix.GetLength(0); }
        }

        public IEnumerable<T> MainDiagonalElements {
            get {
                return Enumerable.Range(0, Height)
                    .Select(x => _matrix[x, x]);
            }
        }

        public object Clone()
        {
            return new Matrix<T>(_matrix.Clone() as T[,]);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _matrix.OfType<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(Matrix<T> other)
        {
            if (other is null) {
                return false;
            }

            return ReferenceEquals(this, other) || Equals(_matrix, other._matrix);
        }

        public Matrix<T> CloneMatrix()
        {
            return (Matrix<T>) Clone();
        }

        /// <summary>
        ///     Returns cloned instance of array as matrix representation
        /// </summary>
        public T[,] GetArray()
        {
            return (T[,]) _matrix.Clone();
        }

        private static bool IsSizeIncorrect(int width, int height)
        {
            return width <= 0 || height <= 0 || width > MaxWidth || height > MaxHeight;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Matrix<T>) obj);
        }

        public override int GetHashCode()
        {
            return _matrix?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Matrix<T> left, Matrix<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Matrix<T> left, Matrix<T> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            var s = "";

            for (var i = 0; i < Height; i++) {
                var rowElements = _matrix.OfType<double>()
                    .Skip(i * Width)
                    .Take(Width);

                s += string.Join(" ", rowElements) + " \n";
            }

            return s;
        }

        /// <summary>
        ///     Gets row via index starting from 0
        /// </summary>
        public IEnumerable<T> GetRow(int row)
        {
            for (var i = 0; i < Width; i++) {
                yield return _matrix[row, i];
            }
        }

        /// <summary>
        ///     Gets column via index starting from 0
        /// </summary>
        public IEnumerable<T> GetColumn(int column)
        {
            for (var i = 0; i < Height; i++) {
                yield return _matrix[i, column];
            }
        }

        /// <summary>
        ///     Gets element of main diagonal via index starting from 0
        /// </summary>
        public T GetMainDiagonalElement(int index)
        {
            return this[index, index];
        }

        /// <summary>
        ///     Creates a clone of matrix
        ///     and swaps rows in cloned matrix
        /// </summary>
        public void SwapRows(int src, int dest)
        {
            for (var i = 0; i < Width; i++) {
                (_matrix[src, i], _matrix[dest, i]) =
                    (_matrix[dest, i], _matrix[src, i]);
            }
        }
    }
}