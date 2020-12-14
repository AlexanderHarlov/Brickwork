using System;
using System.Linq;

namespace Brickwork
{
    /// <summary>
    /// This class is used for user input and validation of that input.
    /// It throws InvalidOperationException when the user's input is not valid.
    /// </summary>
    class Input
    {
        /// <summary>
        /// Static method that reads input from the console. It receives an instance 
        /// of the Brickwork class and fill it's InputData property. This method assumes
        /// that bricks will be labeled from 1 to (M*N)/2, and uses that to calculate expected sum.
        /// If the sum from the user input is different, that means that a brick spawns more than 2
        /// rows or columns,or that bricks are not labeled from 1.
        /// </summary>
        /// <param name="brickwork">Its InputData property will be filled</param>
        public static void ReadAndValidate(Brickwork brickwork)
        {
            string[] dim = Console.ReadLine().Trim().Split(" ");
            if( dim.Length != 2 )
                throw new InvalidOperationException("Input should be only M and N");

            int m = Convert.ToInt32(dim[0]);
            int n = Convert.ToInt32(dim[1]);

            if (m % 2 != 0 || n % 2 != 0)
                throw new InvalidOperationException("M and N must be even");
            if( m > 100 || n > 100 )
                throw new InvalidOperationException("M and N should not exceed 100");

            int[,] grid = new int[m, n];
            int calcSum = 0;

            for (int i = 0; i < m; i++)
            {
                string[] line = Console.ReadLine().Trim().Split(" ");

                if (line.Length != n)
                    throw new InvalidOperationException($"Invalid number of bricks on row {i}");

                for (int j = 0; j < n; j++)
                {
                    grid[i, j] = Convert.ToInt32(line[j]);
                    calcSum += grid[i, j];      // Accumulate sum
                }
            }

            int brickCount = (m * n) / 2;
            // Calculate expected sum using range generation
            int expectedSum = Enumerable.Range(1, brickCount).Sum() * 2;

            if( calcSum != expectedSum )
                throw new InvalidOperationException("Invalid bricks. Bricks should be 1x2 in size, " +
                    "and numbered from 1");

            brickwork.InputData = grid;
        }
    }
}
