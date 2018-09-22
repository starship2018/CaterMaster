using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;

namespace CaterDal
{
    public class HallInfoDal
    {
        /// <summary>
        /// 初始化查询列表
        /// </summary>
        /// <returns></returns>
        public List<HallInfo> GetList()
        {
            string sql = "select * from hallinfo where hisdelete=0";
            DataTable table = SqliteHelper.GetDataTable(sql);
            List<HallInfo> list=new List<HallInfo>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new HallInfo()
                {
                    HId = Convert.ToInt32(row["hid"]),
                    HTitle = row["htitle"].ToString()
                });
            }

            return list;
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        public int Insert(HallInfo hi)
        {
            string sql = "insert into hallinfo (htitle,hisdelete) values(@title,0)";
            SQLiteParameter sp=new SQLiteParameter("@title",hi.HTitle);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        public int Update(HallInfo hi)
        {
            string sql = "update hallinfo set htitle=@title where hid=@id";
            SQLiteParameter [] sp=new SQLiteParameter[]
            {
                new SQLiteParameter("@title",hi.HTitle), 
                new SQLiteParameter("@id",hi.HId), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }


        public int Delete(int index)
        {
            string sql = "update hallinfo set hisdelete =1 where hid=@id";
            SQLiteParameter sp=new SQLiteParameter("@id",index);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }
    }
}
