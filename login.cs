using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WWS
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        //实现窗体移动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);



        bool beginMove = false;//初始化鼠标位置
        int currentXPosition;
        int currentYPosition;
        private void pass_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataTable mytable;
            DataTable mytable2;
            //DataTable mytable3;
            string mysql = "SELECT * FROM oper WHERE 用户名='" + textBox1.Text +
                "' AND 密码='" + textBox2.Text + "'";
            mytable = CommDbOp.Exesql(mysql);
            if (mytable.Rows.Count == 0)
            {
                mysql = "SELECT * FROM 职工 WHERE 职工号='" + textBox1.Text +
                "' AND 职工号='" + textBox2.Text + "'";
                mytable2 = CommDbOp.Exesql(mysql);
                if (mytable2.Rows.Count == 0)
                {




                    MessageBox.Show("不存在该用户");
                    this.Close();
                }

                else //职工用户
                {
                    TempData.userlevel = "职工";
                    TempData.no = textBox1.Text.Trim();
                    this.Hide();
                    Form myform = new main();
                    myform.ShowDialog();
                    this.Close();

                }





            }
            else
            {
                TempData.userlevel = mytable.Rows[0]["级别"].ToString().Trim();
                this.Hide();
                Form myform = new main();
                myform.ShowDialog();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private class BaseTextBox : TextBox
        {

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr LoadLibrary(string lpFileName);
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams prams = base.CreateParams;
                    if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
                    {
                        prams.ExStyle |= 0x020; // transparent 
                        prams.ClassName = "RICHEDIT50W";
                    }
                    return prams;
                }
            }




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }



        //鼠标移动
        //获取鼠标按下时的位置
        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            
            
            
                if (e.Button == MouseButtons.Left)
                {
                    beginMove = true;
                    currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                    currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
                }
            
        }

        //获取鼠标移动到的位置
        private void login_MouseMove(object sender, MouseEventArgs e)
        {
           
            
                if (beginMove)
                {
                    this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                    this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                    currentXPosition = MousePosition.X;
                    currentYPosition = MousePosition.Y;
                }
            
        }


        //释放鼠标时的位置
        private void login_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
                {
                    currentXPosition = 0; //设置初始状态
                    currentYPosition = 0;
                    beginMove = false;
                }
            }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}



    
     
      
      
    

