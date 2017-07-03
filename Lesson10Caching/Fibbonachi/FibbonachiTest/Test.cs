namespace FibbonaciClass.ConsoleTest
{
    using System;
    using System.Diagnostics;

    class Test
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            Console.WriteLine("Test Fibonachi using System.Runtime.Caching ");
            timer.Start();
            Console.WriteLine(FibbonaciNumbers.Generate(7));
            timer.Stop();
            Console.WriteLine(string.Format("Ticks : {0}", timer.ElapsedTicks));
            Console.WriteLine();

            FibbonaciNumbers.Cacher = new InRedisCache();

			Console.WriteLine("Test Fibonachi using Redis");
            timer.Restart();
            Console.WriteLine(FibbonaciNumbers.Generate(7));
            timer.Stop();
            Console.WriteLine(string.Format("Ticks : {0}", timer.ElapsedTicks));
            Console.ReadLine();
        }
    }
}
