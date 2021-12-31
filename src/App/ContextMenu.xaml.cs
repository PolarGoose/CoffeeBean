using CoffeeBean.Utils;
using System;
using System.Diagnostics;
using System.Windows;

namespace CoffeeBean
{
    public partial class ContextMenu
    {
        private readonly string githubUrl = "https://github.com/PolarGoose/CoffeeBean";
        private readonly ScreenLockPreventer screenLockPreventer;

        public ContextMenu(ScreenLockPreventer screenLockPreventer)
        {
            InitializeComponent();

            VersionMenuItem.Text = $"Version: {AssemblyInfoRetriever.InformationalVersion}\n{githubUrl}";

            LaunchAtStartupMenuItem.Checked = AutoStartup.IsEnabled;

            this.screenLockPreventer = screenLockPreventer;
            this.screenLockPreventer.EnabledChanged += () => EnabledMenuItem.Checked = this.screenLockPreventer.Enabled;
        }

        private void About_OnClick(object sender, EventArgs e)
        {
            _ = Process.Start("explorer", githubUrl);
        }

        private void Close_OnClick(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LaunchAtStatrupMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (LaunchAtStartupMenuItem.Checked)
            {
                AutoStartup.Enable();
            }
            else
            {
                AutoStartup.Disable();
            }
        }

        private void EnabledMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (EnabledMenuItem.Checked)
            {
                screenLockPreventer.Enabled = true;
            }
            else
            {
                screenLockPreventer.Enabled = false;
            }
        }
    }
}
