using System;
using System.Threading;

namespace TestCmd
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Hello World!");
                Thread.Sleep(1000);
            }
        }
    }
}