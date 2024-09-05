using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Fuzzy;

internal class Fuzzer : IDisposable
{
    private readonly IOutputProvider outputProvider;
    private readonly HttpClient httpClient;
    private readonly CmdArgs args;
    
    public Fuzzer(
        CmdArgs args,
        IOutputProvider outputProvider)
    {
        this.args = args;
        this.outputProvider = outputProvider;
        this.httpClient = new HttpClient();
    }

    public async Task FuzzAsync(IAsyncEnumerable<string> wordlist)
    {
        Uri baseUrl = new Uri(args.Url);

        await foreach (string w in wordlist)
        {
            Uri url = new Uri(baseUrl, w);
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.NotFound)
            {
                string output = $"{(int)response.StatusCode} - {response.StatusCode} ({url.ToString()})";
                outputProvider.Output(output);
            } 
        }
    }

    public void Dispose()
    {
        outputProvider.Dispose();
        this.httpClient.Dispose();
    }
}

internal interface IOutputProvider : IDisposable
{
    public CmdArgs Args { get; }
    
    public void Output(string item);
}

internal class ConsoleOutputProvider : IOutputProvider
{
    public CmdArgs Args { get; }
    
    public ConsoleOutputProvider(CmdArgs args)
    {
        Args = args;
        PrintStart();
    }
    
    private string PrintStart() =>
        $"""
         --------------------------------------------
         <<<<<<<<<<<     A new scan      >>>>>>>>>>>>
         --------------------------------------------
         || Target: {Args.Url}                     
         || Wordlist: {Args.WordList}
         --------------------------------------------
         """;
    
    public void Output(string item)
    {
        Console.WriteLine(item);
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}

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
        || Target: {Args.Url}                     
        || Wordlist: {Args.WordList}
        --------------------------------------------
        """;

    public void Dispose()
    {
        streamWriter.Dispose();
    }
}