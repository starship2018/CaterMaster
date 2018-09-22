using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterDal
{
    public class TableInfoDal
    {
        /// <summary>
        /// 初始化查询数据
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public List<TableInfo> GetList(Dictionary<string,string> dic)
        {
            string sql = "select ti.*,hi.htitle from tableinfo as ti inner join hallinfo as hi on ti.thallid=hi.hid where ti.tisdelete=0 and hi.hisdelete=0";
            List<SQLiteParameter> sp=new List<SQLiteParameter>();
            if (dic.Count>0)
            {
                foreach (var pair in dic)
                {
                    sql += " and " + pair.Key + "=@" + pair.Key;
                    sp.Add(new SQLiteParameter("@"+pair.Key,pair.Value));
                }
            }

            DataTable table = SqliteHelper.GetDataTable(sql,sp.ToArray());
            List<TableInfo> list=new List<TableInfo>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new TableInfo()
                {
                    TId = Convert.ToInt32(row["tid"]),
                    TTitle = row["ttitle"].ToString(),
                    THallId = Convert.ToInt32(row["thallid"]),
                    TIsFree = Convert.ToBoolean(row["tisfree"]),
                    THalltitle = row["htitle"].ToString()
                });
            }

            return list;
        }
     
        
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="ti"></param>
        /// <returns></returns>
        public int Insert(TableInfo ti)
        {
            string sql = "insert into tableinfo(ttitle,thallid,tisfree,tisdelete) values(@title,@hallid,@isfree,0)";
            SQLiteParameter [] sp=new SQLiteParameter[]
            {
                new SQLiteParameter("@title",ti.TTitle), 
                new SQLiteParameter("@hallid",ti.THallId), 
                new SQLiteParameter("@isfree",ti.TIsFree), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="ti"></param>
        /// <returns></returns>
        public int Update(TableInfo ti)
        {
            string sql = "update tableinfo set ttitle=@title,thallid=@hallid,tisfree=@isfree where tid=@id";
            SQLiteParameter [] sp=new SQLiteParameter[]
            {
                new SQLiteParameter("@title",ti.TTitle), 
                new SQLiteParameter("@hallid",ti.THallId), 
                new SQLiteParameter("@isfree",ti.TIsFree), 
                new SQLiteParameter("@id",ti.TId), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }


        public int Delete(int index)
        {
            string sql = "update tableinfo set tisdelete =1 where tid=@id";
            SQLiteParameter sp=new SQLiteParameter("@id",index);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }
    }
}
