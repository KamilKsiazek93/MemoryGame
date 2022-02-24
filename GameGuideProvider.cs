using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<string> TakeRandomWordsForGame(List<string> words, int difficulty)
        {
            return words.OrderBy(item => Guid.NewGuid()).Take(difficulty);
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
            
        }
    }
}
