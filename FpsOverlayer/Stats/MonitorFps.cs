using ArnoldVinkCode;
using ArnoldVinkStyles;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static ArnoldVinkCode.AVActions;
using static FpsOverlayer.AppTasks;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowStats
    {
        void StartMonitorFps()
        {
            try
            {
                AVActions.TaskStartLoop(UpdateStatsFps, vTask_UpdateStatsFps);
                Debug.WriteLine("Started monitoring fps.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed starting fps monitor: " + ex.Message);
            }
        }

        void TraceEventStart()
        {
            try
            {
                if (vTraceEventSession == null)
                {
                    //Set trace event provider options
                    TraceEventProviderOptions traceEventProviderOptions = new TraceEventProviderOptions();
                    traceEventProviderOptions.EventIDsToEnable = [(int)vTraceEventIdentifiers.DxgKrnl_Present];

                    //Create trace event session
                    vTraceEventSession = new TraceEventSession("FpsOverlayer");
                    vTraceEventSession.EnableProvider(vProvider_DxgKrnl.ToString(), TraceEventLevel.Informational, 0, traceEventProviderOptions);
                    vTraceEventSession.Source.AllEvents += ProcessEvents;

                    //Start trace event session
                    AVActions.TaskStartBackground(TaskTraceEventSource);
                    Debug.WriteLine("Started trace event session.");
                }
            }
            catch (Exception ex)
            {
                //Note: some games using anti-cheat block trace event from starting
                Debug.WriteLine("Failed starting trace event: " + ex.Message);
                TraceEventStop();
            }
        }

        void TraceEventStop()
        {
            try
            {
                if (vTraceEventSession != null)
                {
                    vTraceEventSession.Dispose();
                    vTraceEventSession = null;
                    Debug.WriteLine("Stopped trace event session.");
                }
            }
            catch { }
        }

        void TaskTraceEventSource()
        {
            try
            {
                vTraceEventSession.Source.Process();
            }
            catch { }
        }

        bool UpdateFpsVisibility()
        {
            try
            {
                //Check the total available frames and last added frame time
                if (!vListFrameTimes.Any() || (GetSystemTicksMilli() - vLastFrameTimeUpdate) >= 1000)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CurrentFrametime.Visibility = Visibility.Collapsed;
                        stackpanel_CurrentFps.Visibility = Visibility.Collapsed;
                    });

                    return false;
                }
                else
                {
                    bool showFrametime = vSettings.Load("FrametimeGraphShow", typeof(bool));
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        if (showFrametime)
                        {
                            stackpanel_CurrentFrametime.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            stackpanel_CurrentFrametime.Visibility = Visibility.Collapsed;
                        }

                        bool stringEmpty = string.IsNullOrWhiteSpace(textblock_CurrentFps.Text) || textblock_CurrentFps.Text == vTitleFPS;
                        if (!stringEmpty)
                        {
                            stackpanel_CurrentFps.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            stackpanel_CurrentFps.Visibility = Visibility.Collapsed;
                        }
                    });
                }
            }
            catch { }
            return true;
        }

        async Task UpdateStatsFps()
        {
            try
            {
                while (await TaskCheckLoop(vTask_UpdateStatsFps, 1000))
                {
                    try
                    {
                        //Start trace event session
                        TraceEventStart();

                        //Update fps visibility
                        if (!UpdateFpsVisibility())
                        {
                            continue;
                        }

                        //Get frame times from last second
                        double CurrentFrameTimes = vListFrameTimes.Take(100).Average();

                        //Set frame time string (1sec)
                        string StringCurrentFrameTimes = string.Empty;
                        if (vSettings.Load("FpsShowCurrentLatency", typeof(bool)))
                        {
                            StringCurrentFrameTimes = " " + CurrentFrameTimes.ToString("0.00") + "MS";
                        }

                        //Calculate current fps (1sec)
                        string StringCurrentFramesPerSecond = string.Empty;
                        if (vSettings.Load("FpsShowCurrentFps", typeof(bool)))
                        {
                            int CurrentFramesPerSecond = Convert.ToInt32(1000 / CurrentFrameTimes);
                            StringCurrentFramesPerSecond = " " + CurrentFramesPerSecond.ToString() + "FPS";
                        }

                        //Calculate average fps (setting)
                        string StringAverageFramesPerSecond = string.Empty;
                        if (vSettings.Load("FpsShowAverageFps", typeof(bool)))
                        {
                            int AverageCount = vSettings.Load("FpsAverageSeconds", typeof(int)) * 100;
                            double AverageFrameTimes = vListFrameTimes.Take(AverageCount).Average();
                            int AverageFramesPerSecond = Convert.ToInt32(1000 / AverageFrameTimes);
                            StringAverageFramesPerSecond = " " + AverageFramesPerSecond.ToString() + "AVG";
                        }

                        //Calculate 1% low fps
                        string StringOnePercentLowFramesPerSecond = string.Empty;
                        if (vSettings.Load("FpsShowOnePercentLowFps", typeof(bool)))
                        {
                            int OnePercentLowCount = Math.Max(1, (int)(vListFrameTimes.Count * 0.01));
                            double OnePercentLowFrameTimes = vListFrameTimes.OrderByDescending(x => x).Take(OnePercentLowCount).Average();
                            int OnePercentLowPerSecond = Convert.ToInt32(1000 / OnePercentLowFrameTimes);
                            StringOnePercentLowFramesPerSecond = " " + OnePercentLowPerSecond.ToString() + "LOW";
                        }

                        //Update render api
                        string StringRenderApi = string.Empty;
                        if (vSettings.Load("FpsShowRenderer", typeof(bool)))
                        {
                            if (!vProcessRenderApi.RenderingUI && !string.IsNullOrWhiteSpace(vProcessRenderApi.ApiName3D))
                            {
                                StringRenderApi = " " + vProcessRenderApi.ApiName3D;
                            }
                        }

                        //Update fps counter
                        Debug.WriteLine("(P" + vProcessTarget.Identifier + ")" + StringCurrentFrameTimes + " /" + StringCurrentFramesPerSecond + " /" + StringAverageFramesPerSecond + " /" + StringOnePercentLowFramesPerSecond + " /" + StringRenderApi);
                        string StringDisplay = vTitleFPS + StringCurrentFramesPerSecond + StringCurrentFrameTimes + StringAverageFramesPerSecond + StringOnePercentLowFramesPerSecond + StringRenderApi;
                        StringDisplay = StringDisplay.Trim();

                        AVDispatcherInvoke.DispatcherInvoke(delegate
                        {
                            textblock_CurrentFps.Text = StringDisplay;
                        });
                    }
                    catch { }
                }
            }
            catch { }
        }

        void UpdateStatsFrameTimeGraph(double frameTime)
        {
            try
            {
                double yPoint = frameTime;
                double xPoint = vFrametimeCurrent;
                vFrametimeCurrent += vSettings.Load("FrametimeAccuracy", typeof(double));

                AVDispatcherInvoke.DispatcherInvoke(delegate
                {
                    //Check point height
                    double graphHeight = vSettings.Load("FrametimeHeight", typeof(double)) - 2;
                    if (yPoint < 2)
                    {
                        yPoint = 2;
                    }
                    else if (yPoint > graphHeight)
                    {
                        yPoint = graphHeight;
                    }

                    //Add frametime point
                    vPointFrameTimes.Add(new Point(xPoint, yPoint));
                    stackpanel_CurrentFrametime.ScrollToRightEnd();

                    //Cleanup frametime points (10sec)
                    if (vPointFrameTimes.Count > 1000)
                    {
                        vPointFrameTimes.RemoveAt(0);
                    }
                });

                //Debug.WriteLine("Added frametime to graph: " + frameTime);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed adding frametime to graph: " + ex.Message);
            }
        }

        void ProcessEvents(TraceEvent traceEvent)
        {
            try
            {
                //Debug.WriteLine("Trace event identifier: " + traceEvent.ProcessID + " / " + (vTraceEventIdentifiers)traceEvent.ID + " / " + traceEvent.TimeStampRelativeMSec);

                //Check event identifier
                if ((int)traceEvent.ID != (int)vTraceEventIdentifiers.DxgKrnl_Present)
                {
                    //Debug.WriteLine("DxgKrnl skipping invalid frame.");
                    return;
                }

                //Check process identifiers
                if (vProcessTarget.Identifier != traceEvent.ProcessID && !vProcessTargetChildren.Contains(traceEvent.ProcessID))
                {
                    //Debug.WriteLine("Event process is not foreground window or process.");
                    return;
                }

                //Calculate new frame time
                double timeElapsed = traceEvent.TimeStampRelativeMSec;
                double timeBetween = timeElapsed - vLastFrameTimeStamp;
                vLastFrameTimeUpdate = GetSystemTicksMilli();
                vLastFrameTimeStamp = timeElapsed;

                //Check frametime
                if (timeBetween < 1000)
                {
                    //Add frametime to list
                    vListFrameTimes.Insert(0, timeBetween);

                    //Cleanup frametimes (10sec)
                    if (vListFrameTimes.Count > 1000)
                    {
                        vListFrameTimes.RemoveAt(1001);
                    }

                    //Add frametime to graph
                    UpdateStatsFrameTimeGraph(timeBetween);

                    //Debug.WriteLine("Added frametime to list: " + timeBetween);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed adding frametime to list: " + ex.Message);
            }
        }
    }
}