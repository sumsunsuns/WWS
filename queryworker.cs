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
    public partial class queryworker : Form
    {
        public DataView mydv = new DataView();
        public DataTable mytable = new DataTable();
        public DataTable mytable1 = new DataTable();
        public string condstr = "";           //存放过滤条件,初始时为空
        public queryworker()
        {
            InitializeComponent();
        }


        private void queryworker_Load(object sender, EventArgs e)  //初始化
        {
            mytable.Clear();
            if (condstr != "")
                mytable = CommDbOp.Exesql("SELECT * FROM 职工 WHERE " + condstr);
            else
                mytable = CommDbOp.Exesql("SELECT * FROM 职工");
            mydv = mytable.DefaultView;          //获得DataView对象mydv
            //以下设置dataGridView1的属性
            dataGridView1.DataSource = mydv;
            dataGridView1.ReadOnly = true;      //只读
            dataGridView1.GridColor = Color.RoyalBlue;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("隶书", 12);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //以下设置ComboBox1的绑定数据
            /* mytable1 = CommDbOp.Exesql("SELECT distinct 所在部门 FROM 职工");
             comboBox1.DataSource = mytable1;
             comboBox1.DisplayMember = "所在部门";
             ReSetButton_Click(sender, e);
             label1.Text = "满足条件的职工记录个数：" + mydv.Count.ToString();*/
        }

        private void label1_Click(object sender, EventArgs e)
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
                    condstr = condstr + " AND 姓名 Like '" + textBox2.Text.Trim() + "%'";
                else
                    condstr = "姓名 Like '" + textBox2.Text.Trim() + "%'";
            }
            if (radioButton1.Checked)
            {
                if (condstr != "")
                    condstr = condstr + " AND 性别='男'";
                else
                    condstr = "性别='男'";
            }
            else if (radioButton2.Checked)
            {
                if (condstr != "")
                    condstr = condstr + " AND 性别='女'";
                else
                    condstr = "性别='女'";
            }
           
            if (textBox3.Text != "")
            {
                if (condstr != "")
                    condstr = condstr + " AND 所在部门 Like '" + textBox3.Text.Trim() + "%'";
                else
                    condstr = "所在部门 Like '" + textBox3.Text.Trim() + "%'";
            }



            this.queryworker_Load(sender, e);
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; textBox2.Text = "";
            textBox3.Text = ""; 
            
            radioButton1.Checked = false; radioButton2.Checked = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    MessageBox.Show("职工号：" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Trim() + "\n" +
                    "姓名：" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Trim() + "\n" +
                    "性别：" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString().Trim() + "\n" +
                    "年龄：" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString().Trim() + "\n" +
                    "所在部门：" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString().Trim() + "\n" +
                    "联系方式：" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString().Trim() + "\n",
                    "选择的职工记录");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}











        

       
     

        




