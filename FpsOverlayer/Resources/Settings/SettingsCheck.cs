using System;
using System.Diagnostics;
using System.Linq;
using static ArnoldVinkCode.AVJsonFunctions;
using static FpsOverlayer.AppVariables;
using static LibraryShared.Classes;

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
                if (!vSettings.Check("ColorRenderer")) { vSettings.Set("ColorRenderer", "#FF7070"); }
                if (!vSettings.Check("ColorApp")) { vSettings.Set("ColorApp", "#C000FF"); }
                if (!vSettings.Check("ColorBat")) { vSettings.Set("ColorBat", "#FFE115"); }
                if (!vSettings.Check("ColorTime")) { vSettings.Set("ColorTime", "#21AFFF"); }
                if (!vSettings.Check("ColorCustomText")) { vSettings.Set("ColorCustomText", "#F1F1F1"); }
                if (!vSettings.Check("ColorMon")) { vSettings.Set("ColorMon", "#21A000"); }

                if (!vSettings.Check("GpuIndex")) { vSettings.Set("GpuIndex", "0"); }
                if (!vSettings.Check("GpuCategoryTitle")) { vSettings.Set("GpuCategoryTitle", "GPU"); }
                if (!vSettings.Check("GpuShowCategoryTitle")) { vSettings.Set("GpuShowCategoryTitle", "True"); }
                if (!vSettings.Check("GpuShowName")) { vSettings.Set("GpuShowName", "False"); }
                if (!vSettings.Check("GpuShowPercentage")) { vSettings.Set("GpuShowPercentage", "True"); }
                if (!vSettings.Check("GpuShowMemoryUsed")) { vSettings.Set("GpuShowMemoryUsed", "True"); }
                if (!vSettings.Check("GpuShowTemperature")) { vSettings.Set("GpuShowTemperature", "True"); }
                if (!vSettings.Check("GpuShowTemperatureMemory")) { vSettings.Set("GpuShowTemperatureMemory", "False"); }
                if (!vSettings.Check("GpuShowTemperatureHotspot")) { vSettings.Set("GpuShowTemperatureHotspot", "False"); }
                if (!vSettings.Check("GpuShowMemorySpeed")) { vSettings.Set("GpuShowMemorySpeed", "False"); }
                if (!vSettings.Check("GpuShowCoreFrequency")) { vSettings.Set("GpuShowCoreFrequency", "True"); }
                if (!vSettings.Check("GpuShowFanSpeed")) { vSettings.Set("GpuShowFanSpeed", "False"); }
                if (!vSettings.Check("GpuShowPowerWatt")) { vSettings.Set("GpuShowPowerWatt", "True"); }
                if (!vSettings.Check("GpuShowPowerVolt")) { vSettings.Set("GpuShowPowerVolt", "True"); }

                if (!vSettings.Check("CpuCategoryTitle")) { vSettings.Set("CpuCategoryTitle", "CPU"); }
                if (!vSettings.Check("CpuShowCategoryTitle")) { vSettings.Set("CpuShowCategoryTitle", "True"); }
                if (!vSettings.Check("CpuShowName")) { vSettings.Set("CpuShowName", "False"); }
                if (!vSettings.Check("BoardShowName")) { vSettings.Set("BoardShowName", "False"); }
                if (!vSettings.Check("CpuShowPercentage")) { vSettings.Set("CpuShowPercentage", "True"); }
                if (!vSettings.Check("CpuShowTemperature")) { vSettings.Set("CpuShowTemperature", "True"); }
                if (!vSettings.Check("CpuShowCoreFrequency")) { vSettings.Set("CpuShowCoreFrequency", "True"); }
                if (!vSettings.Check("CpuShowPowerWatt")) { vSettings.Set("CpuShowPowerWatt", "True"); }
                if (!vSettings.Check("CpuShowPowerVolt")) { vSettings.Set("CpuShowPowerVolt", "True"); }
                if (!vSettings.Check("CpuShowFanSpeed")) { vSettings.Set("CpuShowFanSpeed", "False"); }

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

                if (!vSettings.Check("NetIndex")) { vSettings.Set("NetIndex", "0"); }
                if (!vSettings.Check("NetCategoryTitle")) { vSettings.Set("NetCategoryTitle", "NET"); }
                if (!vSettings.Check("NetShowCategoryTitle")) { vSettings.Set("NetShowCategoryTitle", "True"); }
                if (!vSettings.Check("NetShowCurrentUsage")) { vSettings.Set("NetShowCurrentUsage", "False"); }

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
                if (!vSettings.Check("FpsShowOnePercentLowFps")) { vSettings.Set("FpsShowOnePercentLowFps", "True"); }

                //Frame times
                if (!vSettings.Check("FrametimeGraphShow")) { vSettings.Set("FrametimeGraphShow", "True"); }
                if (!vSettings.Check("FrametimeAccuracy")) { vSettings.Set("FrametimeAccuracy", "2"); }
                if (!vSettings.Check("FrametimeWidth")) { vSettings.Set("FrametimeWidth", "400"); }
                if (!vSettings.Check("FrametimeHeight")) { vSettings.Set("FrametimeHeight", "40"); }

                //Fan
                if (!vSettings.Check("FanCategoryTitle")) { vSettings.Set("FanCategoryTitle", "FAN"); }
                if (!vSettings.Check("FanShowCategoryTitle")) { vSettings.Set("FanShowCategoryTitle", "True"); }
                if (!vSettings.Check("FanShowCpu")) { vSettings.Set("FanShowCpu", "True"); }
                if (!vSettings.Check("FanShowGpu")) { vSettings.Set("FanShowGpu", "True"); }
                if (!vSettings.Check("FanShowPump")) { vSettings.Set("FanShowPump", "True"); }
                if (!vSettings.Check("FanShowSystem")) { vSettings.Set("FanShowSystem", "True"); }

                //Applications
                if (!vSettings.Check("AppShowName")) { vSettings.Set("AppShowName", "False"); }

                //Renderer
                if (!vSettings.Check("RendererCategoryTitle")) { vSettings.Set("RendererCategoryTitle", "REN"); }
                if (!vSettings.Check("RendererShowCategoryTitle")) { vSettings.Set("RendererShowCategoryTitle", "False"); }
                if (!vSettings.Check("AppShow3dOnly")) { vSettings.Set("AppShow3dOnly", "False"); }
                if (!vSettings.Check("RendererShowApi")) { vSettings.Set("RendererShowApi", "False"); }

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

                //Check stats order
                CheckStatsOrderExists();
                CheckStatsOrderDouble();

                Debug.WriteLine("Checked the application settings.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to check the application settings: " + ex.Message);
            }
        }

        void CheckStatsOrderExists()
        {
            try
            {
                //Check variables
                bool saveNeeded = false;
                int highestIndex = 0;
                if (vStatsOrderDetails.Any())
                {
                    highestIndex = vStatsOrderDetails.OrderBy(x => x.Index).LastOrDefault().Index;
                }

                //Check stat order
                if (!vStatsOrderDetails.Any(x => x.Identifier == "FrametimeId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "FrametimeId", Name = "Frame time graph" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "TimeId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "TimeId", Name = "Time and Date" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "CustomTextId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "CustomTextId", Name = "Custom text line" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "AppId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "AppId", Name = "Application information" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "RendererId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "RendererId", Name = "Renderer information" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "FpsId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "FpsId", Name = "Frames Per Second" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "CpuId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "CpuId", Name = "Processor information" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "GpuId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "GpuId", Name = "Videocard information" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "MemId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "MemId", Name = "Memory information" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "FanId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "FanId", Name = "Fans information" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "MonId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "MonId", Name = "Monitor information" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "NetId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "NetId", Name = "Network information" });
                    highestIndex++;
                    saveNeeded = true;
                }
                if (!vStatsOrderDetails.Any(x => x.Identifier == "BatId"))
                {
                    vStatsOrderDetails.Add(new StatsOrderDetails { Index = highestIndex, Identifier = "BatId", Name = "Battery information" });
                    highestIndex++;
                    saveNeeded = true;
                }

                //Save stats order to json file
                if (saveNeeded)
                {
                    JsonSaveObject(vStatsOrderDetails, @"Profiles\User\FpsStatsOrderDetails.json");
                    Debug.WriteLine("Added missing stats order.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to check existing stats order: " + ex.Message);
            }
        }

        void CheckStatsOrderReset()
        {
            try
            {
                //Clear current stats order
                vStatsOrderDetails.Clear();

                //Set default stats order
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 0, Identifier = "FrametimeId", Name = "Frame time graph" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 1, Identifier = "TimeId", Name = "Time and Date" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 2, Identifier = "CustomTextId", Name = "Custom text line" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 3, Identifier = "AppId", Name = "Application information" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 4, Identifier = "RendererId", Name = "Renderer information" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 5, Identifier = "FpsId", Name = "Frames Per Second" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 6, Identifier = "CpuId", Name = "Processor information" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 7, Identifier = "GpuId", Name = "Videocard information" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 8, Identifier = "MemId", Name = "Memory information" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 9, Identifier = "FanId", Name = "Fans information" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 10, Identifier = "MonId", Name = "Monitor information" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 11, Identifier = "NetId", Name = "Network information" });
                vStatsOrderDetails.Add(new StatsOrderDetails { Index = 12, Identifier = "BatId", Name = "Battery information" });

                //Save stats order to json file
                JsonSaveObject(vStatsOrderDetails, @"Profiles\User\FpsStatsOrderDetails.json");
                Debug.WriteLine("Stats order reset to default.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to reset stats order: " + ex.Message);
            }
        }

        void CheckStatsOrderDouble()
        {
            try
            {
                //Check if there are double indexes
                bool doubleIndexes = vStatsOrderDetails.GroupBy(x => x.Index).Any(x => x.Count() > 1);

                //Reset stat order when double found
                if (doubleIndexes)
                {
                    Debug.WriteLine("Found double stat order, resetting stats order.");
                    CheckStatsOrderReset();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to check double stats order: " + ex.Message);
            }
        }
    }
}