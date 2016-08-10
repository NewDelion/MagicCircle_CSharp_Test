using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicCircleMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button5.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = true;
            button2.Enabled = true;
            button1.Enabled = false;
            button4.Enabled = false;
            button8.Enabled = false;
            groupBox3.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)//再生
        {

        }

        private void button6_Click(object sender, EventArgs e)//停止
        {

        }

        private void button7_Click(object sender, EventArgs e)//リセット
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//ノードを追加
        {
            NewMC mc = new NewMC();
            mc.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)//子ノードを追加
        {

        }

        private void button4_Click(object sender, EventArgs e)//ノードを削除
        {

        }

        private void button8_Click(object sender, EventArgs e)//ノードを親の階層に接続する
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)//radius
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)//distance from center
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//has frame
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//rotate
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)//rotation amount
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)//initial rotation
        {

        }

        private void button9_Click(object sender, EventArgs e)//color
        {

        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)//reduction rate
        {

        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)//tail length
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)//fill color
        {

        }
    }
}
