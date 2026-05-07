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
                List<IHardware> videocardItemList = vHardwareComputer.Hardware.Where(x => x.HardwareType == HardwareType.GpuAmd || x.HardwareType == HardwareType.GpuNvidia || x.HardwareType == HardwareType.GpuIntel).ToList();
                foreach (IHardware hardwareItem in videocardItemList)
                {
                    combobox_GpuIndex.Items.Add(hardwareItem.Name);
                }

                //List network items
                List<IHardware> networkItemList = vHardwareComputer.Hardware.Where(x => x.HardwareType == HardwareType.Network).OrderByDescending(x => x.Name.ToLower().Contains("ethernet")).ToList();
                foreach (IHardware hardwareItem in networkItemList)
                {
                    combobox_NetIndex.Items.Add(hardwareItem.Name);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to load setting values: " + ex.Message);
            }
        }
    }
}