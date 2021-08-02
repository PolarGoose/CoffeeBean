using System.Diagnostics;
using System.Reflection;

namespace CoffeeBean.Utils
{
    internal static class AssemblyInfoRetriever
    {
        public static string InformationalVersion => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        public static string ExecutableNameWithoutExtension => Process.GetCurrentProcess().ProcessName;
        public static string ExecutableFullName => Process.GetCurrentProcess().MainModule.FileName;
    }
}
