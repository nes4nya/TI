using System;

namespace TI
{
    public static class RailFenceCipher
    {
        public static void CallRail()
        {
            Console.WriteLine("\nRail Fence method: ");
            Console.WriteLine("\nString: ");
            string str = Console.ReadLine();
            Console.WriteLine("\nKey(number): ");
            var key = int.Parse(Console.ReadLine());
            str = str.ToUpperInvariant();
            string encryption = EncryptRailFence(str, key);
            Console.WriteLine("Encryption: " + encryption);
            Console.WriteLine("Decryption: " + DecryptRailFence(encryption, key));
        }

        private static string EncryptRailFence(string text, int key)
        {
            text = text.ToUpperInvariant();
            while (text.IndexOf(' ') != -1)
            {
                text = text.Remove(text.IndexOf(' '), 1);
            }

            var rail = new char[key, text.Length];

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < text.Length; j++)
                {
                    rail[i, j] = '\n';
                }
            }

            bool dirDown = false;
            int row = 0, col = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (row == 0 || row == key - 1) dirDown = !dirDown;

                rail[row, col++] = text[i];

                if (dirDown)
                {
                    row++;
                }
                else
                {
                    row--;
                }
            }

            string result = string.Empty;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < text.Length; j++)
                {
                    if (rail[i, j] != '\n')
                    {
                        result = result.Insert(result.Length, rail[i, j].ToString());
                    }
                }
            }

            return result;
        }

        private static string DecryptRailFence(string cipher, int key)
        {
            cipher = cipher.ToUpperInvariant();

            var rail = new char[key, cipher.Length];

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < cipher.Length; j++)
                {
                    rail[i, j] = '\n';
                }
            }

            bool dirDown = false;

            int row = 0, col = 0;

            for (int i = 0; i < cipher.Length; i++)
            {
                if (row == 0) dirDown = true;
                if (row == key - 1) dirDown = false;

                rail[row, col++] = '*';

                if (dirDown)
                {
                    row++;
                }
                else
                {
                    row--;
                }
            }

            int index = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < cipher.Length; j++)
                {
                    if (rail[i, j] == '*' && index < cipher.Length)
                    {
                        rail[i, j] = cipher[index++];
                    }
                }
            }

            string result = string.Empty;

            row = 0;
            col = 0;
            for (int i = 0; i < cipher.Length; i++)
            {
                if (row == 0) dirDown = true;

                if (row == key - 1) dirDown = false;

                if (rail[row, col] != '*') result = result.Insert(result.Length, rail[row, col++].ToString());

                if (dirDown)
                {
                    row++;
                }
                else
                {
                    row--;
                }
            }

            return result;
        }
    }
}