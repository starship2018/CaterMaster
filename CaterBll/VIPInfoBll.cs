using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    public class VIPInfoBll
    {
        VIPInfoDal dal=new VIPInfoDal();
        //获得模型对象集合
        public List<VIPInfo> GetList(Dictionary<string,string> dic)
        {
            return dal.GetList(dic);
        }

        public bool Add(VIPInfo vi)
        {
            return dal.Insert(vi) > 0;
        }

        public bool Remove(int index)
        {
            return dal.Delete(index) > 0;
        }

        public bool Edit(VIPInfo vip)
        {
            return dal.Update(vip) > 0;
        }
    }
}
