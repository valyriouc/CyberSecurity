using Fuzzy.Content;

namespace Fuzzy;

public static class StringExtensions
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

    public static NewLineType DetectLineEnding(this string self)
    {
        NewLineType? newLine = null;
        for (int i = 0; i < self.Length; i++)
        {
            if (self[i] == '\r')
            {
                if ((i + 1) < self.Length && self[i + 1] == '\n')
                {
                    newLine = NewLineType.Windows;
                }
                else
                {
                    newLine = NewLineType.Mac;
                }
                break;
            }
            
            if (self[i] == '\n')
            {
                newLine = NewLineType.Linux;
                break;
            }
        }

        return newLine ?? NewLineType.Unkown;
    }
}