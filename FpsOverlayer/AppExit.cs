using ArnoldVinkCode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static FpsOverlayer.AppTasks;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public class AppExit
    {
        public static async Task Exit_Prompt()
        {
            try
            {
                List<string> messageAnswers = new List<string>();
                messageAnswers.Add("Exit application");
                messageAnswers.Add("Cancel");

                string messageResult = AVMessageBox.Popup(vWindowSettings, "Do you really want to exit FpsOverlayer?", "This will hide the stats, tools and crosshair overlays.", messageAnswers);
                if (messageResult == "Exit application")
                {
                    await Exit();
                }
            }
            catch { }
        }

        public async static Task Exit()
        {
            try
            {
                Debug.WriteLine("Exiting application.");

                //Stop monitoring hardware
                vHardwareComputer.Close();

                //Stop background tasks
                await TasksBackgroundStop();

                //Disable socket server
                if (vArnoldVinkSockets != null)
                {
                    await vArnoldVinkSockets.SocketServerDisable();
                }

                //Hide visible tray icon
                AppTray.TrayNotifyIcon.Visible = false;

                //Exit application
                Environment.Exit(0);
            }
            catch { }
        }
    }
}