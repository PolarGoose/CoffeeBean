using System;
using System.Collections.Generic;

namespace CoffeeBean.Utils;

internal readonly struct CommandLineArguments
{
    public bool? IsScreenLockEnabled { get; }

    public CommandLineArguments(bool? isScreenLockEnabled)
    {
        IsScreenLockEnabled = isScreenLockEnabled;
    }
}

internal static class CommandLineParser
{
    public static CommandLineArguments Parse(IReadOnlyList<string> args)
    {
        return args.Count switch
        {
            0 => new CommandLineArguments(null),
            1 when args[0] == "enable" => new CommandLineArguments(true),
            1 when args[0] == "disable" => new CommandLineArguments(false),
            1 => throw new Exception($"Unknown command line argument '{args[0]}'"),
            _ => throw new Exception("Too many command line arguments are provided")
        };
    }
}
