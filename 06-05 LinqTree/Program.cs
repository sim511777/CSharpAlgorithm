using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace _06_05_LinqTree {
    public class TreeNode<T> {
        public T Data;
        public List<TreeNode<T>> Nodes { get; } = new List<TreeNode<T>>();
        public TreeNode<T> Parent { get; }

        public TreeNode(T data, TreeNode<T> parent = null) {
            Data = data;
            Parent = parent;
        }

        public TreeNode<T> Add(T data) {
            var node = new TreeNode<T>(data, this);
            Nodes.Add(node);
            return node;
        }

        public IEnumerable<T> TreversePreorder() {
            yield return Data;
            foreach (var node in Nodes) {
                // return node.TreversePreorder();   // 이렇게 리턴해야 겠지만 안되네
                foreach (var data in node.TreversePreorder()) {
                    yield return data;
                }
            }
        }

        public IEnumerable<T> TreversePostorder() {
            foreach (var node in Nodes) {
                foreach (var data in node.TreversePostorder()) {
                    yield return data;
                }
            }
            yield return Data;
        }

        public IEnumerable<T> TreverseLevelorder() {
            var q = new Queue<TreeNode<T>>();
            q.Enqueue(this);
            while (q.Count != 0) {
                var node = q.Dequeue();
                yield return node.Data;
                foreach (var child in node.Nodes) {
                    q.Enqueue(child);
                }
            }
        }
    }

    public class Tree<T> : IEnumerable<T> {
        public TreeNode<T> Root { get; }

        public Tree() : this(default) { }
        public Tree(T rootData) => Root = new TreeNode<T>(rootData);

        public IEnumerator<T> GetEnumerator() => EnumPreorder().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> EnumPreorder() => Root.TreversePreorder();
        public IEnumerable<T> EnumPostorder() => Root.TreversePostorder();
        public IEnumerable<T> EnumLevelorder() => Root.TreverseLevelorder();
    }

    internal class Program {        
        static void Main(string[] args) {
            var tree = new Tree<string>("root");
            var node0 = tree.Root;
            for (int i = 0; i < 3; i++) {
                var node1 = node0.Add($"{node0.Data}_{i}");
                for (int j = 0; j < 3; j++) {
                    var node2 = node1.Add($"{node1.Data}_{j}");
                    for (int k = 0; k < 3; k++) {
                        node2.Add($"{node2.Data}_{k}");
                    }
                }
            }

            Console.WriteLine("== tree ==");
            foreach (var data in tree) {
                Console.WriteLine($"{data}");
            }
            Console.WriteLine();

            Console.WriteLine("== EnumPreorder ==");
            foreach (var data in tree.EnumPreorder()) {
                Console.WriteLine($"{data}");
            }
            Console.WriteLine();

            Console.WriteLine("== EnumPostorder ==");
            foreach (var data in tree.EnumPostorder()) {
                Console.WriteLine($"{data}");
            }
            Console.WriteLine();
            
            Console.WriteLine("== EnumLevelorder ==");
            foreach (var data in tree.EnumLevelorder()) {
                Console.WriteLine($"{data}");
            }
            Console.WriteLine();
        }
    }
}
