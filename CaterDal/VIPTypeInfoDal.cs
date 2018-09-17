using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using System.Data;

namespace CaterDal
{
    public partial class VIPTypeInfoDal
    {
        public List<VIPTypeInfo> GetList()
        {
            //查询未删除的数据
            string sql = "select * from membertypeinfo where misdelete = 0";
            //将数据写入到数据表中
            DataTable table=SqliteHelper.GetDataTable(sql);
            var rows = table.Rows;
            //建立VIP模型集合
            List<VIPTypeInfo> list=new List<VIPTypeInfo>();
            //遍历表格
            foreach (DataRow row in rows)
            {
                list.Add(new VIPTypeInfo()
                {
                    MId = Convert.ToInt32(row["mid"]),
                    MTitle = row["mtitle"].ToString(),
                    MDiscount = Convert.ToDecimal(row["mdiscount"]),
                });
            }
            return list;
        }
    }
}
