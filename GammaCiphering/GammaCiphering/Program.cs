using System.Text;

namespace GammaCiphering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PseudoRandomNumberGenerator prng = new(67, 97, 137, 197);

            while (true)
            {
                Console.WriteLine("Выберите операцию: \n1. Шифрование \n2. Расшифрование \n3. Выход");
                string? choice = Console.ReadLine();

                if (choice == "3")
                    break;

                if (choice != "1" && choice != "2")
                    continue;

                if (choice == "1")
                {
                    Console.WriteLine("Введите текст (пустая строка для завершения):");
                    StringBuilder sb = new StringBuilder();
                    string? line;
                    while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                    {
                        sb.AppendLine(line);
                    }
                    string message = sb.ToString().TrimEnd('\r', '\n');

                    if (string.IsNullOrEmpty(message))
                        continue;

                    byte[] encrypted = EncryptWithGamma(message, prng);
                    string base64 = Convert.ToBase64String(encrypted);
                    Console.WriteLine($"Зашифровано (Base64): {base64}");
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Введите Base64 строку:");
                    string? base64 = Console.ReadLine();

                    if (string.IsNullOrEmpty(base64))
                        continue;

                    try
                    {
                        byte[] toDecrypt = Convert.FromBase64String(base64);
                        string decrypted = DecryptWithGamma(toDecrypt, prng);
                        Console.WriteLine($"Расшифровано:\n{decrypted}");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: введите корректную Base64 строку");
                    }
                }

                Console.WriteLine();
            }
        }

        static byte[] EncryptWithGamma(string text, PseudoRandomNumberGenerator prng)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] result = new byte[textBytes.Length];

            prng.Reset();

            for (int i = 0; i < textBytes.Length; i++)
            {
                byte gamma = (byte)prng.GetRandomNumber();
                result[i] = (byte)(textBytes[i] ^ gamma);
            }

            return result;
        }

        static string DecryptWithGamma(byte[] encryptedBytes, PseudoRandomNumberGenerator prng)
        {
            byte[] result = new byte[encryptedBytes.Length];

            prng.Reset();

            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                byte gamma = (byte)prng.GetRandomNumber();
                result[i] = (byte)(encryptedBytes[i] ^ gamma);
            }

            return Encoding.UTF8.GetString(result);
        }
    }
}