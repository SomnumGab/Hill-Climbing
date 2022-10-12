using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO
{
    class HillClimbing
    {
        public List<Double> bestOnes = new List<double>();
        public List<Double> VMs = new List<double>();

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
        public String GenerateARadnomOne(int a, int b, int decimals, double accuracy)
        {
            Conversions conv = new Conversions();
            Random rand = new Random();
            return conv.ToBin(a, b, conv.ToInt(a, b, Math.Round(rand.NextDouble() * (b - a) + a, decimals), accuracy), accuracy);
        }

        public String Iterration(int a, int b, int decimals, double accuracy)
        {

            Conversions conv = new Conversions();
            var vc = GenerateARadnomOne(a, b, decimals, accuracy);
            var local = false;
            var fvc = conv.Feval(conv.ToReal(a, b, conv.ToIntFromBin(vc), accuracy, decimals));

            while(!local)
            {
                var possibleMutations = GeneratePossibleMutations(vc, a, b, accuracy);
                List<Double> fevals = new List<double>();
                for (int i = 0; i < possibleMutations.Count; i++) fevals.Add(conv.Feval(conv.ToReal(a, b, conv.ToIntFromBin(possibleMutations[i]), accuracy, decimals)));
                var vm = fevals[0]; var index = 0;
                for (int i = 1; i < fevals.Count; i++)
                {
                    if (vm < fevals[i])
                    {
                        vm = fevals[i];
                        index = i;
                    }

                }
                VMs.Add(vm);
                if (fvc < vm)
                {
                    vc = possibleMutations[index];
                    fvc = vm;
                }
                else
                {
                    local = true;
                }
                if (bestOnes.Count != 0)
                    if (bestOnes.Last() < fvc) bestOnes.Add(fvc);
                    else bestOnes.Add(bestOnes.Last());
                else bestOnes.Add(fvc);

            }
            return vc;
        }
        public String ClimbThemHills(int a, int b, int decimals, double accuracy, int T)
        {
            bestOnes = new List<double>();
            VMs = new List<double>();
            String best = "";
            for (int i = 0; i < T; i++)
            {
                best = Iterration(a, b, decimals, accuracy);
            }
            return best;
        }

    }
}
