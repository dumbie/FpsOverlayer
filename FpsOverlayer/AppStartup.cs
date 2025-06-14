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
                AVStartup.SetupDefaults(ProcessPriority.High, true);

                //Clean application update files
                await UpdateCleanup();

                //Check for available application update
                await UpdateCheck("dumbie", "FpsOverlayer", true);

                //Check application settings
                vWindowSettings.Settings_Check();

                //Check application shortcuts
                vWindowSettings.Shortcuts_Check();

                //Backup Notes
                BackupNotes();

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