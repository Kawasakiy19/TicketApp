using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewTicketWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Settings objSettings;
        CreateProfileWindow objNewProfileWindow;
        Profile currentProfile;
        PData pData;
        RenameWindow objRenameWindow;
        SaveFileDialogSample objSaveFileDialogSample;
        public MainWindow()
         {
            InitializeComponent();
            pData = pData.LoadPData();
            currentProfile = currentProfile.LoadSelectedProfile(pData);
            TList.ItemsSource = currentProfile.SetProfile.GetTickets;
        }

        public Profile SetCurrentProfile { set { currentProfile = value; } }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            objSettings = new Settings(this);
            objSettings.SetCreatingForm = this;
            IsEnabled = false;
            objSettings.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (objSettings != null)
            {
                objSettings.Close();
            }
            if (objNewProfileWindow != null)
            {
                objNewProfileWindow.Close();
            }
            Close();
        }

        private void Sell_ListClick(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is Ticket)
            {
                Ticket sell = (Ticket)cmd.DataContext;
                if(sell.Sold == false)
                { 
                    sell.Sold = true;
                    var element = currentProfile.SetProfile.tickets.FindIndex(ch => ch.ID == sell.ID);
                    currentProfile.SetProfile.tickets[element] = sell;
                    TList.Items.Refresh();
                    currentProfile.SaveProfile();
                }
                else if(sell.Sold == true)
                {
                    Log.Text += DateTime.Now.Hour + ":" + DateTime.Now.Minute + " - The ticket \"" + sell.IDS + "\" is already sold!\n";
                }
            }
        }

        public void changeContentOfPData(PData pData)
        {
            this.pData = pData;
            currentProfile = currentProfile.LoadSelectedProfile(pData);
            TList.Items.Refresh();
        }

        public void NewProfileClick(object sender, RoutedEventArgs e)
        {
            objNewProfileWindow = new CreateProfileWindow();
            objNewProfileWindow.SetCreatingForm = this;
            IsEnabled = false;
            objNewProfileWindow.Show();
        }

        public void RegenerateTicketClick(object sender, RoutedEventArgs e)
        {

        }

        public void NewProfileChangesSet(Profile profile)
        {
            currentProfile = profile;
            TList.ItemsSource = profile.SetProfile.tickets;
            TList.Items.Refresh();
        }
        public void SelectProfileClick(object sender, RoutedEventArgs e)
        {

        }

        protected void RenameProfileClick(object sender, RoutedEventArgs e)
        {
            objRenameWindow = new RenameWindow();
            objRenameWindow.SetCreatingForm = this;
            objRenameWindow.Show();
            IsEnabled = false;
        }
        public void ReloadAllProfile()
        {
            
        }
        public void DeleteProfileClick(object sender, EventArgs e)
        {

        }

        protected void ThemeDark(object sender, RoutedEventArgs e)
        {
            this.Background = Brushes.DimGray;
            menu.Background = Brushes.DimGray;
            ValidatorBtn.Background = Brushes.Gray;
            TList.Background = Brushes.Gray;
            TList.BorderBrush = Brushes.Gray;
            Log.Background = Brushes.Gray;
        }

        protected void ThemeWhite(object sender, RoutedEventArgs e)
        {
            this.Background = Brushes.White;
            menu.Background = Brushes.White;
            ValidatorBtn.Background = Brushes.WhiteSmoke;
            TList.BorderBrush = Brushes.WhiteSmoke;  
            TList.Background = Brushes.WhiteSmoke;
            Log.Background = Brushes.WhiteSmoke;
        }
       

        protected void ExitClick(object sender, RoutedEventArgs e)
        {
            OnClosed(e);
        }

        protected void SaveasClick(object sender, RoutedEventArgs e)
        {
            objSaveFileDialogSample = new SaveFileDialogSample();
            objSaveFileDialogSample.Show();
        }
    }
}
