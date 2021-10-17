using System;
using System.Windows;
using System.Windows.Forms;

namespace CoffeeBean
{
    public partial class App
    {
        private NotifyIcon trayIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            _ = WinApi.SetThreadExecutionState(WinApi.EXECUTION_STATE.ES_CONTINUOUS | WinApi.EXECUTION_STATE.ES_DISPLAY_REQUIRED);

            trayIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon(GetResourceStream(new Uri("/Icon/icon.ico", UriKind.Relative)).Stream),
                Visible = true,
                Text = "CoffeeBean",
                ContextMenuStrip = new ContextMenu()
            };
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            trayIcon.Dispose();
        }
    }
}
