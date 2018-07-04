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
    public partial class querywork : Form
    {
        public DataView mydv = new DataView();
        public DataTable mytable = new DataTable();
        public DataTable mytable1 = new DataTable();
        public string condstr = "";           //存放过滤条件,初始时为空
        
        public querywork()
        {
            InitializeComponent();
        }
        private void querywork_Load(object sender, EventArgs e)  //初始化
        {
            mytable.Clear();
            if (condstr != "")
                mytable = CommDbOp.Exesql("SELECT * FROM 工作量 WHERE " + condstr);
            else
                mytable = CommDbOp.Exesql("SELECT * FROM 工作量");
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

            
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
                        condstr = condstr + " AND 月份 Like '" + textBox2.Text.Trim() + "%'";
                    else
                        condstr = "月份 Like '" + textBox2.Text.Trim() + "%'";
                }
               

              
               



                this.querywork_Load(sender, e);
            }


         
            
                

            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                try
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        MessageBox.Show("职工号：" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Trim() + "\n" +
                        "月份：" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Trim() + "\n" +
                        "产品完成数量：" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString().Trim() + "\n",
                        "选择的工作量记录");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = ""; textBox2.Text = "";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
                this.Close();
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
 }


