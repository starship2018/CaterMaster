using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    public partial class VIPTypeInfoBll
    {
        VIPTypeInfoDal dal=new VIPTypeInfoDal();

        public List<VIPTypeInfo> GetList()
        {
            return dal.GetList();
        }

        public Boolean Add(VIPTypeInfo vip)
        {
            return dal.Insert(vip) > 0;
        }

        public Boolean Edit(VIPTypeInfo vip)
        {
            return dal.Update(vip) > 0;
        }

        public Boolean Delete(int deleteid)
        {
            return dal.Delete(deleteid) > 0;
        }
    }
}
