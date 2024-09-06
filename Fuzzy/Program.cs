using Fuzzy.Content;
using Fuzzy.Output;

namespace Fuzzy;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        CmdArgs arguments = CmdArgs.FromArgs(args);
        IContentProvider contentProvider = Program.FromArgs(arguments);
        IOutputProvider outputProvider = new ConsoleOutputProvider(arguments);
        Fuzzer fuzzer = new Fuzzer(
            arguments, 
            contentProvider, 
            outputProvider);
        await fuzzer.FuzzAsync(cts.Token);
    }

    private static IContentProvider FromArgs(CmdArgs args) => args.WType switch
    {
        WordlistType.Url => new WebContentProvider(new Uri(args.WordList)),
        _ => throw new NotImplementedException("Currently we only support wordlists from url!")
    };
}
