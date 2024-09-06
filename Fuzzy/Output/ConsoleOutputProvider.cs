namespace Fuzzy.Output;

internal static class OutputHelper
{
    public static string Banner(CmdArgs args) =>
        $"""
         --------------------------------------------
         <<<<<<<<<<<     A new scan      >>>>>>>>>>>>
         --------------------------------------------
         || Target: {args.BaseUrl}                     
         || Wordlist: {args.WordList}
         --------------------------------------------
         """;
}

internal class ConsoleOutputProvider : IOutputProvider
{
    public CmdArgs Args { get; }
    
    public ConsoleOutputProvider(CmdArgs args)
    {
        Args = args;
        Console.WriteLine(OutputHelper.Banner(args));
    }
    
    public void Output(string item) 
    {
        Console.WriteLine(item);
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}
