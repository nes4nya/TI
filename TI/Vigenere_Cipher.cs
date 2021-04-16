using System;
using System.Collections.Generic;
using System.Text;

namespace TI
{
    public static class VigenereCipher
    {

        public static void CallVigenere()
        {
            Console.WriteLine("\nVigenere method: ");
            Console.WriteLine("\nString: ");
            string str = Console.ReadLine();
            Console.WriteLine("\nKey(string): ");
            string key = Console.ReadLine();
            str = str.ToUpperInvariant();
            key = key.ToUpperInvariant();
            key = GenerateKey(str, key);
            string encryption = CipherText(str, key);
            Console.WriteLine("Encryption: " + encryption);
            Console.WriteLine("Decryption: " + OriginalText(encryption, key));
        }
        private static String GenerateKey(String str, String key)
        {
            int x = str.Length;

            for (int i = 0;; i++)
            {
                if (x == i) i = 0;
                if (key.Length == str.Length) break;
                key += (key[i]);
            }

            return key;
        }

        private static String CipherText(String str, String key)
        {
            String cipher_text = "";

            for (int i = 0; i < str.Length; i++)
            {
                int x = (str[i] + key[i]) % 26;

                x += 'A';

                if (i != 0)
                {
                    cipher_text += (char) (x);
                }
                else
                {
                    cipher_text += (char) (x - 2);
                }
            }

            return cipher_text;
        }

        private static String OriginalText(String cipher_text, String key)
        {
            String orig_text = "";

            for (int i = 0; i < cipher_text.Length && i < key.Length; i++)
            {
                int x = (cipher_text[i] - key[i] + 26) % 26;

                x += 'A';

                if (i != 0)
                {
                    orig_text += (char) (x);
                }
                else
                {
                    orig_text += (char) (x + 2);
                }
            }

            return orig_text;
        }
    }
}