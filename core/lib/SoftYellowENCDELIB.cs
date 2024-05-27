using System;
using System.Collections.Generic;
using System.Linq;
using GreenLib;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SoftYellowENCDELIB
{
    public class Encryptor
    {
        public static void KeyRun()
        {
            string key = Generate256BitKey();
            Console.WriteLine("Your GreenBox Encryption key is: " + key + "\nPress Enter to continue");
            Console.WriteLine("Write this key down physically for maximum security, but a plaintext file on your desktop could be okay.");
            textHelp.ReadClear();
            Console.WriteLine("Do not lose your key, you will lose your data with it. Press Enter to continue");

            string fileNameData = "GreenBoxExport.txt";
            string folderNameData = "GreenBoxExport";

            Console.WriteLine("**FILE NAME**\r\n");
            Console.WriteLine(fileNameData + "\r\n");
            Console.WriteLine("\r\n");
            string fileName = fileNameData;
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderNameData);
            bool exists = System.IO.Directory.Exists(directoryPath);
            if (!exists)
            {
                Console.WriteLine("ERROR: It seems this directory doesn't exist!\nIf you haven't used GreenBox to generate your password, this will not work :(");
                Console.WriteLine("Please message girlnamedluna if you have issues troubleshooting GreenBox");
                Console.ReadLine();
                // This text will only be shown if the directory doesn't exist, but will not create it for the user.
            }
            string filePath = Path.Combine(directoryPath, fileName);



            string folderNameData2 = "GreenBoxExport";
            string fileName2 = "GreenBoxExport.txt";
            string directoryPath2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderNameData2);
            string filePath2 = Path.Combine(directoryPath2, fileName2);
            Directory.CreateDirectory(directoryPath2);
            // Folder created on current user's desktop titled "SOFTYELLOW TEST" with a text document titled "SOFTYELLOW TEST.txt"
            // Inside the .txt is the encrypted code from File.WriteAllBytes(filePath2, encryptedBytes);


            string readText = File.ReadAllText(filePath); // ! readText !
            Console.WriteLine("**FILE CONTENT**\r\n");
            Console.WriteLine(readText);
            Console.WriteLine("\r\n");
            Console.WriteLine("Press Enter to continue");
            textHelp.ReadClear();



            byte[] encryptedBytes = EncryptStringToBytes_Aes(readText, key);
            File.WriteAllBytes(filePath2, encryptedBytes);
            Console.WriteLine("Text Encryption loaded successfully! Do not lose your key! Write it down!");
            Console.WriteLine("Previous data encrypted, you can only get it back with your key. Do not lose your key. Press Enter to view your key once more.");
            Console.WriteLine("\r\nPress Enter to continue and to view your key once more. (This is needed for encryption)");
            textHelp.ReadClear();
            Console.WriteLine("Your key is: " + key + "\nPress Enter to end and to encrypt.");
            Console.ReadLine();
        }
        private static string Generate256BitKey()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 256; // Set key size to 256 bits
                aesAlg.GenerateKey(); // Generate a random 256-bit key
                return Convert.ToBase64String(aesAlg.Key);
            }
        }

        private static byte[] EncryptStringToBytes_Aes(string plainText, string key)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8]; // IV size is same as block size
                aesAlg.Mode = CipherMode.CBC; // Set mode to CBC
                aesAlg.Padding = PaddingMode.PKCS7; // Use PKCS7 padding

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }
    }
}
