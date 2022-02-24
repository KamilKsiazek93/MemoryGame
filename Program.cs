using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MemoryGame.Models;

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
            var numberOfRandomWords = gameProvider.GetNumberOfWordsForUserDifficulty(userDifficulty);
            var maxOfChances = gameProvider.GetMaxOfChances(userDifficulty);
            var firstWordsList = gameProvider.TakeRandomWordsForGame(words, numberOfRandomWords);
            var secondWordsList = gameProvider.GetCopyOfList(firstWordsList);
            var firstRowGame = gameProvider.GetTemplateOfWordsForGame(firstWordsList, 'A');
            var secondRowGame = gameProvider.GetTemplateOfWordsForGame(secondWordsList, 'B');
            gameProvider.PlayGame(firstRowGame, secondRowGame, maxOfChances);
        }
    }
}
