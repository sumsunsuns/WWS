
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWS
{
    public class TempData
    {
        //以下静态字段用来在两个或多个不同窗体间传递数据
        public static string userlevel;     //存放用户级别
        public static int flag;             //存放用户操作标志 1:新增 2:修改 3:删除
        public static string no;            //存放学生学号,教师编号或用户名等
    }
    class CommDbOp
    {
        public static void deldata(string tn)
        {       //删除指定表中所有记录,对于oper表添加一个系统用户
            DataTable mytable;
            string mysql = "DELETE " + tn.Trim();
            mytable = Exesql(mysql);
            if (tn.Trim() == "oper")
            {
                mysql = "INSERT oper VALUES('1234','1234','系统管理员')";
                mytable = Exesql(mysql);
            }
        }
        private static string firststr(string mystr)
        {       //提取字符串中的第一个字符串
            string[] strarr;
            strarr = mystr.Split(' ');
            return strarr[0];
        }
        public static DataTable Exesql(string mysql)
        {
            string mystr = "Data Source=(local);Initial Catalog=WWS;Integrated Security=True";
            SqlConnection myconn = new SqlConnection(mystr);
            myconn.Open();
            SqlCommand mycmd = new SqlCommand(mysql, myconn);
            mycmd.CommandType = System.Data.CommandType.Text;
            string mstr = "INSERT,DELETE,UPDATE";
            if (mstr.Contains(firststr(mysql).ToUpper()))
            {
                mycmd.ExecuteNonQuery();        //执行查询
                myconn.Close();                 //关闭连接
                return null;                    //返回空
            }
            else
            {
                DataSet myds = new DataSet();
                SqlDataAdapter myadp = new SqlDataAdapter();
                myadp.SelectCommand = mycmd;
                mycmd.ExecuteNonQuery();        //执行查询
                myconn.Close();                 //关闭连接
                myadp.Fill(myds);               //填充数据
                return myds.Tables[0];          //返回表
            }
        }

        public static int Exesql(string procName, params SqlParameter[] parameters)
        {

            string mystr = "Data Source=(local);Initial Catalog=WWS;Integrated Security=True";
            SqlConnection myconn = new SqlConnection(mystr);
            myconn.Open();
            SqlCommand mycmd = new SqlCommand(procName, myconn);
            mycmd.CommandType = System.Data.CommandType.StoredProcedure;
            mycmd.CommandText = procName;
            mycmd.Parameters.AddRange(parameters);
            return mycmd.ExecuteNonQuery();
        }

        public static DataTable Exesql_sel(string procName, params SqlParameter[] parameters)
        {

            string mystr = "Data Source=(local);Initial Catalog=WWS;Integrated Security=True";
            SqlConnection myconn = new SqlConnection(mystr);
            myconn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(procName, myconn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddRange(parameters);
            da.Fill(dt);

            return dt;
        }
    }
}
