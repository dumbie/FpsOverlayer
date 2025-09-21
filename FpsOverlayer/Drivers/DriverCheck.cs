using ArnoldVinkCode;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using static ArnoldVinkCode.AVProcess;

namespace FpsOverlayer
{
    public partial class DriverCheck
    {
        //Check if drivers are installed
        public static void CheckDrivers()
        {
            try
            {
                Debug.WriteLine("Checking drivers.");

                //PawnIO drivers
                using (RegistryKey regKeyLocalMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                {
                    using (RegistryKey regKeyPawnIO = regKeyLocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\PawnIO"))
                    {
                        if (regKeyPawnIO == null)
                        {
                            Debug.WriteLine("PawnIO drivers are not installed.");
                            string setup_path = Path.Combine(AVFunctions.ApplicationPathRoot(), "Drivers\\PawnIO_setup.exe");
                            Launch_ShellExecute(setup_path, string.Empty, "-install -silent", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed checking drivers: " + ex.Message);
            }
        }
    }
}