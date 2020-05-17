using LazyProgrammerToolbox.Converters;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace LazyProgrammerToolbox
{
    /// <summary>
    /// Set of extensions method for simplify commands creations
    /// </summary>
    public static class RootCommandExtensions
    {
        /// <summary>
        /// Add command for convert appsettings.json format to Azure app service format
        /// </summary>
        /// <param name="rootCommand">The RootCommand where to append child commands</param>
        /// <returns>RootCommand</returns>
        public static RootCommand AddAppSettingsToAzSettingsCommand(this RootCommand rootCommand)
        {
            var appSettingsToAzSettingsCommand = new Command("appSettingsToAzSettings");
            appSettingsToAzSettingsCommand.Add(new Option<string>("--input"));
            appSettingsToAzSettingsCommand.Add(new Option<string>("--output"));
            appSettingsToAzSettingsCommand.Handler = CommandHandler.Create<string, string>(AppSettingsConverter.AppSettingsToAzSettings);
            rootCommand.Add(appSettingsToAzSettingsCommand);
            return rootCommand;
        }

        /// <summary>
        /// Add command for convert Azure app service format to appsettings.json format
        /// </summary>
        /// <param name="rootCommand"></param>
        /// <returns></returns>
        public static RootCommand AddAzSettingsToAppSettingsCommand(this RootCommand rootCommand)
        {
            var appSettingsToAzSettingsCommand = new Command("AzSettingsToAppSettings");
            appSettingsToAzSettingsCommand.Add(new Option<string>("--input"));
            appSettingsToAzSettingsCommand.Add(new Option<string>("--output"));
            appSettingsToAzSettingsCommand.Handler = CommandHandler.Create<string, string>(AppSettingsConverter.AppSettingsToAzSettings);
            rootCommand.Add(appSettingsToAzSettingsCommand);
            return rootCommand;
        }
    }
}
