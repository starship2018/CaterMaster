using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;

namespace CaterBll
{
    public class TableInfoBll
    {

        TableInfoDal dal=new TableInfoDal();
        public List<TableInfo> GetList(Dictionary<string,string> dic)
        {
            return dal.GetList(dic);
        }

        public bool Add(TableInfo ti)
        {
            return dal.Insert(ti) > 0;
        }

        public bool Edit(TableInfo ti)
        {
            return dal.Update(ti) > 0;
        }

        public bool Remove(int index)
        {
            return dal.Delete(index) > 0;

        }
    }
}
