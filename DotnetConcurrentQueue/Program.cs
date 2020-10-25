using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetConcurrentQueue {
    class Program {
        static void Main(string[] args) {
            var q = new ConcurrentQueue<int>();

            Task tEnq = Task.Factory.StartNew(() => {
                for (int i = 0; i < 100; i++) {
                    q.Enqueue(i);
                    Thread.Sleep(50);
                }
            });

            Task tDeq = Task.Factory.StartNew(() => {
                int result;
                for (int i = 0; i < 100; i++) {
                    if (q.TryDequeue(out result)) {
                        Console.WriteLine(result);
                        Thread.Sleep(100);
                    }
                }
            });

            Task.WaitAll(tEnq, tDeq);
        }
    }
}
