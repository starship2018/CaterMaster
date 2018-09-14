using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    public class ManagerInfoBll
    {
        //创建数据层对象
        ManagerInfoDal miDal=new ManagerInfoDal();

        
        public List<ManagerInfo> GetList()
        {
            //调用查询方法
            return miDal.GetList();
        }

        public bool Add(ManagerInfo mi)
        {
            return miDal.Insert(mi) > 0;
        }

        public bool Edit(ManagerInfo mi)
        {
            return miDal.Update(mi) > 0;
        }

        public bool Remove(ManagerInfo mi)
        {
            return miDal.Delete(mi) > 0;
        }
    }
}
