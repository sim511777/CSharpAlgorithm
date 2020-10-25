using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetStack {
    class Program {
        static void Main(string[] args) {
            Stack<double> s = new Stack<double>();
            s.Push(1.25);
            s.Push(2.50);
            s.Push(3.75);

            double val = s.Pop();
            Console.WriteLine(val);
        }
    }
}
