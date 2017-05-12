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
        float Chance_of_hitting = 0;
        float[] error;
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> okrug = new List<string>();
            string path = @"C:\Users\miked\Desktop\Колледжи.csv";
            string[] readline = File.ReadAllLines(path);          
            triger = false;
            button4.Enabled = true;
            klasterNew.Clear();
            klaster.Clear();
            Graphics g = pictureBox1.CreateGraphics();
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            List<float> X = new List<float>();
            List<float> Y = new List<float>();
            for (int i = 1; i < readline.Count(); i++)
            {              
                for (int j = 0; j < 3; j++)
                {
                    readline[i] = readline[i].Substring(readline[i].IndexOf(';')+1);
                }
                    readline[i] = readline[i].Substring(readline[i].IndexOf('"'));
                    readline[i] = readline[i].Substring(1);
                    okrug.Add(readline[i].Remove(readline[i].IndexOf('"')));
                readline[i] = readline[i].Substring(readline[i].IndexOf("37,"));
                X.Add((float.Parse(readline[i].Remove(readline[i].IndexOf('"'))) - 37)*100);
                readline[i] = readline[i].Substring(readline[i].IndexOf(';') + 2);
                Y.Add((float.Parse(readline[i].Remove(readline[i].IndexOf('"'))) - 55) * 100);
            }
            XGlobal = new float[readline.Count() - 1];
            YGlobal = new float[readline.Count() - 1];
            X.CopyTo(XGlobal);
            Y.CopyTo(YGlobal);
            for(int i = 0; i < XGlobal.Length; i++)
            {
                g.DrawEllipse(Pens.Black, Enlargement * XGlobal[i]+bias_horizon, Enlargement * YGlobal[i]+bias_vertical, width_height, width_height);
            }           
            klaster.Add(new coord((float)66.2131, (float)73.7036));//центральный 55.737036, 37.662131
            klaster.Add(new coord((float)56.5703, (float)81.4916));//Северный 55.814916, 37.565703
            klaster.Add(new coord((float)63.3725, (float)77.6371));//северо-восточный 55.776371, 37.633725
            klaster.Add(new coord((float)71.0262, (float)79.6901));//восточный 55.796901, 37.710262
            klaster.Add(new coord((float)71.5454, (float)75.4081));//юго-восточный 55.754081, 37.715454
            klaster.Add(new coord((float)66.5580, (float)71.0246));//южный 55.710246, 37.665580
            klaster.Add(new coord((float)57.8244, (float)66.2382));//юго-западный 55.662382, 37.578244
            klaster.Add(new coord((float)44.3237, (float)72.7203));//западный 55.727203, 37.443237
            klaster.Add(new coord((float)46.7837, (float)78.4440));//северо-западный 55.784440, 37.467837
            klaster.Add(new coord((float)21.5344, (float)99.0531));//зеленоградский 55.990531, 37.215344
            for (int i = 0; i < klaster.Count; i++)
            {
                    g.DrawRectangle(Pens.Black, Enlargement * klaster[i].X+bias_horizon, Enlargement * klaster[i].Y+bias_vertical, width_height, width_height);
            }
            klasterNew = main(klaster,XGlobal,YGlobal);
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
                        g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Enlargement * klaster[i].X+5+bias_horizon, Enlargement * klaster[i].Y+5+bias_vertical), new PointF(Enlargement * XGlobal[countsGlobal[i][j]]+5+bias_horizon, Enlargement * YGlobal[countsGlobal[i][j]]+5+bias_vertical));
                    }
                    else
                    {
                       g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Enlargement * klasterNew[i].X+5 + bias_horizon, Enlargement * klasterNew[i].Y+5 + bias_vertical), new PointF(Enlargement * XGlobal[countsGlobal[i][j]]+5 + bias_horizon, Enlargement * YGlobal[countsGlobal[i][j]]+5 + bias_vertical));
                    }
                       
                }
            }
            string[] okrugDef = { "Центральный административный округ",  //центральный 55.737036, 37.662131
                "Северный административный округ",                       //Северный 55.814916, 37.565703
                "Северо-Восточный административный округ",               //северо-восточный 55.776371, 37.633725
                "Восточный административный округ",                      //восточный 55.796901, 37.710262
                "Юго-Восточный административный округ",                   //юго-восточный 55.754081, 37.715454
                "Южный административный округ",                          //южный 55.710246, 37.665580
                "Юго-Западный административный округ",                  //юго-западный 55.662382, 37.578244
                "Западный административный округ",                       //западный 55.727203, 37.443237
                "Северо-Западный административный округ",                //северо-западный 55.784440, 37.467837
                "Зеленоградский административный округ" };              //зеленоградский 55.990531, 37.215344
            Chance_of_hitting = 0;
            error = new float[okrugDef.Count()];
            float ch_error = 0 ;
            for (int j = 0; j < okrugDef.Count(); j++)
            {
                for (int i = 0; i < countsGlobal[j].Count(); i++)
                {
                    if (okrug[countsGlobal[j][i]] == okrugDef[j])
                    {
                        Chance_of_hitting++;
                        ch_error++;
                    }
                }
                error[j] = (ch_error / countsGlobal[j].Count)*100;
                ch_error = 0;
            }
            button1.Enabled = false;
            button10.Enabled = true;
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
            string[] okrug = {"Центральный", "Северный","Северо-восточный","Восточный","Юго-западный","Южный","Юго-западный","Западный","Северо-западный" };
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(savefile.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    for (int i = 0; i < countsGlobal.Count; i++)
                    {
                        sw.WriteLine("{0} округ", okrug[i]);
                        for (int j = 0; j < countsGlobal[i].Count; j++)
                        {
                            if (klasterNew[i].Equals(new coord(float.NaN, float.NaN)))
                            {
                                
                            }
                            else
                            {
                                sw.WriteLine("X = 37.{0} Y = 55.{1} ", XGlobal[countsGlobal[i][j]], YGlobal[countsGlobal[i][j]]);
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
                    g.DrawLine(new Pen(Brushes.Black, 1), new PointF(Enlargement * i + 5 , Enlargement * 0), new PointF(Enlargement * i + 5, pictureBox1.Height));
                }
                for (int i = 0; i < pictureBox1.Height; i++)
                {
                    g.DrawLine(new Pen(Brushes.Black, 1), new PointF(0, Enlargement * i + 5), new PointF(pictureBox1.Width, Enlargement * i + 5));

                }
                triger = true;
            }
            else
            {
                triger = false;
                button1_Click(sender, e);
            }
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string[] okrug = { "Центральный административный округ", "Северный административный округ", "Северо-Восточный административный округ", "Восточный административный округ", "Юго-Западный административный округ", "Южный административный округ", "Юго-Восточный административный округ", "Западный административный округ", "Северо-Западный административный округ", "Зеленоградский административный округ" };

            float result = (Chance_of_hitting / XGlobal.Count())*100;
            string output= result+"% общее попадание \n";
            for(int i = 0; i < error.Count(); i++)
            {
                output = output + error[i] + "% "+ okrug[i] +" \n";
            }
                {
                    for (int i = 0; i < okrug.Count(); i++)
                    {
                        output=output +okrug[i]+"\n";
                        for (int j = 0; j < countsGlobal[i].Count; j++)
                        {
                            if (klasterNew[i].Equals(new coord(float.NaN, float.NaN)))
                            {

                            }
                            else
                            {
                                output = output +"|"+ (j+1) + ") X = 37." + 100000*XGlobal[countsGlobal[i][j]] + " Y = 55." + 100000*YGlobal[countsGlobal[i][j]]+" ";        
                            }
                        }
                    output = output + "\n";
                    }

                }          
            MessageBox.Show(output);
        }
    }
}
