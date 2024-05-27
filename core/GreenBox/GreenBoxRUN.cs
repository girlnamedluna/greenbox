using System;
using GreenLib;
using SoftYellowENCDELIB;
using System.Security.Cryptography;

namespace GreenBox
{
    class GreenBox
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Press Enter to generate password");
            Console.ReadLine();
            Console.Clear();
            string biasedKey = KeyGenerator.GetUniqueKeyOriginal_BIASED(24);
            //Console.WriteLine("Biased Key: " + biasedKey);
            //Console.ReadLine();
            //Console.Clear();

            Console.WriteLine("Do you want to encrypt the file? (Y/N)");
            string response = Console.ReadLine().ToLower();

            if (response == "y" || response == "yes")
            {
                SoftYellowENCDELIB.Encryptor.KeyRun();
            }
            else
            {

                string fileNameData = "GreenBoxExport.txt";
                string folderNameData = "GreenBoxExport";

                Console.WriteLine(fileNameData);
                string fileName = fileNameData;
                string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderNameData);
                string filePath = Path.Combine(directoryPath, fileName);
                Console.Clear();

                SoftYellowENCDELIB.Encryptor.KeyRun();

                //System.IO.Directory.CreateDirectory(filePath);

                File.AppendAllText(filePath, biasedKey + Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
