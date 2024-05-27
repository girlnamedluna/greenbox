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
            textHelp.ReadClear();
            string biasedKey = KeyGenerator.GetUniqueKeyOriginal_BIASED(24);
            Console.WriteLine("New Password: " + biasedKey);
            textHelp.ReadClear();

            Console.WriteLine("Do you want to encrypt the file? (Y/N)");
            string response = Console.ReadLine().ToLower();

            if (response == "y" || response == "yes")
            {
                Console.Clear();
                Encryptor.KeyRun();
            }
            else if (response == "n" || response == "no")
            {

                string fileNameData = "GreenBoxExport.txt";
                string folderNameData = "GreenBoxExport";

                Console.WriteLine(fileNameData);
                string fileName = fileNameData;
                string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderNameData);
                string filePath = Path.Combine(directoryPath, fileName);
                Console.Clear();

                //System.IO.Directory.CreateDirectory(filePath);

                File.AppendAllText(filePath, biasedKey + Environment.NewLine + Environment.NewLine);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect syntax");
                Console.WriteLine("Contact girlnamedluna describing your issue");
                Console.ReadLine();
            }
        }
    }
}
