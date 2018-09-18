using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    public class VIPInfo
    {
        public int MId { get; set; }

        public int MTypeId { get; set; }

        public string MName { get; set; }

        public string MPhone { get; set; }

        public decimal MCount { get; set; }

        public bool MIsdelete { get; set; }

        public string MTypeTitle { get; set; }
    }
}
