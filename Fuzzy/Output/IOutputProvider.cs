namespace Fuzzy.Output;

internal interface IOutputProvider : IDisposable
{
    public CmdArgs Args { get; }
    
    public void Output(string item);

    protected void PrintBanner() =>
        Console.WriteLine($"""
                           --------------------------------------------
                           <<<<<<<<<<<     A new scan      >>>>>>>>>>>>
                           --------------------------------------------
                           || Target: {Args.BaseUrl}                     
                           || Wordlist: {Args.WordList}
                           --------------------------------------------
                           """);
}