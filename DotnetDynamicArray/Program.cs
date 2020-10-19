using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetDynamicArray {
    class Program {
        static void Main(string[] args) {
            List<int> a = new List<int>();
            for (int i = 0; i <= 17; i++) {
                a.Add(i);
                Console.WriteLine("{0}: {1}", i, a.Capacity);
            }
        }
    }
}
