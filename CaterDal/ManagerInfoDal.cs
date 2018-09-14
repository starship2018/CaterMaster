using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;

namespace CaterDal
{
    public partial class ManagerInfoDal
    {
        /// <summary>
        /// 获取数据库结果集
        /// </summary>
        /// <returns></returns>
        public List<ManagerInfo> GetList()
        {
            //构造要查询的SQL语句
            string sql = "select * from ManagerInfo";
            //将查询的结果放入datatable中
            DataTable table= SqliteHelper.GetDataTable(sql);
            //将datatable中的数据转存到list中去
            List<ManagerInfo> list = new List<ManagerInfo>();
            foreach (DataRow row in table.rows)
            {
                list.Add(new ManagerInfo()
                {
                    MId=Convert.ToInt32(row["mid"]),
                    MName = row["mname"].ToString(),
                    MPwd = row["mpwd"].ToString(),
                    MType = Convert.ToInt32(row["mtype"])
                });
            }
            return list;
        }


        public int Insert()
        {
            //构建insert语句
            string sql = "insert into managerinfo(mname,mpwd,mtype) values(@name,@pwd,@type)";
            SqlParameter[] sp =
            {
                //new SqlParameter("@name",), 
            };
            return;
        }
    }
}
