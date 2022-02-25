using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MemoryGame.Models;

namespace MemoryGame
{
    public static class GameGuideProvider
    {
        public static char GetUserDifficulty()
        {
            char userDifficulty = 'a';
            while (userDifficulty != 'e' && userDifficulty != 'h')
            {
                userDifficulty = Char.ToLower(Console.ReadKey().KeyChar);
                if (userDifficulty == 'e')
                {
                    Information.DisplayMessagesForEasyMode();
                }
                else if (userDifficulty == 'h')
                {
                    Information.DisplayMessagesForHardMode();
                }
                else
                {
                    Information.DisplayInformationAboutCorrectCharTyping();
                }
            }
            return userDifficulty;
        }

        public static int GetNumberOfWordsForUserDifficulty(char userDifficulty)
        {
            return userDifficulty == 'e' ? 4 : 8;
        }

        public static int GetMaxOfChances(char userDifficulty)
        {
            return userDifficulty == 'e' ? 10 : 15;
        }

        public static List<string> GetRandomWordsForGame(List<string> words, int difficulty)
        {
            Random random = new Random();
            var selectedWords = new List<string>();
            for(int i = 0; i < difficulty;)
            {
                int index = random.Next(words.Count);
                if (!selectedWords.Contains(words[index]))
                {
                    selectedWords.Add(words[index]);
                    i++;
                }
            }
            return selectedWords;
        }

        public static IEnumerable<string> GetCopyOfList(List<string> words)
        {
            return words.OrderBy(item => Guid.NewGuid());
        }

        public static IEnumerable<GameTemplate> GetTemplateOfWordsForGame(IEnumerable<string> words, char key)
        {
            var template = new List<GameTemplate>();
            int index = 1;
            foreach(var word in words)
            {
                template.Add(new GameTemplate { Key = String.Concat(key, index.ToString()), Word = word, IsDiscover = false});
                index++;
            }
            return template;
        }

        public static void PlayGame(IEnumerable<GameTemplate> firstTemplate, IEnumerable<GameTemplate> secondTemplate, int maxOfChances, char userDifficulty)
        {
            int toWin = firstTemplate.ToList().Count;
            var watch = Stopwatch.StartNew();
            while (maxOfChances > 0 && toWin > 0)
            {
                DisplayMatrix(firstTemplate, secondTemplate, maxOfChances);
                Information.AskUserForWordInFistRow();
                var firstPickedKey = GetPickedKey(firstTemplate);
                string firstWord = GetWordByPickedKey(firstTemplate, firstPickedKey);
                ChangeDiscoverWord(firstTemplate, firstPickedKey);
                DisplayMatrix(firstTemplate, secondTemplate, maxOfChances);
                Information.AskUserForWordInSecondRow();
                var secondPickedKey = GetPickedKey(secondTemplate);
                string secondWord = GetWordByPickedKey(secondTemplate, secondPickedKey);
                ChangeDiscoverWord(secondTemplate, secondPickedKey);
                DisplayMatrix(firstTemplate, secondTemplate, maxOfChances);
                if(IsWordTheSame(firstWord, secondWord))
                {
                    ActionsWhenWordsAreTheSame(ref toWin);
                }
                else
                {
                    ActionsWhenWordsAreDifferent(ref firstTemplate, firstPickedKey, ref secondTemplate, secondPickedKey, ref maxOfChances);
                }
                Thread.Sleep(3000);
            }

            watch.Stop();
            Information.DisplayMessageAboutResultGame(maxOfChances, watch);
            Information.DisplayBestResult(FilesOperations.GetTenBestScoresFromFile(userDifficulty));
            if(maxOfChances > 0)
            {
                SaveResultIntoFile(maxOfChances, watch, userDifficulty);
            }
        }

        private static void SaveResultIntoFile(int maxOfChances, Stopwatch watch, char userDifficulty)
        {
            string userName = AskUserAboutName();
            string data = GetTodayData();
            FilesOperations.SaveUserScoreToFile(userName, data, maxOfChances, watch, userDifficulty);
        }

