using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using System.Data;

namespace CaterDal
{
    public class VIPInfoDal
    {
        /// <summary>
        /// 刷新并返回查询结果
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public List<VIPInfo> GetList(Dictionary<string, string> dic)
        {
            string sql = "select mi.*,mti.mTitle " +
                         "from MemberInfo as mi " +
                         "inner join MemberTypeInfo as mti " +
                         "on mi.mTypeId=mti.mid " +
                         "where mi.mIsDelete=0 ";
            List<SQLiteParameter> sp = new List<SQLiteParameter>();
            if (dic != null)
            {
                foreach (var pair in dic)
                {
                    sql += "and " + pair.Key + " like @" + pair.Key;
                    sp.Add(new SQLiteParameter("@" + pair.Key, "%"+pair.Value+"%"));
                }
            }
            DataTable table = SqliteHelper.GetDataTable(sql, sp.ToArray());
            var rows = table.Rows;
            List<VIPInfo> list = new List<VIPInfo>();
            foreach (DataRow row in rows)
            {
                list.Add(new VIPInfo()
                {
                    MId = Convert.ToInt32(row["mid"]),
                    MTypeId = Convert.ToInt32(row["mtypeid"]),
                    MName = row["mname"].ToString(),
                    MPhone = row["mphone"].ToString(),
                    MCount = Convert.ToDecimal(row["mmoney"]),
                    MTypeTitle = row["mtitle"].ToString()
                });
            }
            return list;
        }

        /// <summary>
        /// 插入新的数据
        /// </summary>
        /// <param name="vi"></param>
        /// <returns></returns>
        public int Insert(VIPInfo vi)
        {
            string sql =
                "insert into memberinfo(mtypeid,mname,mphone,mmoney,misdelete) values(@typeid,@name,@phone,@money,@isdelete)";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("@typeid",vi.MTypeId), 
                new SQLiteParameter("@name",vi.MName), 
                new SQLiteParameter("@phone",vi.MPhone), 
                new SQLiteParameter("@money",vi.MCount), 
                new SQLiteParameter("@isdelete",vi.MIsdelete), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }

        public int Delete(int index)
        {
            string sql = "update memberinfo set misdelete=1 where mid=@id";
            SQLiteParameter sp=new SQLiteParameter("@id",index);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }

        public int Update(VIPInfo vip)
        {
            string sql = "update memberinfo set mname=@name,mtypeid=@typeid,mphone=@phone,mmoney=@moneyn where mid=@id";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("@id",vip.MId), 
                new SQLiteParameter("@name",vip.MName), 
                new SQLiteParameter("@typeid",vip.MTypeId), 
                new SQLiteParameter("@phone",vip.MPhone), 
                new SQLiteParameter("@money",vip.MCount), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }
    }
}
