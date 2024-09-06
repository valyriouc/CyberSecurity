namespace Fuzzy.Output;

internal class FileOutputProvider : IOutputProvider
{
    private readonly StreamWriter streamWriter;
    public CmdArgs Args { get; }
    
    public FileOutputProvider(CmdArgs args)
    {
        if (!File.Exists(args.OutputPath))
        {
            streamWriter = new StreamWriter(File.Create(args.OutputPath));
        }
        else
        {
            streamWriter = new StreamWriter(File.Open(args.OutputPath, FileMode.Append));
        }

        Args = args;
        PrintStart();
    }

    public void Output(string item)
    {
        streamWriter.WriteLine(item);
    }

    private string PrintStart() =>
        $"""
         --------------------------------------------
         <<<<<<<<<<<     A new scan      >>>>>>>>>>>>
         --------------------------------------------
         || Target: {Args.BaseUrl}                     
         || Wordlist: {Args.WordList}
         --------------------------------------------
         """;

    public void Dispose()
    {
        streamWriter.Dispose();
    }
}