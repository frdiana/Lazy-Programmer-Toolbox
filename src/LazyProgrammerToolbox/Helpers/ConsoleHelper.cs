using System;
using System.Collections.Generic;
using System.Text;

namespace LazyProgrammerToolbox.Helpers
{
    /// <summary>
    /// Helper class for handle console colors and behaviour
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Write an error message to standard console
        /// </summary>
        /// <param name="message">The text to output into the console</param>
        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Write a succes message to standard console
        /// </summary>
        /// <param name="message"></param>
        public static void WriteSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
