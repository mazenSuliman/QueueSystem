﻿using ClinicStatus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicStatus.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Floor> Floors { get; set; }
    }
}
