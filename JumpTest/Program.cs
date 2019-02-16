using JumpTool;
using System;

namespace JumpTest
{
    class Program
    {
        [JumpTo("Entrypoint2", "A second entry point.")]
        public static void Ep2(int paramA, double paramB)
        {
            Console.WriteLine($"Episode 2: {paramA} {paramB}");
        }

        [JumpTo("Entrypoint1", "An entry point.")]
        public static void Entrypoint1(string ok)
        {
            Console.WriteLine($"Episode 1");
        }

        static void Main(string[] args)
        {
            Jump.Start(args, typeof(Program));
        }
    }
}
