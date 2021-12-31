using Microsoft.Win32;

namespace CoffeeBean.Utils
{
    internal static class AutoStartup
    {
        public static bool IsEnabled =>
            (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "CoffeeBean", null) == AssemblyInfoRetriever.ExecutableFullName;

        public static void Enable()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "CoffeeBean", AssemblyInfoRetriever.ExecutableFullName);
        }

        public static void Disable()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (key != null)
                {
                    key.DeleteValue("CoffeeBean");
                }
            }
        }
    }
}
