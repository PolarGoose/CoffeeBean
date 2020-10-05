using System;
using System.Diagnostics;
using System.Windows;

namespace CoffeeBean.TrayIcon
{
    public partial class ContextMenu
    {
        private readonly string projectGithubLink = "https://github.com/PolarGoose/CoffeeBean";

        public ContextMenu(string version)
        {
            InitializeComponent();
            VersionMenuItem.Text = $"Version: {version}\n{projectGithubLink}";
        }

        private void About_OnClick(object sender, EventArgs e)
        {
            Process.Start("explorer", projectGithubLink);
        }

        private void Close_OnClick(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
