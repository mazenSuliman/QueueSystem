using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicStatus.Entities
{
    public class Doctor
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool status { get; set; }
    }
}
