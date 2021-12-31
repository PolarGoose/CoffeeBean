using System;
using System.Threading;

namespace CoffeeBean.Utils
{
    public sealed class ScreenLockPreventer
    {
        private readonly int threadId;

        public event Action EnabledChanged;

        private bool enabled;
        public bool Enabled
        {
            get => enabled;
            set
            {
                EnsureCalledFromTheSameThread();
                if (enabled != value)
                {
                    ApplyScreenLockPreventerStatus(value);
                    enabled = value;
                    EnabledChanged?.Invoke();
                }
            }
        }

        public ScreenLockPreventer()
        {
            threadId = Thread.CurrentThread.ManagedThreadId;
        }

        private void EnsureCalledFromTheSameThread()
        {
            if (threadId != Thread.CurrentThread.ManagedThreadId)
            {
                throw new Exception("The screen locking status must be changed from the same thread which created the 'ScreenLocker' object");
            }
        }

        private static void ApplyScreenLockPreventerStatus(bool isEnabled)
        {
            if (isEnabled)
            {
                _ = WinApi.SetThreadExecutionState(WinApi.EXECUTION_STATE.ES_CONTINUOUS | WinApi.EXECUTION_STATE.ES_DISPLAY_REQUIRED);
            }
            else
            {
                _ = WinApi.SetThreadExecutionState(WinApi.EXECUTION_STATE.ES_CONTINUOUS);
            }
        }
    }
}
