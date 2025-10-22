using ArnoldVinkCode;
using ArnoldVinkStyles;
using LibreHardwareMonitor.Hardware;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowStats
    {
        void UpdateFanInformation(IList<IHardware> hardwareItems)
        {
            try
            {
                //Check if the information is visible
                bool FanShowCpu = vSettings.Load("FanShowCpu", typeof(bool));
                bool FanShowGpu = vSettings.Load("FanShowGpu", typeof(bool));
                bool FanShowPump = vSettings.Load("FanShowPump", typeof(bool));
                bool FanShowSystem = vSettings.Load("FanShowSystem", typeof(bool));
                bool CpuShowFanSpeed = vSettings.Load("CpuShowFanSpeed", typeof(bool));
                if (!FanShowCpu && !FanShowGpu && !FanShowPump && !FanShowSystem && !CpuShowFanSpeed)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CurrentFan.Visibility = Visibility.Collapsed;
                    });
                    return;
                }

                //Select hardware item
                IHardware hardwareItem = hardwareItems.FirstOrDefault(x => x.HardwareType == HardwareType.Motherboard);

                //Update hardware item
                hardwareItem.Update();

                //Load hardware information
                List<string> fansCpu = new List<string>();
                List<string> fansPump = new List<string>();
                List<string> fansSystem = new List<string>();
                foreach (IHardware subHardware in hardwareItem.SubHardware)
                {
                    try
                    {
                        subHardware.Update();
                        foreach (ISensor sensor in subHardware.Sensors)
                        {
                            //Debug.WriteLine("Fan: " + sensor.Name + "/" + sensor.Identifier + "/" + sensor.Value.ToString());
                            try
                            {
                                string sensorNameLower = sensor.Name.ToLower();
                                if (sensorNameLower.Contains("cpu") && sensor.Identifier.ToString().Contains("/fan/"))
                                {
                                    float rawFanSpeed = (float)sensor.Value;
                                    if (rawFanSpeed > 0)
                                    {
                                        fansCpu.Add(rawFanSpeed.ToString("0") + "RPM");
                                    }
                                }
                                else if (sensorNameLower.Contains("pump") && sensor.Identifier.ToString().Contains("/fan/"))
                                {
                                    float rawFanSpeed = (float)sensor.Value;
                                    if (rawFanSpeed > 0)
                                    {
                                        fansPump.Add(rawFanSpeed.ToString("0") + "RPM");
                                    }
                                }
                                else if (sensorNameLower.Contains("system") && !sensorNameLower.Contains("pump") && sensor.Identifier.ToString().Contains("/fan/"))
                                {
                                    float rawFanSpeed = (float)sensor.Value;
                                    if (rawFanSpeed > 0)
                                    {
                                        fansSystem.Add(rawFanSpeed.ToString("0") + "RPM");
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                    catch { }
                }

                //Set fan string variables
                vHardwareCpuFanSpeed = string.Join(" ", fansCpu);
                vHardwarePumpFanSpeed = string.Join(" ", fansPump);
                vHardwareSystemFanSpeed = string.Join(" ", fansSystem);

                string stringStats = string.Empty;
                if (FanShowCpu && !string.IsNullOrWhiteSpace(vHardwareCpuFanSpeed))
                {
                    stringStats += " (C)" + vHardwareCpuFanSpeed;
                }
                if (FanShowGpu && !string.IsNullOrWhiteSpace(vHardwareGpuFanSpeed))
                {
                    stringStats += " (G)" + vHardwareGpuFanSpeed;
                }
                if (FanShowPump && !string.IsNullOrWhiteSpace(vHardwarePumpFanSpeed))
                {
                    if (string.IsNullOrWhiteSpace(stringStats))
                    {
                        stringStats += " " + vHardwarePumpFanSpeed;
                    }
                    else
                    {
                        stringStats += " (P)" + vHardwarePumpFanSpeed;
                    }
                }
                if (FanShowSystem && !string.IsNullOrWhiteSpace(vHardwareSystemFanSpeed))
                {
                    if (string.IsNullOrWhiteSpace(stringStats))
                    {
                        stringStats += " " + vHardwareSystemFanSpeed;
                    }
                    else
                    {
                        stringStats += " (S)" + vHardwareSystemFanSpeed;
                    }
                }

                if (!string.IsNullOrWhiteSpace(stringStats))
                {
                    string stringDisplay = AVFunctions.StringRemoveStart(vTitleFAN + stringStats, " ");
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        textblock_CurrentFan.Text = stringDisplay;
                        stackpanel_CurrentFan.Visibility = Visibility.Visible;
                    });
                }
                else
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CurrentFan.Visibility = Visibility.Collapsed;
                    });
                }
            }
            catch { }
        }
    }
}