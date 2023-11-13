using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_05_LinqTree {
    public class TreeNode<T> {
        public T Data;        
        public List<TreeNode<T>> Nodes { get; } = new List<TreeNode<T>>();
        public TreeNode(T data) => Data = data;

        public TreeNode<T> Add(T data) {
            var node = new TreeNode<T>(data);
            Nodes.Add(node);
            return node;
        }

        public IEnumerable<T> EnumData() => EnumDataPreorder();
        public IEnumerable<T> EnumDataPreorder() {
            yield return Data;
            foreach (var node in Nodes) {
                foreach (var data in node.EnumDataPreorder()) {
                    yield return data;
                }
            }
        }
        public IEnumerable<T> EnumDataPostorder() {
            foreach (var node in Nodes) {
                foreach (var data in node.EnumDataPostorder()) {
                    yield return data;
                }
            }
            yield return Data;
        }
        public IEnumerable<T> EnumDataBreadthFirst() {
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this);
            while (queue.Count > 0) {
                var current = queue.Dequeue();
                yield return current.Data;
                foreach (var node in current.Nodes) {
                    queue.Enqueue(node);
                }
            }
        }        
    }

    public enum TraversalType {
        Preorder,
        Postorder,
        BreadthFirst
    }

    public class Tree<T> : IEnumerable<T> {
        public TreeNode<T> Root { get; }
        public Tree() : this(default) { }
        public Tree(T rootData) => Root = new TreeNode<T>(rootData);        

        public IEnumerable<T> EnumData(TraversalType treversalType = TraversalType.Preorder) {
            switch (treversalType) {
                case TraversalType.Preorder:
                    return Root.EnumDataPreorder();
                case TraversalType.Postorder:
                    return Root.EnumDataPostorder();
                case TraversalType.BreadthFirst:
                    return Root.EnumDataBreadthFirst();
                default:
                    throw new Exception("Invalid TraversalType");
            }
        }

        public IEnumerator<T> GetEnumerator() => EnumData().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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

            foreach (var treversalType in Enum.GetValues(typeof(TraversalType))) {
                Console.WriteLine($"== {treversalType} == ");
                foreach (var data in tree.EnumData((TraversalType)treversalType)) {
                    Console.WriteLine($"{data}");
                }
                Console.WriteLine();
            }
        }
    }
}
