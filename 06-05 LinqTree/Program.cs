using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace _06_05_LinqTree {
    [DataContract]
    public class TreeNode<T> {
        [DataMember]
        public T Data;
        [DataMember]
        public List<TreeNode<T>> Nodes { get; set; } = new List<TreeNode<T>>();
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

        internal void Foreach(int level, int index, bool isLast, Action<int, int, bool, T> action) {
            action(level, index, isLast, Data);
            for (int i = 0; i < Nodes.Count; i++) {
                bool isLast2 = i == Nodes.Count - 1;
                Nodes[i].Foreach(level + 1, i, isLast2, action);
            }
        }
    }

    public enum TraversalType {
        Preorder,
        Postorder,
        BreadthFirst
    }

    [DataContract]
    public class Tree<T> : IEnumerable<T> {
        [DataMember]
        public TreeNode<T> Root { get; set; }
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

        public void Foreach(Action<int, int, bool, T> action) {
            Root.Foreach(0, 0, true, action);
        }

        //│├└
        override public string ToString() {
            var sb = new System.Text.StringBuilder();
            Foreach((level, index, isLast, data) => {
                //sb.AppendLine($"{new string('│', level)}{(isLast?"└":"├")}{data}");
                sb.AppendLine($"{new string(' ', level)}+{data}");
            });
            return sb.ToString();
        }
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

            var seriliazerType = JsonSerializerType.DataContractJsonSerializer;
            var json = JsonUtil.ObjectToJson(tree, seriliazerType, true);
            Console.WriteLine(json);

            var tree2 = JsonUtil.JsonToObject<Tree<string>>(json, seriliazerType);
            Console.WriteLine(tree2);

            //foreach (var treversalType in Enum.GetValues(typeof(TraversalType))) {
            //    Console.WriteLine($"== {treversalType} == ");
            //    foreach (var data in tree.EnumData((TraversalType)treversalType)) {
            //        Console.WriteLine($"{data}");
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
