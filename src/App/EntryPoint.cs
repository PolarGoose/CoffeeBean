using CoffeeBean.Utils;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;

namespace CoffeeBean;

internal sealed class SingleInstanceManager : WindowsFormsApplicationBase
{
    private NotifyIcon notifyIcon;

    public SingleInstanceManager()
    {
        IsSingleInstance = true;
    }

    protected override bool OnStartup(StartupEventArgs e)
    {
        notifyIcon = new NotifyIcon();
        if (ParseAndApplyCommandLineArguments(e.CommandLine))
        {
            new System.Windows.Application().Run();
        }
        notifyIcon.Dispose();
        return false;
    }

    protected override void OnStartupNextInstance(StartupNextInstanceEventArgs e)
    {
        ParseAndApplyCommandLineArguments(e.CommandLine);
        e.BringToForeground = false;
    }

    private bool ParseAndApplyCommandLineArguments(IReadOnlyList<string> args)
    {
        switch (args)
        {
            case []:
                notifyIcon.IsScreenLockEnabled = Settings.RunEnabled;
                return true;
            case ["enable"]:
                notifyIcon.IsScreenLockEnabled = true;
                return true;
            case ["disable"]:
                notifyIcon.IsScreenLockEnabled = false;
                return true;
            case [var argument]:
                ErrorDialog.Show($"Unknown command line argument '{argument}'.\nUsage:\nCoffeeBean.exe [enable|disable]");
                return false;
            default:
                ErrorDialog.Show("Too many command line arguments are provided.\nUsage:\nCoffeeBean.exe [enable|disable]");
                return false;
        }
    }
}

internal static class EntryPoint
{
    [STAThread]
    private static void Main(string[] args)
    {
        var manager = new SingleInstanceManager();
        manager.Run(args);
    }
}
