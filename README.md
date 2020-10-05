# CoffeeBean
An utility to prevent a policy-enforced screen lock in Windows.<br>
Sometimes Windows is configured to lock the screen after some time of inactivity.<br>
In case this option is enforced by corporate IT policies, it impossible to disable it via Windows settings.<br>
There is a nice [topic on StackExchange](https://superuser.com/questions/329758/how-can-i-prevent-a-policy-enforced-screen-lock-in-windows) which discusses this problem and available solutions.

This utility was created as a convenient and reliable way to solve this problem.

# System requirements
.Net Framework 4.6 or higher
(If you have Windows 10, you don't need to install anything)

# How to use
* Download and unpack the latest release
* Start the executable
* You will get an icon in the system tray
* As you keep this tool running it prevents the screen from auto locking
* You can still lock the screen manually, if you want, by using Windows default methods/shortcuts
* To close the utility, just click "Close" in the tray icon context menu. After utility is closed, screen locking behavior returns to what it was before you started the utility

# How it works
The application uses [built-in into Windows mechanism](https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate) which allows an application to keep system "in use" even if there is no user activity.
It is better than simulating key presses or mouse movements.

# How to build
* Use `Visual Studio 2019` to open the solution file and work with the code
* Run `github/workflows/build.ps1` to build a release (to run this script `git.exe` should be in your PATH)
