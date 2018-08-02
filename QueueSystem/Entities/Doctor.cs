using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueSystem.Entities
{
    public class Doctor
    {
        public string Name { get; set; }
        public int RecWait { get; set; }
        public int DocWait { get; set; }
        public int Seeing { get; set; }
        public int Saw { get; set; }
        public bool InDuty { get; set; }
        public string Code { get; set; }
    }
}
