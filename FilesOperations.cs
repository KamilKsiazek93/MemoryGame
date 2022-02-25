using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using MemoryGame.Models;

namespace MemoryGame
{
    public static class FilesOperations
    {
        public static List<string> GetWordsFromFile()
        {
            var words = new List<string>();
            foreach (string line in File.ReadLines("../../../Words.txt"))
            {
                words.Add(line);
            }
            return words;
        }

        public static void SaveUserScoreToFile(string userName, string data, int maxOfChances, Stopwatch watch)
        {
            string result = userName + '|' + data + '|' + maxOfChances + '|' + watch.ElapsedMilliseconds / 1000 + '|';
            using StreamWriter file = new("../../../Result.txt", append: true);
            file.WriteLine(result);
        }

        public static IEnumerable<Result> GetTenBesttScoresFromFile()
        {
            using StreamReader file = new("../../../Result.txt");
            string line;
            var results = new List<Result>();
            while((line = file.ReadLine()) != null)
            {
                string[] score = line.Split('|');
                results.Add(new Result { Name = score[0], KeptedChances = Int32.Parse(score[2]), GameTime = Int32.Parse(score[3])});
            }
            return results.OrderBy(item => item.GameTime).Take(10);
        }
    }
}
