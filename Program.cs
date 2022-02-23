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
            var gameProvider = new GameGuideProvider();
            var words = fileOperations.GetWordsFromFile();
            Information.DisplayInformationBeforeStartGame();
            char userDifficulty = gameProvider.GetUserDifficulty();
            Console.WriteLine(userDifficulty);
        }
    }
}
