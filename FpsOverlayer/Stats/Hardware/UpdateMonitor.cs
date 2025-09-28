using ArnoldVinkCode;
using ArnoldVinkStyles;
using System.Windows;
using static ArnoldVinkCode.AVDisplayMonitor;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowStats
    {
        void UpdateMonitorInformation()
        {
            try
            {
                //Check if the information is visible
                bool MonShowResolution = vSettings.Load("MonShowResolution", typeof(bool));
                bool MonShowDpiResolution = vSettings.Load("MonShowDpiResolution", typeof(bool));
                bool MonShowColorBitDepth = vSettings.Load("MonShowColorBitDepth", typeof(bool));
                bool MonShowColorFormat = vSettings.Load("MonShowColorFormat", typeof(bool));
                bool MonShowColorHdr = vSettings.Load("MonShowColorHdr", typeof(bool));
                bool MonShowRefreshRate = vSettings.Load("MonShowRefreshRate", typeof(bool));
                if (!MonShowResolution && !MonShowColorBitDepth && !MonShowColorFormat && !MonShowColorHdr && !MonShowRefreshRate)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CurrentMon.Visibility = Visibility.Collapsed;
                    });
                    return;
                }

                //Get current active screen
                int monitorNumber = vSettings.Load("DisplayMonitor", typeof(int));
                DisplayMonitor displayMonitorSettings = GetSingleMonitorDisplayConfig(monitorNumber - 1);

                //Get screen resolution
                string screenResolutionString = string.Empty;
                if (MonShowResolution)
                {
                    if (MonShowDpiResolution)
                    {
                        screenResolutionString = " " + displayMonitorSettings.WidthDpi + "x" + displayMonitorSettings.HeightDpi;
                    }
                    else
                    {
                        screenResolutionString = " " + displayMonitorSettings.WidthNative + "x" + displayMonitorSettings.HeightNative;
                    }
                }

                //Get screen color bit depth
                string screenColorBitDepthString = string.Empty;
                if (MonShowColorBitDepth)
                {
                    screenColorBitDepthString = " " + displayMonitorSettings.BitDepth + "bit";
                }

                //Get screen hdr mode
                string screenHdrModeString = string.Empty;
                if (MonShowColorHdr)
                {
                    screenHdrModeString = " " + (displayMonitorSettings.HdrEnabled ? "HDR" : "SDR");
                }

                //Get screen color format
                string screenColorFormatString = string.Empty;
                if (MonShowColorFormat)
                {
                    screenColorFormatString = " " + displayMonitorSettings.ColorFormat;
                }

                //Get screen refresh rate
                string screenRefreshRateString = string.Empty;
                if (MonShowRefreshRate)
                {
                    int screenRefreshRateInt = displayMonitorSettings.RefreshRate;
                    if (screenRefreshRateInt > 0)
                    {
                        screenRefreshRateString = " " + screenRefreshRateInt + "Hz";
                    }
                }

                //Update the screen resolution
                string stringDisplay = AVFunctions.StringRemoveStart(vTitleMON + screenResolutionString + screenColorBitDepthString + screenHdrModeString + screenColorFormatString + screenRefreshRateString, " ");
                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    textblock_CurrentMon.Text = stringDisplay;
                    stackpanel_CurrentMon.Visibility = Visibility.Visible;
                });
            }
            catch { }
        }
    }
}