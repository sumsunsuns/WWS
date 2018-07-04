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
    public partial class editwork : Form
    {
        DataView mydv = new DataView();
        DataTable mytable = new DataTable();
        DataTable mytable1 = new DataTable();
        string condstr = "";           //存放过滤条件,初始时为空
        public editwork()
        {
            InitializeComponent();
        }

        private void editwork_Load(object sender, EventArgs e)       //初始化
        {
            mytable.Clear();
            mytable = CommDbOp.Exesql("SELECT * FROM 工作量");
            mydv = mytable.DefaultView;          //获得DataView对象mydv
            if (condstr != "") mydv.RowFilter = condstr;
            //以下设置dataGridView1的属性
            dataGridView1.DataSource = mydv;
            dataGridView1.ReadOnly = true;      //只读
            dataGridView1.GridColor = Color.RoyalBlue;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("隶书", 8);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            
            
            enbutton();
        }
        private void enbutton()         //自定义过程
        {     //功能:当记录个数为0时不能使用修改和删除按钮
            label1.Text = "满足条件的数量：" + mydv.Count.ToString();
            if (mydv.Count == 0)
            {
                button4.Enabled = false;
                button3.Enabled = false;
                TempData.no = "";               //将要修改记录的职工号置空值
            }
            else
            {
                button4.Enabled = true;
                button3.Enabled = true;
                TempData.no = mydv[0]["职工号"].ToString().Trim();

                //将第一个职工记录置为当前记录
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //以下根据用户输入求得条件表达式condstr
            condstr = "";
            if (textBox1.Text != "")
                condstr = "职工号 Like '" + textBox1.Text.Trim() + "%'";
            if (textBox2.Text != "")
            {
                if (condstr != "")
                    condstr = condstr + " AND 月份 = '" + textBox2.Text.Trim() + "'";
                else
                    condstr = "月份 = '" + textBox2.Text.Trim() + "'";
            }


            this.editwork_Load(sender, e);
            enbutton();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TempData.flag = 1;                //TempData.flag为全局变量,传递给editwork1窗体
            Form myform = new editwork1();
            myform.ShowDialog();            //采用有模式方式调用
            this.editwork_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TempData.flag = 2;               //TempData.flag为全局变量,传递给editworker1窗体
            if (TempData.no != "")
            {
                Form myform = new editwork1();
                myform.ShowDialog();        //采用有模式方式调用
                this.editwork_Load(sender, e);
                dataGridView1.Refresh();
            }
            else
                MessageBox.Show("先选择要修改的职工记录", "信息提示");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TempData.flag = 3;
            if (TempData.no != "")
            {
                if (MessageBox.Show("真的要删除职工号为" + TempData.no + "的工作量记录吗?",
                    "删除确认",
                    MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    TempData.flag = 3;
                    string mysql = "DELETE 工作量 WHERE 职工号='" + TempData.no.Trim() + "'and 月份='"+
                        textBox2.Text.Trim()+"'";
                    mytable1 = CommDbOp.Exesql(mysql);
                    this.editwork_Load(sender, e);
                }
            }
            else
                MessageBox.Show("先选择要删除的职工记录", "信息提示");
        }

        private void editwork_Load_1(object sender, EventArgs e)
        {
         
        }
    }
}
