using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTicketWPF
{
    public class PData
    {
        PData data;
        public PData()
        {
            data = data.LoadPData();
        }
        public PData(int index, string pName)
        {
            Index = index;
            PName = pName;
        }

        public int Index { get; set; }
        public string PName { get; set; }
        
    }
}
