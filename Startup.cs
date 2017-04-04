namespace ClockwiseMatrix
{
    using System;
    using System.Text;

    public class Startup
    {
        public static Position currPosition = new Position(0, 0);
        public static Position newPosition;

        public static ClockwiseDirection currDirection = ClockwiseDirection.DiagonalDownRight;
        public static int indexOfCurrDirection = (int)currDirection;

        public static int[,] matrix;
        public static int currFillNumber = 1;

        public static void Main()
        {
            int rowsAndCols = int.Parse(Console.ReadLine());

            int rows = rowsAndCols;
            int cols = rowsAndCols;

            matrix = new int[rows, cols];

            Constants.MaxValueOfTheFillNumber = (rows * cols) + 1;

            newPosition = Position.GetNewPosition(indexOfCurrDirection);

            FillTheMatrix();

            PrintTheMatrix();
        }

        private static void PrintTheMatrix()
        {
            StringBuilder result = new StringBuilder();
            for (int currRow = 0; currRow < matrix.GetLength(0); currRow++)
            {
                for (int currCol = 0; currCol < matrix.GetLength(1); currCol++)
                {
                    int currNumber = matrix[currRow, currCol];

                    result.Append(currNumber.ToString().PadRight(4));
                }

                result.AppendLine();
            }

            Console.Write(result.ToString());
        }

        private static void FillTheMatrix()
        {
            ContinueFilling:
            while (PositionIsValid(currPosition))
            {
                matrix[currPosition.Row, currPosition.Col] = currFillNumber;
                currFillNumber++;

                IncrementPositionsRowAndCol(currPosition, newPosition);
            }

            DecrementPositionRowAndCol(currPosition, newPosition);

            if (currFillNumber == Constants.MaxValueOfTheFillNumber)
            {
                return;
            }

            ChangeTheDirection();

            goto ContinueFilling;
        }

        private static void ChangeTheDirection()
        {
            bool foundEmptyPosition = false;

            TryToChangeDirectionAgain:

            int indexOfNewDirection = indexOfCurrDirection;
            Position tempPosition = new Position(currPosition.Row, currPosition.Col);
            indexOfNewDirection++;
            int indexToGetDirection = CalculateDirection(indexOfNewDirection);

            Position tempNewPosition = Position.GetNewPosition(indexToGetDirection);

            IncrementPositionsRowAndCol(tempPosition, tempNewPosition);

            bool foundValidDirection = false;
            while (!PositionsAreTheSame(tempNewPosition, newPosition))
            {
                if (PositionIsValid(tempPosition))
                {
                    foundValidDirection = true;

                    break;
                }

                indexOfNewDirection++;
                indexToGetDirection = CalculateDirection(indexOfNewDirection);

                SetPositionRowAndCol(tempPosition, currPosition);

                tempNewPosition = Position.GetNewPosition(indexToGetDirection);

                IncrementPositionsRowAndCol(tempPosition, tempNewPosition);
            }

            if (foundValidDirection)
            {
                indexOfCurrDirection = indexOfNewDirection;
                SetPositionRowAndCol(newPosition, tempNewPosition);

                if (!foundEmptyPosition)
                {
                    SetPositionRowAndCol(currPosition, tempPosition);
                }

                return;
            }

            foundEmptyPosition = GetTheFirstEmptyPositionPossible(currPosition);

            if (!foundEmptyPosition)
            {
                return;
            }

            goto TryToChangeDirectionAgain;
        }

        private static bool GetTheFirstEmptyPositionPossible(Position position)
        {
            bool foundEmptyPosition = false;

            for (int currRow = 0; currRow < matrix.GetLength(0); currRow++)
            {
                for (int currCol = 0; currCol < matrix.GetLength(1); currCol++)
                {
                    if (matrix[currRow, currCol] == 0)
                    {
                        foundEmptyPosition = true;

                        SetPositionRowAndCol(position, new Position(currRow, currCol));

                        break;
                    }
                }

                if (foundEmptyPosition)
                {
                    break;
                }
            }

            return foundEmptyPosition;
        }

        private static void IncrementPositionsRowAndCol(Position toIncrement, Position toIncrementWith)
        {
            toIncrement.Row += toIncrementWith.Row;
            toIncrement.Col += toIncrementWith.Col;
        }

        private static void SetPositionRowAndCol(Position toBeSet, Position toSetWith)
        {
            toBeSet.Row = toSetWith.Row;
            toBeSet.Col = toSetWith.Col;
        }

        private static void DecrementPositionRowAndCol(Position toDecrement, Position toDecrementWith)
        {
            toDecrement.Row -= toDecrementWith.Row;
            toDecrement.Col -= toDecrementWith.Col;
        }

        private static int CalculateDirection(int indexOfDirection)
        {
            return indexOfDirection % Constants.TotalCountOfDirections;
        }

        private static bool PositionIsValid(Position position)
        {
            bool rowsIsValid = (position.Row >= 0 && position.Row < matrix.GetLength(0));
            bool colIsValid = (position.Col >= 0 && position.Col < matrix.GetLength(1));

            bool positionIsValid = rowsIsValid && colIsValid && matrix[position.Row, position.Col] == 0;

            return positionIsValid;
        }

        private static bool PositionsAreTheSame(Position toBeCompared, Position toCompareWith)
        {
            bool positionsAreTheSame = false;

            if (toBeCompared.Row == toCompareWith.Row && toBeCompared.Col == toCompareWith.Col)
            {
                positionsAreTheSame = true;
            }

            return positionsAreTheSame;
        }
    }
}
