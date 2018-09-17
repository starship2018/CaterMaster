using System;
using System.Collections.Generic;
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
    }
}
