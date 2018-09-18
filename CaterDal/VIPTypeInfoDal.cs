using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using System.Data;
using System.Data.SQLite;

namespace CaterDal
{
    public partial class VIPTypeInfoDal
    {
        /// <summary>
        /// 读取数据，返回模型集合
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 插入新的数据
        /// </summary>
        /// <param name="vip"></param>
        /// <returns></returns>
        public int Insert(VIPTypeInfo vip)
        {
            string sql = "insert into membertypeinfo(mtitle,mdiscount,misdelete) values(@title,@discount,0)";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("@title",vip.MTitle), 
                new SQLiteParameter("@discount",vip.MDiscount)
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="vip"></param>
        /// <returns></returns>
        public int Update(VIPTypeInfo vip)
        {
            string sql = "update membertypeinfo set mtitle=@title,mdiscount=@discount where mid=@id";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("@id",vip.MId), 
                new SQLiteParameter("@title",vip.MTitle), 
                new SQLiteParameter("@discount",vip.MDiscount), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }


        public int Delete(int deleteid)
        {
            string sql = "update membertypeinfo set misdelete=1 where mid=@id";
            SQLiteParameter sp=new SQLiteParameter("@id",deleteid);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }
    }
}
