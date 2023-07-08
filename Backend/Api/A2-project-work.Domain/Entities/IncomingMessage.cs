using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_project_work.Domain.Entities
{
    public class Incomingmessage
    {
        public string? authcode { get; set; }
        public int PortNumber { get; set; }
        public string? unlockcode { get; set; }
        public string? status { get; set; }
        public string? info { get; set; }
        public int BuildingNumber { get; set; }
    }
}
