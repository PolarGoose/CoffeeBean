using Microsoft.Win32;

namespace CoffeeBean.Utils;

internal static class Settings
{
     public static bool RunEnabled
     {
         get => (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\CoffeeBean", "RunEnabled", null) != "0";
         set => Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoffeeBean", "RunEnabled", value ? "1" : "0");
     }
}
