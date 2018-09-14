﻿using System;
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
    }
}
