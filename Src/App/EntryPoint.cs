using CoffeeBean.Utils;
using System;

namespace CoffeeBean
{
    internal class EntryPoint
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var manager = new SingleInstanceManager();
            manager.Run(args);
        }
    }
}
