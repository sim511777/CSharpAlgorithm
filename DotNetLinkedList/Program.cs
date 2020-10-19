using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLinkedList {
    class Program {
        static void Main(string[] args) {
            var list = new LinkedList<string>();
            list.AddLast("Apple");
            list.AddLast("Banana");
            list.AddLast("Lemon");

            var node = list.Find("Banana");
            var newNode = new LinkedListNode<string>("Grape");
            list.AddAfter(node, newNode);

            list.ToList().ForEach(p => Console.WriteLine(p));

            list.Remove("Grape");

            foreach (var m in list) {
                Console.WriteLine(m);
            }
        }
    }
}
