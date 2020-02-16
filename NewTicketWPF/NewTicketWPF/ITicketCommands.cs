using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTicketWPF
{
    public interface ITicketCommands
    {
        void TicketUse();
        void SaveFile();
        void LoadFile();
    }
}
