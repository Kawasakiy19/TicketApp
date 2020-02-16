using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Collections.Generic;

namespace NewTicketWPF
{
    public class Profile
    {
        #region Field
        private TicketManagement _tm;
        #endregion

        #region Constructors
        /// <summary>
        /// creates a profile manager for TicketManagement type
        /// </summary>
        public Profile()
        {
            _tm = new TicketManagement();
        }
        /// <summary>
        /// creates a profile manager for TicketManagement type
        /// </summary>
        /// <param name="tm"></param>
        public Profile(TicketManagement tm, string name)
        {
            ProfileName = name;
            _tm = tm;
        }

        public Profile(string name, int type, int length)
        {
            ProfileName = name;
            _tm = new TicketManagement(type, length);
        }

        #endregion

        #region Properties
        public TicketManagement SetProfile
        {
            get { return _tm; }
            set { _tm = value; }
        }

        public string ProfileName { get; set; }

        #endregion

        #region Methods

        public void SaveProfile()
        {
            try
            {
                string path = "Profiles\\ " + ProfileName + ".csv";
                string savingLine = ProfileName + ":";
                int index = 0;
                foreach (var ticket in _tm.tickets)
                {
                    savingLine += ticket.ID + "," + ticket.Name + "," + ticket.Used + "," + ticket.Sold + "\n";
                    index++;
                }
                File.WriteAllText(path, savingLine);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        public void SaveProfile(string pName)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\Profiles\\ " + pName + ".csv";
                string savingLine = ProfileName + ":";
                int index = 0;
                foreach (var ticket in _tm.tickets)
                {
                    savingLine += ticket.ID + "," + ticket.Name + "," + ticket.Used + "," + ticket.Sold + "\n";
                    index++;
                }
                File.WriteAllText(path, savingLine);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        public void LoadProfile(string fName)
        {
            try
            {
                string file = File.ReadAllText("Profiles\\" + fName + ".csv");
                ProfileName = file.Substring(0, file.IndexOf(':'));
                file = file.Substring(file.IndexOf(':') + 1);
                string[] reader = file.Split('\n');
                foreach (var item in reader)
                {
                    string[] lines = item.Split(',');
                    _tm.tickets.Add(new Ticket(int.Parse(lines[0]), lines[1], bool.Parse(lines[2])));
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        public string DefaultProfileNamer()
        {
            try
            {
                string init = "Profiles\\Profile";
                for (int i = 1; i < 100; i++)
                {
                    init += i.ToString() + ".csv";
                    if (File.Exists(init))
                    {
                        init = "Profiles\\Profile";
                        continue;
                    }
                    return init;
                }
                return null;
            }
            catch (Exception)
            {
                MessageBox.Show("Maximum profiles reached (\"100 Profiles\").");
                return null;
            }
        }

        public override string ToString()
        {
            return String.Format(ProfileName);
        }

        public void Delete(string fileName)
        {
            try
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\Profiles\\" + fileName + ".csv");
            }
            catch(Exception)
            {
            }
        }


        #endregion
    }
}
