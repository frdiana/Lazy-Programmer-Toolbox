using System;
using System.Collections.Generic;
using System.Text;

namespace LazyProgrammerToolbox.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void WriteSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
