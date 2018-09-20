using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    public class DishTypeInfoBll
    {

        DishTypeInfoDal dal=new DishTypeInfoDal();
        public List<DishTypeInfo> GetList()
        {
            return dal.GetList();
        }

        public bool Add(DishTypeInfo dti)
        {
            return dal.Insert(dti) > 0;
        }

        public bool Exit(DishTypeInfo dti)
        {
            return dal.Update(dti) > 0;
        }

        public bool Remove(int index)
        {
            return dal.Delete(index) > 0;
        }
    }
}
