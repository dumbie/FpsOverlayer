using ArnoldVinkCode;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowSettings
    {
        //Load - Application Settings
        async Task Settings_Load()
        {
            try
            {
                checkbox_DisplayBackground.IsChecked = vSettings.Load("DisplayBackground", typeof(bool));

                textblock_DisplayOpacity.Text = textblock_DisplayOpacity.Tag + ": " + vSettings.Load("DisplayOpacity", typeof(string)) + "%";
                slider_DisplayOpacity.Value = vSettings.Load("DisplayOpacity", typeof(double));

                textblock_HardwareUpdateRateMs.Text = textblock_HardwareUpdateRateMs.Tag + ": " + vSettings.Load("HardwareUpdateRateMs", typeof(string)) + "ms";
                slider_HardwareUpdateRateMs.Value = vSettings.Load("HardwareUpdateRateMs", typeof(double));

                textblock_MarginHorizontal.Text = textblock_MarginHorizontal.Tag + ": " + vSettings.Load("MarginHorizontal", typeof(string)) + "px";
                slider_MarginHorizontal.Value = vSettings.Load("MarginHorizontal", typeof(double));

                textblock_MarginVertical.Text = textblock_MarginVertical.Tag + ": " + vSettings.Load("MarginVertical", typeof(string)) + "px";
                slider_MarginVertical.Value = vSettings.Load("MarginVertical", typeof(double));

                checkbox_CheckTaskbarVisible.IsChecked = vSettings.Load("CheckTaskbarVisible", typeof(bool));
                checkbox_StatsFlipBottom.IsChecked = vSettings.Load("StatsFlipBottom", typeof(bool));
                checkbox_HideScreenCapture.IsChecked = vSettings.Load("HideScreenCapture", typeof(bool));

                //Select the current font name
                try
                {
                    combobox_InterfaceFontStyleName.SelectedItem = vSettings.Load("InterfaceFontStyleName", typeof(string));
                }
                catch { }

                combobox_TextPosition.SelectedIndex = vSettings.Load("TextPosition", typeof(int));
                combobox_TextDirection.SelectedIndex = vSettings.Load("TextDirection", typeof(int));

                textblock_TextSize.Text = textblock_TextSize.Tag + ": " + vSettings.Load("TextSize", typeof(string)) + "px";
                slider_TextSize.Value = vSettings.Load("TextSize", typeof(double));

                textbox_CustomText.Text = vSettings.Load("CustomTextString", typeof(string));

                checkbox_TextColorSingle.IsChecked = vSettings.Load("TextColorSingle", typeof(bool));

                textbox_GpuCategoryTitle.Text = vSettings.Load("GpuCategoryTitle", typeof(string));
                checkbox_GpuShowCategoryTitle.IsChecked = vSettings.Load("GpuShowCategoryTitle", typeof(bool));
                checkbox_GpuShowName.IsChecked = vSettings.Load("GpuShowName", typeof(bool));
                checkbox_GpuShowPercentage.IsChecked = vSettings.Load("GpuShowPercentage", typeof(bool));
                checkbox_GpuShowMemoryUsed.IsChecked = vSettings.Load("GpuShowMemoryUsed", typeof(bool));
                checkbox_GpuShowTemperature.IsChecked = vSettings.Load("GpuShowTemperature", typeof(bool));
                checkbox_GpuShowTemperatureHotspot.IsChecked = vSettings.Load("GpuShowTemperatureHotspot", typeof(bool));
                checkbox_GpuShowMemorySpeed.IsChecked = vSettings.Load("GpuShowMemorySpeed", typeof(bool));
                checkbox_GpuShowCoreFrequency.IsChecked = vSettings.Load("GpuShowCoreFrequency", typeof(bool));
                checkbox_GpuShowFanSpeed.IsChecked = vSettings.Load("GpuShowFanSpeed", typeof(bool));
                checkbox_GpuShowPowerWatt.IsChecked = vSettings.Load("GpuShowPowerWatt", typeof(bool));
                checkbox_GpuShowPowerVolt.IsChecked = vSettings.Load("GpuShowPowerVolt", typeof(bool));

                textbox_CpuCategoryTitle.Text = vSettings.Load("CpuCategoryTitle", typeof(string));
                checkbox_CpuShowCategoryTitle.IsChecked = vSettings.Load("CpuShowCategoryTitle", typeof(bool));
                checkbox_CpuShowName.IsChecked = vSettings.Load("CpuShowName", typeof(bool));
                checkbox_BoardShowName.IsChecked = vSettings.Load("BoardShowName", typeof(bool));
                checkbox_CpuShowPercentage.IsChecked = vSettings.Load("CpuShowPercentage", typeof(bool));
                checkbox_CpuShowTemperature.IsChecked = vSettings.Load("CpuShowTemperature", typeof(bool));
                checkbox_CpuShowCoreFrequency.IsChecked = vSettings.Load("CpuShowCoreFrequency", typeof(bool));
                checkbox_CpuShowPowerWatt.IsChecked = vSettings.Load("CpuShowPowerWatt", typeof(bool));
                checkbox_CpuShowPowerVolt.IsChecked = vSettings.Load("CpuShowPowerVolt", typeof(bool));
                checkbox_CpuShowFanSpeed.IsChecked = vSettings.Load("CpuShowFanSpeed", typeof(bool));

                textbox_MemCategoryTitle.Text = vSettings.Load("MemCategoryTitle", typeof(string));
                checkbox_MemShowCategoryTitle.IsChecked = vSettings.Load("MemShowCategoryTitle", typeof(bool));
                checkbox_MemShowName.IsChecked = vSettings.Load("MemShowName", typeof(bool));
                checkbox_MemShowSpeed.IsChecked = vSettings.Load("MemShowSpeed", typeof(bool));
                checkbox_MemShowTemperature.IsChecked = vSettings.Load("MemShowTemperature", typeof(bool));
                checkbox_MemShowPowerVolt.IsChecked = vSettings.Load("MemShowPowerVolt", typeof(bool));
                checkbox_MemShowPercentage.IsChecked = vSettings.Load("MemShowPercentage", typeof(bool));
                checkbox_MemShowUsed.IsChecked = vSettings.Load("MemShowUsed", typeof(bool));
                checkbox_MemShowFree.IsChecked = vSettings.Load("MemShowFree", typeof(bool));
                checkbox_MemShowTotal.IsChecked = vSettings.Load("MemShowTotal", typeof(bool));

                textbox_NetCategoryTitle.Text = vSettings.Load("NetCategoryTitle", typeof(string));
                checkbox_NetShowCategoryTitle.IsChecked = vSettings.Load("NetShowCategoryTitle", typeof(bool));
                checkbox_NetShowCurrentUsage.IsChecked = vSettings.Load("NetShowCurrentUsage", typeof(bool));

                checkbox_AppShowName.IsChecked = vSettings.Load("AppShowName", typeof(bool));

                textbox_BatCategoryTitle.Text = vSettings.Load("BatCategoryTitle", typeof(string));
                checkbox_BatShowCategoryTitle.IsChecked = vSettings.Load("BatShowCategoryTitle", typeof(bool));
                checkbox_BatShowPercentage.IsChecked = vSettings.Load("BatShowPercentage", typeof(bool));

                checkbox_TimeShowCurrentTime.IsChecked = vSettings.Load("TimeShowCurrentTime", typeof(bool));
                checkbox_TimeShowCurrentDate.IsChecked = vSettings.Load("TimeShowCurrentDate", typeof(bool));

                textbox_MonCategoryTitle.Text = vSettings.Load("MonCategoryTitle", typeof(string));
                checkbox_MonShowCategoryTitle.IsChecked = vSettings.Load("MonShowCategoryTitle", typeof(bool));
                checkbox_MonShowResolution.IsChecked = vSettings.Load("MonShowResolution", typeof(bool));
                checkbox_MonShowDpiResolution.IsChecked = vSettings.Load("MonShowDpiResolution", typeof(bool));
                checkbox_MonShowColorBitDepth.IsChecked = vSettings.Load("MonShowColorBitDepth", typeof(bool));
                checkbox_MonShowColorFormat.IsChecked = vSettings.Load("MonShowColorFormat", typeof(bool));
                checkbox_MonShowColorHdr.IsChecked = vSettings.Load("MonShowColorHdr", typeof(bool));
                checkbox_MonShowRefreshRate.IsChecked = vSettings.Load("MonShowRefreshRate", typeof(bool));

                //Frames
                textbox_FpsCategoryTitle.Text = vSettings.Load("FpsCategoryTitle", typeof(string));
                checkbox_FpsShowCategoryTitle.IsChecked = vSettings.Load("FpsShowCategoryTitle", typeof(bool));
                checkbox_FpsShowCurrentFps.IsChecked = vSettings.Load("FpsShowCurrentFps", typeof(bool));
                checkbox_FpsShowCurrentLatency.IsChecked = vSettings.Load("FpsShowCurrentLatency", typeof(bool));
                checkbox_FpsShowAverageFps.IsChecked = vSettings.Load("FpsShowAverageFps", typeof(bool));

                textblock_FpsAverageSeconds.Text = textblock_FpsAverageSeconds.Tag + ": " + vSettings.Load("FpsAverageSeconds", typeof(string)) + " seconds";
                slider_FpsAverageSeconds.Value = vSettings.Load("FpsAverageSeconds", typeof(double));

                //Fan
                textbox_FanCategoryTitle.Text = vSettings.Load("FanCategoryTitle", typeof(string));
                checkbox_FanShowCategoryTitle.IsChecked = vSettings.Load("FanShowCategoryTitle", typeof(bool));
                checkbox_FanShowCpu.IsChecked = vSettings.Load("FanShowCpu", typeof(bool));
                checkbox_FanShowGpu.IsChecked = vSettings.Load("FanShowGpu", typeof(bool));
                checkbox_FanShowSystem.IsChecked = vSettings.Load("FanShowSystem", typeof(bool));

                //Colors
                string ColorBackground = vSettings.Load("ColorBackground", typeof(string));
                colorpicker_ColorBackground.Background = new BrushConverter().ConvertFrom(ColorBackground) as SolidColorBrush;

                string ColorSingle = vSettings.Load("ColorSingle", typeof(string));
                colorpicker_ColorSingle.Background = new BrushConverter().ConvertFrom(ColorSingle) as SolidColorBrush;

                string ColorGpu = vSettings.Load("ColorGpu", typeof(string));
                colorpicker_ColorGpu.Background = new BrushConverter().ConvertFrom(ColorGpu) as SolidColorBrush;

                string ColorCpu = vSettings.Load("ColorCpu", typeof(string));
                colorpicker_ColorCpu.Background = new BrushConverter().ConvertFrom(ColorCpu) as SolidColorBrush;

                string ColorMem = vSettings.Load("ColorMem", typeof(string));
                colorpicker_ColorMem.Background = new BrushConverter().ConvertFrom(ColorMem) as SolidColorBrush;

                string ColorFan = vSettings.Load("ColorFan", typeof(string));
                colorpicker_ColorFan.Background = new BrushConverter().ConvertFrom(ColorFan) as SolidColorBrush;

                string ColorNet = vSettings.Load("ColorNet", typeof(string));
                colorpicker_ColorNet.Background = new BrushConverter().ConvertFrom(ColorNet) as SolidColorBrush;

                string ColorApp = vSettings.Load("ColorApp", typeof(string));
                colorpicker_ColorApp.Background = new BrushConverter().ConvertFrom(ColorApp) as SolidColorBrush;

                string ColorBat = vSettings.Load("ColorBat", typeof(string));
                colorpicker_ColorBat.Background = new BrushConverter().ConvertFrom(ColorBat) as SolidColorBrush;

                string ColorTime = vSettings.Load("ColorTime", typeof(string));
                colorpicker_ColorTime.Background = new BrushConverter().ConvertFrom(ColorTime) as SolidColorBrush;

                string ColorCustomText = vSettings.Load("ColorCustomText", typeof(string));
                colorpicker_ColorCustomText.Background = new BrushConverter().ConvertFrom(ColorCustomText) as SolidColorBrush;

                string ColorMon = vSettings.Load("ColorMon", typeof(string));
                colorpicker_ColorMon.Background = new BrushConverter().ConvertFrom(ColorMon) as SolidColorBrush;

                string ColorFps = vSettings.Load("ColorFps", typeof(string));
                colorpicker_ColorFps.Background = new BrushConverter().ConvertFrom(ColorFps) as SolidColorBrush;

                string ColorFrametime = vSettings.Load("ColorFrametime", typeof(string));
                colorpicker_ColorFrametime.Background = new BrushConverter().ConvertFrom(ColorFrametime) as SolidColorBrush;

                //Frametime
                checkbox_FrametimeGraphShow.IsChecked = vSettings.Load("FrametimeGraphShow", typeof(bool));

                textblock_FrametimeAccuracy.Text = textblock_FrametimeAccuracy.Tag + ": " + vSettings.Load("FrametimeAccuracy", typeof(string)) + "px";
                slider_FrametimeAccuracy.Value = vSettings.Load("FrametimeAccuracy", typeof(double));

                textblock_FrametimeWidth.Text = textblock_FrametimeWidth.Tag + ": " + vSettings.Load("FrametimeWidth", typeof(string)) + "px";
                slider_FrametimeWidth.Value = vSettings.Load("FrametimeWidth", typeof(double));

                textblock_FrametimeHeight.Text = textblock_FrametimeHeight.Tag + ": " + vSettings.Load("FrametimeHeight", typeof(string)) + "px";
                slider_FrametimeHeight.Value = vSettings.Load("FrametimeHeight", typeof(double));

                //Crosshair
                checkbox_CrosshairLaunch.IsChecked = vSettings.Load("CrosshairLaunch", typeof(bool));

                string CrosshairColor = vSettings.Load("CrosshairColor", typeof(string));
                colorpicker_CrosshairColor.Background = new BrushConverter().ConvertFrom(CrosshairColor) as SolidColorBrush;

                textblock_CrosshairOpacity.Text = textblock_CrosshairOpacity.Tag + ": " + vSettings.Load("CrosshairOpacity", typeof(string)) + "%";
                slider_CrosshairOpacity.Value = vSettings.Load("CrosshairOpacity", typeof(double));

                textblock_CrosshairVerticalPosition.Text = textblock_CrosshairVerticalPosition.Tag + ": " + vSettings.Load("CrosshairVerticalPosition", typeof(string)) + "px";
                slider_CrosshairVerticalPosition.Value = vSettings.Load("CrosshairVerticalPosition", typeof(double));

                textblock_CrosshairHorizontalPosition.Text = textblock_CrosshairHorizontalPosition.Tag + ": " + vSettings.Load("CrosshairHorizontalPosition", typeof(string)) + "px";
                slider_CrosshairHorizontalPosition.Value = vSettings.Load("CrosshairHorizontalPosition", typeof(double));

                textblock_CrosshairSize.Text = textblock_CrosshairSize.Tag + ": " + vSettings.Load("CrosshairSize", typeof(string)) + "px";
                slider_CrosshairSize.Value = vSettings.Load("CrosshairSize", typeof(double));

                textblock_CrosshairThickness.Text = textblock_CrosshairThickness.Tag + ": " + vSettings.Load("CrosshairThickness", typeof(string)) + "px";
                slider_CrosshairThickness.Value = vSettings.Load("CrosshairThickness", typeof(double));

                combobox_CrosshairStyle.SelectedIndex = vSettings.Load("CrosshairStyle", typeof(int));

                //Tools
                checkbox_ToolsLaunch.IsChecked = vSettings.Load("ToolsLaunch", typeof(bool));

                //Browser
                checkbox_BrowserUnload.IsChecked = vSettings.Load("BrowserUnload", typeof(bool));

                textblock_BrowserOpacity.Text = textblock_BrowserOpacity.Tag + ": " + vSettings.Load("BrowserOpacity", typeof(string)) + "%";
                slider_BrowserOpacity.Value = vSettings.Load("BrowserOpacity", typeof(double));

                //Update stats position text
                UpdateStatsPositionText();

                //Check if application is set to launch on Windows startup
                cb_SettingsWindowsStartup.IsChecked = AVSettings.StartupShortcutCheck();

                //Display settings
                int monitorNumber = vSettings.Load("DisplayMonitor", typeof(int));
                textblock_SettingsDisplayMonitor.Text = textblock_SettingsDisplayMonitor.Tag + ": " + monitorNumber;
                slider_SettingsDisplayMonitor.Value = monitorNumber;

                //Wait for settings to have loaded
                await Task.Delay(1500);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to load application settings: " + ex.Message);
            }
        }
    }
}