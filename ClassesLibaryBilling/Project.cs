﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesLibaryBilling
{
    [Serializable]
   public class Project
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string SendAddress { get; set; }

        public DateTime? NextPay { get; set; }

        public string Comment { get; set; }

    }
}