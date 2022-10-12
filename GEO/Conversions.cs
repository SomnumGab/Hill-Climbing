using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO
{
    class Conversions
    {
        int CalcL(double a, double b, double accuracy)
        {
            var temp = ((b - a) / accuracy) + 1;
            return (int)Math.Ceiling(Math.Log(temp, 2));
        }
        public int ToInt(double a, double b, double xreal, double accuracy)
        {
            var temp = 1 / (b - a) * (xreal - a) * (Math.Pow(2, (CalcL(a, b, accuracy))) - 1);
            return (int)temp;
        }
        public double ToReal(double a, double b, int xint, double accuracy, int decimals)
        {
            var maf = Math.Pow(2, (CalcL(a, b, accuracy))) - 1;
            var temp = ((xint * (b - a)) / maf) + a;
            return Math.Round(temp, decimals);
        }
        public String ToBin(double a, double b, int xint, double accuracy)
        {
            var l = CalcL(a, b, accuracy);
            String binary = Convert.ToString(xint, 2);
            if (binary.Length == l) return binary;
            else
            {
                var missingZeros = l - binary.Length;
                return new string('0', missingZeros) + binary;
            }
        }
        public int ToIntFromBin(string binary)
        {
            return Convert.ToInt32(binary, 2);
        }

        public double Feval(double xreal)
        {
            return (xreal - (Math.Truncate(xreal))) * (Math.Cos(20 * Math.PI * xreal) - Math.Sin(xreal));
        }
    }
}
