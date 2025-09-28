using System.Windows;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowTools : Window
    {
        //Switch browser visibility
        private void button_ShowHide_Browser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (border_Browser.Visibility == Visibility.Visible)
                {
                    //Reset browser interface
                    Browser_Reset_Interface(string.Empty, false);

                    //Switch visibility
                    border_Browser.Visibility = Visibility.Collapsed;

                    //Update setting
                    vSettings.Set("ToolsShowBrowser", "False");
                }
                else
                {
                    //Switch visibility
                    border_Browser.Visibility = Visibility.Visible;

                    //Update setting
                    vSettings.Set("ToolsShowBrowser", "True");
                }
            }
            catch { }
        }

        //Switch notes visibility
        private void button_ShowHide_Notes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (border_Notes.Visibility == Visibility.Visible)
                {
                    //Switch visibility
                    border_Notes.Visibility = Visibility.Collapsed;

                    //Update setting
                    vSettings.Set("ToolsShowNotes", "False");
                }
                else
                {
                    //Switch visibility
                    border_Notes.Visibility = Visibility.Visible;

                    //Update setting
                    vSettings.Set("ToolsShowNotes", "True");
                }
            }
            catch { }
        }
    }
}