using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MemoryGame.Models;

namespace MemoryGame
{
    public class GameGuideProvider
    {
        public char GetUserDifficulty()
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

        public int GetNumberOfWordsForUserDifficulty(char userDifficulty)
        {
            return userDifficulty == 'e' ? 4 : 8;
        }

        public int GetMaxOfChances(char userDifficulty)
        {
            return userDifficulty == 'e' ? 10 : 15;
        }

        public List<string> TakeRandomWordsForGame(List<string> words, int difficulty)
        {
            Random random = new Random();
            var selectedWords = new List<string>();
            for(int i = 0; i < difficulty; i++)
            {
                int index = random.Next(words.Count);
                selectedWords.Add(words[index]);
            }
            return selectedWords;
        }

        public IEnumerable<string> GetCopyOfList(List<string> words)
        {
            return words.OrderBy(item => Guid.NewGuid());
        }

        public IEnumerable<GameTemplate> GetTemplateOfWordsForGame(IEnumerable<string> words, char key)
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

        public void PlayGame(IEnumerable<GameTemplate> firstTemplate, IEnumerable<GameTemplate> secondTemplate, int maxOfChances)
        {
            int toWin = firstTemplate.ToList().Count;
            while (maxOfChances > 0 && toWin > 0)
            {
                DisplayMatrix(firstTemplate, secondTemplate, maxOfChances);
                Information.AskUserForWordInFistRow();
                var firstPickedKey = GetPickedKey(firstTemplate);
                string firstWord = GetWordByPickedKey(firstTemplate, firstPickedKey);
                firstTemplate = ChangeDiscoverWord(firstTemplate, firstPickedKey);
                DisplayMatrix(firstTemplate, secondTemplate, maxOfChances);
                Information.AskUserForWordInSecondRow();
                var secondPickedKey = GetPickedKey(secondTemplate);
                string secondWord = GetWordByPickedKey(secondTemplate, secondPickedKey);
                secondTemplate = ChangeDiscoverWord(secondTemplate, secondPickedKey);
                DisplayMatrix(firstTemplate, secondTemplate, maxOfChances);
                if (firstWord == secondWord)
                {
                    Information.DisplayMessageIfWordsAreTheSame();
                    toWin--;
                }
                else
                {
                    Information.DisplayMessageIfWodsAreNotTheSame();
                    firstTemplate = ChangeDiscoverWord(firstTemplate, firstPickedKey);
                    secondTemplate = ChangeDiscoverWord(secondTemplate, secondPickedKey);
                    maxOfChances--;
                    if (maxOfChances > 0)
                    {
                        Information.DisplayMessageForTryingAgain();
                    }
                }
                Thread.Sleep(3000);
            }
            Information.DisplayMessageAboutResultGame(toWin);
        }

        private string GetWordByPickedKey(IEnumerable<GameTemplate> template, string pickedKey)
        {
            return template.Where(item => item.Key == pickedKey).Select(item => item.Word).First();
        }

        private IEnumerable<GameTemplate> ChangeDiscoverWord(IEnumerable<GameTemplate> template, string pickedKey)
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

        private string GetPickedKey(IEnumerable<GameTemplate> firstTemplate)
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

        private bool IsWordAvailableToPick(string pickedKey, IEnumerable<GameTemplate> firstTemplate)
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

        public void DisplayRow(IEnumerable<GameTemplate> template, char rowName)
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

        public string PickWordsByUser()
        {
            return Console.ReadLine();
        }

        public void DisplayMatrix(IEnumerable<GameTemplate> firstTemplate, IEnumerable<GameTemplate> secondTemplate, int maxOfChances)
        {
            Console.Clear();
            Information.DisplayInfoAboutLevel(maxOfChances);
            Information.DisplayInfoAboutChancesLeft(maxOfChances);
            Information.DisplayNumberOfRows(firstTemplate.ToList().Count);
            DisplayRow(firstTemplate, 'A');
            Console.WriteLine();
            DisplayRow(secondTemplate, 'B');
        }

        public char AskUserToPlayAgain()
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
