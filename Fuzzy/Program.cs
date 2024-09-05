namespace Fuzzy;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        CmdArgs arguments = CmdArgs.FromArgs(args);
        IAsyncEnumerable<string> input = WordlistProvider.ProvideAsync(arguments.WordList, arguments.WType);
        IOutputProvider provider;
        if (arguments.WType == WordlistType.File)
        {
            provider = new FileOutputProvider(arguments);
        }
        else
        {
            provider = new ConsoleOutputProvider(arguments);
        }

        Fuzzer fuzzer = new Fuzzer(arguments, provider);
        await fuzzer.FuzzAsync(input);
    }
}
