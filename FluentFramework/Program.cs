using FluentFramework.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FluentFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            KillDrivers();
            CopyDrivers();
            //CopyServerJar();
            //StartServer();
        }

        static void KillDrivers()
        {
            var processes = Process.GetProcessesByName("chromedriver");
            foreach (var process in processes)
                process.Kill();
            processes = Process.GetProcessesByName("IEDriverServer");
            foreach (var process in processes)
                process.Kill();
        }

        static void CopyDrivers()
        {
            var driverDirectory = new Settings().DriverDirectory;
            if (Directory.Exists(driverDirectory))
                Directory.Delete(driverDirectory, true);
            Directory.CreateDirectory(driverDirectory);

            var currentDir = Directory.GetCurrentDirectory();
            var driverSourceDir = $"{Directory.GetParent(currentDir).Parent.Parent.FullName}/FluentFramework/Drivers/";

            foreach (var file in Directory.GetFiles(driverSourceDir))
            {
                File.Copy(file, $"{driverDirectory}/{file.Split('/').Last()}");
            }
        }

        static void CopyServerJar()
        {
            var serverDirectory = new Settings().ServerDirectory;
            if (Directory.Exists(serverDirectory))
                Directory.Delete(serverDirectory, true);
            Directory.CreateDirectory(serverDirectory);

            var currentDir = Directory.GetCurrentDirectory();
            var driverSourceDir = $"{Directory.GetParent(currentDir).Parent.Parent.FullName}/FluentFramework/SeleniumServer/";

            foreach (var file in Directory.GetFiles(driverSourceDir))
            {
                File.Copy(file, $"{serverDirectory}/{file.Split('/').Last()}");
            }
        }

        static void StartServer()
        {
            try
            {
                string strCmdText =
                    "/C java -jar C:/Selenium/SeleniumServer/selenium-server-4.0.0-alpha-3.jar standalone";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
