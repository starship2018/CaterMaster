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
    public class DishTypeInfoDal
    {
        /// <summary>
        /// 初始化查询
        /// </summary>
        /// <returns></returns>
        public List<DishTypeInfo> GetList()
        {
            string sql = "select * from dishtypeinfo where disdelete =0";
            DataTable table = SqliteHelper.GetDataTable(sql);
            List<DishTypeInfo> list=new List<DishTypeInfo>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new DishTypeInfo()
                {
                    DId =Convert.ToInt32(row["did"]),
                    DTitle = row["dtitle"].ToString()
                });
            }
            return list;
        }

        
        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="dti"></param>
        /// <returns></returns>
        public int Insert(DishTypeInfo dti)
        {
            string sql = "insert into dishtypeinfo(dtitle,disdelete) values(@title,0)";
            SQLiteParameter sp = new SQLiteParameter("@title", dti.DTitle);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="dti"></param>
        /// <returns></returns>
        public int Update(DishTypeInfo dti)
        {
            string sql = "update dishtypeinfo set dtitle =@title where did=@id";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("@title",dti.DTitle), 
                new SQLiteParameter("@id",dti.DId), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }


        public int Delete(int index)
        {
            string sql = "update dishtypeinfo set disdelete =1 where did=@id";
            SQLiteParameter sp=new SQLiteParameter("@id",index);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }
    }
}
