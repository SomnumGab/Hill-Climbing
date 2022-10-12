using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEO
{
    class Tests
    {
        public int[] result_count = new int[100];
        public int t;
        private int a = -4, b = 12, decimals = 3;
        private double accuracy = 0.001;
        public Tests(int t) 
        {
            Array.Clear(result_count, 0, 100);
            this.t = t;
        }

        public double[] DoTheTesting()
        {
            for (int i = 0; i < t; i++)
            {
                SingleTest();
            }
            int[] histogram = new int[100];
            histogram[0] = result_count[0];
            for (int i = 1; i < 100; i++)
            {
                histogram[i] = histogram[i - 1] + result_count[i];
            }
            double[] percentHistogram = new double[100];

            for (int i = 0; i < 100; i++)
            {
                percentHistogram[i] = (double)result_count[i] * (double)(100/t);
            }
            return percentHistogram;

        }
        public String GenerateARadnomOne(int a, int b, int decimals, double accuracy)
        {
            Conversions conv = new Conversions();
            Random rand = new Random();
            return conv.ToBin(a, b, conv.ToInt(a, b, Math.Round(rand.NextDouble() * (b - a) + a, decimals), accuracy), accuracy);
        }
        public List<String> GeneratePossibleMutations(String xbin, int a, int b, double accuracy)
        {
            Conversions conv = new Conversions();
            List<String> mutated = new List<string>();
            for (int i = 0; i < xbin.Length; i++)
            {
                mutated.Add(MutateAt(xbin, i));
            }
            return mutated;
        }
        private String MutateAt(String xbin, int position)
        {
            if (xbin.ElementAt(position) == '1')
            {
                StringBuilder sb = new StringBuilder(xbin);
                sb[position] = '0';
                xbin = sb.ToString();
            }
            else
            {
                StringBuilder sb = new StringBuilder(xbin);
                sb[position] = '1';
                xbin = sb.ToString();
            }
            return xbin;
        }
        private void SingleTest()
        {
            for (int i = 0; i < 100; i++)
            {
                Conversions conv = new Conversions();
                var vc = GenerateARadnomOne(a, b, decimals, accuracy);
                var local = false;
                var fvc = conv.Feval(conv.ToReal(a, b, conv.ToIntFromBin(vc), accuracy, decimals));

                while (!local)
                {
                    var possibleMutations = GeneratePossibleMutations(vc, a, b, accuracy);
                    List<Double> fevals = new List<double>();
                    for (int y = 0; y < possibleMutations.Count; y++) fevals.Add(conv.Feval(conv.ToReal(a, b, conv.ToIntFromBin(possibleMutations[y]), accuracy, decimals)));
                    var vm = fevals[0]; var index = 0;
                    for (int y = 1; y < fevals.Count; y++)
                    {
                        if (vm < fevals[y])
                        {
                            vm = fevals[y];
                            index = y;
                        }
                    }
                    if (fvc < vm)
                    {
                        vc = possibleMutations[index];
                        fvc = vm;
                    }
                    else
                    {
                        local = true;
                    }

                }
                if (Math.Round(fvc, 1) == 10.9 || Math.Round(fvc, 1) == 4.9 || Math.Round(fvc, 1) == 1.9)
                {
                    result_count[i] += 1;
                    break;
                }
            }
        }

    }
}
