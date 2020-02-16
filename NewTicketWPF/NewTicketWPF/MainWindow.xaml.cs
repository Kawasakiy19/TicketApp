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
        Profile currentProfile;
        PData pData;
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
        }

    }
}
