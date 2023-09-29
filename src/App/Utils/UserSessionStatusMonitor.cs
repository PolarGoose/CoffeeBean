using Microsoft.Win32;
namespace CoffeeBean.Utils;

internal sealed class UserSessionStatusMonitor
{
    private readonly ScreenLockController screenLockController;
    private bool screenLockStatusBeforeSessionWasLocked;

    public UserSessionStatusMonitor(ScreenLockController screenLockController)
    {
        this.screenLockController = screenLockController;
        SystemEvents.SessionSwitch += OnSessionSwitchEvent;
    }

    private void OnSessionSwitchEvent(object sender, SessionSwitchEventArgs e)
    {
        switch (e.Reason)
        {
            case SessionSwitchReason.SessionLock:
                screenLockStatusBeforeSessionWasLocked = screenLockController.Enabled;
                screenLockController.Enabled = false;
                break;
            case SessionSwitchReason.SessionUnlock:
                screenLockController.Enabled = screenLockStatusBeforeSessionWasLocked;
                break;
        }
    }
}
