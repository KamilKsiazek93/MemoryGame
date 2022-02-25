using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MemoryGame.Models;

namespace MemoryGame
{
    class Program
    {

        static void LaunchGame()
        {
            char playAgain = 'y';
            var words = FilesOperations.GetWordsFromFile();
            Information.DisplayInformationBeforeStartGame();
            while (playAgain == 'y')
            {
                Information.DisplayInformationAboutAvailableMode();
                char userDifficulty = GameGuideProvider.GetUserDifficulty();
                var numberOfRandomWords = GameGuideProvider.GetNumberOfWordsForUserDifficulty(userDifficulty);
                var maxOfChances = GameGuideProvider.GetMaxOfChances(userDifficulty);
                var firstWordsList = GameGuideProvider.GetRandomWordsForGame(words, numberOfRandomWords);
                var secondWordsList = GameGuideProvider.GetCopyOfList(firstWordsList);
                var firstRowGame = GameGuideProvider.GetTemplateOfWordsForGame(firstWordsList, 'A');
                var secondRowGame = GameGuideProvider.GetTemplateOfWordsForGame(secondWordsList, 'B');
                GameGuideProvider.PlayGame(firstRowGame, secondRowGame, maxOfChances, userDifficulty);
                playAgain = GameGuideProvider.AskUserToPlayAgain();
            }
            Information.DisplayEndingMessage();
        }

        static void Main(string[] args)
        {
            LaunchGame();
        }
    }
}
