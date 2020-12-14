using System;

namespace Brickwork
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Instance of the Brickwork class, used to store user input and generated result.
                Brickwork brickwork = new Brickwork();

                // Read user input and validate it.
                Input.ReadAndValidate(brickwork);

                // Try to solve the problem.
                if (brickwork.Solve())
                {
                    Output.PrintAsTable(brickwork.Result);
                }
                else
                {
                    Console.WriteLine("-1 - No solution exists!");
                }
            } catch( Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
