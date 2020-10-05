using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace CoffeeBean
{
    public partial class App
    {
        private readonly ScreenLockBlocker lockScreenBlocker = new ScreenLockBlocker();
        private TrayIcon.TrayIcon trayIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var version = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).ProductVersion;
            trayIcon = new TrayIcon.TrayIcon(version);
        }
    }
}
