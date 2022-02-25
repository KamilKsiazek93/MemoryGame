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
        private const string FILE_WITH_WORDS = "words.txt";
        private const string FILE_WITH_RESULTS_EASY_MODE = "ResultsEasy.txt";
        private const string FILE_WITH_RESULTS_HARD_MODE = "ResultsHard.txt";

        public static List<string> GetWordsFromFile()
        {
            var words = new List<string>();
            foreach (string line in File.ReadLines($"../../../{FILE_WITH_WORDS}"))
            {
                words.Add(line);
            }
            return words;
        }

        public static void SaveUserScoreToFile(string userName, string data, int maxOfChances, Stopwatch watch, char userDifficulty)
        {
            string fileName = GetRightFileName(userDifficulty);
            string result = userName + '|' + data + '|' + maxOfChances + '|' + watch.ElapsedMilliseconds / 1000 + '|';
            using StreamWriter file = new($"../../../{fileName}", append: true);
            file.WriteLine(result);
        }

        private static string GetRightFileName(char userDifficulty)
        {
            return userDifficulty == 'e' ? FILE_WITH_RESULTS_EASY_MODE : FILE_WITH_RESULTS_HARD_MODE;
        }

        public static IEnumerable<Result> GetTenBestScoresFromFile(char userDifficulty)
        {
            string fileName = GetRightFileName(userDifficulty);
            string path = $"../../../{fileName}";
            if(!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            using StreamReader file = new(path);
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
