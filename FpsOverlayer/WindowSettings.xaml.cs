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

                button_Browser_Link_Add.Click += button_Browser_Link_Add_Click;
                button_Browser_Link_Remove.Click += button_Browser_Link_Remove_Click;
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

        //Add link to list
        void button_Browser_Link_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string textString = textbox_Browser_LinkString.Text;
                string placeholderString = (string)textbox_Browser_LinkString.GetValue(TextboxPlaceholder.PlaceholderProperty);
                Debug.WriteLine("Adding new link: " + textString);

                //Color brushes
                BrushConverter BrushConvert = new BrushConverter();
                Brush BrushInvalid = BrushConvert.ConvertFromString("#CD1A2B") as Brush;
                Brush BrushValid = BrushConvert.ConvertFromString("#1DB954") as Brush;

                //Check if the text is empty
                if (string.IsNullOrWhiteSpace(textString))
                {
                    textbox_Browser_LinkString.BorderBrush = BrushInvalid;
                    Debug.WriteLine("Please enter a link.");
                    return;
                }

                //Check if the text is place holder
                if (textString == placeholderString)
                {
                    textbox_Browser_LinkString.BorderBrush = BrushInvalid;
                    Debug.WriteLine("Please enter a link.");
                    return;
                }

                //Check if string is valid link
                textString = StringLinkCleanup(textString);
                if (!StringLinkValidate(textString))
                {
                    textbox_Browser_LinkString.BorderBrush = BrushInvalid;
                    Debug.WriteLine("Please enter proper link.");
                    return;
                }

                //Check if text already exists
                if (vFpsBrowserLinks.Any(x => x.String1.ToLower() == textString.ToLower()))
                {
                    textbox_Browser_LinkString.BorderBrush = BrushInvalid;
                    Debug.WriteLine("Link already exists.");
                    return;
                }

                //Clear text from the textbox
                textbox_Browser_LinkString.Text = placeholderString;
                textbox_Browser_LinkString.BorderBrush = BrushValid;

                //Add text string to the list
                ProfileShared profileShared = new ProfileShared();
                profileShared.String1 = textString;

                vFpsBrowserLinks.Add(profileShared);
                JsonSaveObject(vFpsBrowserLinks, @"Profiles\User\FpsBrowserLinks.json");
            }
            catch { }
        }

        //Remove link from list
        void button_Browser_Link_Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProfileShared selectedProfile = (ProfileShared)combobox_LinkString.SelectedItem;
                Debug.WriteLine("Removing link: " + selectedProfile.String1);

                //Remove mapping from list
                vFpsBrowserLinks.Remove(selectedProfile);

                //Save changes to Json file
                JsonSaveObject(vFpsBrowserLinks, @"Profiles\User\FpsBrowserLinks.json");

                //Select the default profile
                combobox_LinkString.SelectedIndex = 0;
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