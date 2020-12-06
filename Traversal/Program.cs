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
            if (node == null) return;

            Console.Write("{0} ", node.Data);
            PreorderTraversal(node.Left);
            PreorderTraversal(node.Right);
        }
        public void InorderTraversal() {
            InorderTraversal(Root);
        }
        private void InorderTraversal(BinaryTreeNode<T> node) {
            if (node == null) return;

            InorderTraversal(node.Left);
            Console.Write("{0} ", node.Data);
            InorderTraversal(node.Right);
        }
        public void PostorderTraversal() {
            PostorderTraversal(Root);
        }
        private void PostorderTraversal(BinaryTreeNode<T> node) {
            if (node == null) return;

            PostorderTraversal(node.Left);
            PostorderTraversal(node.Right);
            Console.Write("{0} ", node.Data);
        }
    }

    class Program {
        static void Main(string[] args) {
            var bt = new BinaryTree<string>("A");

            bt.Root.Left = new BinaryTreeNode<string>("B");
            bt.Root.Right = new BinaryTreeNode<string>("C");
            bt.Root.Left.Left = new BinaryTreeNode<string>("D");
            bt.Root.Left.Right = new BinaryTreeNode<string>("E");
            bt.Root.Right.Left = new BinaryTreeNode<string>("F");

            Console.WriteLine("==== Preorder ====");
            bt.PreorderTraversal();
            Console.WriteLine();

            Console.WriteLine("==== Inorder ====");
            bt.InorderTraversal();
            Console.WriteLine();
            
            Console.WriteLine("==== Postorder ====");
            bt.PostorderTraversal();
            Console.WriteLine();
        }
    }
}
