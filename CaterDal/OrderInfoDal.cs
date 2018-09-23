using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using CaterModel;

namespace CaterDal
{
    public class OrderInfoDal
    {

        #region 创建订单
        public int MakeOrder(int tableid)
        {
            string sql = "insert into orderinfo(odate,ispay,tableid) values(datetime('now', 'localtime'),0,@tableid);update tableinfo set tisfree=0 where tid=@tableid;select oid from orderinfo order by oid desc limit 0,1";
            SQLiteParameter sp = new SQLiteParameter("@tableid", tableid);
            return Convert.ToInt32(SqliteHelper.ExcuteScalar(sql, sp));
        } 
        #endregion

        #region 加载菜单列表
        public int OrderDish(int orderid, int dishid)
        {
            //查询当前是否已经点了这道菜，若是则加1，若不死
            string sql = "select count(*) from orderdetailinfo where orderid=@oid and dishid=@did";
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@oid",orderid), 
                new SQLiteParameter("@did",dishid), 
            };
            int count = Convert.ToInt32(SqliteHelper.ExcuteScalar(sql, sp));
            if (count > 0)
            {
                //已经点过这道菜
                sql = "update orderdetailinfo set count=count+1 where orderid=@oid and dishid=@did";
            }
            else
            {
                //还没点过
                sql = "insert into orderdetailinfo(orderid,dishid,count) values(@oid,@did,1)";
            }

            return SqliteHelper.ExcuteNoQuery(sql, sp); 
        #endregion

            
        }



        #region 加载订单详情列表
        public List<OrderDetailInfo> GetDetailList(int orderid)
        {
            string sql = "select  oti.oid,di.dTitle,di.dPrice,oti.count from dishinfo as di inner join orderdetailinfo as oti on di.did=oti.dishid where oti.orderid=@orderid";
            SQLiteParameter sp = new SQLiteParameter("@orderid", orderid);
            DataTable table = SqliteHelper.GetDataTable(sql, sp);
            List<OrderDetailInfo> list=new List<OrderDetailInfo>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new OrderDetailInfo()
                {
                    OId = Convert.ToInt32(row["oid"]),
                    DTitle =row["dTitle"].ToString(),
                    DPrice = Convert.ToDecimal(row["dPrice"]),
                    Count = Convert.ToInt32(row["count"])
                });
            }

            return list;
        } 
        #endregion


        #region 计算总价格
        public decimal GetTotalMoneyByOrderId(int orderid)
        {
            string sql =
                "select sum(oti.count*di.dprice) from orderdetailinfo as oti inner join dishinfo as di on oti.dishid=di.did where oti.orderid=@orderid";
            SQLiteParameter sp = new SQLiteParameter("@orderid", orderid);
            var money = SqliteHelper.ExcuteScalar(sql, sp);
            if (money == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToDecimal(money);
        } 
        #endregion


        #region 更新总价格
        public int UpdateCountByOrderId(int orderid, int count)
        {
            string sql = "update orderdetailinfo set count=@count where orderid=@orderid";
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@count",count), 
                new SQLiteParameter("@orderid",orderid), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        } 
        #endregion

        #region 提交新的订单
        public int CommitOrder(int orderid, int totalprice)
        {
            string sql = "update orderinfo set omoney=@money where oid=@oid";
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@money",totalprice), 
                new SQLiteParameter("oid",orderid), 
            };
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        } 
        #endregion

        #region 删除订单
        public int Delete(int index)
        {
            string sql = "delete from orderdetailinfo where oid=@id ";
            SQLiteParameter sp = new SQLiteParameter("@id", index);
            return SqliteHelper.ExcuteNoQuery(sql, sp);
        } 
        #endregion

        #region 根据餐桌号拿到订单号
        public int GetOrderById(int tableid)
        {
            string sql = "select oid from orderinfo where tableid=@tableid and ispay=0";
            SQLiteParameter sp = new SQLiteParameter("@tableid", tableid);
            return Convert.ToInt32(SqliteHelper.ExcuteScalar(sql, sp));
        } 
        #endregion

        public int Pay(bool isUseMoney, int memberId, decimal payMoney, int orderid, decimal discount)
        {
            //创建数据库的链接对象
            using (SQLiteConnection conn = new SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["itcastCater"].ConnectionString))
            {
                int result = 0;
                //由数据库链接对象创建事务
                conn.Open();
                SQLiteTransaction tran = conn.BeginTransaction();

                //创建command对象
                SQLiteCommand cmd = new SQLiteCommand();
                //将命令对象启用事务
                cmd.Transaction = tran;
                //执行各命令
                string sql = "";
                SQLiteParameter[] ps;
                try
                {
                    //1、根据是否使用余额决定扣款方式
                    if (isUseMoney)
                    {
                        //使用余额
                        sql = "update MemberInfo set mMoney=mMoney-@payMoney where mid=@mid";
                        ps = new SQLiteParameter[]
                        {
                            new SQLiteParameter("@payMoney", payMoney),
                            new SQLiteParameter("@mid", memberId)
                        };
                        cmd.CommandText = sql;
                        cmd.Parameters.AddRange(ps);
                        result += cmd.ExecuteNonQuery();
                    }

                    //2、将订单状态为IsPage=1
                    sql = "update orderInfo set isPay=1,memberId=@mid,discount=@discount where oid=@oid";
                    ps = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@mid", memberId),
                        new SQLiteParameter("@discount", discount),
                        new SQLiteParameter("@oid", orderid)
                    };
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(ps);
                    result += cmd.ExecuteNonQuery();

                    //3、将餐桌状态IsFree=1
                    sql = "update tableInfo set tIsFree=1 where tid=(select tableId from orderinfo where oid=@oid)";
                    SQLiteParameter p = new SQLiteParameter("@oid", orderid);
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(p);
                    result += cmd.ExecuteNonQuery();
                    //提交事务
                    tran.Commit();
                }
                catch
                {
                    result = 0;
                    //回滚事务
                    tran.Rollback();
                }
                return result;
            }
        }
    }
}
