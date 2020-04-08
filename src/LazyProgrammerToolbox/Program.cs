using CommandLine;
using LazyProgrammerToolbox.CmdLineVerbs;
using LazyProgrammerToolbox.Converters;
using LazyProgrammerToolbox.Helpers;
using System;
using System.IO;

namespace LazyProgrammerToolbox
{
    class Program
    {
        private static ToAzSettingsConverter toAzSettingsConverter = new ToAzSettingsConverter();

        static int Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<ToAzSettings>(args).MapResult(
               (ToAzSettings opts) => ConvertToAzureConfig(opts),
               errs => 1);
            return 0;
        }

        private static int ConvertToAzureConfig(ToAzSettings opts)
        {
            var validationResult = ValidateInputFile(opts.Input);

            if (!validationResult.isValid)
            {
                ConsoleHelper.WriteError(validationResult.errorMessage);
                return 1;
            }
            var fileInAppsettingsFormat = File.ReadAllText(opts.Input);
            var convertionResult = toAzSettingsConverter.Convert(fileInAppsettingsFormat);
            if (convertionResult.Succeded)
            {
                File.WriteAllText(opts.Output, convertionResult.Result);
                string fileName = Path.GetFileName(opts.Output);
                ConsoleHelper.WriteSuccess($"File {fileName} successfully created!");
                return 0;
            }
            else
            {
                ConsoleHelper.WriteError(convertionResult.ErrorMessage);
                return 1;
            }
        }

        private static (bool isValid, string errorMessage) ValidateInputFile(string inputFile)
        {
            if (string.IsNullOrEmpty(inputFile))
            {
                string errorMessage = "inputFile string is null or empty";
                return (false, errorMessage);
            }
            if (!File.Exists(inputFile))
            {
                string errorMessage = "inputFile doesn't exists";
                return (false, errorMessage);
            }
            return (true, string.Empty);


        }

    }
}
