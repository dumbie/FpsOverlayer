using ArnoldVinkCode;
using ArnoldVinkStyles;
using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowStats
    {
        private void UpdateMemoryInformation(IList<IHardware> hardwareItems)
        {
            try
            {
                //Check if the information is visible
                bool showName = vSettings.Load("MemShowName", typeof(bool));
                bool showSpeed = vSettings.Load("MemShowSpeed", typeof(bool));
                bool showTemperature = vSettings.Load("MemShowTemperature", typeof(bool));
                bool showPowerVolt = vSettings.Load("MemShowPowerVolt", typeof(bool));
                bool showPercentage = vSettings.Load("MemShowPercentage", typeof(bool));
                bool showUsed = vSettings.Load("MemShowUsed", typeof(bool));
                bool showFree = vSettings.Load("MemShowFree", typeof(bool));
                bool showTotal = vSettings.Load("MemShowTotal", typeof(bool));
                if (!showName && !showSpeed && !showPercentage && !showUsed && !showFree && !showTotal && !showTemperature && !showPowerVolt)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CurrentMem.Visibility = Visibility.Collapsed;
                    });
                    return;
                }

                //Select hardware item
                IHardware hardwareItem = hardwareItems.FirstOrDefault(x => x.HardwareType == HardwareType.Memory && x.Identifier.ToString() == "/ram");

                //Update hardware item
                hardwareItem.Update();

                string MemoryName = string.Empty;
                string MemorySpeed = string.Empty;
                string MemoryPowerVolt = string.Empty;
                string MemoryPercentage = string.Empty;
                string MemoryBytes = string.Empty;
                string MemoryTemperature = string.Empty;
                float RawMemoryUsed = 0;
                float RawMemoryFree = 0;

                //Set the memory name
                if (showName)
                {
                    MemoryName = vHardwareMemoryName;
                }

                //Set the memory speed
                if (showSpeed)
                {
                    MemorySpeed = " " + vHardwareMemorySpeed;
                }

                //Set the memory voltage
                if (showPowerVolt)
                {
                    MemoryPowerVolt = " " + vHardwareMemoryVoltage;
                }

                //Set memory temperature
                if (showTemperature)
                {
                    MemoryTemperature = GetMemoryTemperatureString(hardwareItems);
                }

                foreach (ISensor sensor in hardwareItem.Sensors)
                {
                    try
                    {
                        if (showPercentage && sensor.SensorType == SensorType.Load)
                        {
                            //Debug.WriteLine("Mem Load Percentage: " + sensor.Name + "/" + sensor.Identifier + "/" + sensor.Value.ToString());
                            if (sensor.Identifier.ToString().EndsWith("load/0"))
                            {
                                MemoryPercentage = " " + Convert.ToInt32(sensor.Value) + "%";
                            }
                        }
                        else if (sensor.SensorType == SensorType.Data)
                        {
                            //Debug.WriteLine("Mem Load Data: " + sensor.Name + "/" + sensor.Identifier + "/" + sensor.Value.ToString());
                            if (sensor.Identifier.ToString().EndsWith("data/0"))
                            {
                                RawMemoryUsed = (float)sensor.Value;
                            }
                            else if (sensor.Identifier.ToString().EndsWith("data/1"))
                            {
                                RawMemoryFree = (float)sensor.Value;
                            }
                        }
                    }
                    catch { }
                }

                if (showUsed)
                {
                    MemoryBytes += " " + RawMemoryUsed.ToString("0.0") + "GB(U)";
                }
                if (showFree)
                {
                    MemoryBytes += " " + RawMemoryFree.ToString("0.0") + "GB(F)";
                }
                if (showTotal)
                {
                    MemoryBytes += " " + Convert.ToInt32(RawMemoryUsed + RawMemoryFree) + "GB(T)";
                }

                bool memoryNameNullOrWhiteSpace = string.IsNullOrWhiteSpace(MemoryName);
                if (!memoryNameNullOrWhiteSpace || !string.IsNullOrWhiteSpace(MemorySpeed) || !string.IsNullOrWhiteSpace(MemoryPowerVolt) || !string.IsNullOrWhiteSpace(MemoryTemperature) || !string.IsNullOrWhiteSpace(MemoryPercentage) || !string.IsNullOrWhiteSpace(MemoryBytes))
                {
                    string stringDisplay = string.Empty;
                    string stringStats = AVFunctions.StringRemoveStart(vTitleMEM + MemoryPercentage + MemoryTemperature + MemorySpeed + MemoryBytes + MemoryPowerVolt, " ");

                    if (string.IsNullOrWhiteSpace(stringStats))
                    {
                        stringDisplay = MemoryName;
                    }
                    else
                    {
                        if (memoryNameNullOrWhiteSpace)
                        {
                            stringDisplay = stringStats;
                        }
                        else
                        {
                            stringDisplay = MemoryName + "\n" + stringStats;
                        }
                    }

                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        textblock_CurrentMem.Text = stringDisplay;
                        stackpanel_CurrentMem.Visibility = Visibility.Visible;
                    });
                }
                else
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CurrentMem.Visibility = Visibility.Collapsed;
                    });
                }
            }
            catch { }
        }

        private string GetMemoryTemperatureString(IList<IHardware> hardwareItems)
        {
            string memoryTemperatureString = string.Empty;
            try
            {
                //Select hardware items
                IEnumerable<IHardware> hardwareItemDimm = hardwareItems.Where(x => x.HardwareType == HardwareType.Memory && x.Identifier.ToString().Contains("dimm"));
                foreach (IHardware hardwareItem in hardwareItemDimm)
                {
                    try
                    {
                        //Update hardware item
                        hardwareItem.Update();

                        foreach (ISensor sensor in hardwareItem.Sensors)
                        {
                            try
                            {
                                if (sensor.SensorType == SensorType.Temperature)
                                {
                                    //Debug.WriteLine("Memory Temp: " + sensor.Name + "/" + sensor.Identifier + "/" + sensor.Value.ToString());
                                    if (sensor.Name.StartsWith("DIMM"))
                                    {
                                        float RawMemTemperature = (float)sensor.Value;
                                        memoryTemperatureString += " " + RawMemTemperature.ToString("0") + "°";
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return memoryTemperatureString;
        }
    }
}