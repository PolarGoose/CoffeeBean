using CoffeeBean.Utils;
using System;
using System.Diagnostics;
using System.Windows;

namespace CoffeeBean;

public partial class ContextMenu
{
    private readonly string githubUrl = "https://github.com/PolarGoose/CoffeeBean";
    private readonly ScreenLockController screenLockController;

    public ContextMenu(ScreenLockController screenLockController)
    {
        InitializeComponent();

        VersionMenuItem.Text = $"Version: {AssemblyInfoRetriever.InformationalVersion}\n{githubUrl}";

        LaunchAtStartupMenuItem.Checked = AutoStartup.IsEnabled;

        this.screenLockController = screenLockController;
        this.screenLockController.EnabledChanged += () => EnabledMenuItem.Checked = this.screenLockController.Enabled;
    }

    private void About_OnClick(object sender, EventArgs e)
    {
        _ = Process.Start("explorer", githubUrl);
    }

    private void Close_OnClick(object sender, EventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void LaunchAtStartupMenuItem_CheckedChanged(object sender, EventArgs e)
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
        screenLockController.Enabled = EnabledMenuItem.Checked;
    }
}
