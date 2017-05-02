using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
        int bias_horizon = 0;
        int bias_vertical = 0;
        bool triger = false;
        List<coord> klasterNew = new List<coord>();
        List<coord> klaster = new List<coord>();
        float[] XGlobal;
        float[] YGlobal;
        private void button1_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            klasterNew.Clear();
            klaster.Clear();
            Graphics g = pictureBox1.CreateGraphics();
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            float[] X = {85,65,62,65,69,60,43,39,62,79,62,57,56,53,63,79,58,55,65,66,61,34,77,70,51,44,70,69,65,63,61,63,51,58,55,58,60,67,58,71,82,70,78,77,81,70,75,74,64,63,58,66,62,66,66,69,49,57,26,38,39,44,41,19,50,72,70,65,51,63};
            float[] Y = {68,71,73,72,68,71,82,79,80,80,73,67,76,73,75,79,75,84,88,59,80,85,71,81,82,84,61,77,78,72,76,77,80,80,85,81,79,87,80,78,78,79,79,82,79,82,69,73,59,70,70,60,62,60,60,61,62,68,61,72,73,83,86,97,87,80,88,73,83,63};           
          
          //  float[] X = { 9, 32, 50, 51, 83, 31, 48, 85, 10, 30, 89, 37, 10, 5, 62, 35, 99, 62, 3, 68, 59, 36, 92, 82, 87, 80, 42, 74, 20, 84, 32, 61, 97, 7, 41, 56, 59, 98, 80, 64, 49, 48, 67, 98, 81, 93, 68, 33, 5, 96 };
            XGlobal = X;
           // float[] Y = { 31, 75, 6, 93, 80, 53, 33, 16, 94, 0, 35, 5, 96, 30, 25, 51, 20, 9, 26, 29, 69, 72, 69, 89, 7, 29, 83, 41, 96, 48, 55, 99, 81, 75, 55, 82, 32, 22, 29, 52, 44, 91, 10, 17, 98, 46, 16, 79, 32, 73 };
            YGlobal = Y;
            for(int i = 0; i < X.Length; i++)
            {
                g.DrawEllipse(Pens.Black, Enlargement * X[i]+bias_horizon, Enlargement * Y[i]+bias_vertical, width_height, width_height);
            }           
            klaster.Add(new coord(66, 73));//центральный 55.737036, 37.662131
            klaster.Add(new coord(56, 81));//Северный 55.814916, 37.565703
            klaster.Add(new coord(63, 77));//северо-восточный 55.776371, 37.633725
            klaster.Add(new coord(71, 79));//восточный 55.796901, 37.710262
            klaster.Add(new coord(71, 75));//юго-восточный 55.754081, 37.715454
            klaster.Add(new coord(66, 71));//южный 55.710246, 37.665580
            klaster.Add(new coord(57, 66));//юго-западный 55.662382, 37.578244
            klaster.Add(new coord(44, 72));//западный 55.727203, 37.443237
            klaster.Add(new coord(46, 78));//северо-западный 55.784440, 37.467837
            for (int i = 0; i < klaster.Count; i++)
            {
                    g.DrawRectangle(Pens.Black, Enlargement * klaster[i].X+bias_horizon, Enlargement * klaster[i].Y+bias_vertical, width_height, width_height);
            }
            klasterNew = main(klaster,X,Y);
            for (int i = 0; i < klaster.Count; i++)
            {
                if (klasterNew[i].Equals(new coord(float.NaN,float.NaN)))
                {
                    g.FillRectangle(new SolidBrush(Color.Red), Enlargement * klaster[i].X+bias_horizon, Enlargement * klaster[i].Y+bias_vertical, width_height, width_height);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.Red), Enlargement * klasterNew[i].X+bias_horizon, Enlargement * klasterNew[i].Y+bias_vertical, width_height, width_height);
                    g.DrawLine(new Pen(Brushes.Green, 3), new PointF(Enlargement * klaster[i].X + 5 + bias_horizon, Enlargement * klaster[i].Y + 5 + bias_vertical), new PointF(Enlargement * klasterNew[i].X + 5 + bias_horizon, Enlargement * klasterNew[i].Y + 5 + bias_vertical));

                }               
            }
            for (int i = 0; i < countsGlobal.Count; i++)
            {
                for(int j = 0; j < countsGlobal[i].Count; j++)
                {
                    if (klasterNew[i].Equals(new coord(float.NaN, float.NaN)))
                    {
                        g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Enlargement * klaster[i].X+5+bias_horizon, Enlargement * klaster[i].Y+5+bias_vertical), new PointF(Enlargement * X[countsGlobal[i][j]]+5+bias_horizon, Enlargement * Y[countsGlobal[i][j]]+5+bias_vertical));
                    }
                    else
                    {
                       g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Enlargement * klasterNew[i].X+5 + bias_horizon, Enlargement * klasterNew[i].Y+5 + bias_vertical), new PointF(Enlargement * X[countsGlobal[i][j]]+5 + bias_horizon, Enlargement * Y[countsGlobal[i][j]]+5 + bias_vertical));
                    }
                       
                }
            }

            button1.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (triger == true)
            {
                Enlargement++;
                button1_Click(sender, e);
            }
         //   else
            {
        //        button9_Click(sender, e);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
        //    if (triger == true)
            {
                if (Enlargement > 1)
                {
                    Enlargement--;
                }
                button1_Click(sender, e);
            }
          //  else
            {
        //        button9_Click(sender, e);
            }
            
        }
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(savefile.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    for (int i = 0; i < countsGlobal.Count; i++)
                    {
                        sw.WriteLine("Принадлежат {0} кластеру", i+1);
                        for (int j = 0; j < countsGlobal[i].Count; j++)
                        {
                            if (klasterNew[i].Equals(new coord(float.NaN, float.NaN)))
                            {
                                
                            }
                            else
                            {
                                sw.WriteLine("X = {0} Y = {1} ", XGlobal[countsGlobal[i][j]], YGlobal[countsGlobal[i][j]]);
                            }

                        }
                    }
                   
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          //  if (triger == true)
            {
                bias_vertical -= 50;
                button1_Click(sender, e);
            }
         //   else
            {
         //       button9_Click(sender, e);
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {          
          //  if (triger == true)
            {
                bias_horizon -= 50;
                button1_Click(sender, e);
            }
        //    else
            {
//button9_Click(sender, e);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
        //    if (triger == true)
            {
                bias_vertical += 50;
                button1_Click(sender, e);
            }
       //     else
            {
        //        button9_Click(sender, e);
            }
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
         //   if (triger == true)
            {
                bias_horizon += 50;
                button1_Click(sender, e);
            }
         //   else
            {
       //         button9_Click(sender, e);
            }
            
        }
        
        private void button9_Click(object sender, EventArgs e)
        {
            if (triger == false)
            {
                Graphics g = pictureBox1.CreateGraphics();
                for (int i = 0; i < pictureBox1.Width; i++)
                {
                    g.DrawLine(new Pen(Brushes.Black, 1), new PointF(Enlargement * i * 1 + 5 + bias_horizon, Enlargement * 0), new PointF(Enlargement * i * 1 + 5 + bias_horizon, Enlargement * pictureBox1.Height));
                }
                for (int i = 0; i < pictureBox1.Height; i++)
                {
                    g.DrawLine(new Pen(Brushes.Black, 1), new PointF(0, Enlargement * i * 1 + 5 + bias_vertical), new PointF(pictureBox1.Width, Enlargement * i * 1 + 5 + bias_vertical));

                }
                triger = true;
            }
            else
            {
                triger = false;
                button1_Click(sender, e);
            }
           
        }
    }
}
