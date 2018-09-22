using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    public class HallInfoBll
    {
        HallInfoDal dal=new HallInfoDal();
        public List<HallInfo> GetList()
        {
            return dal.GetList();
        }


        public bool Add(HallInfo hi)
        {
            return dal.Insert(hi) > 0;
        }

        public bool Edit(HallInfo hi)
        {
            return dal.Update(hi) > 0;
        }

        public bool Remove(int index)
        {
            return dal.Delete(index) > 0;
        }
    }
}
