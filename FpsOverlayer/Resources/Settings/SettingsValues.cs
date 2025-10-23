using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowSettings
    {
        //Load - Setting Values
        void Settings_Values()
        {
            try
            {
                //List videocard items
                List<IHardware> hardwareItemList = vHardwareComputer.Hardware.Where(x => x.HardwareType == HardwareType.GpuAmd || x.HardwareType == HardwareType.GpuNvidia || x.HardwareType == HardwareType.GpuIntel).ToList();
                foreach (IHardware hardwareItem in hardwareItemList)
                {
                    combobox_GpuIndex.Items.Add(hardwareItem.Name);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to load setting values: " + ex.Message);
            }
        }
    }
}