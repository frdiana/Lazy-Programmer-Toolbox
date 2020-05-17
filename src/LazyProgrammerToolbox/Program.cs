using System.CommandLine;

namespace LazyProgrammerToolbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootCommand = new RootCommand();

            rootCommand
                .AddAppSettingsToAzSettingsCommand()
                .AddAzSettingsToAppSettingsCommand();
            
            rootCommand.InvokeAsync(args).Wait();
        }
    }
}
