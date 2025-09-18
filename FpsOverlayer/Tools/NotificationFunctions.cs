using ArnoldVinkStyles;
using System;
using System.Windows;
using static ArnoldVinkStyles.AVImage;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowTools : Window
    {
        //Show notification
        private void Notification_Show_Status(string icon, string text)
        {
            try
            {
                //Update notification
                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    try
                    {
                        //Set notification text
                        image_Notification_Icon.Source = FileToBitmapImage(new string[] { "Assets/Default/Icons/" + icon + ".png" }, null, vImageBackupSource, -1, -1, IntPtr.Zero, 0);
                        textblock_Notification_Status.Text = text;

                        //Show notification
                        border_Notification.Visibility = Visibility.Visible;
                    }
                    catch { }
                });

                //Start notification timer
                vTimerOverlay.Interval = 3000;
                vTimerOverlay.TickSet = delegate
                {
                    try
                    {
                        AVDispatcherInvoke.DispatcherInvoke(delegate
                        {
                            //Hide notification
                            border_Notification.Visibility = Visibility.Collapsed;
                        });
                    }
                    catch { }
                };
                vTimerOverlay.Start();
            }
            catch { }
        }
    }
}