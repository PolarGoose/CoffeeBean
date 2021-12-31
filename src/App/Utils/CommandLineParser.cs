using System;
using System.Collections.Generic;

namespace CoffeeBean.Utils
{
    internal struct CommandLineArguments
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
            if (args.Count == 0)
            {
                return new CommandLineArguments(null);
            }

            if (args.Count == 1)
            {
                if (args[0] == "enable")
                {
                    return new CommandLineArguments(true);
                }
                else if (args[0] == "disable")
                {
                    return new CommandLineArguments(false);
                }
                else
                {
                    throw new Exception($"Unknown command line argument '{args[0]}'");
                }
            }

            throw new Exception($"Too many command line arguments are provided");
        }
    }
}
