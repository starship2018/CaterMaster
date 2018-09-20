using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    public class DishInfo
    {
        public int DId { get; set; }

        public string DTitle { get; set; }

        public int DTypeId { get; set; }

        public decimal DPrice { get; set; }

        public string DChar { get; set; }

        public bool DIsdelete { get; set; }

        public string DTypeTitle { get; set; }
    }
}
