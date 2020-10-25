using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetConcurrentStatck {
    class Program {
        static void Main(string[] args) {
            var s = new ConcurrentStack<int>();

            Task tPush = Task.Factory.StartNew(() => {
                for (int i = 0; i < 100; i++) {
                    s.Push(i);
                    Thread.Sleep(100);
                }
            });

            Task tPop = Task.Factory.StartNew(() => {
                int n = 0;
                int result;
                for (int i = 0; i < 100; i++) {
                    if (s.TryPop(out result)) {
                        Console.WriteLine(result);
                        n++;
                    }
                    Thread.Sleep(150);
                }
            });

            Task.WaitAll(tPush, tPop);
        }
    }
}