        private static void ActionsWhenWordsAreDifferent(ref IEnumerable<GameTemplate> firstTemplate, string firstPickedKey, ref IEnumerable<GameTemplate> secondTemplate, string secondPickedKey ,ref int maxOfChances)
        {
            Information.DisplayMessageIfWodsAreNotTheSame();
            ChangeDiscoverWord(firstTemplate, firstPickedKey);
            ChangeDiscoverWord(secondTemplate, secondPickedKey);
            maxOfChances--;
            if (maxOfChances > 0)
            {
                Information.DisplayMessageForTryingAgain();
            }
        }

        private static void ActionsWhenWordsAreTheSame(ref int toWin)
        {
            Information.DisplayMessageIfWordsAreTheSame();
            toWin--;
        }

        private static bool IsWordTheSame(string firstWord, string secondWord)
        {
            return firstWord == secondWord;
        }

        private static string GetTodayData()
        {
            return DateTime.Today.ToString("D");
        }

        private static string AskUserAboutName()
        {
            Information.DisplayInformationAboutName();
            return Console.ReadLine();
        }

        private static string GetWordByPickedKey(IEnumerable<GameTemplate> template, string pickedKey)
        {
            return template.Where(item => item.Key == pickedKey).Select(item => item.Word).First();
        }

        private static IEnumerable<GameTemplate> ChangeDiscoverWord(IEnumerable<GameTemplate> template, string pickedKey)
        {
            foreach (var item in template)
            {
                if (item.Key == pickedKey)
                {
                    item.IsDiscover = !item.IsDiscover;
                }
            }
            return template;
        }

        private static string GetPickedKey(IEnumerable<GameTemplate> firstTemplate)
        {
            var isWordCorrectPick = false;
            string pickedKey = "";
            while (!isWordCorrectPick)
            {
                pickedKey = PickWordsByUser();
                if (!IsWordAvailableToPick(pickedKey, firstTemplate))
                {
                    Information.DisplayInformationAboutCorrectCharTyping();
                }
                else
                {
                    isWordCorrectPick = true;
                }
            }

            return pickedKey;
        }

        private static bool IsWordAvailableToPick(string pickedKey, IEnumerable<GameTemplate> firstTemplate)
        {
            var pickedWord = firstTemplate.Where(item => item.Key == pickedKey).FirstOrDefault();

            if (pickedWord == null)
            {
                return false;
            }
            if (pickedWord.IsDiscover)
            {
                return false;
            }
            return true;
        }

        public static void DisplayRow(IEnumerable<GameTemplate> template, char rowName)
        {
            Console.Write(rowName + " ");
            foreach(var row in template)
            {
                if (row.IsDiscover)
                {
                    Console.Write(row.Word + " ");
                }
                else
                {
                    Console.Write("X ");
                }
            }
        }

        public static string PickWordsByUser()
        {
            return Console.ReadLine();
        }
        public static void DisplayNumberOfRows(IEnumerable<GameTemplate> template)
        {
            Console.Write("  ");
            var counter = 1;
            foreach(var item in template)
            {
                if(item.IsDiscover)
                {
                    Console.Write(counter + GetEmptySpaces(item.Word));
                }
                else
                {
                    Console.Write(counter + " ");
                }
                counter++;
            }
            Console.WriteLine();
        }

        public static string GetEmptySpaces(string word)
        {
            string emptyString = "";
            for(int i = 0; i< word.Length; i++)
            {
                emptyString += " ";
            }
            return emptyString;
        }

        public static void DisplayMatrix(IEnumerable<GameTemplate> firstTemplate, IEnumerable<GameTemplate> secondTemplate, int maxOfChances)
        {
            Console.Clear();
            Information.DisplayInfoAboutLevel(maxOfChances);
            Information.DisplayInfoAboutChancesLeft(maxOfChances);
            DisplayNumberOfRows(firstTemplate);
            DisplayRow(firstTemplate, 'A');
            Console.WriteLine();
            DisplayRow(secondTemplate, 'B');
            Console.WriteLine();
            DisplayNumberOfRows(secondTemplate);
            Console.WriteLine();
        }

        public static char AskUserToPlayAgain()
        {
            Information.DisplayInformationAboutAgainGame();
            char playAgain = ' ';
            while(playAgain != 'y' && playAgain != 'n')
            {
                playAgain = Char.ToLower(Console.ReadKey().KeyChar);

                if (playAgain != 'y' && playAgain != 'n')
                {
                    Information.DisplayInformationAboutCorrectCharTyping();
                }
            }
            return playAgain;
        }
    }
}
