using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterCommon;
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

        //均以boolean类型作为返回值，是为了确认操作状态是否顺利完成，或者因意外而失败
        public bool Add(ManagerInfo mi)
        {
            return miDal.Insert(mi) > 0;
        }

        public bool Edit(ManagerInfo mi)
        {
            return miDal.Update(mi) > 0;
        }

        public bool Remove(int id)
        {
            return miDal.Delete(id) > 0;
        }

        public LoginState Login(string name,string pwd,out int type)
        {
            type = -1;
            ManagerInfo mi=miDal.GetByName(name);
            if (mi==null)
            {
                return LoginState.NameERROR;
            }
            else
            {
                if (mi.MPwd.Equals(MD5Helper.GetMD5String(pwd)))
                {
                    type = mi.MType;
                    return LoginState.Ok;
                }
                else
                {
                    return LoginState.PwdERROR;
                }
            }
        }
    }
}
