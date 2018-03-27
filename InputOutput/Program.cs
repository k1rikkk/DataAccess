using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InputOutput
{
    internal class Program
    {
        private static FilesProcessor processor;

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            bool stop = false;
            while (!stop)
            {
                Console.WriteLine("Press \'l\' to list all files in directory");
                Console.WriteLine("Press \'c\' to get file content");
                Console.WriteLine("Press \'s\' to exit");
                char cmd = Console.ReadKey().KeyChar;
                Console.Write("\r");
                switch (cmd)
                {
                    case 'l': ListFiles(); break;
                    case 'c': GetContent(); break;
                    case 's': stop = true; break;
                }
            }
        }

        private static bool ListFiles()
        {
            Console.WriteLine("Enter path:");
            processor = new FilesProcessor(Console.ReadLine());
            Console.Clear();
            using (Stream stream = Console.OpenStandardOutput())
            {
                Task<bool> task = processor.PrintFilesAsync(stream);
                task.Wait();
                return task.Result;
            }
        }

        private static void GetContent()
        {
            if ((processor == null) && (!ListFiles()))
                return;
            Console.WriteLine("Enter file name:");
            string fileName = Console.ReadLine();
            Console.WriteLine();
            processor.PrintContentAsync(fileName, Console.OpenStandardOutput()).Wait();
        }
    }
}
