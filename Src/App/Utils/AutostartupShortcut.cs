using IWshRuntimeLibrary;
using System;

namespace CoffeeBean.Utils
{
    internal static class AutostartupShortcut
    {
        private static string FullName => System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Startup),
            $"{AssemblyInfoRetriever.ExecutableNameWithoutExtension}.lnk");

        public static bool Exists => FileExists(FullName);

        public static void Create()
        {
            CreateShortcut(FullName);
        }

        public static void Remove()
        {
            System.IO.File.Delete(FullName);
        }

        private static bool FileExists(string path)
        {
            try
            {
                _ = System.IO.File.GetAttributes(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void CreateShortcut(string fullName)
        {
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(fullName);
            shortcut.Description = AssemblyInfoRetriever.ExecutableNameWithoutExtension;
            shortcut.TargetPath = AssemblyInfoRetriever.ExecutableFullName;
            shortcut.Save();
        }
    }
}
