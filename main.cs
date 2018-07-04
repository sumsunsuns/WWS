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
    public partial class main : Form
    {
        public main()
        {

            InitializeComponent();
        }

        //嵌入子窗体
        private bool checkChildFrmExist(string childFrmName)
        {
            foreach (Form childFrm in this.MdiChildren)
            {
                if (childFrm.Name == childFrmName) //用子窗体的Name进行判断，如果存在则将他激活
                {
                    if (childFrm.WindowState == FormWindowState.Minimized)
                        childFrm.WindowState = FormWindowState.Normal;
                    childFrm.Activate();
                    return true;
                }
            }
            return false;
        }

        //检查窗口是否已打开
        private bool HaveOpened(string FormName)
        {
            bool bOpened = false;

            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Name == FormName)
                {
                    this.MdiChildren[i].Show();
                    this.MdiChildren[i].BringToFront();
                    bOpened = true;
                    break;
                }
            }
            return bOpened;
        }



        private void 查询数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("queryworker") == true)
            {
                return;
            }

            
            Form myform = new queryworker();
           myform.MdiParent = this;

            myform.Show();

            myform.Dock = DockStyle.Fill; //全屏
        }
        private void 编辑数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("editworker") == true)
            {
                return;
            }
            
            Form myform = new editworker();
            myform.MdiParent = this;
            myform.Show();
            myform.Dock = DockStyle.Fill;
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       


        private void 查询数据ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("querywork") == true)
            {
                return;
            }
            Form myform = new querywork();
            myform.MdiParent = this;
            myform.Show();
            myform.Dock = DockStyle.Fill;
            
        }
        private void 编辑数据ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("editwork") == true)
            {
                return;
            }
            Form myform = new editwork();
            myform.MdiParent = this;
            myform.Show();
            myform.Dock = DockStyle.Fill;
        }
        private void 统计数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("countwork") == true)
            {
                return;
            }
            Form myform = new countwork();
            myform.MdiParent = this;
            myform.Show();
            myform.Dock = DockStyle.Fill;
        }

        private void 系统用户编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 系统初始化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show(this, "本功能要清除系统中所有数据,真的初始化吗?", "确认初始化操作", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Yes)
            {
                CommDbOp.deldata("职工");
                CommDbOp.deldata("工作量");
                CommDbOp.deldata("oper");
                MessageBox.Show("系统初始化完毕,下次只能以1234/1234(用户名/口令)进入本系统", "信息提示");
            }
        }

        private void main_Load_1(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            if (TempData.userlevel == "职工")
            {
                编辑数据ToolStripMenuItem.Enabled = false;
                编辑数据ToolStripMenuItem1.Enabled = false;
                统计数据ToolStripMenuItem.Enabled = false;
                系统用户编辑ToolStripMenuItem.Enabled = false;
                系统初始化ToolStripMenuItem.Enabled = false;

            }
        }
    }
}



        

     

       

        

       

       

      

     
        

      

       
        

        

       
    
