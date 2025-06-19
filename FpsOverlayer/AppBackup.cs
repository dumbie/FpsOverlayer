using ArnoldVinkCode;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace FpsOverlayer
{
    public partial class AppBackup
    {
        //Backup Profiles and Notes
        public static void BackupJsonProfiles()
        {
            try
            {
                Debug.WriteLine("Creating notes backup.");

                //Create backup directory
                AVFiles.Directory_Create("Backups", false);

                //Cleanup backups
                FileInfo[] fileInfo = new DirectoryInfo("Backups").GetFiles("*.zip");
                foreach (FileInfo backupFile in fileInfo)
                {
                    try
                    {
                        TimeSpan backupSpan = DateTime.Now - backupFile.CreationTime;
                        if (backupSpan.TotalDays > 5)
                        {
                            backupFile.Delete();
                        }
                    }
                    catch { }
                }

                //Create backups
                string backupTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                ZipFile.CreateFromDirectory("Notes", "Backups\\" + backupTime + "-Notes.zip");
                ZipFile.CreateFromDirectory("Profiles\\User", "Backups\\" + backupTime + "-Profiles.zip");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed making backup: " + ex.Message);
            }
        }
    }
}