﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMIOD.Models
{
    public class Container
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Creation_datetime { get; set; }
        public int Parent { get; set; }

        public virtual Application Application { get; set; }
    }
}