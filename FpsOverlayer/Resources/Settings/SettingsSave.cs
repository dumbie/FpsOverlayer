using ArnoldVinkCode;
using ArnoldVinkStyles;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowSettings
    {
        //Save - Monitor Application Settings
        void Settings_Save()
        {
            try
            {
                cb_SettingsWindowsStartup.Click += (sender, e) =>
                {
                    AVSettings.StartupShortcutManage("Launcher.exe", false);
                };

                checkbox_DisplayBackground.Click += (sender, e) =>
                {
                    vSettings.Set("DisplayBackground", checkbox_DisplayBackground.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                slider_DisplayOpacity.ValueChanged += (sender, e) =>
                {
                    textblock_DisplayOpacity.Text = textblock_DisplayOpacity.Tag + ": " + slider_DisplayOpacity.Value.ToString("0.00") + "%";
                    vSettings.Set("DisplayOpacity", slider_DisplayOpacity.Value);
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                slider_HardwareUpdateRateMs.ValueChanged += (sender, e) =>
                {
                    textblock_HardwareUpdateRateMs.Text = textblock_HardwareUpdateRateMs.Tag + ": " + slider_HardwareUpdateRateMs.Value.ToString() + "ms";
                    vSettings.Set("HardwareUpdateRateMs", slider_HardwareUpdateRateMs.Value);
                };

                slider_MarginHorizontal.ValueChanged += (sender, e) =>
                {
                    textblock_MarginHorizontal.Text = textblock_MarginHorizontal.Tag + ": " + slider_MarginHorizontal.Value.ToString("0") + "px";
                    vSettings.Set("MarginHorizontal", slider_MarginHorizontal.Value);
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                slider_MarginVertical.ValueChanged += (sender, e) =>
                {
                    textblock_MarginVertical.Text = textblock_MarginVertical.Tag + ": " + slider_MarginVertical.Value.ToString("0") + "px";
                    vSettings.Set("MarginVertical", slider_MarginVertical.Value);
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                checkbox_CheckTaskbarVisible.Click += (sender, e) =>
                {
                    vSettings.Set("CheckTaskbarVisible", checkbox_CheckTaskbarVisible.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                checkbox_StatsFlipBottom.Click += (sender, e) =>
                {
                    vSettings.Set("StatsFlipBottom", checkbox_StatsFlipBottom.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                checkbox_HideScreenCapture.Click += (sender, e) =>
                {
                    vSettings.Set("HideScreenCapture", checkbox_HideScreenCapture.IsChecked.ToString());
                    vWindowStats.UpdateWindowAffinity();
                    vWindowCrosshair.UpdateWindowAffinity();
                    vWindowTools.UpdateWindowAffinity();
                };

                combobox_InterfaceFontStyleName.SelectionChanged += (sender, e) =>
                {
                    vSettings.Set("InterfaceFontStyleName", combobox_InterfaceFontStyleName.SelectedItem.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                combobox_TextPosition.SelectionChanged += (sender, e) =>
                {
                    vSettings.Set("TextPosition", combobox_TextPosition.SelectedIndex.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                combobox_TextDirection.SelectionChanged += (sender, e) =>
                {
                    vSettings.Set("TextDirection", combobox_TextDirection.SelectedIndex.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                slider_TextSize.ValueChanged += (sender, e) =>
                {
                    textblock_TextSize.Text = textblock_TextSize.Tag + ": " + slider_TextSize.Value.ToString("0") + "px";
                    vSettings.Set("TextSize", slider_TextSize.Value);
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                textbox_CustomText.TextChanged += (sender, e) =>
                {
                    TextBox senderTextbox = (TextBox)sender;
                    vSettings.Set("CustomTextString", senderTextbox.Text);
                };

                checkbox_TextColorSingle.Click += (sender, e) =>
                {
                    vSettings.Set("TextColorSingle", checkbox_TextColorSingle.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                combobox_GpuIndex.SelectionChanged += (sender, e) =>
                {
                    ComboBox senderComboBox = (ComboBox)sender;
                    vSettings.Set("GpuIndex", senderComboBox.SelectedIndex.ToString());
                };
                textbox_GpuCategoryTitle.TextChanged += (sender, e) =>
                {
                    TextBox senderTextbox = (TextBox)sender;
                    vSettings.Set("GpuCategoryTitle", senderTextbox.Text);
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_GpuShowCategoryTitle.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("GpuShowCategoryTitle", senderCheckBox.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_GpuShowName.Click += (sender, e) => { vSettings.Set("GpuShowName", checkbox_GpuShowName.IsChecked.ToString()); };
                checkbox_GpuShowPercentage.Click += (sender, e) => { vSettings.Set("GpuShowPercentage", checkbox_GpuShowPercentage.IsChecked.ToString()); };
                checkbox_GpuShowMemoryUsed.Click += (sender, e) => { vSettings.Set("GpuShowMemoryUsed", checkbox_GpuShowMemoryUsed.IsChecked.ToString()); };
                checkbox_GpuShowTemperature.Click += (sender, e) => { vSettings.Set("GpuShowTemperature", checkbox_GpuShowTemperature.IsChecked.ToString()); };
                checkbox_GpuShowTemperatureHotspot.Click += (sender, e) => { vSettings.Set("GpuShowTemperatureHotspot", checkbox_GpuShowTemperatureHotspot.IsChecked.ToString()); };
                checkbox_GpuShowMemorySpeed.Click += (sender, e) => { vSettings.Set("GpuShowMemorySpeed", checkbox_GpuShowMemorySpeed.IsChecked.ToString()); };
                checkbox_GpuShowCoreFrequency.Click += (sender, e) => { vSettings.Set("GpuShowCoreFrequency", checkbox_GpuShowCoreFrequency.IsChecked.ToString()); };
                checkbox_GpuShowFanSpeed.Click += (sender, e) => { vSettings.Set("GpuShowFanSpeed", checkbox_GpuShowFanSpeed.IsChecked.ToString()); };
                checkbox_GpuShowPowerWatt.Click += (sender, e) => { vSettings.Set("GpuShowPowerWatt", checkbox_GpuShowPowerWatt.IsChecked.ToString()); };
                checkbox_GpuShowPowerVolt.Click += (sender, e) => { vSettings.Set("GpuShowPowerVolt", checkbox_GpuShowPowerVolt.IsChecked.ToString()); };

                textbox_CpuCategoryTitle.TextChanged += (sender, e) =>
                {
                    TextBox senderTextbox = (TextBox)sender;
                    vSettings.Set("CpuCategoryTitle", senderTextbox.Text);
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_CpuShowCategoryTitle.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("CpuShowCategoryTitle", senderCheckBox.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_CpuShowName.Click += (sender, e) => { vSettings.Set("CpuShowName", checkbox_CpuShowName.IsChecked.ToString()); };
                checkbox_BoardShowName.Click += (sender, e) => { vSettings.Set("BoardShowName", checkbox_BoardShowName.IsChecked.ToString()); };
                checkbox_CpuShowPercentage.Click += (sender, e) => { vSettings.Set("CpuShowPercentage", checkbox_CpuShowPercentage.IsChecked.ToString()); };
                checkbox_CpuShowTemperature.Click += (sender, e) => { vSettings.Set("CpuShowTemperature", checkbox_CpuShowTemperature.IsChecked.ToString()); };
                checkbox_CpuShowCoreFrequency.Click += (sender, e) => { vSettings.Set("CpuShowCoreFrequency", checkbox_CpuShowCoreFrequency.IsChecked.ToString()); };
                checkbox_CpuShowPowerWatt.Click += (sender, e) => { vSettings.Set("CpuShowPowerWatt", checkbox_CpuShowPowerWatt.IsChecked.ToString()); };
                checkbox_CpuShowPowerVolt.Click += (sender, e) => { vSettings.Set("CpuShowPowerVolt", checkbox_CpuShowPowerVolt.IsChecked.ToString()); };
                checkbox_CpuShowFanSpeed.Click += (sender, e) => { vSettings.Set("CpuShowFanSpeed", checkbox_CpuShowFanSpeed.IsChecked.ToString()); };

                textbox_MemCategoryTitle.TextChanged += (sender, e) =>
                {
                    TextBox senderTextbox = (TextBox)sender;
                    vSettings.Set("MemCategoryTitle", senderTextbox.Text);
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_MemShowCategoryTitle.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("MemShowCategoryTitle", senderCheckBox.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_MemShowName.Click += (sender, e) => { vSettings.Set("MemShowName", checkbox_MemShowName.IsChecked.ToString()); };
                checkbox_MemShowSpeed.Click += (sender, e) => { vSettings.Set("MemShowSpeed", checkbox_MemShowSpeed.IsChecked.ToString()); };
                checkbox_MemShowTemperature.Click += (sender, e) => { vSettings.Set("MemShowTemperature", checkbox_MemShowTemperature.IsChecked.ToString()); };
                checkbox_MemShowPowerVolt.Click += (sender, e) => { vSettings.Set("MemShowPowerVolt", checkbox_MemShowPowerVolt.IsChecked.ToString()); };
                checkbox_MemShowPercentage.Click += (sender, e) => { vSettings.Set("MemShowPercentage", checkbox_MemShowPercentage.IsChecked.ToString()); };
                checkbox_MemShowUsed.Click += (sender, e) => { vSettings.Set("MemShowUsed", checkbox_MemShowUsed.IsChecked.ToString()); };
                checkbox_MemShowFree.Click += (sender, e) => { vSettings.Set("MemShowFree", checkbox_MemShowFree.IsChecked.ToString()); };
                checkbox_MemShowTotal.Click += (sender, e) => { vSettings.Set("MemShowTotal", checkbox_MemShowTotal.IsChecked.ToString()); };

                textbox_NetCategoryTitle.TextChanged += (sender, e) =>
                {
                    TextBox senderTextbox = (TextBox)sender;
                    vSettings.Set("NetCategoryTitle", senderTextbox.Text);
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_NetShowCategoryTitle.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("NetShowCategoryTitle", senderCheckBox.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_NetShowCurrentUsage.Click += (sender, e) => { vSettings.Set("NetShowCurrentUsage", checkbox_NetShowCurrentUsage.IsChecked.ToString()); };

                checkbox_AppShowName.Click += (sender, e) => { vSettings.Set("AppShowName", checkbox_AppShowName.IsChecked.ToString()); };

                textbox_BatCategoryTitle.TextChanged += (sender, e) =>
                {
                    TextBox senderTextbox = (TextBox)sender;
                    vSettings.Set("BatCategoryTitle", senderTextbox.Text);
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_BatShowCategoryTitle.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("BatShowCategoryTitle", senderCheckBox.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_BatShowPercentage.Click += (sender, e) => { vSettings.Set("BatShowPercentage", checkbox_BatShowPercentage.IsChecked.ToString()); };

                checkbox_TimeShowCurrentTime.Click += (sender, e) => { vSettings.Set("TimeShowCurrentTime", checkbox_TimeShowCurrentTime.IsChecked.ToString()); };
                checkbox_TimeShowCurrentDate.Click += (sender, e) => { vSettings.Set("TimeShowCurrentDate", checkbox_TimeShowCurrentDate.IsChecked.ToString()); };

                textbox_MonCategoryTitle.TextChanged += (sender, e) =>
                {
                    TextBox senderTextbox = (TextBox)sender;
                    vSettings.Set("MonCategoryTitle", senderTextbox.Text);
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_MonShowCategoryTitle.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("MonShowCategoryTitle", senderCheckBox.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_MonShowResolution.Click += (sender, e) => { vSettings.Set("MonShowResolution", checkbox_MonShowResolution.IsChecked.ToString()); };
                checkbox_MonShowDpiResolution.Click += (sender, e) => { vSettings.Set("MonShowDpiResolution", checkbox_MonShowDpiResolution.IsChecked.ToString()); };
                checkbox_MonShowColorBitDepth.Click += (sender, e) => { vSettings.Set("MonShowColorBitDepth", checkbox_MonShowColorBitDepth.IsChecked.ToString()); };
                checkbox_MonShowColorFormat.Click += (sender, e) => { vSettings.Set("MonShowColorFormat", checkbox_MonShowColorFormat.IsChecked.ToString()); };
                checkbox_MonShowColorHdr.Click += (sender, e) => { vSettings.Set("MonShowColorHdr", checkbox_MonShowColorHdr.IsChecked.ToString()); };
                checkbox_MonShowRefreshRate.Click += (sender, e) => { vSettings.Set("MonShowRefreshRate", checkbox_MonShowRefreshRate.IsChecked.ToString()); };

                //Frames
                textbox_FpsCategoryTitle.TextChanged += (sender, e) =>
                {
                    TextBox senderTextbox = (TextBox)sender;
                    vSettings.Set("FpsCategoryTitle", senderTextbox.Text);
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_FpsShowCategoryTitle.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("FpsShowCategoryTitle", senderCheckBox.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_FpsShowCurrentFps.Click += (sender, e) => { vSettings.Set("FpsShowCurrentFps", checkbox_FpsShowCurrentFps.IsChecked.ToString()); };
                checkbox_FpsShowCurrentLatency.Click += (sender, e) => { vSettings.Set("FpsShowCurrentLatency", checkbox_FpsShowCurrentLatency.IsChecked.ToString()); };
                checkbox_FpsShowAverageFps.Click += (sender, e) => { vSettings.Set("FpsShowAverageFps", checkbox_FpsShowAverageFps.IsChecked.ToString()); };

                slider_FpsAverageSeconds.ValueChanged += (sender, e) =>
                {
                    textblock_FpsAverageSeconds.Text = textblock_FpsAverageSeconds.Tag + ": " + slider_FpsAverageSeconds.Value.ToString("0") + " seconds";
                    vSettings.Set("FpsAverageSeconds", slider_FpsAverageSeconds.Value);
                };

                //Fan
                textbox_FanCategoryTitle.TextChanged += (sender, e) =>
                {
                    TextBox senderTextbox = (TextBox)sender;
                    vSettings.Set("FanCategoryTitle", senderTextbox.Text);
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_FanShowCategoryTitle.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("FanShowCategoryTitle", senderCheckBox.IsChecked.ToString());
                    vWindowStats.UpdateFpsOverlayStyle();
                };
                checkbox_FanShowCpu.Click += (sender, e) => { vSettings.Set("FanShowCpu", checkbox_FanShowCpu.IsChecked.ToString()); };
                checkbox_FanShowGpu.Click += (sender, e) => { vSettings.Set("FanShowGpu", checkbox_FanShowGpu.IsChecked.ToString()); };
                checkbox_FanShowPump.Click += (sender, e) => { vSettings.Set("FanShowPump", checkbox_FanShowPump.IsChecked.ToString()); };
                checkbox_FanShowSystem.Click += (sender, e) => { vSettings.Set("FanShowSystem", checkbox_FanShowSystem.IsChecked.ToString()); };

                //Colors
                colorpicker_ColorSingle.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorSingle.Background = newBrush;
                        vSettings.Set("ColorSingle", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorBackground.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorBackground.Background = newBrush;
                        vSettings.Set("ColorBackground", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorGpu.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorGpu.Background = newBrush;
                        vSettings.Set("ColorGpu", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorCpu.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorCpu.Background = newBrush;
                        vSettings.Set("ColorCpu", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorMem.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorMem.Background = newBrush;
                        vSettings.Set("ColorMem", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorFan.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorFan.Background = newBrush;
                        vSettings.Set("ColorFan", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorNet.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorNet.Background = newBrush;
                        vSettings.Set("ColorNet", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorApp.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorApp.Background = newBrush;
                        vSettings.Set("ColorApp", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorBat.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorBat.Background = newBrush;
                        vSettings.Set("ColorBat", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorTime.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorTime.Background = newBrush;
                        vSettings.Set("ColorTime", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorCustomText.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorCustomText.Background = newBrush;
                        vSettings.Set("ColorCustomText", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorMon.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorMon.Background = newBrush;
                        vSettings.Set("ColorMon", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorFps.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorFps.Background = newBrush;
                        vSettings.Set("ColorFps", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                colorpicker_ColorFrametime.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_ColorFrametime.Background = newBrush;
                        vSettings.Set("ColorFrametime", newColor.ToString());
                        vWindowStats.UpdateFpsOverlayStyle();
                    }
                };

                //Frametime
                checkbox_FrametimeGraphShow.Click += (sender, e) =>
                {
                    vSettings.Set("FrametimeGraphShow", checkbox_FrametimeGraphShow.IsChecked.ToString());
                };

                slider_FrametimeAccuracy.ValueChanged += (sender, e) =>
                {
                    textblock_FrametimeAccuracy.Text = textblock_FrametimeAccuracy.Tag + ": " + slider_FrametimeAccuracy.Value.ToString("0") + "px";
                    vSettings.Set("FrametimeAccuracy", slider_FrametimeAccuracy.Value);
                };

                slider_FrametimeWidth.ValueChanged += (sender, e) =>
                {
                    textblock_FrametimeWidth.Text = textblock_FrametimeWidth.Tag + ": " + slider_FrametimeWidth.Value.ToString("0") + "px";
                    vSettings.Set("FrametimeWidth", slider_FrametimeWidth.Value);
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                slider_FrametimeHeight.ValueChanged += (sender, e) =>
                {
                    textblock_FrametimeHeight.Text = textblock_FrametimeHeight.Tag + ": " + slider_FrametimeHeight.Value.ToString("0") + "px";
                    vSettings.Set("FrametimeHeight", slider_FrametimeHeight.Value);
                    vWindowStats.UpdateFpsOverlayStyle();
                };

                //Crosshair
                checkbox_CrosshairLaunch.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("CrosshairLaunch", senderCheckBox.IsChecked.ToString());
                };

                colorpicker_CrosshairColor.Click += async (sender, e) =>
                {
                    Color? newColor = await new ColorPickerPreset().Popup(null);
                    if (newColor != null)
                    {
                        SolidColorBrush newBrush = new SolidColorBrush((Color)newColor);
                        colorpicker_CrosshairColor.Background = newBrush;
                        vSettings.Set("CrosshairColor", newColor.ToString());
                        vWindowCrosshair.UpdateCrosshairOverlayStyle();
                    }
                };

                slider_CrosshairOpacity.ValueChanged += (sender, e) =>
                {
                    textblock_CrosshairOpacity.Text = textblock_CrosshairOpacity.Tag + ": " + slider_CrosshairOpacity.Value.ToString("0.00") + "%";
                    vSettings.Set("CrosshairOpacity", slider_CrosshairOpacity.Value);
                    vWindowCrosshair.UpdateCrosshairOverlayStyle();
                };

                slider_CrosshairVerticalPosition.ValueChanged += (sender, e) =>
                {
                    textblock_CrosshairVerticalPosition.Text = textblock_CrosshairVerticalPosition.Tag + ": " + slider_CrosshairVerticalPosition.Value.ToString("0") + "px";
                    vSettings.Set("CrosshairVerticalPosition", slider_CrosshairVerticalPosition.Value);
                    vWindowCrosshair.UpdateCrosshairOverlayStyle();
                };

                slider_CrosshairHorizontalPosition.ValueChanged += (sender, e) =>
                {
                    textblock_CrosshairHorizontalPosition.Text = textblock_CrosshairHorizontalPosition.Tag + ": " + slider_CrosshairHorizontalPosition.Value.ToString("0") + "px";
                    vSettings.Set("CrosshairHorizontalPosition", slider_CrosshairHorizontalPosition.Value);
                    vWindowCrosshair.UpdateCrosshairOverlayStyle();
                };

                slider_CrosshairSize.ValueChanged += (sender, e) =>
                {
                    textblock_CrosshairSize.Text = textblock_CrosshairSize.Tag + ": " + slider_CrosshairSize.Value.ToString("0") + "px";
                    vSettings.Set("CrosshairSize", slider_CrosshairSize.Value);
                    vWindowCrosshair.UpdateCrosshairOverlayStyle();
                };

                slider_CrosshairThickness.ValueChanged += (sender, e) =>
                {
                    textblock_CrosshairThickness.Text = textblock_CrosshairThickness.Tag + ": " + slider_CrosshairThickness.Value.ToString("0") + "px";
                    vSettings.Set("CrosshairThickness", slider_CrosshairThickness.Value);
                    vWindowCrosshair.UpdateCrosshairOverlayStyle();
                };

                combobox_CrosshairStyle.SelectionChanged += (sender, e) =>
                {
                    vSettings.Set("CrosshairStyle", combobox_CrosshairStyle.SelectedIndex.ToString());
                    vWindowCrosshair.UpdateCrosshairOverlayStyle();
                };

                //Tools
                checkbox_ToolsLaunch.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("ToolsLaunch", senderCheckBox.IsChecked.ToString());
                };

                //Browser
                checkbox_BrowserUnload.Click += (sender, e) =>
                {
                    CheckBox senderCheckBox = (CheckBox)sender;
                    vSettings.Set("BrowserUnload", senderCheckBox.IsChecked.ToString());
                };

                slider_BrowserOpacity.ValueChanged += async (sender, e) =>
                {
                    textblock_BrowserOpacity.Text = textblock_BrowserOpacity.Tag + ": " + slider_BrowserOpacity.Value.ToString("0.00") + "%";
                    vSettings.Set("BrowserOpacity", slider_BrowserOpacity.Value);
                    await vWindowTools.Browser_Update_Opacity();
                };

                //Display settings
                slider_SettingsDisplayMonitor.ValueChanged += (sender, e) =>
                {
                    textblock_SettingsDisplayMonitor.Text = textblock_SettingsDisplayMonitor.Tag + ": " + Convert.ToInt32(slider_SettingsDisplayMonitor.Value);
                    vSettings.Set("DisplayMonitor", slider_SettingsDisplayMonitor.Value);
                    vWindowStats.UpdateWindowPosition();
                    vWindowCrosshair.UpdateWindowPosition();
                    vWindowTools.UpdateWindowPosition();
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to save the application settings: " + ex.Message);
            }
        }
    }
}