using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MemoryGame
{
    public class FilesOperations
    {
        public List<string> GetWordsFromFile()
        {
            var words = new List<string>();
            foreach (string line in File.ReadLines("../../../Words.txt"))
            {
                words.Add(line);
            }
            return words;
        }
    }
}
