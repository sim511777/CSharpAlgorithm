using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetQueue {
    class Program {
        static void Main(string[] args) {
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            int data = q.Dequeue();
            Console.WriteLine(data);
            foreach (var item in q) {
                Console.WriteLine(item);
            }
        }
    }
}
