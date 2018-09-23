using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterDal
{
    public class SqliteHelper
    {
        //从配置文件中读取数据库连接字符串
            static string connStr = ConfigurationManager.ConnectionStrings["catermaster"].ConnectionString;

        //从数据库有三种执行方法 insert update delete

        /// <summary>
        /// 增，删，改
        /// </summary>
        /// <param name="sql">非查询语句</param>
        /// <param name="sp">参数数组列表</param>
        /// <returns></returns>
        public static int ExcuteNoQuery(string sql,params SQLiteParameter[] sp)
        {
            //创建连接对象
            using (SQLiteConnection conn=new SQLiteConnection(connStr))
            {
                //创建命令对象
                using (SQLiteCommand cmd=conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    //添加参数列表
                    cmd.Parameters.AddRange(sp);
                    //执行命令并返回影响行数
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// 查，将结果封装在datatable中。
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="sp">参数列表</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql,params SQLiteParameter[] sp)
        {
            using (SQLiteConnection conn=new SQLiteConnection(connStr))
            {
                //创建适配器对象
                SQLiteDataAdapter adapter=new SQLiteDataAdapter(sql,conn);
                //创建内存数据表
                DataTable table=new DataTable();
                //添加参数列表
                adapter.SelectCommand.Parameters.AddRange(sp);
                //将结果写入数据表
                adapter.Fill(table);
                return table;
            }
        }
        
        
        
        /// <summary>
        /// 执行查询，以object的格式并返回第一行第一列的第一个值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static object ExcuteScalar(string sql,params SQLiteParameter [] sp)
        {
            using (SQLiteConnection conn=new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd=conn.CreateCommand())
                {
                    
                    //添加参数
                    cmd.Parameters.AddRange(sp);
                    cmd.CommandText = sql;
                    //执行查询
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
