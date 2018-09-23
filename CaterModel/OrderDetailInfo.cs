using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    public class OrderDetailInfo
    {
        public int OId { get; set; }

        public int OrderId { get; set; }

        public int DishId { get; set; }

        public int Count { get; set; }

        public string DTitle { get; set; }

        public decimal DPrice { get; set; }
    }
}
