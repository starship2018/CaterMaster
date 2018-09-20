using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterCommon;
using CaterModel;

namespace CaterDal
{
    public class DishInfoDal
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public List<DishInfo> GetList(Dictionary<string,string> dic)
        {
            string sql = @"select di.*,dti.dtitle as dtypetitle from dishinfo as di inner join dishtypeinfo as dti on di.dtypeid=dti.did where di.disdelete=0 and dti.disdelete=0";
            List<SQLiteParameter> sp=new List<SQLiteParameter>();
            if (dic.Count>0)
            {
                foreach (var pair in dic)
                {
                    sql += " and di."+pair.Key+" like @"+pair.Key;
                    sp.Add(new SQLiteParameter("@"+pair.Key,"%"+pair.Value+"%"));
                }
            }
            DataTable table=SqliteHelper.GetDataTable(sql,sp.ToArray());

            List<DishInfo> list=new List<DishInfo>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new DishInfo()
                {
                    DId = Convert.ToInt32(row["did"]),
                    DTitle = row["dtitle"].ToString(),
                    DTypeTitle = row["dtypetitle"].ToString(),
                    DPrice = Convert.ToDecimal(row["dprice"]),
                    DChar = row["dchar"].ToString()
                });
            }

            return list;
        }
        
        
        /// <summary>
        /// 得到汉字首字母大写字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetChar(string str)
        {
            return PinyinHelper.GetPinyin(str);
        }
        
        
        
        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public int Insert(DishInfo dish)
        {
            string sql =
                "insert into dishinfo(dtitle,dtypeid,dprice,dchar,disdelete) values(@title,@typeid,@price,@char,0)";
            SQLiteParameter [] sp=new SQLiteParameter[]
            {
                new SQLiteParameter("@title",dish.DTitle), 
                new SQLiteParameter("@typeid",dish.DTypeId), 
                new SQLiteParameter("@price",dish.DPrice), 
                new SQLiteParameter("@char",dish.DChar), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public int Update(DishInfo dish)
        {
            string sql = "update dishinfo set dtitle=@title,dtypeid=@typeid,dprice=@price where did=@id";
            SQLiteParameter [] sp=new SQLiteParameter[]
            {
                new SQLiteParameter("@title",dish.DTitle), 
                new SQLiteParameter("@typeid",dish.DTypeId), 
                new SQLiteParameter("@price",dish.DPrice), 
                new SQLiteParameter("@id",dish.DId), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }


        public int Delete(int index)
        {
            string sql = "update dishinfo set disdelete = 1 where did=@id";
            SQLiteParameter sp=new SQLiteParameter("@id",index);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }
    }
}
