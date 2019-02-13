using JumpTool;
using System;

namespace JumpTest
{
    class Program
    {
        [JumpTo("Entrypoint2")]
        [JumpTo("Ep2")]
        public static void Ep2(int testA, double flex)
        {
            Console.WriteLine($"Episode 2: {testA}");
        }

        [JumpTo("Entrypoint1")]
        [JumpTo("Ep1")]
        public static void Entrypoint1()
        {
            Console.WriteLine("Episode 1");
        }

        static void Main(string[] args)
        {
            Jump.Start(args, typeof(Program));
        }
    }
}
