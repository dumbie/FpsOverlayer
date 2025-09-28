using ArnoldVinkCode;
using ArnoldVinkStyles;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static ArnoldVinkCode.AVClasses;
using static ArnoldVinkCode.AVTaskbarInformation;
using static FpsOverlayer.AppVariables;
using static LibraryShared.Enums;

namespace FpsOverlayer
{
    public partial class WindowStats
    {
        //Hide the stats visibility
        public void HideFpsOverlayVisibility()
        {
            try
            {
                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    grid_StatsOverlayer.Visibility = Visibility.Collapsed;
                });
                AppTray.TrayNotifyIcon.Icon = new Icon(AVEmbedded.EmbeddedResourceToStream(null, "FpsOverlayer.Assets.AppIcon-Disabled.ico"));
            }
            catch { }
        }

        //Show the stats visibility
        public void ShowFpsOverlayVisibility()
        {
            try
            {
                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    grid_StatsOverlayer.Visibility = Visibility.Visible;
                });
                AppTray.TrayNotifyIcon.Icon = new Icon(AVEmbedded.EmbeddedResourceToStream(null, "FpsOverlayer.Assets.AppIcon.ico"));
            }
            catch { }
        }

        //Switch the stats visibility
        public void SwitchFpsOverlayVisibility()
        {
            try
            {
                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    if (grid_StatsOverlayer.Visibility == Visibility.Visible)
                    {
                        vManualHiddenFpsOverlay = true;
                        UpdateFpsOverlayPositionVisibility(vTargetProcess.ExeNameNoExt);
                    }
                    else
                    {
                        vManualHiddenFpsOverlay = false;
                        UpdateFpsOverlayPositionVisibility(vTargetProcess.ExeNameNoExt);
                    }
                });
            }
            catch { }
        }

        //Move window to next position
        public void ChangeFpsOverlayPosition()
        {
            try
            {
                int nextPosition = vSettings.Load("TextPosition", typeof(int)) + 1;
                if (nextPosition > 7)
                {
                    nextPosition = 0;
                }

                Debug.WriteLine("Changing text postion to: " + nextPosition);
                vSettings.Set("TextPosition", nextPosition.ToString());

                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    UpdateFpsOverlayStyle();
                });
            }
            catch { }
        }

        //Get window position
        public OverlayPosition GetFpsOverlayPosition(string processName)
        {
            try
            {
                //Load the text position
                OverlayPosition targetTextPosition = (OverlayPosition)vSettings.Load("TextPosition", typeof(int));
                if (!string.IsNullOrWhiteSpace(processName))
                {
                    ProfileShared FpsPositionProcessName = vFpsPositionProcessName.FirstOrDefault(x => x.String1.ToLower() == processName.ToLower());
                    if (FpsPositionProcessName != null)
                    {
                        Debug.WriteLine("Found fps position for: " + FpsPositionProcessName.String1 + " / " + FpsPositionProcessName.Int1);
                        targetTextPosition = (OverlayPosition)FpsPositionProcessName.Int1;
                    }
                }

                return targetTextPosition;
            }
            catch { }
            return OverlayPosition.TopLeft;
        }

        //Update window position and visibility
        public void UpdateFpsOverlayPositionVisibility(string processName)
        {
            try
            {
                //Get target overlay position
                OverlayPosition targetOverlayPosition = GetFpsOverlayPosition(processName);

                //Hide or show overlay
                if (vManualHiddenFpsOverlay || targetOverlayPosition == OverlayPosition.Hidden)
                {
                    HideFpsOverlayVisibility();
                    return;
                }
                else
                {
                    ShowFpsOverlayVisibility();
                }

                //Move fps to set position
                if (targetOverlayPosition == OverlayPosition.TopLeft)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        double marginHorizontal = vSettings.Load("MarginHorizontal", typeof(double));
                        double marginVertical = vSettings.Load("MarginVertical", typeof(double));
                        if (vTaskBarPosition == AppBarPosition.ABE_LEFT) { marginHorizontal += vTaskBarAdjustMargin; }
                        else if (vTaskBarPosition == AppBarPosition.ABE_TOP) { marginVertical += vTaskBarAdjustMargin; }
                        grid_StatsOverlayer.Margin = new Thickness(marginHorizontal, marginVertical, 0, 0);
                        grid_StatsOverlayer.VerticalAlignment = VerticalAlignment.Top;
                        grid_StatsOverlayer.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentMem.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentGpu.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentCpu.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentNet.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentFps.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentFrametime.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentApp.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentTime.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CustomText.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentMon.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentBat.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentFan.HorizontalAlignment = HorizontalAlignment.Left;
                    });
                }
                else if (targetOverlayPosition == OverlayPosition.TopCenter)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        double marginHorizontal = vSettings.Load("MarginHorizontal", typeof(double));
                        double marginVertical = vSettings.Load("MarginVertical", typeof(double));
                        if (vTaskBarPosition == AppBarPosition.ABE_TOP) { marginVertical += vTaskBarAdjustMargin; }
                        grid_StatsOverlayer.Margin = new Thickness(marginHorizontal, marginVertical, 0, 0);
                        grid_StatsOverlayer.VerticalAlignment = VerticalAlignment.Top;
                        grid_StatsOverlayer.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentMem.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentGpu.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentCpu.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentNet.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentFps.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentFrametime.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentApp.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentTime.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CustomText.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentMon.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentBat.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentFan.HorizontalAlignment = HorizontalAlignment.Center;
                    });
                }
                else if (targetOverlayPosition == OverlayPosition.TopRight)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        double marginHorizontal = vSettings.Load("MarginHorizontal", typeof(double));
                        double marginVertical = vSettings.Load("MarginVertical", typeof(double));
                        if (vTaskBarPosition == AppBarPosition.ABE_RIGHT) { marginHorizontal += vTaskBarAdjustMargin; }
                        else if (vTaskBarPosition == AppBarPosition.ABE_TOP) { marginVertical += vTaskBarAdjustMargin; }
                        grid_StatsOverlayer.Margin = new Thickness(0, marginVertical, marginHorizontal, 0);
                        grid_StatsOverlayer.VerticalAlignment = VerticalAlignment.Top;
                        grid_StatsOverlayer.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentMem.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentGpu.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentCpu.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentNet.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentFps.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentFrametime.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentApp.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentTime.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CustomText.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentMon.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentBat.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentFan.HorizontalAlignment = HorizontalAlignment.Right;
                    });
                }
                else if (targetOverlayPosition == OverlayPosition.MiddleRight)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        double marginHorizontal = vSettings.Load("MarginHorizontal", typeof(double));
                        double marginVertical = vSettings.Load("MarginVertical", typeof(double));
                        if (vTaskBarPosition == AppBarPosition.ABE_RIGHT) { marginHorizontal += vTaskBarAdjustMargin; }
                        grid_StatsOverlayer.Margin = new Thickness(0, marginVertical, marginHorizontal, 0);
                        grid_StatsOverlayer.VerticalAlignment = VerticalAlignment.Center;
                        grid_StatsOverlayer.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentMem.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentGpu.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentCpu.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentNet.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentFps.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentFrametime.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentApp.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentTime.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CustomText.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentMon.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentBat.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentFan.HorizontalAlignment = HorizontalAlignment.Right;
                    });
                }
                else if (targetOverlayPosition == OverlayPosition.BottomRight)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        double marginHorizontal = vSettings.Load("MarginHorizontal", typeof(double));
                        double marginVertical = vSettings.Load("MarginVertical", typeof(double));
                        if (vTaskBarPosition == AppBarPosition.ABE_RIGHT) { marginHorizontal += vTaskBarAdjustMargin; }
                        else if (vTaskBarPosition == AppBarPosition.ABE_BOTTOM) { marginVertical += vTaskBarAdjustMargin; }
                        marginVertical += vKeypadAdjustMargin;
                        grid_StatsOverlayer.Margin = new Thickness(0, 0, marginHorizontal, marginVertical);
                        grid_StatsOverlayer.VerticalAlignment = VerticalAlignment.Bottom;
                        grid_StatsOverlayer.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentMem.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentGpu.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentCpu.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentNet.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentFps.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentFrametime.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentApp.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentTime.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CustomText.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentMon.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentBat.HorizontalAlignment = HorizontalAlignment.Right;
                        stackpanel_CurrentFan.HorizontalAlignment = HorizontalAlignment.Right;
                    });
                }
                else if (targetOverlayPosition == OverlayPosition.BottomCenter)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        double marginHorizontal = vSettings.Load("MarginHorizontal", typeof(double));
                        double marginVertical = vSettings.Load("MarginVertical", typeof(double));
                        if (vTaskBarPosition == AppBarPosition.ABE_BOTTOM) { marginVertical += vTaskBarAdjustMargin; }
                        marginVertical += vKeypadAdjustMargin;
                        grid_StatsOverlayer.Margin = new Thickness(marginHorizontal, 0, 0, marginVertical);
                        grid_StatsOverlayer.VerticalAlignment = VerticalAlignment.Bottom;
                        grid_StatsOverlayer.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentMem.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentGpu.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentCpu.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentNet.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentFps.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentFrametime.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentApp.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentTime.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CustomText.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentMon.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentBat.HorizontalAlignment = HorizontalAlignment.Center;
                        stackpanel_CurrentFan.HorizontalAlignment = HorizontalAlignment.Center;
                    });
                }
                else if (targetOverlayPosition == OverlayPosition.BottomLeft)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        double marginHorizontal = vSettings.Load("MarginHorizontal", typeof(double));
                        double marginVertical = vSettings.Load("MarginVertical", typeof(double));
                        if (vTaskBarPosition == AppBarPosition.ABE_LEFT) { marginHorizontal += vTaskBarAdjustMargin; }
                        else if (vTaskBarPosition == AppBarPosition.ABE_BOTTOM) { marginVertical += vTaskBarAdjustMargin; }
                        marginVertical += vKeypadAdjustMargin;
                        grid_StatsOverlayer.Margin = new Thickness(marginHorizontal, 0, 0, marginVertical);
                        grid_StatsOverlayer.VerticalAlignment = VerticalAlignment.Bottom;
                        grid_StatsOverlayer.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentMem.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentGpu.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentCpu.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentNet.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentFps.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentFrametime.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentApp.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentTime.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CustomText.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentMon.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentBat.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentFan.HorizontalAlignment = HorizontalAlignment.Left;
                    });
                }
                else if (targetOverlayPosition == OverlayPosition.MiddleLeft)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        double marginHorizontal = vSettings.Load("MarginHorizontal", typeof(double));
                        double marginVertical = vSettings.Load("MarginVertical", typeof(double));
                        if (vTaskBarPosition == AppBarPosition.ABE_LEFT) { marginHorizontal += vTaskBarAdjustMargin; }
                        grid_StatsOverlayer.Margin = new Thickness(marginHorizontal, marginVertical, 0, 0);
                        grid_StatsOverlayer.VerticalAlignment = VerticalAlignment.Center;
                        grid_StatsOverlayer.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentMem.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentGpu.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentCpu.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentNet.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentFps.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentFrametime.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentApp.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentTime.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CustomText.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentMon.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentBat.HorizontalAlignment = HorizontalAlignment.Left;
                        stackpanel_CurrentFan.HorizontalAlignment = HorizontalAlignment.Left;
                    });
                }
            }
            catch { }
        }

        //Update fps overlay style
        public void UpdateFpsOverlayStyle()
        {
            try
            {
                //Update the stats titles
                if (vSettings.Load("GpuShowCategoryTitle", typeof(bool)))
                {
                    vTitleGPU = vSettings.Load("GpuCategoryTitle", typeof(string));
                }
                else
                {
                    vTitleGPU = string.Empty;
                }
                if (vSettings.Load("CpuShowCategoryTitle", typeof(bool)))
                {
                    vTitleCPU = vSettings.Load("CpuCategoryTitle", typeof(string));
                }
                else
                {
                    vTitleCPU = string.Empty;
                }
                if (vSettings.Load("MemShowCategoryTitle", typeof(bool)))
                {
                    vTitleMEM = vSettings.Load("MemCategoryTitle", typeof(string));
                }
                else
                {
                    vTitleMEM = string.Empty;
                }
                if (vSettings.Load("NetShowCategoryTitle", typeof(bool)))
                {
                    vTitleNET = vSettings.Load("NetCategoryTitle", typeof(string));
                }
                else
                {
                    vTitleNET = string.Empty;
                }
                if (vSettings.Load("MonShowCategoryTitle", typeof(bool)))
                {
                    vTitleMON = vSettings.Load("MonCategoryTitle", typeof(string));
                }
                else
                {
                    vTitleMON = string.Empty;
                }
                if (vSettings.Load("FpsShowCategoryTitle", typeof(bool)))
                {
                    vTitleFPS = vSettings.Load("FpsCategoryTitle", typeof(string));
                }
                else
                {
                    vTitleFPS = string.Empty;
                }
                if (vSettings.Load("BatShowCategoryTitle", typeof(bool)))
                {
                    vTitleBAT = vSettings.Load("BatCategoryTitle", typeof(string));
                }
                else
                {
                    vTitleBAT = string.Empty;
                }
                if (vSettings.Load("FanShowCategoryTitle", typeof(bool)))
                {
                    vTitleFAN = vSettings.Load("FanCategoryTitle", typeof(string));
                }
                else
                {
                    vTitleFAN = string.Empty;
                }

                //Load stats order identifier
                int TimeId = vSettings.Load("TimeId", typeof(int));
                int CustomTextId = vSettings.Load("CustomTextId", typeof(int));
                int MonId = vSettings.Load("MonId", typeof(int));
                int AppId = vSettings.Load("AppId", typeof(int));
                int FpsId = vSettings.Load("FpsId", typeof(int));
                int FrametimeId = vSettings.Load("FrametimeId", typeof(int));
                int NetId = vSettings.Load("NetId", typeof(int));
                int CpuId = vSettings.Load("CpuId", typeof(int));
                int GpuId = vSettings.Load("GpuId", typeof(int));
                int MemId = vSettings.Load("MemId", typeof(int));
                int BatId = vSettings.Load("BatId", typeof(int));
                int FanId = vSettings.Load("FanId", typeof(int));

                //Update the stats text orientation and order
                if (vSettings.Load("TextDirection", typeof(int)) == 1)
                {
                    //Reverse stats order when on bottom
                    if (vSettings.Load("StatsFlipBottom", typeof(bool)))
                    {
                        OverlayPosition overlayPosition = GetFpsOverlayPosition(vTargetProcess.ExeNameNoExt);
                        if (overlayPosition == OverlayPosition.BottomLeft || overlayPosition == OverlayPosition.BottomCenter || overlayPosition == OverlayPosition.BottomRight)
                        {
                            TimeId = vTotalStatsCount - TimeId;
                            CustomTextId = vTotalStatsCount - CustomTextId;
                            MonId = vTotalStatsCount - MonId;
                            AppId = vTotalStatsCount - AppId;
                            FpsId = vTotalStatsCount - FpsId;
                            FrametimeId = vTotalStatsCount - FrametimeId;
                            NetId = vTotalStatsCount - NetId;
                            CpuId = vTotalStatsCount - CpuId;
                            GpuId = vTotalStatsCount - GpuId;
                            MemId = vTotalStatsCount - MemId;
                            BatId = vTotalStatsCount - BatId;
                            FanId = vTotalStatsCount - FanId;
                        }
                    }

                    //Vertical text order
                    stackpanel_CurrentTime.SetValue(Grid.RowProperty, TimeId);
                    stackpanel_CustomText.SetValue(Grid.RowProperty, CustomTextId);
                    stackpanel_CurrentMon.SetValue(Grid.RowProperty, MonId);
                    stackpanel_CurrentApp.SetValue(Grid.RowProperty, AppId);
                    stackpanel_CurrentFps.SetValue(Grid.RowProperty, FpsId);
                    stackpanel_CurrentFrametime.SetValue(Grid.RowProperty, FrametimeId);
                    stackpanel_CurrentNet.SetValue(Grid.RowProperty, NetId);
                    stackpanel_CurrentCpu.SetValue(Grid.RowProperty, CpuId);
                    stackpanel_CurrentGpu.SetValue(Grid.RowProperty, GpuId);
                    stackpanel_CurrentMem.SetValue(Grid.RowProperty, MemId);
                    stackpanel_CurrentBat.SetValue(Grid.RowProperty, BatId);
                    stackpanel_CurrentFan.SetValue(Grid.RowProperty, FanId);

                    stackpanel_CurrentMem.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentGpu.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentCpu.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentNet.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentFps.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentFrametime.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentApp.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentTime.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CustomText.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentMon.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentBat.SetValue(Grid.ColumnProperty, 0);
                    stackpanel_CurrentFan.SetValue(Grid.ColumnProperty, 0);
                }
                else
                {
                    //Horizontal text order
                    stackpanel_CurrentTime.SetValue(Grid.ColumnProperty, TimeId);
                    stackpanel_CustomText.SetValue(Grid.ColumnProperty, CustomTextId);
                    stackpanel_CurrentMon.SetValue(Grid.ColumnProperty, MonId);
                    stackpanel_CurrentApp.SetValue(Grid.ColumnProperty, AppId);
                    stackpanel_CurrentFps.SetValue(Grid.ColumnProperty, FpsId);
                    stackpanel_CurrentFrametime.SetValue(Grid.ColumnProperty, FrametimeId);
                    stackpanel_CurrentNet.SetValue(Grid.ColumnProperty, NetId);
                    stackpanel_CurrentCpu.SetValue(Grid.ColumnProperty, CpuId);
                    stackpanel_CurrentGpu.SetValue(Grid.ColumnProperty, GpuId);
                    stackpanel_CurrentMem.SetValue(Grid.ColumnProperty, MemId);
                    stackpanel_CurrentBat.SetValue(Grid.ColumnProperty, BatId);
                    stackpanel_CurrentFan.SetValue(Grid.ColumnProperty, FanId);

                    stackpanel_CurrentMem.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentGpu.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentCpu.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentNet.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentFps.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentFrametime.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentApp.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentTime.SetValue(Grid.RowProperty, 0);
                    stackpanel_CustomText.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentMon.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentBat.SetValue(Grid.RowProperty, 0);
                    stackpanel_CurrentFan.SetValue(Grid.RowProperty, 0);
                }

                //Update the stats background
                SolidColorBrush brushBackground = null;
                if (vSettings.Load("DisplayBackground", typeof(bool)))
                {
                    string colorBackground = vSettings.Load("ColorBackground", typeof(string));
                    brushBackground = new BrushConverter().ConvertFrom(colorBackground) as SolidColorBrush;
                }
                else
                {
                    brushBackground = new SolidColorBrush(Colors.Transparent);
                }
                stackpanel_CurrentMem.Background = brushBackground;
                stackpanel_CurrentGpu.Background = brushBackground;
                stackpanel_CurrentCpu.Background = brushBackground;
                stackpanel_CurrentNet.Background = brushBackground;
                stackpanel_CurrentFps.Background = brushBackground;
                stackpanel_CurrentFrametime.Background = brushBackground;
                stackpanel_CurrentApp.Background = brushBackground;
                stackpanel_CurrentTime.Background = brushBackground;
                stackpanel_CustomText.Background = brushBackground;
                stackpanel_CurrentMon.Background = brushBackground;
                stackpanel_CurrentBat.Background = brushBackground;
                stackpanel_CurrentFan.Background = brushBackground;

                //Adjust window font family
                UpdateWindowFontFamily();

                //Update the stats text size
                double targetTextSize = vSettings.Load("TextSize", typeof(double));
                textblock_CurrentMem.FontSize = targetTextSize;
                textblock_CurrentMem.LineHeight = targetTextSize;
                textblock_CurrentMem.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CurrentGpu.FontSize = targetTextSize;
                textblock_CurrentGpu.LineHeight = targetTextSize;
                textblock_CurrentGpu.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CurrentCpu.FontSize = targetTextSize;
                textblock_CurrentCpu.LineHeight = targetTextSize;
                textblock_CurrentCpu.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CurrentNet.FontSize = targetTextSize;
                textblock_CurrentNet.LineHeight = targetTextSize;
                textblock_CurrentNet.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CurrentFps.FontSize = targetTextSize;
                textblock_CurrentFps.LineHeight = targetTextSize;
                textblock_CurrentFps.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CurrentApp.FontSize = targetTextSize;
                textblock_CurrentApp.LineHeight = targetTextSize;
                textblock_CurrentApp.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CurrentTime.FontSize = targetTextSize;
                textblock_CurrentTime.LineHeight = targetTextSize;
                textblock_CurrentTime.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CustomText.FontSize = targetTextSize;
                textblock_CustomText.LineHeight = targetTextSize;
                textblock_CustomText.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CurrentMon.FontSize = targetTextSize;
                textblock_CurrentMon.LineHeight = targetTextSize;
                textblock_CurrentMon.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CurrentBat.FontSize = targetTextSize;
                textblock_CurrentBat.LineHeight = targetTextSize;
                textblock_CurrentBat.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                textblock_CurrentFan.FontSize = targetTextSize;
                textblock_CurrentFan.LineHeight = targetTextSize;
                textblock_CurrentFan.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;

                //Update the stats colors
                if (vSettings.Load("TextColorSingle", typeof(bool)))
                {
                    string ColorSingle = vSettings.Load("ColorSingle", typeof(string));
                    SolidColorBrush brushForeground = new BrushConverter().ConvertFrom(ColorSingle) as SolidColorBrush;
                    textblock_CurrentMem.Foreground = brushForeground;
                    textblock_CurrentGpu.Foreground = brushForeground;
                    textblock_CurrentCpu.Foreground = brushForeground;
                    textblock_CurrentNet.Foreground = brushForeground;
                    textblock_CurrentFps.Foreground = brushForeground;
                    polyline_Chart.Stroke = brushForeground;
                    textblock_CurrentApp.Foreground = brushForeground;
                    textblock_CurrentTime.Foreground = brushForeground;
                    textblock_CustomText.Foreground = brushForeground;
                    textblock_CurrentMon.Foreground = brushForeground;
                    textblock_CurrentBat.Foreground = brushForeground;
                    textblock_CurrentFan.Foreground = brushForeground;
                }
                else
                {
                    string ColorMem = vSettings.Load("ColorMem", typeof(string));
                    string ColorGpu = vSettings.Load("ColorGpu", typeof(string));
                    string ColorCpu = vSettings.Load("ColorCpu", typeof(string));
                    string ColorNet = vSettings.Load("ColorNet", typeof(string));
                    string ColorFps = vSettings.Load("ColorFps", typeof(string));
                    string ColorFrametime = vSettings.Load("ColorFrametime", typeof(string));
                    string ColorApp = vSettings.Load("ColorApp", typeof(string));
                    string ColorTime = vSettings.Load("ColorTime", typeof(string));
                    string ColorCustomText = vSettings.Load("ColorCustomText", typeof(string));
                    string ColorMon = vSettings.Load("ColorMon", typeof(string));
                    string ColorBat = vSettings.Load("ColorBat", typeof(string));
                    string ColorFan = vSettings.Load("ColorFan", typeof(string));
                    textblock_CurrentMem.Foreground = new BrushConverter().ConvertFrom(ColorMem) as SolidColorBrush;
                    textblock_CurrentGpu.Foreground = new BrushConverter().ConvertFrom(ColorGpu) as SolidColorBrush;
                    textblock_CurrentCpu.Foreground = new BrushConverter().ConvertFrom(ColorCpu) as SolidColorBrush;
                    textblock_CurrentNet.Foreground = new BrushConverter().ConvertFrom(ColorNet) as SolidColorBrush;
                    textblock_CurrentFps.Foreground = new BrushConverter().ConvertFrom(ColorFps) as SolidColorBrush;
                    polyline_Chart.Stroke = new BrushConverter().ConvertFrom(ColorFrametime) as SolidColorBrush;
                    textblock_CurrentApp.Foreground = new BrushConverter().ConvertFrom(ColorApp) as SolidColorBrush;
                    textblock_CurrentTime.Foreground = new BrushConverter().ConvertFrom(ColorTime) as SolidColorBrush;
                    textblock_CustomText.Foreground = new BrushConverter().ConvertFrom(ColorCustomText) as SolidColorBrush;
                    textblock_CurrentMon.Foreground = new BrushConverter().ConvertFrom(ColorMon) as SolidColorBrush;
                    textblock_CurrentBat.Foreground = new BrushConverter().ConvertFrom(ColorBat) as SolidColorBrush;
                    textblock_CurrentFan.Foreground = new BrushConverter().ConvertFrom(ColorFan) as SolidColorBrush;
                }

                //Update frametime graph size
                stackpanel_CurrentFrametime.Height = vSettings.Load("FrametimeHeight", typeof(double));
                stackpanel_CurrentFrametime.Width = vSettings.Load("FrametimeWidth", typeof(double));

                //Update fps overlay opacity
                grid_StatsOverlayer.Opacity = vSettings.Load("DisplayOpacity", typeof(double));

                //Update fps overlay position and visibility
                UpdateFpsOverlayPositionVisibility(vTargetProcess.ExeNameNoExt);
            }
            catch { }
        }
    }
}