﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOW.Domain.DTO
{
    public class DepartmentDTO
    {
        [Required, MaxLength(500)]
        public string DepartmentName { get; set; }
    }
}
