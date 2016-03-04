using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelegateDecompiler;

namespace DelegateDecompilerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Ellipse> e = new List<Ellipse>();
            e.Add(new Ellipse(2, 3));
            e.Add(new Ellipse(1, 4.75));
            e.Add(new Ellipse(6, 4));
            e.Add(new Ellipse(0.02, 2));
            e.Add(new Ellipse(1, 2));

            var greatEllipse = e.AsQueryable().Where(x => x.Area > 10);
            Console.WriteLine(greatEllipse.Count());

            var decompiled = greatEllipse.Decompile();
            Console.WriteLine(decompiled.ToString());

            var decompileCount = decompiled.Count();
            Console.WriteLine("Decompile count : " + decompileCount);
        }
    }
}
