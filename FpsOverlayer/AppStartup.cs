using ArnoldVinkCode;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static ArnoldVinkCode.AVInteropDll;
using static ArnoldVinkCode.AVSettings;
using static ArnoldVinkCode.AVUpdate;
using static FpsOverlayer.AppBackup;
using static FpsOverlayer.AppHotkeys;
using static FpsOverlayer.AppVariables;
using static FpsOverlayer.DriverCheck;
using static FpsOverlayer.SocketHandlers;

namespace FpsOverlayer
{
    public class AppStartup
    {
        public async static Task Startup()
        {
            try
            {
                Debug.WriteLine("Welcome to application.");

                //Setup application defaults
                AVStartup.SetupDefaults(ProcessPriorityClasses.HIGH_PRIORITY_CLASS, true);

                //Clean application update files
                await UpdateCleanup();

                //Check for available application update
                await UpdateCheck("dumbie", "FpsOverlayer", true);

                //Check application user folders
                vWindowSettings.Folders_Check();

                //Check application settings
                vWindowSettings.Settings_Check();

                //Check application shortcuts
                vWindowSettings.Shortcuts_Check();

                //Backup Profiles and Notes
                BackupJsonProfiles();

                //Check if drivers are installed
                CheckDrivers();

                //Show windows
                vWindowStats.Show();
                vWindowTools.Show();
                vWindowCrosshair.Show();

                //Register keyboard hotkeys
                AVInputOutputHotkeyHook.Start();
                AVInputOutputHotkeyHook.EventHotkeyPressedList += EventHotkeyPressed;

                //Enable the socket server
                await EnableSocketServer();
            }
            catch { }
        }

        //Enable the socket server
        private static async Task EnableSocketServer()
        {
            try
            {
                int socketServerPort = SettingLoad(vConfigurationFpsOverlayer, "ServerPort", typeof(int));
                vArnoldVinkSockets = new ArnoldVinkSockets("127.0.0.1", socketServerPort, false, true);
                vArnoldVinkSockets.vSocketTimeout = 250;
                vArnoldVinkSockets.EventBytesReceived += ReceivedSocketHandler;
                await vArnoldVinkSockets.SocketServerEnable();
            }
            catch { }
        }
    }
}