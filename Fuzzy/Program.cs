namespace Fuzzy;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        CmdArgs arguments = CmdArgs.FromArgs(args);
        IAsyncEnumerable<string> input = WordlistProvider.ProvideAsync(arguments.WordList, arguments.WType);
        IOutputProvider provider = new ConsoleOutputProvider(arguments);
        Console.WriteLine("Hello");
        Fuzzer fuzzer = new Fuzzer(arguments, provider);
        await fuzzer.FuzzAsync(input);
    }
}
