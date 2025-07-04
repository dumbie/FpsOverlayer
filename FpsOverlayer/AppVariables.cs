﻿using ArnoldVinkCode;
using LibreHardwareMonitor.Hardware;
using Microsoft.Diagnostics.Tracing.Session;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Media;
using System.Windows.Threading;
using static ArnoldVinkCode.AVClasses;
using static ArnoldVinkCode.AVJsonFunctions;
using static ArnoldVinkCode.AVProcess;
using static ArnoldVinkCode.AVSettings;
using static ArnoldVinkCode.AVTaskbarInformation;

namespace FpsOverlayer
{
    public class AppVariables
    {
        //Application Variables
        public static Configuration vConfigurationFpsOverlayer = SettingLoadConfig("FpsOverlayer.exe.csettings");
        public static bool vManualHiddenFpsOverlay = false;
        public const int vTotalStatsCount = 11;

        //Application Windows
        public static WindowStats vWindowStats = new WindowStats();
        public static WindowCrosshair vWindowCrosshair = new WindowCrosshair();
        public static WindowTools vWindowTools = new WindowTools();
        public static WindowSettings vWindowSettings = new WindowSettings();
        public static AppTray vAppTray = new AppTray();

        //Dispatcher Timers
        public static DispatcherTimer vDispatcherTimerOverlay = new DispatcherTimer();

        //Image Variables
        public static string vImageBackupSource = "Assets/Default/Icons/Unknown.png";

        //Interaction Variables
        public static bool vSingleTappedEvent = true;

        //Process Variables
        public static ProcessMulti vProcessCurrent = Get_ProcessMultiCurrent();
        public static ProcessMulti vTargetProcess = new ProcessMulti(0, 0);

        //Margin Variables
        public static int vKeypadAdjustMargin = 0;
        public static int vTaskBarAdjustMargin = 0;
        public static AppBarPosition vTaskBarPosition = AppBarPosition.ABE_BOTTOM;

        //Frames per second
        public static long vLastFrameTimeUpdate = 0;
        public static double vLastFrameTimeStamp = 0;
        public static List<double> vListFrameTimes = new List<double>();

        //Frametimes graph
        public static uint vFrametimeCurrent = 0;
        public static PointCollection vPointFrameTimes = new PointCollection();

        //Strings
        public static string vTitleGPU = "GPU";
        public static string vTitleCPU = "CPU";
        public static string vTitleMEM = "MEM";
        public static string vTitleFAN = "FAN";
        public static string vTitleNET = "NET";
        public static string vTitleMON = "MON";
        public static string vTitleFPS = "FPS";
        public static string vTitleBAT = "BAT";

        //Tools Variables
        public static bool vToolsBlockInteract = false;
        public static WebView2 vBrowserWebView = null;

        //Hardware
        public static Computer vHardwareComputer = null;
        public static string vHardwareMotherboardName = string.Empty;
        public static string vHardwareMemoryName = string.Empty;
        public static string vHardwareMemorySpeed = string.Empty;
        public static string vHardwareMemoryVoltage = string.Empty;
        public static string vHardwareCpuFanSpeed = string.Empty;
        public static string vHardwareGpuFanSpeed = string.Empty;

        //Trace Events
        public static TraceEventSession vTraceEventSession;

        //Trace Events - Event IDs
        public const int vEventID_DxgKrnlPresent = 184;

        //Trace Events - Provider Guids
        public static Guid vProvider_DxgKrnl = Guid.Parse("{802ec45a-1e99-4b83-9920-87c98277ba9d}");

        //Sockets Variables
        public static ArnoldVinkSockets vArnoldVinkSockets = null;

        //Application Lists
        public static List<ShortcutTriggerKeyboard> vShortcutTriggers = JsonLoadFile<List<ShortcutTriggerKeyboard>>(@"Profiles\User\FpsShortcutsKeyboard.json");
        public static ObservableCollection<string> vNotesFiles = new ObservableCollection<string>();
        public static ObservableCollection<ProfileShared> vFpsBrowserLinks = JsonLoadFile<ObservableCollection<ProfileShared>>(@"Profiles\User\FpsBrowserLinks.json");
        public static ObservableCollection<ProfileShared> vFpsPositionProcessName = JsonLoadFile<ObservableCollection<ProfileShared>>(@"Profiles\User\FpsPositionProcessName.json");
    }
}