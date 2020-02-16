using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTicketWPF
{
    public static class Extensions
    {
        public static List<Profile> LoadAllProfile(this List<Profile> profiles)
        {
            try
            {
                profiles = new List<Profile>();
                TicketManagement tm = new TicketManagement();
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Profiles"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Profiles");
                }
                string[] allProfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Profiles");
                string readFile = null;
                string[] streamLine = null;
                string[] rows = null;
                string name = null;
                for (int i = 0; i < allProfiles.Length; i++)
                {
                    if (allProfiles[i].Contains(".csv"))
                    {
                        readFile = File.ReadAllText(allProfiles[i]);
                        name = readFile.Substring(0, readFile.IndexOf(':'));
                        readFile = readFile.Substring(readFile.IndexOf(':') + 1);
                        streamLine = readFile.Split('\n');
                        foreach (var prop in streamLine)
                        {
                            if (prop == "")
                            {
                                break;
                            }
                            rows = prop.Split(',');
                            tm.tickets.Add(new Ticket(int.Parse(rows[0]), rows[1], bool.Parse(rows[2]), bool.Parse(rows[3])));
                        }
                        profiles.Add(new Profile(tm, name));
                    }
                }
                return profiles;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public static Profile LoadFirstProfile(this Profile profile)
        {
            try
            {
                profile = new Profile();
                TicketManagement tm = new TicketManagement();
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Profiles"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Profiles");
                }
                string[] allProfile = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Profiles");
                string readFile = null;
                string[] streamLine = null;
                string[] rows = null;
                string name = null;
                if (allProfile[0].Contains(".csv"))
                {
                    readFile = File.ReadAllText(allProfile[0]);
                    name = readFile.Substring(0, readFile.IndexOf(':'));
                    readFile = readFile.Substring(readFile.IndexOf(':') + 1);
                    streamLine = readFile.Split('\n');

                    if (streamLine[0] == "")
                    {
                        return null;
                    }
                    rows = streamLine[0].Split(',');
                    tm.tickets.Add(new Ticket(int.Parse(rows[0]), rows[1], bool.Parse(rows[2]), bool.Parse(rows[3])));
                    profile = new Profile(tm, name);
                    return profile;
                }
                return null;
            }
            catch (Exception)
            {
                MessageBox.Show("The \"Tickets\" folder is empty or the containing files is not readable.\n Try runing the program as administrator.");
                return null;
            }
        }

        public static Profile LoadSelectedProfile(this Profile profile, PData pData)
        {
            try
            {
                TicketManagement tm = new TicketManagement();
                profile = new Profile();
                string reader = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Profiles\\" + pData.PName + ".csv");
                string[] lines = reader.Split('\n');
                string[] pr = null;
                foreach (var prop in lines)
                {
                    pr = prop.Split(',');
                    pr[0] = pr[0].Substring(pr[0].IndexOf(':') + 1);
                    tm.tickets.Add(new Ticket(int.Parse(pr[0]), pr[1], bool.Parse(pr[2]), bool.Parse(pr[3])));
                }
                profile = new Profile(tm, pData.PName);
                return profile;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static void SaveAllTickets(this List<Profile> profiles)
        {
            try
            {
                foreach (var profile in profiles)
                {
                    profile.SaveProfile();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void SavePData(this Profile profile, int value)
        {
            string fileContains = value.ToString() + "," + profile.ProfileName;
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\PData.csv", fileContains);
        }

        public static PData LoadPData(this PData pData)
        {
            try
            {
                string file = File.ReadAllText(Directory.GetCurrentDirectory() + "\\PData.csv");
                string[] data = file.Split(',');
                pData = new PData(int.Parse(data[0]), data[1]);
                return pData;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
