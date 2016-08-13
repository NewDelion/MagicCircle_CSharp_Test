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
    public partial class NewMC : Form
    {
        public NewMC()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;

            setControl_Rotation_Amount(false);
            setControl_Initial_Rotation(false);

        }

        public void setControl_Distance_from_center(bool enable, float value = 0)
        {
            label2.Enabled = enable;
            numericUpDown2.Enabled = enable;
            numericUpDown2.Value = (decimal)value;
        }

        public void setControl_Has_Frame(bool enable, bool check = false)
        {
            checkBox2.Enabled = enable;
            checkBox2.Checked = check;
        }

        public void setControl_Rotate(bool enable, bool check = false)
        {
            checkBox1.Enabled = enable;
            checkBox1.Checked = check;
        }

        public void setControl_Rotation_Amount(bool enable, float value = 1)
        {
            label3.Enabled = enable;
            numericUpDown3.Enabled = enable;
            numericUpDown3.Value = (decimal)value;
        }

        public void setControl_Initial_Rotation(bool enable, float value = 0)
        {
            label4.Enabled = enable;
            numericUpDown4.Enabled = enable;
            numericUpDown4.Value = (decimal)value;
        }

        public void setControl_Reduction_rate(bool visible, float value = 0.96f)
        {
            label6.Visible = visible;
            numericUpDown5.Visible = visible;
            numericUpDown5.Value = (decimal)value;
        }

        public void setControl_Tail_Length(bool visible, int length = 30)
        {
            label7.Visible = visible;
            numericUpDown6.Visible = visible;
            numericUpDown6.Value = length;
        }

        public void setControl_Fill_Color(bool visible, bool check = false)
        {
            checkBox3.Visible = visible;
            checkBox3.Checked = check;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    setControl_Distance_from_center(true);
                    setControl_Rotate(true);
                    setControl_Has_Frame(true);
                    setControl_Reduction_rate(false);
                    setControl_Tail_Length(false);
                    setControl_Fill_Color(false);
                    break;
                case 1:
                    setControl_Distance_from_center(true);
                    setControl_Rotate(false, true);
                    setControl_Has_Frame(false, false);
                    setControl_Reduction_rate(false);
                    setControl_Tail_Length(false);
                    setControl_Fill_Color(false);
                    break;
                case 2:
                    setControl_Distance_from_center(true);
                    setControl_Rotate(true);
                    setControl_Has_Frame(false, false);
                    setControl_Reduction_rate(true);
                    setControl_Tail_Length(true);
                    setControl_Fill_Color(true);
                    break;
                case 3:
                case 4:
                    setControl_Distance_from_center(false, 0);
                    setControl_Rotate(true);
                    setControl_Has_Frame(true);
                    setControl_Reduction_rate(false);
                    setControl_Tail_Length(false);
                    setControl_Fill_Color(false);
                    break;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            setControl_Rotation_Amount(checkBox1.Checked, decimal.ToSingle(numericUpDown3.Value));
            setControl_Initial_Rotation(checkBox1.Checked, decimal.ToSingle(numericUpDown4.Value));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = this.color;
            
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.color = colorDialog1.Color;
                button1.Text = string.Format("({0}, {1}, {2})", this.color.R, this.color.G, this.color.B);
            }
        }

        public Color color { get; set; }
        public MC result = null;

        private void button2_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    this.result = new MC()
                    {
                        color = this.color,
                        distance_from_center = decimal.ToSingle(numericUpDown2.Value),
                        has_frame = checkBox2.Checked,
                        radius = decimal.ToSingle(numericUpDown1.Value),
                        rotate = checkBox1.Checked,
                        rotation = decimal.ToSingle(numericUpDown4.Value),
                        rotation_amount = decimal.ToSingle(numericUpDown3.Value),
                    };
                    break;
                case 1:
                    this.result = new MC_Orb()
                    {
                        color = this.color,
                        distance_from_center = decimal.ToSingle(numericUpDown2.Value),
                        radius = decimal.ToSingle(numericUpDown1.Value),
                        rotation = decimal.ToSingle(numericUpDown4.Value),
                        rotation_amount = decimal.ToSingle(numericUpDown3.Value)
                    };
                    break;
                case 2:
                    this.result = new MC_Tail()
                    {
                        color = this.color,
                        distance_from_center = decimal.ToSingle(numericUpDown2.Value),
                        radius = decimal.ToSingle(numericUpDown1.Value),
                        rotate = checkBox1.Checked,
                        rotation = decimal.ToSingle(numericUpDown4.Value),
                        rotation_amount = decimal.ToSingle(numericUpDown3.Value),
                        reduction_rate = decimal.ToSingle(numericUpDown5.Value),
                        tail_length = decimal.ToInt32(numericUpDown6.Value),
                        fill = checkBox3.Checked
                    };
                    break;
                case 3:
                    this.result = new MC_Star()
                    {
                        color = this.color,
                        has_frame = checkBox2.Checked,
                        radius = decimal.ToSingle(numericUpDown1.Value),
                        rotate = checkBox1.Checked,
                        rotation = decimal.ToSingle(numericUpDown4.Value),
                        rotation_amount = decimal.ToSingle(numericUpDown3.Value)
                    };
                    break;
                case 4:
                    this.result = new MC_Hexagram()
                    {
                        color = this.color,
                        has_frame = checkBox2.Checked,
                        radius = decimal.ToSingle(numericUpDown1.Value),
                        rotate = checkBox1.Checked,
                        rotation = decimal.ToSingle(numericUpDown4.Value),
                        rotation_amount = decimal.ToSingle(numericUpDown3.Value)
                    };
                    break;
            }

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewMC_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
