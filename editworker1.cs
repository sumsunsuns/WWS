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
    public partial class editworker1 : Form
    {
        public editworker1()
        {
            InitializeComponent();
        }

        private void editworker1_Load(object sender, EventArgs e)  //初始化
        {
            if (TempData.flag == 1)    //新增职工记录
            {
                textBox1.Text = ""; textBox2.Text = "";
                textBox3.Text = ""; textBox4.Text = "";
                textBox5.Text = "";
                radioButton1.Checked = false; radioButton2.Checked = false;
                textBox1.Enabled = true;
                textBox1.Focus();
            }
            else                   //修改职工记录
            {
                DataTable mytable1 = new DataTable();
                mytable1 = CommDbOp.Exesql("SELECT * FROM 职工 WHERE 职工号='" + TempData.no + "'");
                textBox1.Text = mytable1.Rows[0]["职工号"].ToString().Trim();
                textBox2.Text = mytable1.Rows[0]["姓名"].ToString().Trim();
                textBox3.Text = mytable1.Rows[0]["年龄"].ToString().Trim();
                textBox4.Text = mytable1.Rows[0]["所在部门"].ToString().Trim();
                textBox5.Text = mytable1.Rows[0]["联系方式"].ToString().Trim();
                if (mytable1.Rows[0]["性别"].ToString() == "男")
                    radioButton1.Checked = true;
                else if (mytable1.Rows[0]["性别"].ToString() == "女")
                    radioButton2.Checked = true;
                textBox1.Enabled = false;       //不允许修改职工号
                textBox2.Focus();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string xb, mysql;
            DataTable mytable1 = new DataTable();
            if (textBox1.Text.ToString() == "")
            {
                MessageBox.Show("必须输入职工号", "信息提示");
                return;
            }
            if (textBox2.Text.ToString() == "")
            {
                MessageBox.Show("必须输入姓名", "信息提示");
                return;
            }
            if (textBox3.Text.ToString() == "")
            {
                MessageBox.Show("必须输入年龄", "信息提示");
                return;
            }
            if (textBox4.Text.Trim() == "")
            {
                MessageBox.Show("必须输入所在部门", "信息提示");
                return;
            }
            if(textBox5.Text.Trim() == "")
            {
                MessageBox.Show("必须输入联系方式", "信息提示");
                return;
            }
            if (radioButton1.Checked)
                xb = "男";
            else if (radioButton2.Checked)
                xb = "女";
            else
                xb = "";
            try
            {
                if (TempData.flag == 1)  //新增职工记录
                {
                    mytable1 = CommDbOp.Exesql("SELECT * FROM 职工 WHERE 职工号='" + textBox1.Text + "'");
                    if (mytable1.Rows.Count == 1)
                    {
                        MessageBox.Show("输入的职工号重复,不能新增职工记录", "信息提示");
                        textBox1.Focus();
                        return;
                    }
                    else          //不重复时插入职工记录
                    {
                        mysql = "INSERT INTO 职工 VALUES( '" + textBox1.Text.Trim() + "','" +
                            textBox2.Text.Trim() + "','" +
                            xb + "','" +
                            textBox3.Text.Trim() + "','" +
                            textBox4.Text.Trim() + "','" +
                            textBox5.Text.Trim() + "')";
                          //  textBox1.Text.Trim() + ")";
                        //默认职工的初始密码与其职工号相同
                        mytable1 = CommDbOp.Exesql(mysql);
                        this.Close();
                    }
                }
                else               //修改职工记录
                {
                    mysql = "UPDATE 职工 SET 姓名='" + textBox2.Text.Trim() +
                        "',性别='" + xb +
                        "',年龄='" + textBox3.Text.Trim() +
                        "',所在部门='" + textBox4.Text.Trim() +
                        "',联系方式='" + textBox5.Text.Trim() +
                       // "',密码='" + textBox1.Text.Trim() +
                        "' WHERE 职工号='" + textBox1.Text.Trim() + "'";
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

        private void button2_Click(object sender, EventArgs e)   //取消
        {
            this.Close();
        }
    }
}
