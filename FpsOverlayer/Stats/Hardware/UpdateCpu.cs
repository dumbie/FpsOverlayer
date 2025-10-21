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
        void UpdateCpuInformation(IList<IHardware> hardwareItems)
        {
            try
            {
                //Check if the information is visible
                bool CpuShowName = vSettings.Load("CpuShowName", typeof(bool));
                bool BoardShowName = vSettings.Load("BoardShowName", typeof(bool));
                bool CpuShowPercentage = vSettings.Load("CpuShowPercentage", typeof(bool));
                bool CpuShowTemperature = vSettings.Load("CpuShowTemperature", typeof(bool));
                bool CpuShowCoreFrequency = vSettings.Load("CpuShowCoreFrequency", typeof(bool));
                bool CpuShowPowerWatt = vSettings.Load("CpuShowPowerWatt", typeof(bool));
                bool CpuShowPowerVolt = vSettings.Load("CpuShowPowerVolt", typeof(bool));
                bool CpuShowFanSpeed = vSettings.Load("CpuShowFanSpeed", typeof(bool));
                if (!CpuShowName && !BoardShowName && !CpuShowPercentage && !CpuShowTemperature && !CpuShowCoreFrequency && !CpuShowPowerWatt && !CpuShowPowerVolt && !CpuShowFanSpeed)
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CurrentCpu.Visibility = Visibility.Collapsed;
                    });
                    return;
                }

                //Select hardware item
                IHardware hardwareItem = hardwareItems.FirstOrDefault(x => x.HardwareType == HardwareType.Cpu);

                //Update hardware item
                hardwareItem.Update();

                string CpuName = string.Empty;
                string BoardName = string.Empty;
                string CpuPercentage = string.Empty;
                string CpuTemperature = string.Empty;
                string CpuFrequency = string.Empty;
                string CpuPowerWattage = string.Empty;
                string CpuPowerVoltage = string.Empty;
                string CpuFanSpeed = string.Empty;

                //Set the processor name
                if (CpuShowName)
                {
                    CpuName = hardwareItem.Name;
                }

                //Set the motherboard name
                if (BoardShowName)
                {
                    BoardName = vHardwareMotherboardName;
                }

                //Set the cpu fan speed
                if (CpuShowFanSpeed)
                {
                    CpuFanSpeed = " " + vHardwareCpuFanSpeed;
                }

                foreach (ISensor sensor in hardwareItem.Sensors)
                {
                    try
                    {
                        if (CpuShowPercentage && sensor.SensorType == SensorType.Load)
                        {
                            //Debug.WriteLine("CPU Load: " + sensor.Name + "/" + sensor.Identifier + "/" + sensor.Value.ToString());
                            if (sensor.Name == "CPU Total" || sensor.Name == "CPU Core")
                            {
                                CpuPercentage = " " + Convert.ToInt32(sensor.Value) + "%";
                            }
                        }
                        else if (CpuShowTemperature && sensor.SensorType == SensorType.Temperature)
                        {
                            //Debug.WriteLine("CPU Temp: " + sensor.Name + "/" + sensor.Identifier + "/" + sensor.Value.ToString());
                            if (sensor.Name == "CPU Package" || sensor.Name == "CPU Cores" || sensor.Name == "Core (Tctl/Tdie)")
                            {
                                float RawCpuTemperature = (float)sensor.Value;
                                CpuTemperature = " " + RawCpuTemperature.ToString("0") + "°";
                            }
                        }
                        else if (CpuShowCoreFrequency && sensor.SensorType == SensorType.Clock)
                        {
                            //Debug.WriteLine("CPU Frequency: " + sensor.Name + "/" + sensor.Identifier + "/" + sensor.Value.ToString());
                            if (sensor.Name == "CPU Core #1" || sensor.Name == "Cores (Average)")
                            {
                                float RawCpuFrequency = (float)sensor.Value;
                                if (RawCpuFrequency < 1000)
                                {
                                    CpuFrequency = " " + RawCpuFrequency.ToString("0") + "MHz";
                                }
                                else
                                {
                                    CpuFrequency = " " + (RawCpuFrequency / 1000).ToString("0.00") + "GHz";
                                }
                            }
                        }
                        else if (CpuShowPowerWatt && sensor.SensorType == SensorType.Power)
                        {
                            //Debug.WriteLine("CPU Wattage: " + sensor.Name + "/" + sensor.Identifier + "/" + sensor.Value.ToString());
                            if (sensor.Identifier.ToString().EndsWith("power/0"))
                            {
                                CpuPowerWattage = " " + Convert.ToInt32(sensor.Value) + "W";
                            }
                        }
                        else if (CpuShowPowerVolt && sensor.SensorType == SensorType.Voltage)
                        {
                            //Debug.WriteLine("CPU Voltage: " + sensor.Name + "/" + sensor.Identifier + "/" + sensor.Value.ToString());
                            float RawPowerVoltage = (float)sensor.Value;
                            if (RawPowerVoltage <= 0)
                            {
                                CpuPowerVoltage = " 0V";
                            }
                            else
                            {
                                CpuPowerVoltage = " " + RawPowerVoltage.ToString("0.000") + "V";
                            }
                        }
                    }
                    catch { }
                }

                bool cpuNameNullOrWhiteSpace = string.IsNullOrWhiteSpace(CpuName);
                bool boardNameNullOrWhiteSpace = string.IsNullOrWhiteSpace(BoardName);
                if (!cpuNameNullOrWhiteSpace || !boardNameNullOrWhiteSpace || !string.IsNullOrWhiteSpace(CpuPercentage) || !string.IsNullOrWhiteSpace(CpuTemperature) || !string.IsNullOrWhiteSpace(CpuFrequency) || !string.IsNullOrWhiteSpace(CpuPowerWattage) || !string.IsNullOrWhiteSpace(CpuPowerVoltage) || !string.IsNullOrWhiteSpace(CpuFanSpeed))
                {
                    string stringDisplay = string.Empty;
                    string stringStats = AVFunctions.StringRemoveStart(vTitleCPU + CpuPercentage + CpuTemperature + CpuFrequency + CpuFanSpeed + CpuPowerWattage + CpuPowerVoltage, " ");
                    if (string.IsNullOrWhiteSpace(stringStats))
                    {
                        if (!cpuNameNullOrWhiteSpace && !boardNameNullOrWhiteSpace)
                        {
                            stringDisplay = CpuName + "\n" + BoardName;
                        }
                        else if (!cpuNameNullOrWhiteSpace)
                        {
                            stringDisplay = CpuName;
                        }
                        else if (!boardNameNullOrWhiteSpace)
                        {
                            stringDisplay = BoardName;
                        }
                    }
                    else
                    {
                        if (cpuNameNullOrWhiteSpace && boardNameNullOrWhiteSpace)
                        {
                            stringDisplay = stringStats;
                        }
                        else
                        {
                            if (!cpuNameNullOrWhiteSpace && !boardNameNullOrWhiteSpace)
                            {
                                stringDisplay = CpuName + "\n" + BoardName + "\n" + stringStats;
                            }
                            else if (!cpuNameNullOrWhiteSpace)
                            {
                                stringDisplay = CpuName + "\n" + stringStats;
                            }
                            else if (!boardNameNullOrWhiteSpace)
                            {
                                stringDisplay = BoardName + "\n" + stringStats;
                            }
                        }
                    }

                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        textblock_CurrentCpu.Text = stringDisplay;
                        stackpanel_CurrentCpu.Visibility = Visibility.Visible;
                    });
                }
                else
                {
                    AVDispatcherInvoke.DispatcherInvoke(delegate
                    {
                        stackpanel_CurrentCpu.Visibility = Visibility.Collapsed;
                    });
                }
            }
            catch { }
        }
    }
}