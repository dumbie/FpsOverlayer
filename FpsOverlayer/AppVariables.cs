using ArnoldVinkCode;
using LibreHardwareMonitor.Hardware;
using Microsoft.Diagnostics.Tracing.Session;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using static ArnoldVinkCode.AVActions;
using static ArnoldVinkCode.AVClasses;
using static ArnoldVinkCode.AVJsonFunctions;
using static ArnoldVinkCode.AVTaskbarInformation;
using static LibraryShared.Classes;

namespace FpsOverlayer
{
    public class AppVariables
    {
        //Application Variables
        public static AVSettingsConfig vSettings = new AVSettingsConfig("FpsOverlayer.exe.csettings");
        public static bool vManualHiddenFpsOverlay = false;

        //Application Windows
        public static WindowStats vWindowStats = new WindowStats();
        public static WindowCrosshair vWindowCrosshair = new WindowCrosshair();
        public static WindowTools vWindowTools = new WindowTools();
        public static WindowSettings vWindowSettings = new WindowSettings();
        public static AppTray vAppTray = new AppTray();

        //Overlay Variables
        public static AVHighResTimer vTimerOverlay = new AVHighResTimer();

        //Image Variables
        public static string vImageBackupSource = "Assets/Default/Icons/Unknown.png";

        //Interaction Variables
        public static bool vSingleTappedEvent = true;

        //Process Variables
        public static AVProcess vProcessTarget = new AVProcess(0);
        public static int[] vProcessTargetChildren = new int[0];
        public static RenderApiDetails vProcessRenderApi = new RenderApiDetails();

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
        public static string vTitleREN = "REN";
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
        public static string vHardwarePumpFanSpeed = string.Empty;
        public static string vHardwareSystemFanSpeed = string.Empty;

        //Trace Events
        public static TraceEventSession vTraceEventSession = null;

        //Trace Event Identifiers
        public enum vTraceEventIdentifiers : int
        {
            D3D9PresentStart = 1,
            D3D9PresentStop = 2,
            DXGIPresent_Start = 42,
            DXGIPresent_Stop = 43,
            DXGIPresentMPO_Start = 55,
            DXGIPresentMPO_Stop = 56,
            DWM_GetPresentHistory = 64,
            DWM_Schedule_Present_Start = 15,
            DWM_FlipChain_Pending = 69,
            DWM_FlipChain_Complete = 70,
            DWM_FlipChain_Dirty = 101,
            DWM_Schedule_SurfaceUpdate = 196,
            DxgKrnl_Flip = 168,
            DxgKrnl_FlipMPO = 252,
            DxgKrnl_QueueSubmit = 178,
            DxgKrnl_QueueComplete = 180,
            DxgKrnl_MMIOFlip = 116,
            DxgKrnl_MMIOFlipMPO = 259,
            DxgKrnl_HSyncDPC = 382,
            DxgKrnl_VSyncDPC = 17,
            DxgKrnl_Present = 184,
            DxgKrnl_PresentHistoryDetailed = 215,
            DxgKrnl_SubmitPresentHistory = 171,
            DxgKrnl_PropagatePresentHistory = 172,
            DxgKrnl_Blit = 166,
            Win32K_TokenCompositionSurfaceObject = 201,
            Win32K_TokenStateChanged = 301
        }

        //Trace Events - Provider Guids
        public static Guid vProvider_D3D9 = Guid.Parse("{783ACA0A-790E-4D7F-8451-AA850511C6B9}");
        public static Guid vProvider_Dxgi = Guid.Parse("{CA11C036-0102-4A2D-A6AD-F03CFED5D3C9}");
        public static Guid vProvider_DxgKrnl = Guid.Parse("{802EC45A-1E99-4B83-9920-87C98277BA9D}");

        //Sockets Variables
        public static ArnoldVinkSockets vArnoldVinkSockets = null;

        //Application Lists
        public static ObservableCollection<string> vNotesFiles = new ObservableCollection<string>();
        public static List<ShortcutTriggerKeyboard> vShortcutTriggers = JsonLoadFile<List<ShortcutTriggerKeyboard>>(@"Profiles\User\FpsShortcutsKeyboard.json");
        public static ObservableCollection<StatsOrderDetails> vStatsOrderDetails = JsonLoadFile<ObservableCollection<StatsOrderDetails>>(@"Profiles\User\FpsStatsOrderDetails.json");
        public static ObservableCollection<ProfileShared> vFpsBrowserLinks = JsonLoadFile<ObservableCollection<ProfileShared>>(@"Profiles\User\FpsBrowserLinks.json");
        public static ObservableCollection<ProfileShared> vFpsPositionProcessName = JsonLoadFile<ObservableCollection<ProfileShared>>(@"Profiles\User\FpsPositionProcessName.json");
    }
}