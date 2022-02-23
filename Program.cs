using System;
using System.Collections.Generic;
using System.IO;

namespace MemoryGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileOperations = new FilesOperations();
            var words = fileOperations.GetWordsFromFile();
            foreach(string word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}
