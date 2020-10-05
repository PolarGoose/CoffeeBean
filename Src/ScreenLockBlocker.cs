using System;
using System.Runtime.InteropServices;

namespace CoffeeBean
{
    internal static class WinApi
    {
        // https://www.pinvoke.net/default.aspx/kernel32/SetThreadExecutionState.html

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
    }

    internal sealed class ScreenLockBlocker : IDisposable
    {
        private readonly WinApi.EXECUTION_STATE previousState;

        public ScreenLockBlocker()
        {
            // https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate
            previousState = WinApi.SetThreadExecutionState(WinApi.EXECUTION_STATE.ES_CONTINUOUS |
                                                           WinApi.EXECUTION_STATE.ES_DISPLAY_REQUIRED);
        }

        public void Dispose()
        {
            WinApi.SetThreadExecutionState(previousState);
            GC.SuppressFinalize(this);
        }

        ~ScreenLockBlocker()
        {
            Dispose();
        }
    }
}
