﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NewTicketWPF
{
    /// <summary>
    /// Interaction logic for RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow : Window
    {
        List<Profile> pf;
        MainWindow mainWindow;
        Profile selectedProfile;
        public RenameWindow()
        {
            InitializeComponent();
            pf = pf.LoadAllProfile();
            FillProfileBox();
            if (ProfileBox != null)
            {
                ProfileBox.SelectedIndex = 0;
            }
        }
        public MainWindow SetCreatingForm
        {
            set
            {
                mainWindow = value;
            }
        }
        public MainWindow GetCreatingForm
        {
            get
            {
                return mainWindow;
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            mainWindow.IsEnabled = true;
            base.OnClosed(e);
        }
        public void FillProfileBox()
        {
            foreach (var p in pf)
            {
                ProfileBox.Items.Add(p);
            }
        }
        private void ProfileRename(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pf.Count() > 0 && RenameField.Text != "")
                {
                    selectedProfile = pf[ProfileBox.SelectedIndex];
                    selectedProfile.Delete(selectedProfile.ProfileName);
                    selectedProfile.ProfileName = RenameField.Text;
                    selectedProfile.SaveProfile();
                    selectedProfile.SavePData(ProfileBox.SelectedIndex);
                    RenameField.Text = "";
                }
                else if (RenameField.Text == "")
                {
                    MessageBox.Show("Give a name for the selected profile before renaming it!");
                }
                else if (pf.Count() == 0 || ProfileBox.SelectedIndex < 0)
                {
                    MessageBox.Show("There is no profile selected, please select a profile before renaming it.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
