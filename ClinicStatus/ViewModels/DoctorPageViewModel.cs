using ClinicStatus.Entities;
using ClinicStatus.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicStatus.ViewModels
{
    public class DoctorPageViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public Clinic clinic { get; set; }
        public int c { get; set; }
    }
}
