using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmbLibTestCommandRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("line1");
            Console.WriteLine("line2");
            Console.WriteLine("line3");

            Console.Error.WriteLine("ERR1");
            Console.Error.WriteLine("ERR2");
            Console.Error.WriteLine("ERR3");

            Console.WriteLine("line4");
            Console.WriteLine("line5");
            Console.WriteLine("line6");

            Console.Error.WriteLine("ERR4");
            Console.Error.WriteLine("ERR5");
            Console.Error.WriteLine("ERR6");

        }
    }
}
