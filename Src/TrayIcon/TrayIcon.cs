using System;
using System.Windows.Forms;

namespace CoffeeBean.TrayIcon
{
    internal sealed class TrayIcon
    {
        private readonly NotifyIcon notifyIcon;

        public TrayIcon(string version)
        {
            notifyIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon(System.Windows.Application.GetResourceStream(new Uri("/TrayIcon/icon.ico", UriKind.Relative)).Stream),
                Visible = true,
                Text = "CoffeeBean",
                ContextMenuStrip = new ContextMenu(version)
            };
        }
    }
}
