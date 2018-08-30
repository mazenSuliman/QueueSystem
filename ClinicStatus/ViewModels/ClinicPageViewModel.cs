using ClinicStatus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicStatus.ViewModels
{
    public class ClinicePageViewModel
    {
        public IEnumerable<Clinic> Clinic { get; set; }
        public Floor Floor { get; set; }

    }
}
