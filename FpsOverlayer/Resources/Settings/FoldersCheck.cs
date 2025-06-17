using ArnoldVinkCode;
using System;
using System.Diagnostics;

namespace FpsOverlayer
{
    public partial class WindowSettings
    {
        //Check application user folders
        public void Folders_Check()
        {
            try
            {
                AVFiles.Directory_Create(@"Assets\User\Fonts", false);

                Debug.WriteLine("Checked application user folders.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed checking application user folders: " + ex.Message);
            }
        }
    }
}