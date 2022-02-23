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
                userDifficulty = Console.ReadKey().KeyChar;
                if (Char.ToLower(userDifficulty) == 'e')
                {
                    Information.DisplayMessagesForEasyMode();
                }
                else if (Char.ToLower(userDifficulty) == 'h')
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
