using System.Windows;

namespace CoffeeBean.Utils;

internal static class ErrorDialog
{
    public static void Show(string text)
    {
        MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
