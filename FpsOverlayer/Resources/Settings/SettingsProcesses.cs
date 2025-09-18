﻿using ArnoldVinkStyles;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static ArnoldVinkCode.AVClasses;
using static ArnoldVinkCode.AVJsonFunctions;

namespace FpsOverlayer
{
    public partial class WindowSettings
    {
        //Add application to the list
        void Textbox_AddApp_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    AddPositionProcesses();
                }
            }
            catch { }
        }

        //Add application to the list
        void Button_AddApp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddPositionProcesses();
            }
            catch { }
        }

        //Add position processes
        void AddPositionProcesses()
        {
            try
            {
                string processNameString = textbox_AddApp.Text;
                string placeholderString = (string)textbox_AddApp.GetValue(TextboxPlaceholder.PlaceholderProperty);
                Debug.WriteLine("Adding new process: " + processNameString);

                //Color brushes
                BrushConverter BrushConvert = new BrushConverter();
                Brush BrushInvalid = BrushConvert.ConvertFromString("#CD1A2B") as Brush;
                Brush BrushValid = BrushConvert.ConvertFromString("#1DB954") as Brush;

                //Check if the name is empty
                if (string.IsNullOrWhiteSpace(processNameString))
                {
                    textbox_AddApp.BorderBrush = BrushInvalid;
                    Debug.WriteLine("Please enter an application process.");
                    return;
                }

                //Check if the name is place holder
                if (processNameString == placeholderString)
                {
                    textbox_AddApp.BorderBrush = BrushInvalid;
                    Debug.WriteLine("Please enter an application process.");
                    return;
                }

                //Check if process already exists
                if (AppVariables.vFpsPositionProcessName.Any(x => x.String1.ToLower() == processNameString.ToLower()))
                {
                    textbox_AddApp.BorderBrush = BrushInvalid;
                    Debug.WriteLine("Application process already exists.");
                    return;
                }

                //Clear name from the textbox
                textbox_AddApp.Text = placeholderString;

                ProfileShared FpsPositionProcessName = new ProfileShared();
                FpsPositionProcessName.String1 = processNameString;
                FpsPositionProcessName.Int1 = 0;

                AppVariables.vFpsPositionProcessName.Add(FpsPositionProcessName);
                JsonSaveObject(AppVariables.vFpsPositionProcessName, @"Profiles\User\FpsPositionProcessName.json");

                textbox_AddApp.BorderBrush = BrushValid;
            }
            catch { }
        }

        //Bind the lists to the listbox elements
        void ListBoxBindLists()
        {
            try
            {
                combobox_LinkString.ItemsSource = AppVariables.vFpsBrowserLinks;
                combobox_LinkString.DisplayMemberPath = "String1";
                combobox_LinkString.SelectedIndex = 0;

                listbox_Apps.ItemsSource = AppVariables.vFpsPositionProcessName;

                Debug.WriteLine("Lists bound to interface.");
            }
            catch { }
        }
    }
}