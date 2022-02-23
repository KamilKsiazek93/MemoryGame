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
    }
}
