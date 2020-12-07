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


        //  루트 노드를 스택에 넣는다
        //  스택이 빌 때까지 루프를 돈다        
        //      스택에서 Pop하여 노드를 출력한다
        //      오른쪽 노드가 있으면 스택에 저장한다
        //      왼쪽 노드가 있으면 스택에 저장한다
        public void PreorderIterative() {
            if (Root == null) return;

            var stack = new Stack<BinaryTreeNode<T>>();
            stack.Push(Root);
            while (stack.Count != 0) {
                var node = stack.Pop();
                Console.Write("{0} ", node.Data);
                if (node.Right != null) stack.Push(node.Right);
                if (node.Left != null) stack.Push(node.Left);
            }
        }

        //  루트 노드에서 최좌측(Leftmost) 노드까지 스택에 저장한다
        //  스택이 빌 때까지 루프를 돈다
        //      스택에서 Pop하여 노드를 출력한다
        //      오른쪽 노드가 있으면 오른쪽 노드부터 오른쪽 서브트리의 최좌측(Leftmost) 노드까지 스택에 저장한다
        public void InorderIterative() {
            var stack = new Stack<BinaryTreeNode<T>>();
            var node = Root;
            while (node != null) {
                stack.Push(node);
                node = node.Left;
            }

            while (stack.Count > 0) {
                node = stack.Pop();
                Console.Write("{0} ", node.Data);
                if (node.Right != null) {
                    node = node.Right;
                    while (node != null) {
                        stack.Push(node);
                        node = node.Left;
                    }
                }
            }
        }

        public void InorderIterative2() {
            var stack = new Stack<BinaryTreeNode<T>>();
            var node = Root;
            while (node != null || stack.Count > 0) {
                while (node != null) {
                    stack.Push(node);
                    node = node.Left;
                }

                node = stack.Pop();
                Console.Write("{0} ", node.Data);
                node = node.Right;
            }
        }

        //  루트 노드에서 최좌측(Leftmost) 노드까지 오른쪽 자식노드와 루트 노드를 스택에 저장한다
        //  스택이 빌 때까지 루프를 돈다
        //      스택에서 Pop하여 변수 N에 저장하고, N의 오른쪽 노드가 스택의 Top과 동일한지 체크한다.
        //      동일하지 않으면, 변수 N을 출력한다.
        //      만약 동일하면, 스택에 루트와 오른쪽 노드가 있었다는 의미이다. 
        //       스택의 Top에 있는 오른쪽 노드를 Pop하고 기존 루트(변수 N)를 다시 스택에 Push 한다. 
        //       다시 말하면, 스택에 처음 Push할 때 오른쪽 자식노드와 루트 순으로 Push 하였으므로
        //       Pop할 때 루트부터 Pop할 수 있게 되고 이를 통해 스택 상에 루트와 오른쪽 자식 노드가 있는지
        //       판단할 수 있게 된다. 이를 통해 오른쪽 서브트리를 처리해야 한다는 것을 판단한 이후에
        //       다시 루트노드를 스택에 Push 하고 오른쪽 서브트리를 처리한다.      
        //       오른쪽 서브트리를 처리하기 위해서는 다시 오른쪽 서브트리의 루트 노드에서 최좌측(Leftmost) 노드까지 
        //       오른쪽 자식노드와 루트 노드를 스택에 저장하여 동일한 방식으로 반복하여 처리하면 된다.
        public void PostorderIterative() {
            var stack = new Stack<BinaryTreeNode<T>>();
            var node = Root;
            while (node != null) {
                if (node.Right != null) {
                    stack.Push(node.Right);
                }
                stack.Push(node);
                node = node.Left;
            }

            while (stack.Count > 0) {
                node = stack.Pop();
                if (node.Right != null &&
                    stack.Count > 0 &&
                    node.Right == stack.Peek()) {
                    var right = stack.Pop();
                    stack.Push(node);
                    node = right;
                    while (node != null) {
                        if (node.Right != null) {
                            stack.Push(node.Right);
                        }
                        stack.Push(node);
                        node = node.Left;
                    }
                } else {
                    Console.Write("{0} ", node.Data);
                }
            }
        }

        public void PostorderIterative2() {
            var stack = new Stack<BinaryTreeNode<T>>();
            var node = Root;
            while (node != null || stack.Count > 0) {
                while (node != null) {
                    if (node.Right != null) {
                        stack.Push(node.Right);
                    }
                    stack.Push(node);
                    node = node.Left;
                }

                node = stack.Pop();
                if (node.Right != null &&
                    stack.Count > 0 &&
                    node.Right == stack.Peek()) {
                    var right = stack.Pop();
                    stack.Push(node);
                    node = right;
                } else {
                    Console.Write("{0} ", node.Data);
                    node = null;
                }
            }
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

            Console.WriteLine("==== Preorder Iterative ====");
            bt.PreorderIterative();
            Console.WriteLine();
            Console.WriteLine("==== Inorder Iterative ====");
            bt.InorderIterative();
            Console.WriteLine();
            Console.WriteLine("==== Inorder Iterative 2 ====");
            bt.InorderIterative2();
            Console.WriteLine();
            Console.WriteLine("==== Postorder Iterative ====");
            bt.PostorderIterative();
            Console.WriteLine();
            Console.WriteLine("==== Postorder Iterative 2 ====");
            bt.PostorderIterative2();
            Console.WriteLine();
        }
    }
}
