namespace Fuzzy;

internal static class StringExtensions
{
    public static ArgType ToArgType(this string self) => self switch
    {
        "-u" => ArgType.Url,
        "-w" => ArgType.Wordlist,
        "-o" => ArgType.Output,
        "-t" => ArgType.WType,
        _ => throw new NotSupportedException(),
    };

    public static WordlistType ToWordlistType(this string self) => 
        Enum.Parse<WordlistType>(self);
    
}