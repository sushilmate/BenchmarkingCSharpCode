using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BenchmarkingCSharpCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var summery = BenchmarkRunner.Run<PerformanceChecker>();

            Console.WriteLine(summery);
            Console.ReadLine();
        }
    }

    [RankColumn]
    [MemoryDiagnoser]
    public class PerformanceChecker
    {
        public PerformanceChecker()
        {
            FillArray();
        }

        [GlobalSetup]
        private void FillArray()
        {
            var random = new Random();
            items = new List<int>(10000);

            for (int i = 0; i < 10000; i++)
            {
                items.Add(random.Next(1, 20000));
            }
        }

        public List<int> items { get; set; }
                
        [Benchmark]
        public long SumByLinq()
        {
            long sum = items.Sum();
            return sum;
        }

        [Benchmark]
        public long SumByForLoop()
        {
            long sum = 0;
            for (int i = 0; i < items.Count; i++)
            {
                sum += items[i];
            }
            return sum;
        }

        [Benchmark]
        public long SumByForeach()
        {
            long sum = 0;
            foreach (var item in items)
            {
                sum += item;
            }
            return sum;
        }
    }
}