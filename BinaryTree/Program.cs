using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
    public class BinaryTreeNode<T> {
        public T Data { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T data) {
            this.Data = data;
        }
    }

    public class BinaryTree<T> {
        public BinaryTreeNode<T> Root { get; private set; }

        public BinaryTree(T root) {
            Root = new BinaryTreeNode<T>(root);
        }

        public void PreorderTraversal() {
            PreorderTraversal(Root);
        }

        private void PreorderTraversal(BinaryTreeNode<T> node) {
            if (node == null)
                return;

            Console.WriteLine("{0} ", node.Data);
            PreorderTraversal(node.Left);
            PreorderTraversal(node.Right);
        }
    }

    class Program {
        static void Main(string[] args) {
            var bt = new BinaryTree<int>(1);

            bt.Root.Left = new BinaryTreeNode<int>(2);
            bt.Root.Right = new BinaryTreeNode<int>(3);
            bt.Root.Left.Left = new BinaryTreeNode<int>(4);

            bt.PreorderTraversal();
        }
    }
}
