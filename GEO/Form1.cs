using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GEO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var decimals = comboBox1.SelectedIndex + 3;
            var accuracy = Math.Pow(10, (-1 * decimals));
            var a = Int32.Parse(textBox1.Text);
            var b = Int32.Parse(textBox2.Text);
            var T = Int32.Parse(textBox3.Text);
            Random rand = new Random();
            Conversions conv = new Conversions();
            HillClimbing hc = new HillClimbing();

           var best = hc.ClimbThemHills(a, b, decimals, accuracy, T);

            chart1.Series.Clear();

            chart1.Series.Add("f(xbest)");
            chart1.Series["f(xbest)"].ChartType = SeriesChartType.Line;
            chart1.Series.Add("f(vm)");
            chart1.Series["f(vm)"].ChartType = SeriesChartType.Line;
            for (int i = 0; i < hc.bestOnes.Count; i++)
            {
                chart1.Series["f(xbest)"].Points.AddY(hc.bestOnes[i]);
                chart1.Series["f(vm)"].Points.AddY(hc.VMs[i]);
            }


            label6.Text = "Najlepszy otrzymany wynik to - xreal:"+ conv.ToReal(a, b, conv.ToIntFromBin(best), accuracy, decimals)+" feval:"+ conv.Feval(conv.ToReal(a, b, conv.ToIntFromBin(best), accuracy, decimals))+ " xbin:"+ best;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tests te = new Tests(1000);
            var results = te.DoTheTesting();

            chart2.Series.Clear();

            chart2.Series.Add("Percent");
            chart2.Series["Percent"].ChartType = SeriesChartType.Column;

            for (int i = 0; i < results.Length; i++)
            {
                chart2.Series["Percent"].Points.AddY(results[i]);
            }

        }
    }
    public struct Possible
    {
        public int bitToChange;
        public double feval;
    }
}
