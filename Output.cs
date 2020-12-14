using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brickwork
{
    /// <summary>
    /// This class is used to print the second layer of the brickwork as a table with borders made from
    /// dash ('-') and asterix ('*').
    /// </summary>
    class Output
    {
        /// <summary>
        /// Private method used to append a horizontal brick. It appends to current line the brick number
        /// twice, separed by three spaces ( two for separation and one for alignment with vertical bricks ).
        /// </summary>
        /// <param name="number">Brick number</param>
        /// <param name="maxWidth">Maximum width a brick number can be</param>
        /// <param name="currLine">Current line to append the tile</param>
        /// <param name="nextLine">The next line to append the lower border</param>
        private static void AppendHorizontalBrick( int number, int maxWidth, 
            StringBuilder currLine, StringBuilder nextLine)
        {
            currLine.Append("* "); 
            nextLine.Append("*-");
            AppendNumber(number, maxWidth, currLine);
            currLine.Append("   ");
            AppendNumber(number, maxWidth, currLine);
            currLine.Append(" ");
            nextLine.Append('-', 2 * maxWidth + 4);
        }

        /// <summary>
        /// Private method used to append half of a vertical brick. It appends to current line the brick number
        /// and the correct border on the next line.
        /// </summary>
        /// <param name="number">Brick number</param>
        /// <param name="maxWidth">Maximum width a brick number can be</param>
        /// <param name="currLine">Current line to append the tile half</param>
        /// <param name="nextLine">The next line to append the lower border</param>
        /// <param name="upper">true if the method is used to print the upper half of a vertical brick, false otherwise</param>
        private static void AppendVerticalHalf( int number, int maxWidth,
            StringBuilder currLine, StringBuilder nextLine, bool upper)
        {
            currLine.Append("* "); 
            nextLine.Append(upper ? "* " : "*-");
            AppendNumber(number, maxWidth, currLine);
            currLine.Append(" ");
            nextLine.Append(upper ? ' ' : '-', maxWidth + 1);

        }

        /// <summary>
        /// Append number to the current line, padding with spaces on the left if the number's length is not
        /// equal to the maximum possible.
        /// </summary>
        /// <param name="number">Brick number</param>
        /// <param name="maxWidth">Maximum width a brick number can be</param>
        /// <param name="currentLine">The line to append the number to</param>
        private static void AppendNumber( int number, int maxWidth, StringBuilder currentLine)
        {
            string numStr = number.ToString();
            int numLen = numStr.Length;
            while (numLen++ < maxWidth)
            {
                currentLine.Append(" ");
            }
            currentLine.Append(numStr);
        }

        /// <summary>
        /// This method is used to append the upper border of the table. It takes the first row of the generated table
        /// and finds all the asterixes, then generate "dash-border" with the asterixes in place.
        /// </summary>
        /// <param name="table">The generated table</param>
        private static void AddTableTopBorder( StringBuilder table)
        {
            string firstLine = table.ToString().Split("\n")[0];
            StringBuilder header = new StringBuilder();
            header.Append('*');

            int asterixIdx = firstLine.IndexOf('*', 1);
            int lastIdx = 0;
            while(asterixIdx != -1)
            {
                header.Append('-', asterixIdx - lastIdx - 1);
                header.Append('*');
                lastIdx = asterixIdx;
                asterixIdx = firstLine.IndexOf('*', lastIdx + 1);
                
            }
            header.Append('\n');
            table.Insert(0, header);
        }

        /// <summary>
        /// Publis static method that is used to print the two dimensional array as a table, where each brick is 
        /// bordered with dashes ('-') and asterixes ('*').
        /// </summary>
        /// <param name="grid">The second layer of the brickwork</param>
        public static void PrintAsTable( int[,] grid)
        {
            StringBuilder table = new StringBuilder();
            StringBuilder currentLine = new StringBuilder();
            StringBuilder nextLine = new StringBuilder();
            
            int maxWidth = grid.Cast<int>().Max().ToString().Length;
            for( int row = 0; row < grid.GetLength(0); row++)
            {
                currentLine.Clear();
                nextLine.Clear();
                for( int col = 0; col < grid.GetLength(1); col++)
                {
                    // Check if cell is the upper part of a vertical brick
                    if(row < grid.GetLength(0) - 1 && (grid[row,col] == grid[row+1,col]))
                    {
                        AppendVerticalHalf(grid[row, col], maxWidth, currentLine, nextLine, true);
                    }
                    // Check if cell is the lower part of a vertical brick
                    else if (row > 0 && (grid[row,col] == grid[row - 1, col]))
                    {
                        AppendVerticalHalf(grid[row, col], maxWidth, currentLine, nextLine, false);
                    }
                    // Check if cell is the part of a horizontal brick
                    else if (col < grid.GetLength(1) - 1 && (grid[row,col] == grid[row, col + 1]))
                    {
                        AppendHorizontalBrick(grid[row, col], maxWidth, currentLine, nextLine);
                        // skip next number, AppendHorizontalBrick has appended it
                        col++;  
                    }
                }

                // Append end of line border
                currentLine.Append("*\n");
                nextLine.Append("*\n");

                // Append generated lines to table
                table.Append(currentLine);
                table.Append(nextLine);
            }

            // Add the table top border
            AddTableTopBorder(table);

            Console.Write(table.ToString());
        }
    }
}
