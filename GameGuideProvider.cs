using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void TakeRandomWordsForGame(List<string> words, int difficulty)
        {
            var challange = words.OrderBy(item => Guid.NewGuid()).Take(difficulty);
            foreach(var item in challange)
            {
                Console.WriteLine(item);
            }
        }
    }
}
