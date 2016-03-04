using System;
using System.Collections.Generic;
using System.Linq;
using LinqStatistics;

namespace LinqStatisticsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<decimal> data = new decimal[] { 2, 5, 7.11m, 3.12m, 3.14m, 1.99m, 0.99m, 3, 2, -2, 5.71m, -3.446m, 2.11m };
            IEnumerable<decimal> data2 = new decimal[] { 3, 4.11m, 0.23m, 4, 1, 0, 0, 2.11m, 3, 3, -1, 0 , 1 };

            Console.WriteLine("Greater than 1 count = {0}", data.Where(x => x > 1).Count());
            Console.WriteLine("Count = {0}", data.Count());
            Console.WriteLine("Average = {0}", data.Average());
            Console.WriteLine("Median = {0}", data.Median());
            Console.WriteLine("Mode = {0}", data.Mode());
            Console.WriteLine("Sample Variance = {0}", data.Variance());
            Console.WriteLine("Sample Standard Deviation = {0}", data.StandardDeviation());
            Console.WriteLine("Population Variance = {0}", data.VarianceP());
            Console.WriteLine("Population Standard Deviation = {0}", data.StandardDeviationP());
            Console.WriteLine("Range = {0}", data.Range());

            Console.WriteLine("Pearson: {0}", data.Pearson(data2));
        }
    }
}
