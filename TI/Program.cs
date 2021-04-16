using System;

namespace TI
{
    class Program
    {
        static void Main(string[] args)
        {
            int num;
            string userChoice;

            do
            {
                Console.WriteLine("Select the cipher: ");
                Console.WriteLine("1: Rail Fence");
                Console.WriteLine("2: Columnar Transposition");
                Console.WriteLine("3: Rotating Matrix");
                Console.WriteLine("4: Vigenere Cipher");
                Console.WriteLine("5: Quit");
                Console.Write("Enter the number of your choice: ");
                userChoice = Console.ReadLine();

                if(!Int32.TryParse(userChoice, out num)) continue;

                if (userChoice == "5")
                {
                    Environment.Exit(0);
                }

                Console.WriteLine("Choice = " + userChoice);

                if(userChoice == "1")
                {
                    RailFenceCipher.CallRail();
                }

                if(userChoice == "2")
                {
                    ColumnarTranspositionCipher.CallColumnar();
                }

                if(userChoice == "3")
                {
                    RotatingGrillCipher.CallRotateGrill();
                } 

                if(userChoice == "4")
                {
                    VigenereCipher.CallVigenere();
                }

            } while (true);
        }
    }
}
