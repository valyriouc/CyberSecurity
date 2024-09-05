namespace Fuzzy;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        CmdArgs arguments = CmdArgs.FromArgs(args);
        IAsyncEnumerable<string> input = WordlistProvider.ProvideAsync(arguments.WordList, arguments.WType);
        
    }
}
