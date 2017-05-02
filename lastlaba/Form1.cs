using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lastlaba
{
    public partial class Form1 : Form
    {
       public struct coord
        {
        public float X;
        public float Y;
            public coord(float X, float Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        static float Center_of_mass(List<int> temp, float[] mas)
        {
            float coord = 0;
            for (int i = 0; i < temp.Count; i++)
            {
                coord += mas[temp[i]];
            }
            return coord / temp.Count;
        }
        static float Adaptive_adder(float[,] w, float[]x, int j,int m)
        {
            float y = 0;
            for (int i = 0; i < m; i++)
            {
                y += w[j,i] * x[i];
            }
            return y;
        }
       static List<List<int>> countsGlobal = new List<List<int>>();
        static List<coord> main(List<coord> klaster, float[] X, float[] Y)
        {
            countsGlobal.Clear();
            List<coord> Cor = new List<coord>();
            //float[] X = { 85, 65, 62, 65, 69, 60, 43, 39, 62, 79, 62, 57, 56, 53, 63, 79, 58, 55, 65, 66, 61, 34, 77, 70, 51, 44, 70, 69, 65, 63, 61, 63, 51, 58, 55, 58, 60, 67, 58, 71, 82, 70, 78, 77, 81, 70, 75, 74, 64, 63, 58, 66, 62, 66, 66, 69, 49, 57, 26, 38, 39, 44, 41, 19, 50, 72, 70, 65, 51, 63 };
           // float[] Y = { 68, 71, 73, 72, 68, 71, 82, 79, 80, 80, 73, 67, 76, 73, 75, 79, 75, 84, 88, 59, 80, 85, 71, 81, 82, 84, 61, 77, 78, 72, 76, 77, 80, 80, 85, 81, 79, 87, 80, 78, 78, 79, 79, 82, 79, 82, 69, 73, 59, 70, 70, 60, 62, 60, 60, 61, 62, 68, 61, 72, 73, 83, 86, 97, 87, 80, 88, 73, 83, 63 };
            float[,] W = new float[klaster.Count, X.Length];
            List<double> dist = new List<double>();
            for(int j = 0; j < klaster.Count; j++)
            {
                for (int i = 0; i < X.Length; i++)
                {
                    W[j, i] = distance(klaster[j].X, klaster[j].Y, X[i], Y[i]);                  
                }
            }
            for(int i = 0; i < klaster.Count; i++) { countsGlobal.Add(new List<int>()); }          
            for (int i = 0; i < X.Length; i++)
            {
                float Attitudes = float.MaxValue;
                int count = 0;
                for (int j = 0; j < klaster.Count; j++)
                {
                    if (W[j, i] < Attitudes)
                    {
                        Attitudes = W[j, i];
                        count = j;
                    }
                    
                }
                countsGlobal[count].Add(i);
            }
            List<coord> klasterNew = new List<coord>();
            for (int i = 0; i < klaster.Count; i++)
            {
                klasterNew.Add(new coord(Center_of_mass(countsGlobal[i], X), Center_of_mass(countsGlobal[i], Y)));
            }
            if (klaster.SequenceEqual(klasterNew))
            {
                return klaster;
            }
            else { main(klasterNew,X,Y); }
            return klasterNew;
        }
        static float distance(double x_central, double y_central,double x_point, double y_point)
        {
            return (float)Math.Sqrt(Math.Pow(Math.Abs(x_central-x_point), 2) + Math.Pow(Math.Abs(y_central-y_point),2)); ;
        }
        int Enlargement = 1;
        int width_height = 10;
       
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            //float[] X = {85,65,62,65,69,60,43,39,62,79,62,57,56,53,63,79,58,55,65,66,61,34,77,70,51,44,70,69,65,63,61,63,51,58,55,58,60,67,58,71,82,70,78,77,81,70,75,74,64,63,58,66,62,66,66,69,49,57,26,38,39,44,41,19,50,72,70,65,51,63};
            //float[] Y = {68,71,73,72,68,71,82,79,80,80,73,67,76,73,75,79,75,84,88,59,80,85,71,81,82,84,61,77,78,72,76,77,80,80,85,81,79,87,80,78,78,79,79,82,79,82,69,73,59,70,70,60,62,60,60,61,62,68,61,72,73,83,86,97,87,80,88,73,83,63};           
            List<double> dist = new List<double>();
            float[] X = { 6, 16, 40, 34, 98, 12, 37, 2, 21, 77, 91, 29, 2, 83, 68, 81, 72, 4, 79, 15 };
            float[] Y = { 99, 47, 9, 14, 85, 31, 68, 67, 48, 20, 19, 81, 53, 100, 14, 16, 80, 90, 20, 42 };
            for(int i = 0; i < X.Length; i++)
            {
                g.DrawEllipse(Pens.Black, Enlargement * X[i], Enlargement * Y[i], width_height, width_height);
            }
            List<coord> klaster = new List<coord>();
            klaster.Add(new coord(0, 0));
            klaster.Add(new coord(0, 50));
            klaster.Add(new coord(0, 100));
            klaster.Add(new coord(50, 0));
            klaster.Add(new coord(50, 50));
            klaster.Add(new coord(50, 100));
            klaster.Add(new coord(100, 0));
            klaster.Add(new coord(100, 50));
            klaster.Add(new coord(100, 100));
            for (int i = 0; i < klaster.Count; i++)
            {
                    g.DrawRectangle(Pens.Black, Enlargement * klaster[i].X, Enlargement * klaster[i].Y, width_height, width_height);
            }
            List<coord> klasterNew = main(klaster,X,Y);
            for (int i = 0; i < klaster.Count; i++)
            {
                if (klasterNew[i].Equals(new coord(float.NaN,float.NaN)))
                {
                    g.FillRectangle(new SolidBrush(Color.Red), Enlargement * klaster[i].X, Enlargement * klaster[i].Y, width_height, width_height);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.Red), Enlargement * klasterNew[i].X, Enlargement * klasterNew[i].Y, width_height, width_height);                   
                }               
            }
            for (int i = 0; i < countsGlobal.Count; i++)
            {
                for(int j = 0; j < countsGlobal[i].Count; j++)
                {
                    if (klasterNew[i].Equals(new coord(float.NaN, float.NaN)))
                    {
                        g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Enlargement * klaster[i].X, Enlargement * klaster[i].Y), new PointF(Enlargement * X[countsGlobal[i][j]], Enlargement * Y[countsGlobal[i][j]]));
                    }
                    else
                    {
                       g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Enlargement * klasterNew[i].X, Enlargement * klasterNew[i].Y), new PointF(Enlargement * X[countsGlobal[i][j]], Enlargement * Y[countsGlobal[i][j]]));
                    }
                       
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Enlargement++;
            pictureBox1.Width += 100;
            pictureBox1.Height += 100;
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            button1_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Enlargement--;
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            button1_Click(sender, e);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
