# CoffeeBean
![Screenshot](doc/Screenshot.png)<br>
A utility to prevent a policy-enforced screen lock in Windows.<br>
Windows can be configured to lock the screen after some time of inactivity.<br>
In case corporate IT policies enforce this option, it is impossible to disable it via Windows settings.<br>
This utility is a convenient and reliable way to solve this problem.

# System requirements
* Windows 7 x32/x64 or higher (you might need to install [.Net Framework 4.6.2](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net462-web-installer))

# How to install
* Download `CoffeeBean.exe.zip` (portable executable) or `CoffeeBean.msi` from the latest [release](https://github.com/PolarGoose/CoffeeBean/releases)
* Run the installer if you want to install the application. The installer will install into a `%AppData%\CoffeeBean` folder and create a desktop icon.

# How to use
* Start the application, and you will get an icon in the system tray
* As you keep this tool running, it prevents the screen from auto-locking
* You can still lock the screen manually, if you want, by using Windows default methods/shortcuts (for example, by pressing `Win+L`)
* To close the utility, click `Close` in the tray icon context menu. After the utility is closed, the locking behavior returns to what it was before you started the utility
* Use the `Launch at startup` context menu item if you want the application to run at Windows startup.
* Use the `Enable` context menu item to turn the screen lock prevention functionality on or off.

## Running the application from console
There are command line arguments to control screen lock functionality. You can use them in the following way:
* Launch the application with screen lock prevention functionality enabled or enable the screen lock prevention functionality of an already running instance of the application:
```
CoffeeBean.exe enable
```
* Launch the application with screen lock prevention functionality disabled or turn off screen lock functionality of an already running instance of the application:
```
CoffeeBean.exe disable
```

# How it works
The application uses [SetThreadExecutionState](https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate) WinAPI method, which allows an application to keep the system "in use" even if there is no user activity. It is better than simulating key presses or mouse movements. For instance, it is the function that video players use to keep the screen on while playing a video.

# How to build
* Use `Visual Studio 2022` with [Wix Toolset Visual Studio 2022 Extension](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2022Extension) and [Wix Toolset](https://wixtoolset.org/releases/)
* Run `github/workflows/build.ps1` to build a release (to run this script `git.exe` should be in your PATH)

# References
* [Discussion of the screen locking problem on StackExchange](https://superuser.com/questions/329758/how-can-i-prevent-a-policy-enforced-screen-lock-in-windows)
