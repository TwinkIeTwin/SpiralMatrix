using System;

namespace MatrixManipulations
{
    /// <summary>
    /// Matrix manipulation.
    /// </summary>
    public static class MatrixExtension
    {
        private enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        /// <summary>
        /// contains method for define is result of condition true or not.
        /// </summary>
        private interface IPredicate
        {
            /// <summary>
            /// define is result of condition true or not.
            /// </summary>
            /// <param name="x"> position x in matrix.</param>
            /// <param name="y"> position y in matrix.</param>
            /// <param name="borderLeft">position which x can not be less than.</param>
            /// <param name="borderRight">position which x can not be more than.</param>
            /// <param name="borderUp">position which y can not be less than. </param>
            /// <param name="borderDown">position which y can not be more than.</param>
            /// <returns>true if all variables are in defined conditions, otherwise false.</returns>
            bool IsTrue(int x, int y, int borderLeft, int borderRight, int borderUp, int borderDown);
        }

        /// <summary>
        /// Method fills the matrix with natural numbers, starting from 1 in the top-left corner,
        /// increasing in an inward, clockwise spiral order.
        /// </summary>
        /// <param name="size">Matrix order.</param>
        /// <returns>Filled matrix.</returns>
        /// <exception cref="ArgumentException">Throw ArgumentException, if matrix order less or equal zero.</exception>
        /// <example> size = 3
        ///     1 2 3
        ///     8 9 4
        ///     7 6 5.
        /// </example>
        /// <example> size = 4
        ///     1  2  3  4
        ///     12 13 14 5
        ///     11 16 15 6
        ///     10 9  8  7.
        /// </example>
        /// <exception cref="ArgumentException">
        /// thrown when size is equal or less than zero.
        /// </exception>
        public static int[,] GetMatrix(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("can not be less or equal zero.", nameof(size));
            }

            int[,] array = new int[size, size];

            int countOfElements = size * size;
            int x = 0, y = 0;
            int borderDown = size, borderUp = 0, borderLeft = 0, borderRight = size;
            int countOfFiled = 0;
            while (countOfFiled < countOfElements)
            {
                FillByDirectionUntil(GetDirection(Direction.Right), new ConditionXIncrements());
                borderUp++;
                x--;
                y++;
                FillByDirectionUntil(GetDirection(Direction.Down), new ConditionYIncrements());
                borderDown--;
                x--;
                y--;
                FillByDirectionUntil(GetDirection(Direction.Left), new ConditionXDecrements());
                borderLeft++;
                x++;
                y--;
                FillByDirectionUntil(GetDirection(Direction.Up), new ConditionYDecrements());
                borderRight--;
                x++;
                y++;
            }

            void FillByDirectionUntil((int dx, int dy) direction, IPredicate predicate)
            {
                for (; predicate.IsTrue(x, y, borderLeft, borderRight, borderUp, borderDown); x += direction.dx, y += direction.dy)
                {
                    array[y, x] = ++countOfFiled;
                }
            }

            return array;
        }

        private static (int x, int y) GetDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Right => (1, 0),
                Direction.Left => (-1, 0),
                Direction.Up => (0, -1),
                Direction.Down => (0, 1),
                _ => (0, 0),
            };
        }

        /// <summary>
        /// define result of condition for decrementing x.
        /// </summary>
        private class ConditionXIncrements : IPredicate
        {
            /// <summary>
            /// define is result of condition true or not.
            /// </summary>
            /// <param name="x"> position x in matrix.</param>
            /// <param name="y"> position y in matrix.</param>
            /// <param name="borderLeft">position which x can not be less than.</param>
            /// <param name="borderRight">position which x can not be more than.</param>
            /// <param name="borderUp">position which y can not be less than. </param>
            /// <param name="borderDown">position which y can not be more than.</param>
            /// <returns>true if x is less than borderRight, otherwise false.</returns>
            public bool IsTrue(int x, int y, int borderLeft, int borderRight, int borderUp, int borderDown)
            {
                return x < borderRight;
            }
        }

        /// <summary>
        /// define result of condition for incrementing y.
        /// </summary>
        private class ConditionYIncrements : IPredicate
        {
            /// <summary>
            /// define is result of condition true or not.
            /// </summary>
            /// <param name="x"> position x in matrix.</param>
            /// <param name="y"> position y in matrix.</param>
            /// <param name="borderLeft">position which x can not be less than.</param>
            /// <param name="borderRight">position which x can not be more than.</param>
            /// <param name="borderUp">position which y can not be less than. </param>
            /// <param name="borderDown">position which y can not be more than.</param>
            /// <returns>true if all y is less than borderDown, otherwise false.</returns>
            public bool IsTrue(int x, int y, int borderLeft, int borderRight, int borderUp, int borderDown)
            {
                return y < borderDown;
            }
        }

        /// <summary>
        /// define result of condition for decrementing x.
        /// </summary>
        private class ConditionXDecrements : IPredicate
        {
            /// <summary>
            /// define is result of condition true or not.
            /// </summary>
            /// <param name="x"> position x in matrix.</param>
            /// <param name="y"> position y in matrix.</param>
            /// <param name="borderLeft">position which x can not be less than.</param>
            /// <param name="borderRight">position which x can not be more than.</param>
            /// <param name="borderUp">position which y can not be less than. </param>
            /// <param name="borderDown">position which y can not be more than.</param>
            /// <returns>true if all x is more or equal than borderLeft, otherwise false.</returns>
            public bool IsTrue(int x, int y, int borderLeft, int borderRight, int borderUp, int borderDown)
            {
                return x >= borderLeft;
            }
        }

        /// <summary>
        /// define result of condition for decrementing y.
        /// </summary>
        private class ConditionYDecrements : IPredicate
        {
            /// <summary>
            /// define is result of condition true or not.
            /// </summary>
            /// <param name="x"> position x in matrix.</param>
            /// <param name="y"> position y in matrix.</param>
            /// <param name="borderLeft">position which x can not be less than.</param>
            /// <param name="borderRight">position which x can not be more than.</param>
            /// <param name="borderUp">position which y can not be less than. </param>
            /// <param name="borderDown">position which y can not be more than.</param>
            /// <returns>true if y is more or equal borderUp, otherwise false.</returns>
            public bool IsTrue(int x, int y, int borderLeft, int borderRight, int borderUp, int borderDown)
            {
                return y >= borderUp;
            }
        }
    }
}