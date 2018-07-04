using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWS
{
    public partial class editwork1 : Form
    {
        public editwork1()
        {
            InitializeComponent();
        }


        private void editworker1_Load(object sender, EventArgs e)  //初始化
        {
            if (TempData.flag == 1)    //新增职工工作量记录
            {
                textBox1.Text = ""; textBox2.Text = "";
                textBox3.Text = "";                           
                textBox1.Enabled = true;
                textBox1.Focus();
            }
            else                   //修改职工工作量记录
            {
                DataTable mytable1 = new DataTable();
                mytable1 = CommDbOp.Exesql("SELECT * FROM 工作量 WHERE 职工号='" + TempData.no + "'");
                textBox1.Text = mytable1.Rows[0]["职工号"].ToString().Trim();
                textBox2.Text = mytable1.Rows[0]["月份"].ToString().Trim();
                textBox3.Text = mytable1.Rows[0]["完成数量"].ToString().Trim();                           
                textBox1.Enabled = false;       //不允许修改职工号
                textBox2.Focus();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string  mysql;
            DataTable mytable1 = new DataTable();
            if (textBox1.Text.ToString() == "")
            {
                MessageBox.Show("必须输入职工号", "信息提示");
                return;
            }
            if (textBox2.Text.ToString() == "")
            {
                MessageBox.Show("必须输入月份", "信息提示");
                return;
            }
            if (textBox3.Text.ToString() == "")
            {
                MessageBox.Show("必须输入产品完成数量", "信息提示");
                return;
            }
           
           
           
            try
            {
                if (TempData.flag == 1)  //新增职工记录
                {
                    mytable1 = CommDbOp.Exesql("SELECT * FROM 工作量 WHERE 职工号='" + textBox1.Text + "'and 月份='"+ textBox2.Text+"'");
                    if (mytable1.Rows.Count == 1)
                    {
                        MessageBox.Show("输入的记录重复,不能新增记录", "信息提示");
                        textBox1.Focus();
                        textBox2.Focus();
                        return;
                    }
                    else          //不重复时插入职工记录
                    {
                        mysql = "INSERT INTO 工作量 VALUES( '" + textBox1.Text.Trim() + "','" +
                            textBox2.Text.Trim() + "','" +
                            
                            textBox3.Text.Trim() + 
                             "')";
                        //  textBox1.Text.Trim() + ")";
                        //默认职工的初始密码与其职工号相同
                        mytable1 = CommDbOp.Exesql(mysql);
                        this.Close();
                    }
                }
                else               //修改职工记录
                {
                    mysql = "UPDATE 工作量 SET 完成数量='" + textBox3.Text.Trim() +
                                                             
                        // "',密码='" + textBox1.Text.Trim() +
                        "' WHERE 职工号='" + textBox1.Text.Trim() + "'and 月份='"+ textBox2.Text.Trim()+"'";
                    //默认职工的初始密码与其职工号相同
                    mytable1 = CommDbOp.Exesql(mysql);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "信息提示");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
