using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public static class Information
    {
        public static void DisplayInformationBeforeStartGame()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome in Memery Game. You can choose one of two levels: easy or hard");
            Console.WriteLine("In easy mode you have to match 4 pair of words. You have 10 chances for whole game");
            Console.WriteLine("In hard mode you have 9 pair of words and 15 chances");
        }

        public static void DisplayInformationAboutAvailableMode()
        {
            Console.WriteLine();
            Console.WriteLine("Type e if you want to play in easy mode or type h if you want to play in hard mode");
        }

        public static void DisplayInformationAboutCorrectCharTyping()
        {
            Console.WriteLine();
            Console.WriteLine("Incorrect character. Type again");
        }

        public static void DisplayMessagesForEasyMode()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome in easy mode");
        }

        public static void DisplayMessagesForHardMode()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome in hard mode");
        }

        public static void DisplayInfoAboutChancesLeft(int maxOfChances)
        {
            Console.WriteLine("Guess chances: {0}", maxOfChances);
            Console.WriteLine();
        }

        public static void DisplayEndingMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Have a nice day. See you again!");
        }

        public static void DisplayInfoAboutLevel(int maxOfChances)
        {
            if (maxOfChances == 10)
            {
                Console.WriteLine("Level: easy");
            }
            else
            {
                Console.WriteLine("Level: hard");
            }
        }

        public static void AskUserForWordInFistRow()
        {
            Console.WriteLine();
            Console.WriteLine("Pick word that you want to discover, for example: A1");
            Console.WriteLine();
        }

        public static void AskUserForWordInSecondRow()
        {
            Console.WriteLine();
            Console.WriteLine("Pick word that you want to discover, for example: B1");
            Console.WriteLine();
        }

        public static void DisplayMessageAboutResultGame(int maxOfChances, Stopwatch watch)
        {
            if (maxOfChances == 0)
            {
                Console.WriteLine("Game over.");
            }
            else
            {
                Console.WriteLine("You win!!");
                Console.WriteLine("You kept {0} chances", maxOfChances);
            }
            Console.WriteLine("The game took you {0} seconds", watch.ElapsedMilliseconds / 1000);
        }

        public static void DisplayMessageForTryingAgain()
        {
            Console.WriteLine("Try again!");
        }

        public static void DisplayMessageIfWodsAreNotTheSame()
        {
            Console.WriteLine();
            Console.WriteLine("Words are not the same");
        }

        public static void DisplayMessageIfWordsAreTheSame()
        {
            Console.WriteLine();
            Console.WriteLine("Words are the same. Great!");
        }

        public static void DisplayInformationAboutAgainGame()
        {
            Console.WriteLine();
            Console.WriteLine("Do you want to play again? Type 'y' if you want to play again, or type 'n' if you want to close game");
            Console.WriteLine();
        }

        public static void DisplayInformationAboutName()
        {
            Console.WriteLine();
            Console.WriteLine("Type your name for save result in db: ");
        }
    }
}
