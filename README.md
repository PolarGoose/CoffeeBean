# CoffeeBean
![Screenshot](doc/Screenshot.png)<br>
An utility to prevent a policy-enforced screen lock in Windows.<br>
Sometimes Windows is configured to lock the screen after some time of inactivity.<br>
In case this option is enforced by corporate IT policies, it impossible to disable it via Windows settings.<br>
There is a nice [topic on StackExchange](https://superuser.com/questions/329758/how-can-i-prevent-a-policy-enforced-screen-lock-in-windows) which discusses this problem and available solutions.

This utility was created as a convenient and reliable way to solve this problem.

# System requirements
.Net Framework 4.6.1 (Windows 10 already has it)

# How to install
* Download `CoffeeBean.zip` (portable executable) or `CoffeeBean.msi` from the latest [release](https://github.com/PolarGoose/CoffeeBean/releases)
* Run the installer if you want to install the application. The installer will install into a `%AppData%\CoffeeBean` folder and create a desktop icon.

# How to use
* Start the application and you will get an icon in the system tray
* As you keep this tool running it prevents the screen from auto locking
* You can still lock the screen manually, if you want, by using Windows default methods/shortcuts (f.e. by pressing Win+L)
* To close the utility, just click "Close" in the tray icon context menu. After utility is closed, screen locking behavior returns to what it was before you started the utility
* If you want the utility to run on startup, use `Launch at startup` context menu item, it will create a shortcut in the `%AppData%\Microsoft\Windows\Start Menu\Programs\Startup` folder

# How it works
The application uses [built-in into Windows mechanism](https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate) which allows an application to keep system "in use" even if there is no user activity.
It is better than simulating key presses or mouse movements.

# How to build
* Use `Visual Studio 2019` with [Wix Toolset Visual Studio 2019 Extension](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019Extension) and [Wix Toolset](https://wixtoolset.org/releases/)
* Run `github/workflows/build.ps1` to build a release (to run this script `git.exe` should be in your PATH)
