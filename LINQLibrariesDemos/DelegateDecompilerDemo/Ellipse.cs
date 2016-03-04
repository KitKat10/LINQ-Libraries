using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelegateDecompiler;

namespace DelegateDecompilerDemo
{
    public class Ellipse
    {
        public Ellipse(double a, double b)
        {
            A = a;
            B = b;
        }

        public double A { get; set; }

        public double B { get; set; }

        [Computed]
        public double Area
        {
            get
            {
                return A * B * Math.PI;
            }
        }
    }
}
