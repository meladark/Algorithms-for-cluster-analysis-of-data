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
        public Form1()
        {
            InitializeComponent();
        }
        static int Adaptive_adder(int[,] w, int[]x, int j,int m)
        {
            int y = 0;
            for (int i = 1; i < m; i++)
            {
                y += w[j, i] * x[i];
            }
            return y + w[j, 0];
        }
        static double distance(double x_central, double y_central,double x_point, double y_point)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(x_central-x_point), 2) + Math.Pow(Math.Abs(y_central-y_point),2)); ;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            int x = 100;
            int y = 100;
            int width = 10;
            int height = 10;
            g.DrawEllipse(Pens.Black, x, y, width, height);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
