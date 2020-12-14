using System;
using System.Collections.Generic;
using System.Linq;

namespace Brickwork
{
    /// <summary>
    /// This class is used to find a solution for the second layer of brickwork.
    /// It stores the input data, a list of possible brick positions and the result.
    /// </summary>
    class Brickwork
    {
        /// <summary>
        /// Inner struct that describes the two halves of a brick. Each member is a tuple of row
        /// and column index of each half of the brick.
        /// </summary>
        struct Brick
        {
            public (int row, int col) first; 
            public (int row, int col) second;

            public static bool operator ==(Brick b1, Brick b2)
            {
                return b1.Equals(b2);
            }

            public static bool operator !=(Brick b1, Brick b2)
            {
                return !b1.Equals(b2);
            }
        }
        /// <summary>
        /// List of possible bricks.
        /// </summary>
        private List<Brick> possibleBricks = new List<Brick>();

        private List<Brick> removedBricks = new List<Brick>();
        /// <summary>
        /// Two dimensional array which stores the user input data.
        /// </summary>
        public int[,] InputData { get; set; }
        /// <summary>
        /// Two dimensional array which stores the second layer of the brickwork.
        /// </summary>
        public int[,] Result { get; set; }
        /// <summary>
        /// Private method that returns if there are empty cells in the result.
        /// </summary>
        /// <returns>True if result has empty cells, false otherwise</returns>
        private bool HasEmptyCells() => Result.Cast<int>().Contains(0);

        /// <summary>
        /// This method iterates the possible brick locations and put them in the result grid.
        /// If the result layout contains empty cells, a new iteration is started, where the first
        /// brick to be pasted is the next possible.
        /// </summary>
        /// <returns></returns>
        private bool FindLayout()
        {
            for( int i = 0; i < possibleBricks.Count; i++)
            {
                int brickNum = 1;
                Result = new int[Result.GetLength(0), Result.GetLength(1)];
                PlaceBrick(possibleBricks[i], ref brickNum);

                for(int j = 0; j < possibleBricks.Count; j++)
                {
                    if (i == j) continue;
                    if(PlaceBrick(possibleBricks[j], ref brickNum) == false)
                    {
                        if (HasEmptyCells() == false)
                            return true;
                    }
                }

                if (HasEmptyCells() == false)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// This method is used to put a brick on the current result map. 
        /// </summary>
        /// <param name="b">The brick to put</param>
        /// <param name="brickNum">The brick's number</param>
        /// <returns>True if brick can be put, false otherwise</returns>
        private bool PlaceBrick(Brick b, ref int brickNum)
        {
            if (Result[b.first.row, b.first.col] == 0 &&
                Result[b.second.row, b.second.col] == 0 )
            {
                Result[b.first.row, b.first.col] = brickNum;
                Result[b.second.row, b.second.col] = brickNum;

                brickNum++;

                return true;
            }

            return false;
        }

        /// <summary>
        /// This method generates possible brick locations by iterating the input data 
        /// and checks for possible brick placement.A possible placement is between two different bricks 
        /// - could be between two horizontal, between horizontal and vertical or between two verticals.
        /// </summary>
        private void GeneratePossibleBricks()
        {
            int rows = InputData.GetLength(0);
            int cols = InputData.GetLength(1);

            Result = new int[rows, cols];

            // Iterate the input data and generate possible brick locations. 
            // It checks for a different brick on top and on the right of the current one.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i > 0 && InputData[i - 1, j] != InputData[i, j])
                    {
                        possibleBricks.Add(new Brick { first = (i - 1, j), second = (i, j) });
                    }
                    if (j < (cols - 1) && InputData[i, j + 1] != InputData[i, j])
                    {
                        possibleBricks.Add(new Brick { first = (i, j + 1), second = (i, j) });
                    }
                }
            }
        }

        /// <summary>
        /// This method solves the Brickwork problem by generating possible brick locations and trying
        /// to find suitable layout from these locations.
        /// 
        /// After all possible brick locations are generated
        /// a map filled with zeroes is created. The possible brick locations are iterated and placed on the map, if
        /// the location is zero. 
        /// </summary>
        /// <returns>true if solution was found, false if not</returns>
        public bool Solve()
        {
            GeneratePossibleBricks();
            return FindLayout();
        }
    }
}
