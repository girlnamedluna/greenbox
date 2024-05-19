using System;
using System.IO;
using System.Globalization;
using GreenLib;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace GreenBox
{
    class GreenBox
    {
        static void Main(string[] args)
        {
            string biasedKey = KeyGenerator.GetUniqueKeyOriginal_BIASED(24);
            Console.WriteLine("Biased Key: " + biasedKey);

            string fileNameData = "greenBoxExport.txt";
            string folderNameData = "GreenBox Export";

            Console.WriteLine(fileNameData);


            string fileName = fileNameData;

            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderNameData);

            string filePath = Path.Combine(directoryPath, fileName);

            Directory.CreateDirectory(directoryPath);

            File.AppendAllText(filePath, biasedKey + Environment.NewLine + Environment.NewLine);
        }
    }
}
