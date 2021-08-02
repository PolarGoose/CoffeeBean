using CoffeeBean.Utils;
using System;
using System.Diagnostics;
using System.Windows;

namespace CoffeeBean
{
    public partial class ContextMenu
    {
        private readonly string githubLink = "https://github.com/PolarGoose/CoffeeBean";

        public ContextMenu()
        {
            InitializeComponent();
            VersionMenuItem.Text = $"Version: {AssemblyInfoRetriever.InformationalVersion}\n{githubLink}";
            LaunchAtStartupMenuItem.Checked = AutostartupShortcut.Exists;
        }

        private void About_OnClick(object sender, EventArgs e)
        {
            _ = Process.Start("explorer", githubLink);
        }

        private void Close_OnClick(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnLaunchAtStatrup_Click(object sender, EventArgs e)
        {
            try
            {
                if (AutostartupShortcut.Exists)
                {
                    AutostartupShortcut.Remove();
                }
                else
                {
                    AutostartupShortcut.Create();
                }
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(ex.Message);
            }

            LaunchAtStartupMenuItem.Checked = AutostartupShortcut.Exists;
        }
    }
}
