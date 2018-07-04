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
    public partial class countwork : Form
    {
        DataView mydv = new DataView();
        DataTable mytable = new DataTable();
        DataTable mytable1 = new DataTable();
        string condstr = "";           //存放过滤条件,初始时为空
        public countwork()
        {
            InitializeComponent();
        }

        private void countwork_Load(object sender, EventArgs e)       //初始化
        {
            mytable.Clear();
            mytable = CommDbOp.Exesql("select  职工号,  " +
                "SUM(完成数量) 完成总量, RANK()OVER " +
                "(order by  SUM(完成数量)  desc) 名次 " +
                " from 工作量  group by  职工号  " +
                " order by   完成总量 desc");
            mydv = mytable.DefaultView;          //获得DataView对象mydv
            if (condstr != "") mydv.RowFilter = condstr;
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


            //以下设置ComboBox1的绑定数据
            // mytable1 = CommDbOp.Exesql("SELECT distinct 班号 FROM student");

            //  ReSetButton_Click(sender, e);
            // enbutton();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mysql;
            DataTable mytable1 = new DataTable();
            try
            {
                mysql = "select  职工号,  SUM(完成数量) 完成总量, " +
                    "RANK()OVER (order by SUM(完成数量) desc) 名次 " +
                    " from 工作量  group by  职工号   order by  完成总量 desc";

            mytable1 = CommDbOp.Exesql(mysql);

            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "信息提示");
            }

            this.countwork_Load(sender, e);




        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
