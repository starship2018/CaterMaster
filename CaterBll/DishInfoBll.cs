using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    public class DishInfoBll
    {
        DishInfoDal dal=new DishInfoDal();
        public List<DishInfo> GetList(Dictionary<string,string> dic)
        {
            return dal.GetList(dic);
        }

        public string GetChar(string str)
        {
            return dal.GetChar(str);
        }

        public bool Add(DishInfo dish)
        {
            return dal.Insert(dish) > 0;
        }

        public bool Edit(DishInfo dish)
        {
            return dal.Update(dish) > 0;
        }

        public bool Remove(int index)
        {
            return dal.Delete(index) > 0;
        }
    }
}
