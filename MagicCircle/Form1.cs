using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicCircle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            magic.center_x = 200;
            magic.center_y = 200;
            magic.radius = 90;
            magic.distance_from_center = 0;
            magic.rotate = false;
            magic.has_frame = true;

            MC child1 = new MC()
            {
                radius = 24,
                distance_from_center = 0,
                rotate = false,
                has_frame = false
            };

            MC child2 = new MC()
            {
                radius = 33,
                distance_from_center = 57,
                rotate = true,
                rotation_amount = -1,
                has_frame = true
            };

            MC child3 = new MC()
            {
                radius = 10,
                distance_from_center = 17,
                rotate = true,
                rotation_amount = 5,
                has_frame = true
            };

            MC child4 = new MC()
            {
                radius = 35,
                distance_from_center = 90,
                rotate = false,
                rotation = -60,
                has_frame = true
            };

            MC_Orb child5 = new MC_Orb()
            {
                radius = 10,
                distance_from_center = 17,
                rotation_amount = 5
            };

            MC_Orb child6 = new MC_Orb()
            {
                radius = 5,
                distance_from_center = 57,
                rotation_amount = -1,
                color = Color.Transparent
            };

            MC_Tail child7 = new MC_Tail()
            {
                radius = 7,
                distance_from_center = 17,
                rotation_amount = 5,
                reduction_rate = 0.96f,
                tail_length = 40,
                fill = true,
            };
            MC_Tail child8 = new MC_Tail()
            {
                radius = 5,
                distance_from_center = 57,
                rotation_amount = 1,
                reduction_rate = 1,
                tail_length = 20,
                fill = true
            };
            MC_Star child9 = new MC_Star()
            {
                radius = 30,
                rotate = true,
                rotation_amount = 5,
                color = Color.Cyan,
                has_frame = true
            };

            magic.AddChild(child1);
            child6.AddChild(child9);
            magic.AddChild(child6);
            //child6.AddChild(child5);
            magic.AddChild(child4);

            magic.CalcChildCenter();//これによって追加した順番に依存していた初期中心座標が正しい座標に再設定される
        }

        MC magic = new MC();

        private void timer1_Tick(object sender, EventArgs e)
        {
            magic.Tick();
            var g = pictureBox1.CreateGraphics();
            var DoubleBufferedImage = magic.DrawDoubleBuffer(pictureBox1.Width, pictureBox1.Height);
            g.DrawImage(DoubleBufferedImage, 0, 0);
            DoubleBufferedImage.Dispose();
            g.Dispose();
            
        }
    }

    //半径
    //中心からの距離
    //回転の有無
    //回転量
    //初期角度
    //フレームの有無
    //
    //色
    //縮小率
    //尾の長さ
    //塗りつぶし
    //

    public class MC
    {
        public MC owner { get; set; }
        public float center_x { get; set; }
        public float center_y { get; set; }
        public float radius { get; set; }
        public float distance_from_center { get; set; }
        public bool rotate { get; set; }
        public float rotation { get; set; }
        public float rotation_amount { get; set; }
        public bool has_frame { get; set; }
        public Color color { get; set; }
        public List<MC> child { get; set; }


        public void AddChild(MC child)
        {
            if (this.child == null)
                this.child = new List<MC>();

            PointF center = GetPosition();
            child.center_x = center.X;
            child.center_y = center.Y;
            child.owner = this;
            this.child.Add(child);
        }

        public void CalcChildCenter()
        {
            if (this.child != null)
                this.child.ForEach(d => d.UpdateCenter());
        }

        public PointF GetPosition()
        {
            double radian = this.rotation * Math.PI / 180.0;
            float result_x = this.center_x + this.distance_from_center * (float)Math.Cos(radian);
            float result_y = this.center_y + this.distance_from_center * (float)Math.Sin(radian);

            return new PointF(result_x, result_y);
        }

        public void UpdateCenter()
        {
            var owner_center = this.owner.GetPosition();
            this.center_x = owner_center.X;
            this.center_y = owner_center.Y;
            if (this.child != null)
                this.child.ForEach(d => d.UpdateCenter());
        }

        public void Rotate()
        {
            this.rotation += this.rotation_amount;
            if (this.rotation > 360)
                this.rotation -= 360;
            if (this.rotation < 0)
                this.rotation += 360;
            if (this.child != null)
                this.child.ForEach(d => d.UpdateCenter());
        }

        public virtual void Tick()
        {
            if (this.rotate)
                Rotate();
            if (this.child != null)
                this.child.ForEach(d => d.Tick());
        }

        public virtual void Draw(Graphics g)
        {
            PointF pos = GetPosition();
            Pen pen = new Pen(this.color == Color.Empty ? Color.Red : this.color);
            if (this.has_frame)
                g.DrawEllipse(pen, pos.X - this.radius, pos.Y - this.radius, this.radius * 2, this.radius * 2);
            if (this.child != null)
                this.child.ForEach(d => d.Draw(g));
        }

        public Bitmap DrawDoubleBuffer(int width, int height)
        {
            Bitmap doubleBuffering_bitmap = new Bitmap(width, height);
            Graphics doubleBuffering_g = Graphics.FromImage(doubleBuffering_bitmap);
            doubleBuffering_g.Clear(Color.White);

            Draw(doubleBuffering_g);

            doubleBuffering_g.Dispose();
            return doubleBuffering_bitmap;
        }
    }

    public class MC_Orb : MC
    {
        public override void Tick()
        {
            Rotate();
            if (this.child != null)
                this.child.ForEach(d => d.Tick());
        }
        public override void Draw(Graphics g)
        {
            PointF pos = GetPosition();
            Brush brush = new SolidBrush(this.color == Color.Empty ? Color.LightGreen : this.color);
            g.FillEllipse(brush, pos.X - this.radius, pos.Y - this.radius, this.radius * 2, this.radius * 2);
            if (this.child != null)
                this.child.ForEach(d => d.Draw(g));
        }
    }

    public class MC_Tail : MC
    {
        public List<PointF> tail { get; set; }
        public float reduction_rate { get; set; }
        public int tail_length { get; set; }
        public bool fill { get; set; }

        public override void Tick()
        {
            Rotate();
            if (this.child != null)
                this.child.ForEach(d => d.Tick());
        }

        public override void Draw(Graphics g)
        {
            PointF pos = GetPosition();
            if (this.tail == null)
                this.tail = new List<PointF>();
            this.tail.Add(new PointF(pos.X, pos.Y));
            if (this.tail.Count > tail_length)
                this.tail.RemoveAt(0);
            float radius = this.radius;
            object color = null;
            if (this.fill)
                color = this.color == Color.Empty ? Brushes.LightGreen : new SolidBrush(this.color);
            else
                color = this.color == Color.Empty ? Pens.LightGreen : new Pen(this.color);
            foreach (PointF p in this.tail.Reverse<PointF>())
            {
                if (this.fill)
                    g.FillEllipse((Brush)color, p.X - radius, p.Y - radius, radius * 2, radius * 2);
                else
                    g.DrawEllipse((Pen)color, p.X - radius, p.Y - radius, radius * 2, radius * 2);
                radius *= this.reduction_rate;
            }
            if (this.child != null)
                this.child.ForEach(d => d.Draw(g));
        }
    }

    public class MC_Star : MC
    {
        public override void Draw(Graphics g)
        {
            //distance_from_center = 0
            List<float> vertex_theta = new float[] { 0, 72, 144, 216, 288 }.ToList();
            for (int i = 0; i < vertex_theta.Count; i++)
                vertex_theta[i] = vertex_theta[i] - 90 + this.rotation;
            this.distance_from_center = this.radius;
            float backup_rotation = this.rotation;
            var vertex_points = vertex_theta.Select(d =>
            {
                this.rotation = d;
                return GetPosition();
            }).ToArray();
            this.rotation = backup_rotation;
            Pen pen = new Pen(this.color == Color.Empty ? Color.YellowGreen : this.color);
            for (int i = 0; i < vertex_theta.Count; i++)
            {
                int start_index = i;
                int end_index = i + 2;
                if (end_index >= vertex_theta.Count)
                    end_index -= vertex_theta.Count;
                g.DrawLine(pen, vertex_points[start_index], vertex_points[end_index]);
            }
            if (this.has_frame)
                g.DrawEllipse(pen, center_x - this.radius, center_y - this.radius, this.radius * 2, this.radius * 2);
            if (this.child != null)
                this.child.ForEach(d => d.Draw(g));
        }
    }
}
