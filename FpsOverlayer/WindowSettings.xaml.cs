using ArnoldVinkStyles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using static ArnoldVinkCode.AVClasses;
using static ArnoldVinkCode.AVFunctions;
using static ArnoldVinkCode.AVJsonFunctions;
using static FpsOverlayer.AppVariables;

namespace FpsOverlayer
{
    public partial class WindowSettings : Window
    {
        //Window Initialize
        public WindowSettings() { InitializeComponent(); }

        //Window Initialized
        protected override async void OnSourceInitialized(EventArgs e)
        {
            try
            {
                //Load available fonts
                LoadAvailableFonts();

                //Load and save settings
                Settings_Values();
                await Settings_Load();
                Settings_Save();

                //Load and save shortcuts
                Shortcuts_Load();
                Shortcuts_Save();

                //Bind the lists to the listbox elements
                ListBoxBindLists();

                //Register Interface Handlers
                RegisterInterfaceHandlers();
            }
            catch { }
        }

        //Load available fonts
        void LoadAvailableFonts()
        {
            try
            {
                //Clear all the fonts
                combobox_InterfaceFontStyleName.Items.Clear();

                //Add default fonts
                combobox_InterfaceFontStyleName.Items.Add("Segoe UI");
                combobox_InterfaceFontStyleName.Items.Add("Verdana");
                combobox_InterfaceFontStyleName.Items.Add("Consolas");
                combobox_InterfaceFontStyleName.Items.Add("Arial");

                //Add custom fonts
                DirectoryInfo directoryInfoUser = new DirectoryInfo("Assets/User/Fonts");
                FileInfo[] fontFilesUser = directoryInfoUser.GetFiles("*.ttf", SearchOption.TopDirectoryOnly);
                DirectoryInfo directoryInfoDefault = new DirectoryInfo("Assets/Default/Fonts");
                FileInfo[] fontFilesDefault = directoryInfoDefault.GetFiles("*.ttf", SearchOption.TopDirectoryOnly);
                IEnumerable<FileInfo> fontFiles = fontFilesUser.Concat(fontFilesDefault);

                foreach (FileInfo fontFile in fontFiles)
                {
                    combobox_InterfaceFontStyleName.Items.Add(Path.GetFileNameWithoutExtension(fontFile.Name));
                }
            }
            catch { }
        }

        //Register Interface Handlers
        void RegisterInterfaceHandlers()
        {
            try
            {
                //Main menu functions
                lb_Menu.PreviewKeyUp += lb_Menu_KeyPressUp;
                lb_Menu.PreviewMouseUp += lb_Menu_MousePressUp;

                button_AddApp.Click += Button_AddApp_Click;
                textbox_AddApp.PreviewKeyDown += Textbox_AddApp_PreviewKeyDown;

                button_ToolsShowHide.Click += button_ToolsShowHide_Click;
                button_CrosshairShowHide.Click += Button_CrosshairShowHide_Click;
            }
            catch { }
        }

        void Button_CrosshairShowHide_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vWindowCrosshair.SwitchCrosshairVisibility(true);
            }
            catch { }
        }

        void button_ToolsShowHide_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vWindowTools.SwitchToolsVisibility();
            }
            catch { }
        }

        //Application Close Handler
        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                this.Hide();
            }
            catch { }
        }
    }
}