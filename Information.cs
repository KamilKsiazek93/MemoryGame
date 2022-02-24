using System;
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
        public static void DisplayNumberOfRows(int count)
        {
            Console.Write("  ");
            for (int i = 1; i <= count; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        public static void AskUserForWordInFistRow()
        {
            Console.WriteLine();
            Console.WriteLine("Pick word that you want to discover, for example: A1");
            Console.WriteLine();
        }

        public static void AskUserForWordInSecondRow()
        {
            Console.WriteLine("Pick word that you want to discover, for example: B1");
            Console.WriteLine();
        }
    }
}
