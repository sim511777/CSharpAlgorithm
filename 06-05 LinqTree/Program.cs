using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_05_LinqTree {
    public class SimpleTreeNode<T> {
        public T Data { get; set; }
        public List<SimpleTreeNode<T>> Children { get; set; }

        public SimpleTreeNode(T data) {
            this.Data = data;
            this.Children = new List<SimpleTreeNode<T>>();
        }

        public void AddChild(T data) {
            this.Children.Add(new SimpleTreeNode<T>(data));
        }
    }

    public class SimpleTree<T> : IEnumerable<T> {
        public SimpleTreeNode<T> Root { get; set; }

        public SimpleTree(T rootData) {
            this.Root = new SimpleTreeNode<T>(rootData);
        }

        public SimpleTree(SimpleTreeNode<T> root) {
            this.Root = root;
        }

        public SimpleTree<T> GetSubTree(SimpleTreeNode<T> node) {
            return new SimpleTree<T>(node);
        }

        public void PreOrderTreversal(Action<T> action) {
            PreOrderTraversal(Root, action);
        }

        public void PostOrderTreversal(Action<T> action) {
            PreOrderTraversal(Root, action);
        }

        public void BreadthFirstTraversal(Action<T> action) {
            PreOrderTraversal(Root, action);
        }        

        public static void PreOrderTraversal(SimpleTreeNode<T> node, Action<T> action) {
            if (node == null) return;
            action(node.Data);
            foreach (var child in node.Children) {
                PreOrderTraversal(child, action);
            }
        }

        public static void PostOrderTraversal(SimpleTreeNode<T> node, Action<T> action) {
            if (node == null) return;
            foreach (var child in node.Children) {
                PostOrderTraversal(child, action);
            }
            action(node.Data);
        }

        public static void BreadthFirstTraversal(SimpleTreeNode<T> node, Action<T> action) {
            if (node == null) return;
            Queue<SimpleTreeNode<T>> queue = new Queue<SimpleTreeNode<T>>();
            queue.Enqueue(node);

            while (queue.Count > 0) {
                var current = queue.Dequeue();
                action(current.Data);
                foreach (var child in current.Children) {
                    queue.Enqueue(child);
                }
            }
        }

        public IEnumerator<T> GetEnumerator() {
            return GetEnumerableNodes(Root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private static IEnumerable<T> GetEnumerableNodes(SimpleTreeNode<T> node) {
            if (node != null) {
                yield return node.Data;
                foreach (var child in node.Children) {
                    foreach (var childData in GetEnumerableNodes(child)) {
                        yield return childData;
                    }
                }
            }
        }
    }

    internal class Program {
        static void Main(string[] args) {
        }
    }
}
