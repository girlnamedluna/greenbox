using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace SoftYellowDEC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your key: ");
            string key = Console.ReadLine();
            Console.Clear();

            string folderNameData2 = "GreenBoxExport";
            string fileName2 = "GreenBoxExport.txt";
            string directoryPath2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderNameData2);
            string filePath2 = Path.Combine(directoryPath2, fileName2);

            byte[] encryptedBytes = File.ReadAllBytes(filePath2);
            string decryptedText = DecryptedStringFromBytes_Aes(encryptedBytes, key);
            Console.WriteLine("Crypted text is: " +  decryptedText);
        }
        private static string DecryptedStringFromBytes_Aes(byte[] cipherText, string key)
        {
            string folderNameData2 = "GreenBoxExport";
            string fileName2 = "GreenBoxExport.txt";
            string directoryPath2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderNameData2);
            string filePath2 = Path.Combine(directoryPath2, fileName2);
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    plaintext = srDecrypt.ReadToEnd();
                }
                Console.WriteLine("File successfully decrypted! Please press Enter to close the program");
                Console.ReadLine();
            }
            File.WriteAllText(filePath2, plaintext);
            return plaintext;
        }
    }
}
