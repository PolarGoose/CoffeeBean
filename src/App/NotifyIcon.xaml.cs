using CoffeeBean.Utils;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CoffeeBean;

public partial class NotifyIcon
{
    private static readonly string githubUrl = "https://github.com/PolarGoose/CoffeeBean";
    private readonly Icon enabledIcon = LoadIcon("iconEnabled");
    private readonly Icon disabledIcon = LoadIcon("iconDisabled");
    private bool screenLockEnabled;
    private bool screenLockStatusBeforeSessionWasLocked;

    public static string VersionMenuText =>
        $"Version: {AssemblyInfoRetriever.InformationalVersion}\n{githubUrl}";

    public bool IsScreenLockEnabled
    {
        get => screenLockEnabled;
        set
        {
            if (screenLockEnabled == value)
            {
                return;
            }
            ApplyScreenLockStatus(value);
            screenLockEnabled = value;
            EnabledMenuItem.Checked = value;
            TrayIcon.Icon = value ? enabledIcon : disabledIcon;
        }
    }

    public NotifyIcon()
    {
        InitializeComponent();
        TrayIcon.Icon = disabledIcon;
        LaunchAtStartupMenuItem.Checked = AutoStartup.IsEnabled;
        LaunchEnabledMenuItem.Checked = Settings.RunEnabled;
        SystemEvents.SessionSwitch += OnSessionSwitchEvent;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            SystemEvents.SessionSwitch -= OnSessionSwitchEvent;
            TrayIcon.Dispose();
            TrayMenu.Dispose();
            enabledIcon.Dispose();
            disabledIcon.Dispose();
        }

        base.Dispose(disposing);
    }

    private static Icon LoadIcon(string name)
    {
        using var stream = System.Windows.Application.GetResourceStream(new Uri($"/Icons/{name}.ico", UriKind.Relative)).Stream;
        return new Icon(stream);
    }

    private static void ApplyScreenLockStatus(bool isEnabled)
    {
        if(isEnabled)
        {
            WinApi.SetThreadExecutionState(WinApi.EXECUTION_STATE.ES_CONTINUOUS | WinApi.EXECUTION_STATE.ES_DISPLAY_REQUIRED);
        }
        else
        {
            WinApi.SetThreadExecutionState(WinApi.EXECUTION_STATE.ES_CONTINUOUS);
        }
    }

    private void OnSessionSwitchEvent(object sender, SessionSwitchEventArgs e)
    {
        switch (e.Reason)
        {
            case SessionSwitchReason.SessionLock:
                screenLockStatusBeforeSessionWasLocked = IsScreenLockEnabled;
                IsScreenLockEnabled = false;
                break;
            case SessionSwitchReason.SessionUnlock:
                IsScreenLockEnabled = screenLockStatusBeforeSessionWasLocked;
                break;
        }
    }

    private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            IsScreenLockEnabled = !IsScreenLockEnabled;
        }
    }

    private void About_OnClick(object sender, EventArgs e)
    {
        _ = Process.Start("explorer", githubUrl);
    }

    private void Close_OnClick(object sender, EventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
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

    private void LaunchEnabledMenuItem_CheckedChanged(object sender, EventArgs e)
    {
        Settings.RunEnabled = LaunchEnabledMenuItem.Checked;
    }

    private void EnabledMenuItem_CheckedChanged(object sender, EventArgs e)
    {
        IsScreenLockEnabled = EnabledMenuItem.Checked;
    }
}
