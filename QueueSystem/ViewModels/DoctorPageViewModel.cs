using QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueSystem.ViewModels
{
    public class DoctorPageViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public Clinic Clinic { get; set; }
        public int c { get; set; }
    }
}
