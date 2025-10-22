using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowSettings
    {
        //Check - Application Settings
        public void Settings_Check()
        {
            try
            {
                //Server settings
                if (!vSettings.Check("ServerPort")) { vSettings.Set("ServerPort", "26761"); }

                //Display settings
                if (!vSettings.Check("DisplayMonitor")) { vSettings.Set("DisplayMonitor", "1"); }

                if (!vSettings.Check("DisplayBackground")) { vSettings.Set("DisplayBackground", "False"); }
                if (!vSettings.Check("DisplayOpacity")) { vSettings.Set("DisplayOpacity", "0,90"); }
                if (!vSettings.Check("MarginHorizontal")) { vSettings.Set("MarginHorizontal", "5"); }
                if (!vSettings.Check("MarginVertical")) { vSettings.Set("MarginVertical", "5"); }
                if (!vSettings.Check("CheckTaskbarVisible")) { vSettings.Set("CheckTaskbarVisible", "True"); }
                if (!vSettings.Check("StatsFlipBottom")) { vSettings.Set("StatsFlipBottom", "False"); }
                if (!vSettings.Check("HideScreenCapture")) { vSettings.Set("HideScreenCapture", "True"); }
                if (!vSettings.Check("InterfaceFontStyleName")) { vSettings.Set("InterfaceFontStyleName", "Segoe UI"); }
                if (!vSettings.Check("TextPosition")) { vSettings.Set("TextPosition", "0"); }
                if (!vSettings.Check("TextDirection")) { vSettings.Set("TextDirection", "1"); }
                if (!vSettings.Check("TextSize")) { vSettings.Set("TextSize", "18"); }
                if (!vSettings.Check("HardwareUpdateRateMs")) { vSettings.Set("HardwareUpdateRateMs", "1000"); }

                if (!vSettings.Check("TextColorSingle")) { vSettings.Set("TextColorSingle", "False"); }
                if (!vSettings.Check("ColorBackground")) { vSettings.Set("ColorBackground", "#1D1D1D"); }
                if (!vSettings.Check("ColorSingle")) { vSettings.Set("ColorSingle", "#F1F1F1"); }
                if (!vSettings.Check("ColorGpu")) { vSettings.Set("ColorGpu", "#A3FF39"); }
                if (!vSettings.Check("ColorCpu")) { vSettings.Set("ColorCpu", "#00EAFF"); }
                if (!vSettings.Check("ColorMem")) { vSettings.Set("ColorMem", "#FFA200"); }
                if (!vSettings.Check("ColorFan")) { vSettings.Set("ColorFan", "#E7CEB5"); }
                if (!vSettings.Check("ColorFps")) { vSettings.Set("ColorFps", "#FF0505"); }
                if (!vSettings.Check("ColorFrametime")) { vSettings.Set("ColorFrametime", "#F1F1F1"); }
                if (!vSettings.Check("ColorNet")) { vSettings.Set("ColorNet", "#FF00A8"); }
                if (!vSettings.Check("ColorApp")) { vSettings.Set("ColorApp", "#C000FF"); }
                if (!vSettings.Check("ColorBat")) { vSettings.Set("ColorBat", "#FFE115"); }
                if (!vSettings.Check("ColorTime")) { vSettings.Set("ColorTime", "#21AFFF"); }
                if (!vSettings.Check("ColorCustomText")) { vSettings.Set("ColorCustomText", "#F1F1F1"); }
                if (!vSettings.Check("ColorMon")) { vSettings.Set("ColorMon", "#21A000"); }

                if (!vSettings.Check("GpuCategoryTitle")) { vSettings.Set("GpuCategoryTitle", "GPU"); }
                if (!vSettings.Check("GpuShowCategoryTitle")) { vSettings.Set("GpuShowCategoryTitle", "True"); }
                if (!vSettings.Check("GpuShowName")) { vSettings.Set("GpuShowName", "False"); }
                if (!vSettings.Check("GpuShowPercentage")) { vSettings.Set("GpuShowPercentage", "True"); }
                if (!vSettings.Check("GpuShowMemoryUsed")) { vSettings.Set("GpuShowMemoryUsed", "True"); }
                if (!vSettings.Check("GpuShowTemperature")) { vSettings.Set("GpuShowTemperature", "True"); }
                if (!vSettings.Check("GpuShowTemperatureHotspot")) { vSettings.Set("GpuShowTemperatureHotspot", "False"); }
                if (!vSettings.Check("GpuShowMemorySpeed")) { vSettings.Set("GpuShowMemorySpeed", "False"); }
                if (!vSettings.Check("GpuShowCoreFrequency")) { vSettings.Set("GpuShowCoreFrequency", "True"); }
                if (!vSettings.Check("GpuShowFanSpeed")) { vSettings.Set("GpuShowFanSpeed", "True"); }
                if (!vSettings.Check("GpuShowPowerWatt")) { vSettings.Set("GpuShowPowerWatt", "True"); }
                if (!vSettings.Check("GpuShowPowerVolt")) { vSettings.Set("GpuShowPowerVolt", "False"); }

                if (!vSettings.Check("CpuCategoryTitle")) { vSettings.Set("CpuCategoryTitle", "CPU"); }
                if (!vSettings.Check("CpuShowCategoryTitle")) { vSettings.Set("CpuShowCategoryTitle", "True"); }
                if (!vSettings.Check("CpuShowName")) { vSettings.Set("CpuShowName", "False"); }
                if (!vSettings.Check("BoardShowName")) { vSettings.Set("BoardShowName", "False"); }
                if (!vSettings.Check("CpuShowPercentage")) { vSettings.Set("CpuShowPercentage", "True"); }
                if (!vSettings.Check("CpuShowTemperature")) { vSettings.Set("CpuShowTemperature", "True"); }
                if (!vSettings.Check("CpuShowCoreFrequency")) { vSettings.Set("CpuShowCoreFrequency", "True"); }
                if (!vSettings.Check("CpuShowPowerWatt")) { vSettings.Set("CpuShowPowerWatt", "True"); }
                if (!vSettings.Check("CpuShowPowerVolt")) { vSettings.Set("CpuShowPowerVolt", "False"); }
                if (!vSettings.Check("CpuShowFanSpeed")) { vSettings.Set("CpuShowFanSpeed", "True"); }

                if (!vSettings.Check("MemCategoryTitle")) { vSettings.Set("MemCategoryTitle", "MEM"); }
                if (!vSettings.Check("MemShowCategoryTitle")) { vSettings.Set("MemShowCategoryTitle", "True"); }
                if (!vSettings.Check("MemShowName")) { vSettings.Set("MemShowName", "False"); }
                if (!vSettings.Check("MemShowSpeed")) { vSettings.Set("MemShowSpeed", "True"); }
                if (!vSettings.Check("MemShowTemperature")) { vSettings.Set("MemShowTemperature", "True"); }
                if (!vSettings.Check("MemShowPowerVolt")) { vSettings.Set("MemShowPowerVolt", "False"); }
                if (!vSettings.Check("MemShowPercentage")) { vSettings.Set("MemShowPercentage", "True"); }
                if (!vSettings.Check("MemShowUsed")) { vSettings.Set("MemShowUsed", "True"); }
                if (!vSettings.Check("MemShowFree")) { vSettings.Set("MemShowFree", "False"); }
                if (!vSettings.Check("MemShowTotal")) { vSettings.Set("MemShowTotal", "True"); }

                if (!vSettings.Check("NetCategoryTitle")) { vSettings.Set("NetCategoryTitle", "NET"); }
                if (!vSettings.Check("NetShowCategoryTitle")) { vSettings.Set("NetShowCategoryTitle", "True"); }
                if (!vSettings.Check("NetShowCurrentUsage")) { vSettings.Set("NetShowCurrentUsage", "False"); }

                if (!vSettings.Check("AppShowName")) { vSettings.Set("AppShowName", "False"); }

                if (!vSettings.Check("MonCategoryTitle")) { vSettings.Set("MonCategoryTitle", "MON"); }
                if (!vSettings.Check("MonShowCategoryTitle")) { vSettings.Set("MonShowCategoryTitle", "True"); }
                if (!vSettings.Check("MonShowResolution")) { vSettings.Set("MonShowResolution", "True"); }
                if (!vSettings.Check("MonShowDpiResolution")) { vSettings.Set("MonShowDpiResolution", "False"); }
                if (!vSettings.Check("MonShowColorBitDepth")) { vSettings.Set("MonShowColorBitDepth", "True"); }
                if (!vSettings.Check("MonShowColorFormat")) { vSettings.Set("MonShowColorFormat", "True"); }
                if (!vSettings.Check("MonShowColorHdr")) { vSettings.Set("MonShowColorHdr", "True"); }
                if (!vSettings.Check("MonShowRefreshRate")) { vSettings.Set("MonShowRefreshRate", "True"); }

                if (!vSettings.Check("BatCategoryTitle")) { vSettings.Set("BatCategoryTitle", "BAT"); }
                if (!vSettings.Check("BatShowCategoryTitle")) { vSettings.Set("BatShowCategoryTitle", "True"); }
                if (!vSettings.Check("BatShowPercentage")) { vSettings.Set("BatShowPercentage", "True"); }

                if (!vSettings.Check("TimeShowCurrentTime")) { vSettings.Set("TimeShowCurrentTime", "True"); }
                if (!vSettings.Check("TimeShowCurrentDate")) { vSettings.Set("TimeShowCurrentDate", "True"); }

                if (!vSettings.Check("CustomTextString")) { vSettings.Set("CustomTextString", ""); }

                //Frames per second
                if (!vSettings.Check("FpsCategoryTitle")) { vSettings.Set("FpsCategoryTitle", "FPS"); }
                if (!vSettings.Check("FpsShowCategoryTitle")) { vSettings.Set("FpsShowCategoryTitle", "False"); }
                if (!vSettings.Check("FpsShowCurrentFps")) { vSettings.Set("FpsShowCurrentFps", "True"); }
                if (!vSettings.Check("FpsShowCurrentLatency")) { vSettings.Set("FpsShowCurrentLatency", "True"); }
                if (!vSettings.Check("FpsShowAverageFps")) { vSettings.Set("FpsShowAverageFps", "True"); }
                if (!vSettings.Check("FpsAverageSeconds")) { vSettings.Set("FpsAverageSeconds", "10"); }

                //Frame times
                if (!vSettings.Check("FrametimeGraphShow")) { vSettings.Set("FrametimeGraphShow", "True"); }
                if (!vSettings.Check("FrametimeAccuracy")) { vSettings.Set("FrametimeAccuracy", "2"); }
                if (!vSettings.Check("FrametimeWidth")) { vSettings.Set("FrametimeWidth", "400"); }
                if (!vSettings.Check("FrametimeHeight")) { vSettings.Set("FrametimeHeight", "40"); }

                //Fan
                if (!vSettings.Check("FanCategoryTitle")) { vSettings.Set("FanCategoryTitle", "FAN"); }
                if (!vSettings.Check("FanShowCategoryTitle")) { vSettings.Set("FanShowCategoryTitle", "True"); }
                if (!vSettings.Check("FanShowCpu")) { vSettings.Set("FanShowCpu", "False"); }
                if (!vSettings.Check("FanShowGpu")) { vSettings.Set("FanShowGpu", "False"); }
                if (!vSettings.Check("FanShowPump")) { vSettings.Set("FanShowPump", "True"); }
                if (!vSettings.Check("FanShowSystem")) { vSettings.Set("FanShowSystem", "True"); }

                //Crosshair
                if (!vSettings.Check("CrosshairLaunch")) { vSettings.Set("CrosshairLaunch", "False"); }
                if (!vSettings.Check("CrosshairColor")) { vSettings.Set("CrosshairColor", "#FFFFFF"); }
                if (!vSettings.Check("CrosshairOpacity")) { vSettings.Set("CrosshairOpacity", "0,80"); }
                if (!vSettings.Check("CrosshairVerticalPosition")) { vSettings.Set("CrosshairVerticalPosition", "0"); }
                if (!vSettings.Check("CrosshairHorizontalPosition")) { vSettings.Set("CrosshairHorizontalPosition", "0"); }
                if (!vSettings.Check("CrosshairSize")) { vSettings.Set("CrosshairSize", "10"); }
                if (!vSettings.Check("CrosshairStyle")) { vSettings.Set("CrosshairStyle", "0"); }
                if (!vSettings.Check("CrosshairThickness")) { vSettings.Set("CrosshairThickness", "1"); }

                //Tools
                if (!vSettings.Check("ToolsLaunch")) { vSettings.Set("ToolsLaunch", "False"); }
                if (!vSettings.Check("ToolsShowBrowser")) { vSettings.Set("ToolsShowBrowser", "True"); }
                if (!vSettings.Check("ToolsShowNotes")) { vSettings.Set("ToolsShowNotes", "True"); }

                //Browser
                if (!vSettings.Check("BrowserUnload")) { vSettings.Set("BrowserUnload", "True"); }
                if (!vSettings.Check("BrowserOpacity")) { vSettings.Set("BrowserOpacity", "0,70"); }

                //Check stats position
                CheckStatsPositionExists(false);
                CheckStatsPositionDouble();

                Debug.WriteLine("Checked the application settings.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to check the application settings: " + ex.Message);
            }
        }

        void CheckStatsPositionExists(bool forceReset)
        {
            try
            {
                if (forceReset || !vSettings.Check("FpsId")) { vSettings.Set("FpsId", "0"); }
                if (forceReset || !vSettings.Check("AppId")) { vSettings.Set("AppId", "1"); }
                if (forceReset || !vSettings.Check("CpuId")) { vSettings.Set("CpuId", "2"); }
                if (forceReset || !vSettings.Check("GpuId")) { vSettings.Set("GpuId", "3"); }
                if (forceReset || !vSettings.Check("MemId")) { vSettings.Set("MemId", "4"); }
                if (forceReset || !vSettings.Check("FanId")) { vSettings.Set("FanId", "5"); }
                if (forceReset || !vSettings.Check("MonId")) { vSettings.Set("MonId", "6"); }
                if (forceReset || !vSettings.Check("NetId")) { vSettings.Set("NetId", "7"); }
                if (forceReset || !vSettings.Check("BatId")) { vSettings.Set("BatId", "8"); }
                if (forceReset || !vSettings.Check("CustomTextId")) { vSettings.Set("CustomTextId", "9"); }
                if (forceReset || !vSettings.Check("TimeId")) { vSettings.Set("TimeId", "10"); }
                if (forceReset || !vSettings.Check("FrametimeId")) { vSettings.Set("FrametimeId", "11"); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to check existing stats position: " + ex.Message);
            }
        }

        void CheckStatsPositionDouble()
        {
            try
            {
                //Load used stat positions
                List<int> usedStatPositions = new List<int>()
                {
                    vSettings.Load("AppId", typeof(int)),
                    vSettings.Load("FpsId", typeof(int)),
                    vSettings.Load("FrametimeId", typeof(int)),
                    vSettings.Load("NetId", typeof(int)),
                    vSettings.Load("CpuId", typeof(int)),
                    vSettings.Load("GpuId", typeof(int)),
                    vSettings.Load("MemId", typeof(int)),
                    vSettings.Load("TimeId", typeof(int)),
                    vSettings.Load("CustomTextId", typeof(int)),
                    vSettings.Load("MonId", typeof(int)),
                    vSettings.Load("BatId", typeof(int)),
                    vSettings.Load("FanId", typeof(int))
                };

                //Check if there are double positions
                bool doublePositions = usedStatPositions.GroupBy(x => x).Any(x => x.Count() > 1);

                //Reset stat positions when double found
                if (doublePositions)
                {
                    Debug.WriteLine("Found double stat positions, resetting the order.");
                    CheckStatsPositionExists(true);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to check double stats position: " + ex.Message);
            }
        }
    }
}