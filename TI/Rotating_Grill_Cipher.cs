using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TI
{
    public static class RotatingGrillCipher
    {
        private static char[,] charMatrix;

        public static void CallRotateGrill()
        {
            Console.WriteLine("\nRotating lattice method: ");
            Console.WriteLine("\nEnter the text (string): ");
            string text = Console.ReadLine();
            Console.WriteLine("\nEnter the even grid size: ");
            var scanner = Console.ReadLine();
            int n = int.Parse(scanner ?? string.Empty);
            bool flag = true;
            while (flag)
            {
                if (n % 2 != 0 || n * n < text.Length)
                {
                    Console.WriteLine("Data entered incorrectly!\n Try again: ");
                }
                else
                {
                    flag = false;
                    String upperText = text.ToUpperInvariant();
                    upperText = upperText.Replace(" ", "");
                    int[,] lattice = LatticeInput(n);

                    String encryption = RotatingCipherEncrypt(upperText, n, lattice);
                    Console.WriteLine("Encryption: " + encryption);
                    Console.WriteLine("Decryption: " + RotatingCipherDecrypt(encryption, n, lattice));
                }
            }
        }

        private static int[,] RotateMatrix(int[,] matrix)
        {
            int SIDE = matrix.GetLength(0);
            int[,] resMatrix = new int[SIDE, SIDE];

            for (int i = 0; i < SIDE; i++)
            {
                for (int j = 0; j < SIDE; j++)
                {
                    resMatrix[i, j] = matrix[SIDE - j - 1, i];
                }
            }

            return resMatrix;
        }

        private static char[,] RotateMatrix(char[,] matrix)
        {
            int SIDE = matrix.Length;
            char[,] resMatrix = new char[SIDE, SIDE];

            for (int i = 0; i < SIDE; i++)
            {
                for (int j = 0; j < SIDE; j++)
                {
                    resMatrix[i, j] = matrix[SIDE - j - 1, i];
                }
            }

            return resMatrix;
        }

        private static int[,] LatticeInput(int n)
        {
            int[,] boolLattice = new int[n, n];
            int[,] numbLattice = new int[n, n];

            for (int i = 0; i < n; i++) 
            {
                for (int j = 0; j < n; j++)
                {
                    boolLattice[i, j] = 0;
                    numbLattice[i, j] = 0;
                }
            }

            for (int turnCount = 0; turnCount < 4; turnCount++)
            {
                int number = 1;
                for (int i = 0; i < n / 2; i++)
                {
                    for (int j = 0; j < n / 2; j++)
                    {
                        numbLattice[i, j] = number;
                        number++;
                    }
                }

                numbLattice = RotateMatrix(numbLattice);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(numbLattice[i, j] + " ");
                }

                Console.WriteLine();
            }

            for (int i = 0; i < (n / 2) * (n / 2); i++)
            {
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Enter hole coordinates for " + (i + 1) + ":");
                    Console.Write("x: ");
                    int x = int.Parse(Console.ReadLine());
                    Console.Write("y: ");
                    int y = int.Parse(Console.ReadLine());
                    if (numbLattice[x, y] == (i + 1))
                    {
                        boolLattice[x, y] = 1;
                        flag = false;
                    }
                    else
                    {
                        Console.WriteLine("Data entered incorrectly!");
                    }
                }
            }

            return boolLattice;
        }

        private static String RotatingCipherEncrypt(String text, int n, int[,] lattice)
        {
            charMatrix = new char[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) charMatrix[i, j] = (char) (RandomNumberGenerator.GetInt32(26) + 'A');
            }

            int index = 0;

            for (int turnCount = 0; turnCount < 4; turnCount++)
            {
                if (index <= text.Length)
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (lattice[i, j] == 1)
                            {
                                if (index < text.Length)
                                {
                                    charMatrix[i, j] = text[index];
                                }

                                if (index == text.Length)
                                {
                                    charMatrix[i, j] = '0';
                                }

                                index++;
                            }
                        }
                    }
                }

                lattice = RotateMatrix(lattice); 
            }

            String resultText = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) resultText = resultText + charMatrix[i, j];
            }

            return resultText;
        }

        private static String RotatingCipherDecrypt(String text, int n, int[,] lattice)
        {
            charMatrix = new char[n, n];

            int index = 0;
            for (int i = 0; i < n; i++) 
            {
                for (int j = 0; j < n; j++)
                {
                    charMatrix[i, j] = text[index];
                    index++;
                }
            }

            String resultText = "";

            for (int turnCount = 0; turnCount < 4; turnCount++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (lattice[i, j] == 1)
                        {
                            resultText = resultText + charMatrix[i, j];
                        }
                    }
                }

                lattice = RotateMatrix(lattice); 
            }

            int indOfZero = resultText.IndexOf('0');
            if (indOfZero != -1)
            {
                return resultText.Substring(0, resultText.IndexOf('0'));
            }
            else
            {
                return resultText;
            }
        }
    }
}