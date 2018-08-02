using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueSystem.Entities
{
    public class Ticket
    {
        public int ID { get; set; }
        public string Speciality { get; set; }
        public string Doctor { get; set; }
        public int DoctorCode { get; set; }
        public string Floor { get; set; }
        public DateTime Time { get; set; }
    }
}
