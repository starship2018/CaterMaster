using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterCommon;
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
            foreach (DataRow row in table.Rows)
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

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="mi">在UI层封装好的模型对象</param>
        /// <returns></returns>
        public int Insert(ManagerInfo mi)
        {
            //构建insert语句
            string sql = "insert into managerinfo(mname,mpwd,mtype) values(@name,@pwd,@type)";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("@name",mi.MName), 
                new SQLiteParameter("@pwd",MD5Helper.GetMD5String(mi.MPwd)),
                 new SQLiteParameter("@type",mi.MType), 
            };
            return SqliteHelper.ExcuteNoQuery(sql,sp);
        }

        /// <summary>
        /// 修改管理员，注意到密码是否发生了修改
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public int Update(ManagerInfo mi)
        {
            //要进行密码的判断，因为最终密码要经过MD5加密，这是不可逆的
            //若用户修改了密码，则重新加密，若没有，则不变
            //对于这种更改条目不确定的情况，方法是对SQL语句和参数列表根据实际情况逐条相加，而不是写死。SQL语句可以使用字符串拼接，而参数列表就放在list集合中而不是数组中，因为数组的长度不可变，集合长度可变，这里明显list更好用！
            List<SQLiteParameter> sp = new List<SQLiteParameter>();
            string sql = "update managerinfo set mname=@name,mtype=@type";
            sp.Add(new SQLiteParameter("@name",mi.MName));
            sp.Add(new SQLiteParameter("@type",mi.MType));
            if (!mi.MPwd.Equals("这是原来的密码吗"))
            {
                sql += ",mpwd=@pwd";
                sp.Add(new SQLiteParameter("@pwd",MD5Helper.GetMD5String(mi.MPwd)));
            }
            sql += " where mid=@id";
            sp.Add(new SQLiteParameter("@id",mi.MId));
            return SqliteHelper.ExcuteNoQuery(sql, sp.ToArray());
        }


        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete from managerinfo where mid=@id";
            SQLiteParameter sp=new SQLiteParameter("@id",id);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        }


        public ManagerInfo GetByName(string name)
        {
            string sql = "select * from managerinfo where mname=@name";
            SQLiteParameter sp =new SQLiteParameter("@name", name);
            DataTable table=SqliteHelper.GetDataTable(sql, sp);
            ManagerInfo mi = null;
            //判断取到值的table是否为空？
            if (table.Rows.Count>0)
            {
                mi=new ManagerInfo();
                mi.MId=Convert.ToInt32(table.Rows[0][0]);
                mi.MName = table.Rows[0][1].ToString();
                mi.MPwd = table.Rows[0][2].ToString();
                mi.MType = Convert.ToInt32(table.Rows[0][3]);
            }
            else
            {

            }
            return mi;
        }
    }
}
