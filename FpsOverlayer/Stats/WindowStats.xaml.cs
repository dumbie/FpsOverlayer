﻿using ArnoldVinkCode;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using static ArnoldVinkCode.AVInteropDll;
using static ArnoldVinkCode.AVSettings;
using static ArnoldVinkCode.AVWindowFunctions;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowStats : Window
    {
        //Window Initialize
        public WindowStats() { InitializeComponent(); }

        //Window Variables
        public IntPtr vInteropWindowHandle = IntPtr.Zero;
        public bool vWindowVisible = false;

        //Window Initialized
        protected override void OnSourceInitialized(EventArgs e)
        {
            try
            {
                //Get interop window handle
                vInteropWindowHandle = new WindowInteropHelper(this).EnsureHandle();

                //Set render mode to software
                HwndSource hwndSource = HwndSource.FromHwnd(vInteropWindowHandle);
                HwndTarget hwndTarget = hwndSource.CompositionTarget;
                hwndTarget.RenderMode = RenderMode.SoftwareOnly;

                //Update window style
                WindowUpdateStyle(vInteropWindowHandle, true, true, true, true);

                //Update window display affinity
                UpdateWindowAffinity();

                //Update window position
                UpdateWindowPosition();

                //Update the fps overlay style
                UpdateFpsOverlayStyle();

                //Bind all the lists to ListBox
                ListBoxBindLists();

                //Start process monitoring
                StartMonitorProcess();

                //Start taskbar monitoring
                StartMonitorTaskbar();

                //Start fps monitoring
                StartMonitorFps();

                //Start hardware monitoring
                StartMonitorHardware();

                //Check if resolution has changed
                SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
            }
            catch { }
        }

        //Bind the lists to the listbox elements
        void ListBoxBindLists()
        {
            try
            {
                polyline_Chart.Points = vPointFrameTimes;
            }
            catch { }
        }

        //Hide the window
        public new void Hide()
        {
            try
            {
                //Update the window visibility
                UpdateWindowVisibility(false);
            }
            catch { }
        }

        //Show the window
        public new void Show()
        {
            try
            {
                //Update the window visibility
                UpdateWindowVisibility(true);
            }
            catch { }
        }

        //Update the window visibility
        public void UpdateWindowVisibility(bool visible)
        {
            try
            {
                if (visible)
                {
                    if (!vWindowVisible)
                    {
                        //Create and show the window
                        base.Show();

                        //Update window visibility
                        WindowUpdateVisibility(vInteropWindowHandle, true);

                        //Update window style
                        WindowUpdateStyle(vInteropWindowHandle, true, true, true, true);

                        this.Title = "Stats Overlayer (Visible)";
                        vWindowVisible = true;
                        Debug.WriteLine("Showing the window.");
                    }
                }
                else
                {
                    if (vWindowVisible)
                    {
                        //Update window visibility
                        WindowUpdateVisibility(vInteropWindowHandle, false);

                        this.Title = "Stats Overlayer (Hidden)";
                        vWindowVisible = false;
                        Debug.WriteLine("Hiding the window.");
                    }
                }
            }
            catch { }
        }

        //Update window position
        public void UpdateWindowPosition()
        {
            try
            {
                //Get the current active screen
                int monitorNumber = SettingLoad(vConfigurationFpsOverlayer, "DisplayMonitor", typeof(int));

                //Move the window position
                WindowUpdatePosition(monitorNumber, vInteropWindowHandle, AVWindowPosition.FullScreen);
            }
            catch { }
        }

        //Update window display affinity
        public void UpdateWindowAffinity()
        {
            try
            {
                if (SettingLoad(vConfigurationFpsOverlayer, "HideScreenCapture", typeof(bool)))
                {
                    SetWindowDisplayAffinity(vInteropWindowHandle, DisplayAffinityFlags.WDA_EXCLUDEFROMCAPTURE);
                }
                else
                {
                    SetWindowDisplayAffinity(vInteropWindowHandle, DisplayAffinityFlags.WDA_NONE);
                }
            }
            catch { }
        }

        //Update windows on resolution change
        public async void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            try
            {
                //Wait for resolution change
                await Task.Delay(2000);

                //Update windows on change
                UpdateWindowOnChange();
            }
            catch { }
        }

        //Adjust window font family
        void UpdateWindowFontFamily()
        {
            try
            {
                string interfaceFontStyleName = SettingLoad(vConfigurationFpsOverlayer, "InterfaceFontStyleName", typeof(string));
                if (interfaceFontStyleName == "Segoe UI" || interfaceFontStyleName == "Verdana" || interfaceFontStyleName == "Consolas" || interfaceFontStyleName == "Arial")
                {
                    this.FontFamily = new FontFamily(interfaceFontStyleName);
                }
                else
                {
                    string fontPathUser = AVFunctions.ApplicationPathRoot() + "/Assets/User/Fonts/" + interfaceFontStyleName + ".ttf";
                    string fontPathDefault = AVFunctions.ApplicationPathRoot() + "/Assets/Default/Fonts/" + interfaceFontStyleName + ".ttf";
                    if (File.Exists(fontPathUser))
                    {
                        ICollection<FontFamily> fontFamilies = Fonts.GetFontFamilies(fontPathUser);
                        this.FontFamily = fontFamilies.FirstOrDefault();
                    }
                    else if (File.Exists(fontPathDefault))
                    {
                        ICollection<FontFamily> fontFamilies = Fonts.GetFontFamilies(fontPathDefault);
                        this.FontFamily = fontFamilies.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed setting window font: " + ex.Message);
            }
        }
    }
}