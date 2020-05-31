﻿/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2020 Matt McManis
https://github.com/MattMcManis/Axiom
https://axiomui.github.io
mattmcmanis@outlook.com

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see <http://www.gnu.org/licenses/>. 
---------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class MainWindow : Window
    {
        public static void ConfigDirectoryOpen(string path)
        {
            if (Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
            else
            {
                // Yes/No Dialog Confirmation
                //
                MessageBoxResult resultExport = MessageBox.Show("The Axiom UI directory does not exist in this location yet. Automatically create it?",
                                                                "Directory Not Found",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Information);
                switch (resultExport)
                {
                    // Create
                    case MessageBoxResult.Yes:
                        try
                        {
                            Directory.CreateDirectory(path);
                            Process.Start("explorer.exe", path);
                        }
                        catch
                        {
                            MessageBox.Show("Could not create directory. May require Administrator privileges.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                        break;
                    // Use Default
                    case MessageBoxResult.No:
                        break;
                }
            }
        }


        /// <summary>
        /// Config Open Directory - Label Button
        /// </summary>
        private void lblConfigPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Open Directory
            switch (VM.ConfigureView.ConfigPath_SelectedItem)
            {
                // AppData Local
                case "AppData Local":
                    ConfigDirectoryOpen(appDataLocalDir + @"Axiom UI\");
                    break;

                // AppData Roaming
                case "AppData Roaming":
                    ConfigDirectoryOpen(appDataRoamingDir + @"Axiom UI\");
                    break;

                // Documents
                case "Documents":
                    ConfigDirectoryOpen(documentsDir + @"Axiom UI\");
                    break;

                // App Root
                case "App Root":
                    Process.Start("explorer.exe", appRootDir);
                    break;
            }
        }

        /// <summary>
        /// Config Open Directory - Button
        /// </summary>
        private void btnConfigPath_Click(object sender, RoutedEventArgs e)
        {
            // Open Directory
            switch (VM.ConfigureView.ConfigPath_SelectedItem)
            {
                // AppData Local
                case "AppData Local":
                    ConfigDirectoryOpen(appDataLocalDir + @"Axiom UI\");
                    break;

                // AppData Roaming
                case "AppData Roaming":
                    ConfigDirectoryOpen(appDataRoamingDir + @"Axiom UI\");
                    break;

                // Documents
                case "Documents":
                    ConfigDirectoryOpen(documentsDir + @"Axiom UI\");
                    break;

                // App Root
                case "App Root":
                    Process.Start("explorer.exe", appRootDir);
                    break;
            }
        }


        /// <summary>
        /// Config Directory - ComboBox
        /// </summary>
        private void cboConfigPath_SelectionChangedSelectionChangeCommitted(object sender, SelectionChangedEventArgs e)
        {
            ////MessageBox.Show(e.RemovedItems.Count.ToString()); //debug
            //// 0 first startup
            //// 1 first bind
            //if (e.RemovedItems.Count != 1 && e.RemovedItems.Count != 0)
            //{
            //    string path = string.Empty;
            //    bool access = true;

            //    // AppData Local
            //    if (VM.ConfigureView.ConfigPath_SelectedItem == "AppData Local")
            //    {
            //        // Check Folder Write Access
            //        if (hasWriteAccessToFolder(appDataLocalDir) == false)
            //        {
            //            access = false;
            //        }
            //    }
            //    // AppData Roaming
            //    else if (VM.ConfigureView.ConfigPath_SelectedItem == "AppData Roaming")
            //    {
            //        // Check Folder Write Access
            //        if (hasWriteAccessToFolder(appDataRoamingDir) == false)
            //        {
            //            access = false;
            //        }
            //    }
            //    // Documents
            //    else if (VM.ConfigureView.ConfigPath_SelectedItem == "Documents")
            //    {
            //        // Check Folder Write Access
            //        if (hasWriteAccessToFolder(documentsDir) == false)
            //        {
            //            access = false;
            //        }
            //    }
            //    // App Root
            //    else if (VM.ConfigureView.ConfigPath_SelectedItem == "App Root")
            //    {
            //        // Check Folder Write Access
            //        if (appDir.Contains(programFilesDir) ||
            //            appDir.Contains(programFilesX86Dir) ||
            //            appDir.Contains(programFilesX64Dir) ||
            //            hasWriteAccessToFolder(appDir) == false
            //            )
            //        {
            //            access = false;
            //        }
            //    }

            //    // Display Warning if Axiom can't write to location
            //    if (access == false)
            //    {
            //        MessageBox.Show("Axiom does not have write access to this location.",
            //                        "Notice",
            //                        MessageBoxButton.OK,
            //                        MessageBoxImage.Warning);
            //    }
            //}
        }



        /// <summary>
        /// Presets Open Directory - Button
        /// </summary>
        private void lblCustomPresetsPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Directory.Exists(VM.ConfigureView.CustomPresetsPath_Text))
            {
                // Yes/No Dialog Confirmation
                //
                MessageBoxResult resultExport = MessageBox.Show("Presets folder does not yet exist. Automatically create it?",
                                                                "Directory Not Found",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Information);
                switch (resultExport)
                {
                    // Create
                    case MessageBoxResult.Yes:
                        try
                        {
                            Directory.CreateDirectory(VM.ConfigureView.CustomPresetsPath_Text);
                        }
                        catch
                        {
                            MessageBox.Show("Could not create Presets folder. May require Administrator privileges.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                        break;
                    // Use Default
                    case MessageBoxResult.No:
                        break;
                }
            }

            // Open Directory
            if (IsValidPath(VM.ConfigureView.CustomPresetsPath_Text))
            {
                if (Directory.Exists(VM.ConfigureView.CustomPresetsPath_Text))
                {
                    Process.Start("explorer.exe", VM.ConfigureView.CustomPresetsPath_Text);
                }
            }
        }

        /// <summary>
        /// Custom Presets Path - Textbox
        /// </summary>
        private void tbxCustomPresetsPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.CustomPresetsFolderBrowser();
        }

        // Drag and Drop
        private void tbxCustomPresetsPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxCustomPresetsPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];

            // If Path has file, extract Directory only
            if (Path.HasExtension(buffer.First()))
            {
                VM.ConfigureView.CustomPresetsPath_Text = Path.GetDirectoryName(buffer.First()).TrimEnd('\\') + @"\";
            }

            // Use Folder Path
            else
            {
                VM.ConfigureView.CustomPresetsPath_Text = buffer.First();
            }

            // -------------------------
            // Load Custom Presets
            // Refresh Presets ComboBox
            // -------------------------
            Profiles.Profiles.LoadCustomPresets();
        }

        /// <summary>
        /// CustomPresets Auto Path - Label Button
        /// </summary>
        private void btnCustomPresetsAuto_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Display Folder Path in Textbox
            // -------------------------
            switch (VM.ConfigureView.ConfigPath_SelectedItem)
            {
                // AppData Local
                case "AppData Local":
                    VM.ConfigureView.CustomPresetsPath_Text = appDataLocalDir + @"Axiom UI\presets\";
                    break;

                // AppData Roaming
                case "AppData Roaming":
                    VM.ConfigureView.CustomPresetsPath_Text = appDataRoamingDir + @"Axiom UI\presets\";
                    break;

                // Documents
                case "Documents":
                    VM.ConfigureView.CustomPresetsPath_Text = documentsDir + @"Axiom UI\presets\";
                    break;

                // App Root
                case "App Root":
                    if (appRootDir.Contains(programFilesDir) &&
                        appRootDir.Contains(programFilesX86Dir) &&
                        appRootDir.Contains(programFilesX64Dir)
                        )
                    {
                        // Change Program Files to AppData Local
                        VM.ConfigureView.CustomPresetsPath_Text = appDataLocalDir + @"Axiom UI\presets\";
                    }
                    else
                    {
                        VM.ConfigureView.CustomPresetsPath_Text = appRootDir + @"presets\";
                    }
                    break;
            }

            // -------------------------
            // Load Custom Presets
            // Refresh Presets ComboBox
            // -------------------------
            Profiles.Profiles.LoadCustomPresets();
        }


        /// <summary>
        /// FFmpeg Open Directory - Label Button
        /// </summary>
        private void lblFFmpegPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string ffmpegPath = string.Empty;

            // If Configure FFmpeg Path is <auto>
            if (VM.ConfigureView.FFmpegPath_Text == "<auto>")
            {
                // Included Binary
                if (File.Exists(appRootDir + @"ffmpeg\bin\ffmpeg.exe"))
                {
                    ffmpegPath = appRootDir + @"ffmpeg\bin\";
                }
                // Using Enviornment Variable
                else
                {
                    var envar = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

                    List<string> files = new List<string>();
                    string exePath = string.Empty;

                    // Get Environment Variable Paths
                    foreach (var path in envar.Split(';'))
                    {
                        if (IsValidPath(path))
                        {
                            if (Directory.Exists(path))
                            {
                                // Get all files in Path
                                files = Directory.GetFiles(path, "ffmpeg.exe")
                                                 .Select(Path.GetFullPath)
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .ToList();

                                // Find ffmpeg.exe in files list
                                if (files != null && files.Count > 0)
                                {
                                    foreach (string file in files)
                                    {
                                        if (file.Contains("ffmpeg.exe"))
                                        {
                                            exePath = file;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ffmpegPath = Path.GetDirectoryName(exePath).TrimEnd('\\') + @"\";
                    //MessageBox.Show(exePath); //debug
                }
            }

            // Use User Custom Path
            else
            {
                ffmpegPath = Path.GetDirectoryName(VM.ConfigureView.FFmpegPath_Text).TrimEnd('\\') + @"\";
            }


            // Open Directory
            if (IsValidPath(ffmpegPath))
            {
                if (Directory.Exists(ffmpegPath))
                {
                    Process.Start("explorer.exe", ffmpegPath);
                }
            }
        }

        /// <summary>
        /// FFmpeg Path - Textbox
        /// </summary>
        private void tbxFFmpegPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.FFmpegFolderBrowser();
        }

        // Drag and Drop
        private void tbxFFmpegPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxFFmpegPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            VM.ConfigureView.FFmpegPath_Text = buffer.First();
        }

        /// <summary>
        /// FFmpeg Auto Path - Button
        /// </summary>
        private void btnFFmpegAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            VM.ConfigureView.FFmpegPath_Text = "<auto>";
        }


        /// <summary>
        /// FFprobe Open Directory - Label Button
        /// </summary>
        private void lblFFprobePath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string ffprobePath = string.Empty;

            // If Configure FFprobe Path is <auto>
            if (VM.ConfigureView.FFprobePath_Text == "<auto>")
            {
                // Included Binary
                if (File.Exists(appRootDir + @"ffmpeg\bin\ffprobe.exe"))
                {
                    ffprobePath = appRootDir + @"ffmpeg\bin\";
                }
                // Using Enviornment Variable
                else
                {
                    var envar = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

                    List<string> files = new List<string>();
                    string exePath = string.Empty;

                    // Get Environment Variable Paths
                    foreach (var path in envar.Split(';'))
                    {
                        if (IsValidPath(path))
                        {
                            if (Directory.Exists(path))
                            {
                                // Get all files in Path
                                files = Directory.GetFiles(path, "ffprobe.exe")
                                                 .Select(Path.GetFullPath)
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .ToList();

                                // Find ffprobe.exe in files list
                                if (files != null && files.Count > 0)
                                {
                                    foreach (string file in files)
                                    {
                                        if (file.Contains("ffprobe.exe"))
                                        {
                                            exePath = file;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ffprobePath = Path.GetDirectoryName(exePath).TrimEnd('\\') + @"\";
                }
            }

            // Use User Custom Path
            else
            {
                ffprobePath = Path.GetDirectoryName(VM.ConfigureView.FFprobePath_Text).TrimEnd('\\') + @"\";
            }


            // Open Directory
            if (IsValidPath(ffprobePath))
            {
                if (Directory.Exists(ffprobePath))
                {
                    Process.Start("explorer.exe", ffprobePath);
                }
            }
        }

        /// <summary>
        /// FFprobe Path - Textbox
        /// </summary>
        private void tbxFFprobePath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.FFprobeFolderBrowser();
        }

        // Drag and Drop
        private void tbxFFprobePath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxFFprobePath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            VM.ConfigureView.FFprobePath_Text = buffer.First();
        }

        /// <summary>
        /// FFprobe Auto Path - Button
        /// </summary>
        private void btnFFprobeAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            VM.ConfigureView.FFprobePath_Text = "<auto>";
        }


        /// <summary>
        /// FFplay Open Directory - Label Button
        /// </summary>
        private void lblFFplayPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string ffplayPath = string.Empty;

            // If Configure FFplay Path is <auto>
            if (VM.ConfigureView.FFplayPath_Text == "<auto>")
            {
                // Included Binary
                if (File.Exists(appRootDir + @"ffmpeg\bin\ffplay.exe"))
                {
                    ffplayPath = appRootDir + @"ffmpeg\bin\";
                }
                // Using Enviornment Variable
                else
                {
                    var envar = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

                    List<string> files = new List<string>();
                    string exePath = string.Empty;

                    // Get Environment Variable Paths
                    foreach (var path in envar.Split(';'))
                    {
                        if (IsValidPath(path))
                        {
                            if (Directory.Exists(path))
                            {
                                // Get all files in Path
                                files = Directory.GetFiles(path, "ffplay.exe")
                                                 .Select(Path.GetFullPath)
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .ToList();

                                // Find ffplay.exe in files list
                                if (files != null && files.Count > 0)
                                {
                                    foreach (string file in files)
                                    {
                                        if (file.Contains("ffplay.exe"))
                                        {
                                            exePath = file;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ffplayPath = Path.GetDirectoryName(exePath).TrimEnd('\\') + @"\";
                }
            }

            // Use User Custom Path
            else
            {
                ffplayPath = Path.GetDirectoryName(VM.ConfigureView.FFplayPath_Text).TrimEnd('\\') + @"\";
            }


            // Open Directory
            if (IsValidPath(ffplayPath))
            {
                if (Directory.Exists(ffplayPath))
                {
                    Process.Start("explorer.exe", ffplayPath);
                }
            }
        }

        /// <summary>
        /// FFplay Path - Textbox
        /// </summary>
        private void tbxFFplayPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.FFplayFolderBrowser();
        }

        // Drag and Drop
        private void tbxFFplayPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxFFplayPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            VM.ConfigureView.FFplayPath_Text = buffer.First();
        }

        /// <summary>
        /// FFplay Auto Path - Button
        /// </summary>
        private void btnFFplayAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            VM.ConfigureView.FFplayPath_Text = "<auto>";
        }


        /// <summary>
        /// youtube-dl Open Directory - Label Button
        /// </summary>
        private void lblyoutubedlPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string youtubedlPath = string.Empty;

            // If Configure youtube-dl Path is <auto>
            if (VM.ConfigureView.youtubedlPath_Text == "<auto>")
            {
                // Included Binary
                if (File.Exists(appRootDir + @"youtube-dl\youtube-dl.exe"))
                {
                    youtubedlPath = appRootDir + @"youtube-dl\";
                }
                // Using Enviornment Variable
                else
                {
                    var envar = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

                    List<string> files = new List<string>();
                    string exePath = string.Empty;

                    // Get Environment Variable Paths
                    foreach (var path in envar.Split(';'))
                    {
                        if (IsValidPath(path))
                        {
                            if (Directory.Exists(path))
                            {
                                // Get all files in Path
                                files = Directory.GetFiles(path, "youtube-dl.exe")
                                                 .Select(Path.GetFullPath)
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .ToList();

                                // Find youtube-dl.exe in files list
                                if (files != null && files.Count > 0)
                                {
                                    foreach (string file in files)
                                    {
                                        if (file.Contains("youtube-dl.exe"))
                                        {
                                            exePath = file;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    youtubedlPath = Path.GetDirectoryName(exePath).TrimEnd('\\') + @"\";
                }
            }

            // Use User Custom Path
            else
            {
                youtubedlPath = Path.GetDirectoryName(VM.ConfigureView.youtubedlPath_Text).TrimEnd('\\') + @"\";
            }


            // Open Directory
            if (IsValidPath(youtubedlPath))
            {
                if (Directory.Exists(youtubedlPath))
                {
                    Process.Start("explorer.exe", youtubedlPath);
                }
            }
        }

        /// <summary>
        /// youtubedl Path - Textbox
        /// </summary>
        private void tbxyoutubedlPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.youtubedlFolderBrowser();
        }

        // Drag and Drop
        private void tbxyoutubedlPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxyoutubedlPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            VM.ConfigureView.youtubedlPath_Text = buffer.First();
        }

        /// <summary>
        /// youtubedl Auto Path - Button
        /// </summary>
        private void btnyoutubedlAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            VM.ConfigureView.youtubedlPath_Text = "<auto>";
        }


        /// <summary>
        /// Log Open Directory - Label Button
        /// </summary>
        private void lblLogPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsValidPath(VM.ConfigureView.LogPath_Text))
            {
                if (Directory.Exists(VM.ConfigureView.LogPath_Text))
                {
                    Process.Start("explorer.exe", VM.ConfigureView.LogPath_Text);
                }
            }
        }

        // Drag and Drop
        private void tbxLogPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxLogPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];

            // If Path has file, extract Directory only
            if (Path.HasExtension(buffer.First()))
            {
                VM.ConfigureView.LogPath_Text = Path.GetDirectoryName(buffer.First()).TrimEnd('\\') + @"\";
            }

            // Use Folder Path
            else
            {
                VM.ConfigureView.LogPath_Text = buffer.First();
            }
        }

        /// <summary>
        /// Log Checkbox - Checked
        /// </summary>
        private void cbxLog_Checked(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.LogPath_IsEnabled = true;
        }

        /// <summary>
        /// Log Checkbox - Unchecked
        /// </summary>
        private void cbxLog_Unchecked(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.LogPath_IsEnabled = false;
        }

        /// <summary>
        /// Log Path - Textbox
        /// </summary>
        private void tbxLogPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.LogFolderBrowser();
        }

        /// <summary>
        /// Log Auto Path - Button
        /// </summary>
        private void btnLogPathAuto_Click(object sender, RoutedEventArgs e)
        {
            // Uncheck Log Checkbox
            VM.ConfigureView.LogCheckBox_IsChecked = false;

            // Clear Path in Textbox
            VM.ConfigureView.LogPath_Text = Log.logDir;
        }


        /// <summary>
        /// Threads - ComboBox
        /// </summary>
        private void threadSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Custom ComboBox Editable
            //if (VM.ConfigureView.Threads_SelectedItem == "Custom" || cboThreads.SelectedValue == null)
            //{
            //    cboThreads.IsEditable = true;
            //}

            // Other Items Disable Editable
            //if (VM.ConfigureView.Threads_SelectedItem != "Custom" && cboThreads.SelectedValue != null)
            //{
            //    cboThreads.IsEditable = false;
            //}

            // Maintain Editable Combobox while typing
            //if (cboThreads.IsEditable == true)
            //{
            //    cboThreads.IsEditable = true;

            //    // Clear Custom Text
            //    cboThreads.SelectedIndex = -1;
            //}

            // Set the threads to pass to MainWindow
            Controls.Configure.threads = VM.ConfigureView.Threads_SelectedItem;
        }

        // Key Down
        private void threadSelect_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            Allow_Only_Number_Keys(e);
        }

        /// <summary>
        /// Theme Select - ComboBox
        /// </summary>
        private void themeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Controls.Configure.theme = VM.ConfigureView.Theme_SelectedItem;

            // Change Theme Resource
            App.Current.Resources.MergedDictionaries.Clear();
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("Themes/" + "Theme" + Controls.Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
            });
        }


        /// <summary>
        /// Updates Auto Check - Checked
        /// </summary>
        private void tglUpdateAutoCheck_Checked(object sender, RoutedEventArgs e)
        {
            // Update Toggle Text
            VM.ConfigureView.UpdateAutoCheck_Text = "On";
        }
        /// <summary>
        /// Updates Auto Check - Unchecked
        /// </summary>
        private void tglUpdateAutoCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            // Update Toggle Text
            VM.ConfigureView.UpdateAutoCheck_Text = "Off";
        }


        // --------------------------------------------------
        // Reset Saved Settings - Button
        // --------------------------------------------------
        private void btnResetConfig_Click(object sender, RoutedEventArgs e)
        {
            // Check if Directory Exists
            if (File.Exists(Controls.Configure.configFile))
            {
                // Show Yes No Window
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
                                                                "Delete " + Controls.Configure.configFile,
                                                                "Delete Directory Confirm",
                                                                System.Windows.Forms.MessageBoxButtons.YesNo);

                // Yes
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        if (File.Exists(Controls.Configure.configFile))
                        {
                            File.Delete(Controls.Configure.configFile);
                        }
                    }
                    catch
                    {

                    }

                    // Load Defaults
                    VM.ConfigureView.LoadConfigDefaults();
                    VM.MainView.LoadControlsDefaults();
                    VM.FormatView.LoadControlsDefaults();
                    VM.VideoView.LoadControlsDefaults();
                    VM.SubtitleView.LoadControlsDefaults();
                    VM.AudioView.LoadControlsDefaults();

                    // Restart Program
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
                // No
                else if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    //do nothing
                }
            }

            // If Axiom Folder Not Found
            else
            {
                MessageBox.Show("Config file " + Controls.Configure.configFile + " not Found.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }


        /// <summary>
        /// Thread Detect
        /// </summary>
        public static String ThreadDetect()
        {
            // Default / Off
            if (VM.ConfigureView.Threads_SelectedItem == "Default")
            {
                // Optimal
                Controls.Configure.threads = "";

                return Controls.Configure.threads;
            }

            // Fallback if maxthreads was not detected in SystemInfo()
            if (string.IsNullOrWhiteSpace(Controls.Configure.threads))
            {
                // Optimal
                Controls.Configure.threads = "-threads 0";

                return Controls.Configure.threads;
            }

            // Options
            switch (VM.ConfigureView.Threads_SelectedItem)
            {
                // Empty
                case "":
                    Controls.Configure.threads = string.Empty;
                    break;

                // Default / Off
                //case "Default":
                //    Configure.threads = string.Empty;
                //    break;

                // Optimal
                case "Optimal":
                    Controls.Configure.threads = "-threads 0";
                    break;

                // All
                case "All":
                    // e.g. -threads 8
                    Controls.Configure.threads = "-threads " + Controls.Configure.maxthreads;
                    break;

                // Selected Number
                default:
                    // e.g. -threads 5
                    Controls.Configure.threads = "-threads " + VM.ConfigureView.Threads_SelectedItem;
                    break;
            }

            return Controls.Configure.threads;
        }

    }
}