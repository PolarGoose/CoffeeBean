using Microsoft.VisualBasic.ApplicationServices;

namespace CoffeeBean.Utils
{
    // https://stackoverflow.com/a/19326/7585517
    // https://github.com/microsoft/WPF-Samples/tree/master/Application%20Management/SingleInstanceDetection
    internal sealed class SingleInstanceManager : WindowsFormsApplicationBase
    {
        public SingleInstanceManager()
        {
            IsSingleInstance = true;
        }

        protected override bool OnStartup(StartupEventArgs e)
        {
            App.Main();
            return false;
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            base.OnStartupNextInstance(eventArgs);
            eventArgs.BringToForeground = false;
        }
    }
}
