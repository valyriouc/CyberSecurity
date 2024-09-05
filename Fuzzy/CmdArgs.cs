using System;

namespace Fuzzy;

internal class CmdArgs
{
    public string Url { get; private set; }
    
    public string WordList { get; private set; }

    public string? OutputPath { get; private set; }

    public WordlistType WType { get; private set; } = WordlistType.File;

    private readonly string[] args;
    
    private CmdArgs(string[] args)
    {
        this.args = args;
    }

    private void Parse()
    {
        for (int i = 0; i < args.Length; i++)
        {
            ArgType t = args[i].ToArgType();
            switch (t)
            {
                case ArgType.Output:
                    OutputPath = args[i + 1];
                    break;
                case ArgType.Url:
                    Url = args[i + 1];
                    break;
                case ArgType.Wordlist:
                    WordList = args[i + 1];
                    break;                
                case ArgType.WType:
                    WType = args[i + 1].ToWordlistType();
                    break;
            }

            i += 1;
        }
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Url))
        {
            throw new ArgumentException("Url is required!");
        }

        if (string.IsNullOrWhiteSpace(WordList))
        {
            throw new ArgumentException("Wordlist is required!");
        }
    }
    
    public static CmdArgs FromArgs(string[] args)
    {
        CmdArgs result = new(args);
        result.Parse();
        result.Validate();
        return result;
    }
}