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
            button7.Enabled = false;
            button2.Enabled = true;
            button1.Enabled = false;
            button4.Enabled = false;
            button8.Enabled = false;
            groupBox3.Enabled = false;

            top_node = new MC()
            {
                name = (current_id++).ToString(),
                center_x = pictureBox1.Width / 2,
                center_y = pictureBox1.Height / 2,
                distance_from_center = 0,
                radius = 0,
                rotate = false,
                color = Color.Transparent,
                has_frame = false
            };

            //byte[] hash = new System.Security.Cryptography.SHA512CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes("test"));
            byte[] hash = new System.Security.Cryptography.SHA512CryptoServiceProvider().ComputeHash(BitConverter.GetBytes(Environment.TickCount));

            int used = Decode_from_Hash(hash, top_node);
            Console.WriteLine(used);

            foreach (var c in top_node.child)
            {
                treeView1.Nodes.Add(c.name, c.GetType().ToString());
                setTree(treeView1.Nodes[c.name]);
            }
        }

        public void setTree(TreeNode node)
        {
            var mc = GetCircle(node.Name, top_node);
            if (mc == null)
                return;
            if (mc.child == null)
                return;
            if (mc.child.Count == 0)
                return;
            foreach (var c in mc.child)
            {
                node.Nodes.Add(c.name, c.GetType().ToString());
                setTree(node.Nodes[c.name]);
            }
        }

        public int Decode_from_Hash(byte[] hash, MC target_top)
        {
            if (hash == null)
                return 0;
            if (hash.Length < 5)
                return 0;
            int use = 0;
            for (; use + 5 < hash.Length; )
            {
                //MC, Orb, Tail, Star, Hexagram
                int type = hash[use] % 5;
                int seed = BitConverter.ToInt32(hash, use + 1);//1~4
                use += 5;
                Random r = new Random(seed);
                switch (type)
                {
                    case 0:
                    {
                        var mc = new MC()
                        {
                            name = (current_id++).ToString(),
                            radius = r.Next(1, pictureBox1.Height * 5) / 10.0f,
                            distance_from_center = r.Next(0, pictureBox1.Height * 5) / 10.0f,
                            has_frame = r.Next() % 2 == 0,
                            rotate = r.Next() % 2 == 0,
                            rotation_amount = r.Next(0, 3599) / 10.0f,
                            rotation = r.Next(0, 3599) / 10.0f,
                            color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))
                        };
                        if (r.Next() % 5 == 0)
                            use += Decode_from_Hash(hash.Skip(use).ToArray(), mc);
                        target_top.AddChild(mc);
                        break;
                    }
                    case 1:
                    {
                        var mc = new MC_Orb()
                        {
                            name = (current_id++).ToString(),
                            radius = 20,//r.Next(1, pictureBox1.Height * 5) / 10.0f,
                            distance_from_center = r.Next(0, pictureBox1.Height * 5) / 10.0f,
                            rotation_amount = r.Next(0, 3599) / 10.0f,
                            rotation = r.Next(0, 3599) / 10.0f,
                            color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)),
                        };
                        if (r.Next() % 5 == 0)
                            use += Decode_from_Hash(hash.Skip(use).ToArray(), mc);
                        target_top.AddChild(mc);
                        break;
                    }
                    case 2:
                    {
                        var mc = new MC_Tail()
                        {
                            name = (current_id++).ToString(),
                            radius = r.Next(1, 300/*pictureBox1.Height * 5*/) / 10.0f,
                            distance_from_center = r.Next(0, pictureBox1.Height * 5) / 10.0f,
                            rotate = r.Next() % 2 == 0,
                            rotation_amount = r.Next(0, 3599) / 10.0f,
                            rotation = r.Next(0, 3599) / 10.0f,
                            reduction_rate = r.Next(0, 100) / 100f,
                            tail_length = r.Next(1, 3600),
                            fill = r.Next() % 2 == 0,
                            color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))
                        };
                        if (r.Next() % 5 == 0)
                            use += Decode_from_Hash(hash.Skip(use).ToArray(), mc);
                        target_top.AddChild(mc);
                        break;
                    }
                    case 3:
                    {
                        var mc = new MC_Star()
                        {
                            name = (current_id++).ToString(),
                            radius = r.Next(1, pictureBox1.Height * 5) / 10.0f,
                            has_frame = r.Next() % 2 == 0,
                            rotate = r.Next() % 2 == 0,
                            rotation_amount = r.Next(0, 3599) / 10.0f,
                            rotation = r.Next(0, 3599) / 10.0f,
                            color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))
                        };
                        if (r.Next() % 5 == 0)
                            use += Decode_from_Hash(hash.Skip(use).ToArray(), mc);
                        target_top.AddChild(mc);
                        break;
                    }
                    case 4:
                    {
                        var mc = new MC_Hexagram()
                        {
                            name = (current_id++).ToString(),
                            radius = r.Next(1, pictureBox1.Height * 5) / 10.0f,
                            has_frame = r.Next() % 2 == 0,
                            rotate = r.Next() % 2 == 0,
                            rotation_amount = r.Next(0, 3599) / 10.0f,
                            rotation = r.Next(0, 3599) / 10.0f,
                            color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))
                        };
                        if (r.Next() % 5 == 0)
                            use += Decode_from_Hash(hash.Skip(use).ToArray(), mc);
                        target_top.AddChild(mc);
                        break;
                    }
                }
                if (r.Next() % 4 == 0)
                    break;
            }
            return use;
        }

        MC top_node = null;
        Dictionary<string, float> backup_initial_rotation = new Dictionary<string, float>();
        int current_id = 0;

        public void Backup(MC mc)
        {
            backup_initial_rotation[mc.name] = mc.rotation;
            if (mc.child != null)
                foreach (MC c in mc.child)
                    Backup(c);
        }

        public void Restore(MC mc)
        {
            foreach (var key in backup_initial_rotation.Keys)
            {
                var c = GetCircle(key, mc);
                if (c != null)
                    c.rotation = backup_initial_rotation[key];
            }
        }

        private void button5_Click(object sender, EventArgs e)//再生
        {
            if (!button7.Enabled)
            {
                backup_initial_rotation.Clear();
                Backup(top_node);
            }

            button5.Enabled = false;
            button6.Enabled = true;
            button7.Enabled = false;
            groupBox3.Enabled = false;
            groupBox1.Enabled = false;
            treeView1.Enabled = false;

            
            

            timer1.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)//停止
        {
            button5.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = true;
            if (treeView1.SelectedNode != null)
                groupBox3.Enabled = true;
            groupBox1.Enabled = true;
            treeView1.Enabled = true;
            timer1.Enabled = false;

            if (treeView1.SelectedNode == null)
                return;
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            setControl_Initial_Rotation(target.rotate, target.rotation);
            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)//リセット
        {
            Restore(top_node);
            if (treeView1.SelectedNode == null)
                return;
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            setControl_Initial_Rotation(target.rotate, target.rotation);
            button7.Enabled = false;
            Draw();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            top_node.Tick();
            Draw();
        }

        public bool draw = true;
        public void Draw()
        {
            var double_buffering_bitmap = top_node.DrawDoubleBuffer(pictureBox1.Width, pictureBox1.Height);
            var g = pictureBox1.CreateGraphics();
            g.DrawImage(double_buffering_bitmap, 0, 0);
            double_buffering_bitmap.Dispose();
            g.Dispose();
        }

        public MC GetCircle(string name, MC mc)
        {
            if (mc.name.Equals(name))
                return mc;
            if (mc.child == null || mc.child.Count == 0)
                return null;
            foreach (MC c in mc.child)
            {
                var result =  GetCircle(name, c);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void button2_Click(object sender, EventArgs e)//ノードを追加
        {
            NewMC mc = new NewMC();
            mc.ShowDialog();
            if (mc.result == null)
                return;
            string name = "0";
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Level == 0)
                treeView1.Nodes.Add(current_id.ToString(), mc.result.GetType().ToString());
            else
            {
                treeView1.SelectedNode.Parent.Nodes.Add(current_id.ToString(), mc.result.GetType().ToString());
                name = treeView1.SelectedNode.Parent.Name;
            }

            
            var owner = GetCircle(name, top_node);
            mc.result.name = current_id.ToString();
            owner.AddChild(mc.result);

            current_id++;
            top_node.CalcChildCenter();
            Draw();
        }

        private void button1_Click(object sender, EventArgs e)//子ノードを追加
        {
            NewMC mc = new NewMC();
            mc.ShowDialog();
            if (mc.result == null)
                return;

            treeView1.SelectedNode.Nodes.Add(current_id.ToString(), mc.result.GetType().ToString());
            if (!treeView1.SelectedNode.IsExpanded)
                treeView1.SelectedNode.Expand();

            var owner = GetCircle(treeView1.SelectedNode.Name, top_node);
            mc.result.name = current_id.ToString();
            owner.AddChild(mc.result);

            current_id++;
            top_node.CalcChildCenter();
            Draw();
        }

        private void button4_Click(object sender, EventArgs e)//ノードを削除
        {
            if (MessageBox.Show("子ノードもまとめて削除されます。本当に削除しますか？", "確認", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                var target = GetCircle(treeView1.SelectedNode.Name, top_node);
                target.owner.child.Remove(target);
                treeView1.SelectedNode.Remove();
                button7.Enabled = false;
                Draw();
            }
        }

        private void button8_Click(object sender, EventArgs e)//ノードを親の階層に接続する
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            var owner = target.owner;
            target.owner.owner.AddChild(target);
            owner.child.Remove(target);

            if (treeView1.SelectedNode.Level > 1)
            {
                treeView1.SelectedNode.Parent.Parent.Nodes.Add((TreeNode)treeView1.SelectedNode.Clone());
                treeView1.SelectedNode.Remove();
            }
            else
            {
                treeView1.Nodes.Add((TreeNode)treeView1.SelectedNode.Clone());
                treeView1.SelectedNode.Remove();
            }
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                button1.Enabled = false;
                button4.Enabled = false;
                button8.Enabled = false;
                groupBox3.Enabled = false;
                return;
            }
            button1.Enabled = true;
            button4.Enabled = true;
            button8.Enabled = e.Node.Level > 0;
            groupBox3.Enabled = true;

            var target = GetCircle(e.Node.Name, top_node);
            if (target is MC_Orb)
                setEdit_MC_Orb((MC_Orb)target);
            else if (target is MC_Tail)
                setEdit_MC_Tail((MC_Tail)target);
            else if (target is MC_Star)
                setEdit_MC_Star((MC_Star)target);
            else if (target is MC_Hexagram)
                setEdit_MC_Hexagram((MC_Hexagram)target);
            else
                setEdit_MC(target);
        }

        public void setControl_Radius(bool enable, float value = 1)
        {
            label1.Enabled = enable;
            numericUpDown1.Enabled = enable;
            numericUpDown1.Value = (decimal)value;
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
            while (value > 359.9)
                value -= 360;
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

        public void setControl_Color(bool enable, Color color)
        {
            button9.Enabled = enable;
            button9.Text = string.Format("({0}, {1}, {2})", color.R, color.G, color.B);
        }

        public void setEdit_MC(MC target)
        {
            setControl_Radius(true, target.radius);
            setControl_Distance_from_center(true, target.distance_from_center);
            setControl_Rotate(true, target.rotate);
            setControl_Rotation_Amount(target.rotate, target.rotation_amount);
            setControl_Initial_Rotation(target.rotate, target.rotation);
            setControl_Has_Frame(true, target.has_frame);
            setControl_Reduction_rate(false);
            setControl_Tail_Length(false);
            setControl_Fill_Color(false);
            setControl_Color(true, target.color);
        }

        public void setEdit_MC_Orb(MC_Orb target)
        {
            setControl_Radius(true, target.radius);
            setControl_Distance_from_center(true, target.distance_from_center);
            setControl_Rotate(false, true);
            setControl_Rotation_Amount(true, target.rotation_amount);
            setControl_Initial_Rotation(true, target.rotation);
            setControl_Has_Frame(false, false);
            setControl_Reduction_rate(false);
            setControl_Tail_Length(false);
            setControl_Fill_Color(false);
            setControl_Color(true, target.color);
        }

        public void setEdit_MC_Tail(MC_Tail target)
        {
            setControl_Radius(true, target.radius);
            setControl_Distance_from_center(true, target.distance_from_center);
            setControl_Rotate(true, target.rotate);
            setControl_Rotation_Amount(target.rotate, target.rotation_amount);
            setControl_Initial_Rotation(target.rotate, target.rotation);
            setControl_Has_Frame(false, false);
            setControl_Reduction_rate(true, target.reduction_rate);
            setControl_Tail_Length(true, target.tail_length);
            setControl_Fill_Color(true, target.fill);
            setControl_Color(true, target.color);
        }

        public void setEdit_MC_Star(MC_Star target)
        {
            setControl_Radius(true, target.radius);
            setControl_Distance_from_center(false, 0);
            setControl_Rotate(true, target.rotate);
            setControl_Rotation_Amount(target.rotate, target.rotation_amount);
            setControl_Initial_Rotation(target.rotate, target.rotation);
            setControl_Has_Frame(true, target.has_frame);
            setControl_Reduction_rate(false);
            setControl_Tail_Length(false);
            setControl_Fill_Color(false);
            setControl_Color(true, target.color);
        }

        public void setEdit_MC_Hexagram(MC_Hexagram target)
        {
            setControl_Radius(true, target.radius);
            setControl_Distance_from_center(false, 0);
            setControl_Rotate(true, target.rotate);
            setControl_Rotation_Amount(target.rotate, target.rotation_amount);
            setControl_Initial_Rotation(target.rotate, target.rotation);
            setControl_Has_Frame(true, target.has_frame);
            setControl_Reduction_rate(false);
            setControl_Tail_Length(false);
            setControl_Fill_Color(false);
            setControl_Color(true, target.color);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)//radius
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            target.radius = decimal.ToSingle(numericUpDown1.Value);
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)//distance from center
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            target.distance_from_center = decimal.ToSingle(numericUpDown2.Value);
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//has frame
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            target.has_frame = checkBox2.Checked;
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//rotate
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            target.rotate = checkBox1.Checked;

            setControl_Initial_Rotation(target.rotate, target.rotation);
            setControl_Rotation_Amount(target.rotate, target.rotation_amount);
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)//rotation amount
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            target.rotation_amount = decimal.ToSingle(numericUpDown3.Value);
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)//initial rotation
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            target.rotation = decimal.ToSingle(numericUpDown4.Value);
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void button9_Click(object sender, EventArgs e)//color
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            colorDialog1.Color = target.color;

            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                target.color = colorDialog1.Color;
                button9.Text = string.Format("({0}, {1}, {2})", target.color.R, target.color.G, target.color.B);
                top_node.CalcChildCenter();
                if (target is MC_Tail)
                    ((MC_Tail)target).tail = null;
                button7.Enabled = false;
                Draw();
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)//reduction rate
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            if (target is MC_Tail)
                ((MC_Tail)target).reduction_rate = decimal.ToSingle(numericUpDown5.Value);
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)//tail length
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            if (target is MC_Tail)
                ((MC_Tail)target).tail_length = decimal.ToInt32(numericUpDown6.Value);
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)//fill color
        {
            var target = GetCircle(treeView1.SelectedNode.Name, top_node);
            if (target == null)
                return;
            if (target is MC_Tail)
                ((MC_Tail)target).fill = checkBox3.Checked;
            top_node.CalcChildCenter();
            if (target is MC_Tail)
                ((MC_Tail)target).tail = null;
            button7.Enabled = false;
            Draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool now = timer1.Enabled;
            if (now)
                timer1.Enabled = false;
            top_node.child = null;
            byte[] hash = new System.Security.Cryptography.SHA512CryptoServiceProvider().ComputeHash(BitConverter.GetBytes(Environment.TickCount));
            int used = Decode_from_Hash(hash, top_node);
            Console.WriteLine(used);
            treeView1.Nodes.Clear();
            foreach (var c in top_node.child)
            {
                treeView1.Nodes.Add(c.name, c.GetType().ToString());
                setTree(treeView1.Nodes[c.name]);
            }
            if (now)
                timer1.Enabled = true;
        }
    }
}
