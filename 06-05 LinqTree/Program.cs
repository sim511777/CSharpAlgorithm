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

        public IEnumerable<T> TreversePreorder() {
            yield return Data;
            foreach (var node in Nodes) {
                // yield return node.TreversePreorder();   // 이렇게 리턴해야 겠지만 
                foreach (var data in node.TreversePreorder()) {
                    yield return data;
                }
            }
        }
    }

    [DataContract]
    public class Tree<T> : IEnumerable<T> {
        [DataMember]
        public TreeNode<T> Root { get; set; }

        public Tree() : this(default) { }
        public Tree(T rootData) => Root = new TreeNode<T>(rootData);

        public IEnumerator<T> GetEnumerator() => Root.TreversePreorder().GetEnumerator();
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

            var seriliazerType = JsonSerializerType.DataContractJsonSerializer;
            var json = JsonUtil.ObjectToJson(tree, seriliazerType, true);
            Console.WriteLine(json);

            var tree2 = JsonUtil.JsonToObject<Tree<string>>(json, seriliazerType);
            Console.WriteLine(tree2);

            foreach (var data in tree) {
                Console.WriteLine($"{data}");
            }
            Console.WriteLine();
        }
    }
}
