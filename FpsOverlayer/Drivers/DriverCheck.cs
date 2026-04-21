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
                using (RegistryKey regKeyLocalMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (RegistryKey regKeyPawnIO = regKeyLocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\PawnIO"))
                    {
                        //Get version string
                        string installed_version = regKeyPawnIO?.GetValue("DisplayVersion")?.ToString();

                        //Check version value
                        if (!string.IsNullOrWhiteSpace(installed_version))
                        {
                            //Get path to driver setup
                            string setup_path = Path.Combine(AVFunctions.ApplicationPathRoot(), "Drivers\\PawnIO_setup.exe");

                            //Get current driver information
                            FileVersionInfo setup_information = FileVersionInfo.GetVersionInfo(setup_path);

                            //Compare driver version
                            if (setup_information.FileVersion != installed_version)
                            {
                                Debug.WriteLine("PawnIO drivers are not up-to-date.");
                                Launch_ApplicationDesktop(setup_path, string.Empty, "-install -silent", true, true);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("PawnIO drivers are not installed.");
                            string setup_path = Path.Combine(AVFunctions.ApplicationPathRoot(), "Drivers\\PawnIO_setup.exe");
                            Launch_ApplicationDesktop(setup_path, string.Empty, "-install -silent", true, true);
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