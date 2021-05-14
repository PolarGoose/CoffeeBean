using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace CoffeeBean
{
    public partial class App
    {
        private TrayIcon.TrayIcon trayIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            WinApi.SetThreadExecutionState(WinApi.EXECUTION_STATE.ES_CONTINUOUS | WinApi.EXECUTION_STATE.ES_DISPLAY_REQUIRED);
            var version = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).ProductVersion;
            trayIcon = new TrayIcon.TrayIcon(version);
        }
    }
}
