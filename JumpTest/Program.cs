using JumpTool;
using System;

namespace JumpTest
{
    using static Console;
    using static Jump;

    class Program
    {
        [JumpTo("Entrypoint2", "A second entry point.")]
        public static void Ep2(int paramA, double paramB)
        {
            WriteLine($"Episode 2: {paramA} {paramB}");
        }

        [JumpTo("Entrypoint1", "An entry point.")]
        public static void Entrypoint1(string ok)
        {
            WriteLine($"Episode 1");
        }

        static void Main(string[] args)
        {
            Start(args, typeof(Program));
        }
    }
}
