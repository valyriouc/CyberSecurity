namespace Fuzzy;

internal static class WordlistProvider
{
    public static IAsyncEnumerable<string> ProvideAsync(string wordlist, WordlistType type) 
    {
        if (type == WordlistType.File)
        {
            return FromFileAsync(wordlist);
        }
        else
        {
            return FromUrlAsync(wordlist);
        }
    }

    private static async IAsyncEnumerable<string> FromFileAsync(string wordlist)
    {
        if (!File.Exists(wordlist))
        {
            throw new FileNotFoundException($"File {wordlist} does not exist!");
        }

        foreach (string s in await File.ReadAllLinesAsync(wordlist))
        {
            yield return s;
        }
    }

    private static async IAsyncEnumerable<string> FromUrlAsync(string wordlist)
    {
        using HttpClient client = new HttpClient();

        string body = await client.GetStringAsync(wordlist);

        foreach (string item in body.Split(Environment.NewLine))
        {
            yield return item;
        }
     }
}