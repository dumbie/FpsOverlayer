using ArnoldVinkCode;
using ArnoldVinkStyles;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using static ArnoldVinkCode.AVActions;
using static ArnoldVinkCode.AVProcess;
using static ArnoldVinkCode.AVWindowFunctions;
using static FpsOverlayer.AppTasks;
using static FpsOverlayer.AppVariables;
using static LibraryShared.Classes;

namespace FpsOverlayer
{
    public partial class WindowStats
    {
        void StartMonitorProcess()
        {
            try
            {
                AVActions.TaskStartLoop(LoopMonitorProcess, vTask_MonitorProcess);
                Debug.WriteLine("Started monitoring processes.");
            }
            catch { }
        }

        async Task LoopMonitorProcess()
        {
            try
            {
                while (await TaskCheckLoop(vTask_MonitorProcess, 2000))
                {
                    try
                    {
                        //Update the current time
                        UpdateCurrentTime();

                        //Update custom text
                        UpdateCustomText();

                        //Get and check the focused process
                        AVProcess foregroundProcess = Get_ProcessForeground();
                        if (foregroundProcess == null || !foregroundProcess.Validate())
                        {
                            Debug.WriteLine("No active or valid process found.");

                            //Reset fps counter
                            ResetFpsCounter();

                            //Hide application name and frames
                            HideApplicationNameFrames();

                            continue;
                        }

                        Debug.WriteLine("Checking process: (" + foregroundProcess.Identifier + ") " + foregroundProcess.ExeNameNoExt);

                        //Update application name
                        //Note: Some applications change window title while running
                        UpdateApplicationName(foregroundProcess.WindowTitleMain);

                        //Update process render api
                        //Note: Some applications change 3d rendering modules while running
                        RenderApiDetails foregroundRenderApi = GetRenderApi(foregroundProcess);

                        //Check if foreground window or render api changed
                        if (vProcessTarget.Identifier != foregroundProcess.Identifier || vProcessRenderApi.ApiName3D != foregroundRenderApi.ApiName3D)
                        {
                            Debug.WriteLine("New foreground window or render api detected (" + foregroundProcess.Identifier + ") " + foregroundProcess.ExeNameNoExt);

                            //Update current process
                            vProcessTarget = foregroundProcess;

                            //Update current render api
                            vProcessRenderApi = foregroundRenderApi;

                            //Reset fps counter
                            ResetFpsCounter();

                            //Update overlays position and visibility
                            UpdateFpsOverlayPositionVisibility(foregroundProcess, foregroundRenderApi);

                            //Update windows on change
                            UpdateWindowOnChange();
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        //Update window on change
        void UpdateWindowOnChange()
        {
            try
            {
                //Update stats window style
                WindowUpdateStyle(vInteropWindowHandle, true, true, true, true);

                //Update window position
                UpdateWindowPosition();
            }
            catch { }
        }

        //Hide the application name and frames
        void HideApplicationNameFrames()
        {
            try
            {
                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    stackpanel_CurrentApp.Visibility = Visibility.Collapsed;
                    stackpanel_CurrentFps.Visibility = Visibility.Collapsed;
                    stackpanel_CurrentFrametime.Visibility = Visibility.Collapsed;
                });
            }
            catch { }
        }

        //Update the current time
        void UpdateCurrentTime()
        {
            try
            {
                string currentTimeString = string.Empty;
                bool showTime = vSettings.Load("TimeShowCurrentTime", typeof(bool));
                bool showDate = vSettings.Load("TimeShowCurrentDate", typeof(bool));

                if (showTime)
                {
                    currentTimeString = DateTime.Now.ToShortTimeString();
                }

                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    vWindowTools.textblock_Time.Text = DateTime.Now.ToShortTimeString();
                });

                if (showDate)
                {
                    if (string.IsNullOrWhiteSpace(currentTimeString))
                    {
                        currentTimeString += DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        currentTimeString += " (" + DateTime.Now.ToShortDateString() + ")";
                    }
                }

                //Check if time string is empty
                if (string.IsNullOrWhiteSpace(currentTimeString))
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CurrentTime.Visibility = Visibility.Collapsed;
                    });
                }
                else
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        textblock_CurrentTime.Text = currentTimeString;
                        stackpanel_CurrentTime.Visibility = Visibility.Visible;
                    });
                }
            }
            catch { }
        }

        //Update custom text
        void UpdateCustomText()
        {
            try
            {
                string customText = vSettings.Load("CustomTextString", typeof(string));
                //Debug.WriteLine("Setting custom text: " + customText);

                if (!string.IsNullOrWhiteSpace(customText))
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        textblock_CustomText.Text = customText;
                        stackpanel_CustomText.Visibility = Visibility.Visible;
                    });
                }
                else
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CustomText.Visibility = Visibility.Collapsed;
                    });
                }
            }
            catch { }
        }

        //Update the application name
        void UpdateApplicationName(string processTitle)
        {
            try
            {
                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    if (vSettings.Load("AppShowName", typeof(bool)))
                    {
                        if (!string.IsNullOrWhiteSpace(processTitle) && processTitle != "Unknown")
                        {
                            textblock_CurrentApp.Text = processTitle;
                            stackpanel_CurrentApp.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            stackpanel_CurrentApp.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        stackpanel_CurrentApp.Visibility = Visibility.Collapsed;
                    }
                });
            }
            catch { }
        }

        void ResetFpsCounter()
        {
            try
            {
                //Reset fps variables
                vLastFrameTimeStamp = 0;
                vListFrameTimes.Clear();

                //Reset frametime variables
                vFrametimeCurrent = 0;
                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    vPointFrameTimes.Clear();
                });

                Debug.WriteLine("Reset the frames per second counter.");
            }
            catch { }
        }
    }
}