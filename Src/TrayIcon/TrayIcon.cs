using System.Windows.Forms;
using CoffeeBean.Properties;

namespace CoffeeBean.TrayIcon
{
    internal sealed class TrayIcon
    {
        private readonly NotifyIcon notifyIcon;

        public TrayIcon(string version)
        {
            notifyIcon = new NotifyIcon
            {
                Icon = Resources.icon,
                Visible = true,
                Text = "CoffeeBean",
                ContextMenuStrip = new ContextMenu(version)
            };
        }
    }
}
