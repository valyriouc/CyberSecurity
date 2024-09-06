using System.Net;

namespace Fuzzy.Content;

public enum NewLineType
{
    Windows,
    Linux,
    Mac,
    Unkown
}

public static class NewLineTypeExt
{
    public static string ToCharacter(this NewLineType self) => self switch
    {
        NewLineType.Windows => "\r\n",
        NewLineType.Linux => "\n",
        NewLineType.Mac => "\r",
        NewLineType.Unkown => throw new NotSupportedException("The newline character is unkown!")
    };
}

public class WebContentProvider : IContentProvider
{
    private readonly HttpClient httpClient;
    private readonly Uri wordlistUri;
    
    private string? content = null;
    private NewLineType newLine;
    
    public WebContentProvider(Uri uri)
    {
        wordlistUri = uri;
        httpClient = new HttpClient();
        newLine = Environment.NewLine.DetectLineEnding();
    }

    public async Task LoadContentAsync(CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await httpClient.GetAsync(wordlistUri, cancellationToken);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            Console.WriteLine($"Could not get the wordlist (Status: {response.StatusCode})!");
        }
        content = await response.Content.ReadAsStringAsync(cancellationToken);
        newLine = content.DetectLineEnding();
    }

    public IEnumerable<string> GetPathParts()
    {
        if (content is null)
        {
            throw new Exception("Content provider is not initialized!");
        }

        return content.Split(newLine.ToCharacter());
    }

    public void Dispose()
    {
        // TODO release managed resources here
        httpClient.Dispose();
    }
}