﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterDal
{
    public class TableInfo
    {
        public int TId { get; set; }

        public string TTitle { get; set; }

        public int THallId { get; set; }

        public bool TIsFree { get; set; }

        public bool TIsDelete { get; set; }

        public string THalltitle { get; set; }
    }
}
