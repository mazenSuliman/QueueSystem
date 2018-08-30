using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicStatus.Entities
{
    public class Clinic
    {
        public int Speciality { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
    }
}
