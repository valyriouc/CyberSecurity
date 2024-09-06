using Fuzzy;
using Fuzzy.Content;

namespace FuzzyTests;

public class StringExtensionsShould
{
    [Theory]
    [InlineData("\n", NewLineType.Linux)]
    [InlineData("\r\n", NewLineType.Windows)]
    [InlineData("\r", NewLineType.Mac)]
    [InlineData("", NewLineType.Unkown)]
    public void DetectCorrectLineEnding(string content, NewLineType expected)
    {
        NewLineType actual = content.DetectLineEnding();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Hello and welcome\nto my new world", NewLineType.Linux)]
    [InlineData("Hello and welcome\r\nto my new world", NewLineType.Windows)]
    [InlineData("Hello and welcome\rto my new world", NewLineType.Mac)]
    [InlineData("Hello and welcome to my new world", NewLineType.Unkown)]
    public void DetectCorrectLineEndingWithinLongString(string content, NewLineType expected)
    {
        NewLineType actual = content.DetectLineEnding();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("-u", ArgType.Url)]
    [InlineData("-w", ArgType.Wordlist)]
    [InlineData("-o", ArgType.Output)]
    [InlineData("-t", ArgType.WType)]
    public void DetectCorrectArgType(string content, ArgType expected)
    {
        ArgType actual = content.ToArgType();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Url", WordlistType.Url)]
    [InlineData("File", WordlistType.File)]
    public void DetectCorrectWordlistType(string content, WordlistType expected)
    {
        WordlistType actual = content.ToWordlistType();
        Assert.Equal(expected, actual);
    }
}