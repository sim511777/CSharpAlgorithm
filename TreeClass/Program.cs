using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeTreversal {
    class TreeNode<T> {
        public T Data;
        public List<TreeNode<T>> children = new List<TreeNode<T>>();
        public TreeNode(T data) {
            Data = data;
        }
        public override string ToString() {
            return Data.ToString();
        }
    }

    class Tree<T> {
        public TreeNode<T> root;
        public TreeNode<T> AddChild(TreeNode<T> parent, T data) {
            var node = new TreeNode<T>(data);
            if (parent == null) {
                if (root != null)
                    throw new Exception("root node is not null");
                root = node;
            } else {
                parent.children.Add(node);
            }
            return node;
        }

        public void PreorderTreversal(Action<T> action) {
            PreorderTreversalRecursive(action, root);
        }
        private void PreorderTreversalRecursive(Action<T> action, TreeNode<T> node) {
            if (node == null)
                return;
            action(node.Data);
            foreach(var child in node.children) {
                PreorderTreversalRecursive(action, child);
            }
        }

        public void PostorderTreversal(Action<T> action) {
            PostTreversalRecursive(action, root);
        }
        private void PostTreversalRecursive(Action<T> action, TreeNode<T> node) {
            if (node == null)
                return;
            foreach(var child in node.children) {
                PostTreversalRecursive(action, child);
            }
            action(node.Data);
        }

        public void LevelorderTreversal(Action<T> action) {
            Queue<TreeNode<T>> q = new Queue<TreeNode<T>>();
            q.Enqueue(root);
            while (q.Count != 0) {
                var node = q.Dequeue();
                action(node.Data);
                foreach(var chlidNode in node.children) {
                    q.Enqueue(chlidNode);
                }
            }
        }
    }

    class Program {
        static void Main(string[] args) {
            var tree = new Tree<string>();
            var node0 = tree.AddChild(null, "0");
            for (int i = 0; i < 3; i++) {
                var node1 = tree.AddChild(node0, $"{node0.Data} - {i}");
                for (int j = 0; j < 3; j++) {
                    var node2 = tree.AddChild(node1, $"{node1.Data} - {j}");
                    for (int k = 0; k < 3; k++) {
                        tree.AddChild(node2, $"{node2.Data} - {k}");
                    }
                }
            }

            Console.WriteLine("PreorderTreversal");
            tree.PreorderTreversal(data => Console.WriteLine(data));
            Console.WriteLine("PostorderTreversal");
            tree.PostorderTreversal(data => Console.WriteLine(data));
            Console.WriteLine("LevelorderTreversal");
            tree.LevelorderTreversal(data => Console.WriteLine(data));

            Console.WriteLine();
        }
    }
}
