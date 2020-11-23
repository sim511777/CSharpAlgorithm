using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCRSNode {
    // LCRS Expression
    class LCRSNode {
        public object Data { get; set; }
        public LCRSNode LeftChild { get; set; }
        public LCRSNode RightSibling { get; set; }
        public LCRSNode(object data) {
            this.Data = data;
        }
    }

    class LCRSTree {
        public LCRSNode Root { get; private set; }

        public LCRSTree(object rootData) {
            this.Root = new LCRSNode(rootData);
        }

        // 자식노드 추가
        public LCRSNode AddChild(LCRSNode parent,
           object data) {
            if (parent == null) return null;

            LCRSNode child = new LCRSNode(data);

            if (parent.LeftChild == null) {
                parent.LeftChild = child;
            } else {
                var node = parent.LeftChild;
                while (node.RightSibling != null) {
                    node = node.RightSibling;
                }
                node.RightSibling = child;
            }

            return child;
        }

        // 형제노드 추가
        public LCRSNode AddSibling(LCRSNode node,
           object data) {
            if (node == null) return null;
            while (node.RightSibling != null) {
                node = node.RightSibling;
            }

            var sibling = new LCRSNode(data);
            node.RightSibling = sibling;

            return sibling;
        }

        // 레벨순으로 트리노드 출력
        public void PrintLevelOrder() {
            var q = new Queue<LCRSNode>();
            q.Enqueue(this.Root);

            while (q.Count > 0) {
                var node = q.Dequeue();

                while (node != null) {
                    Console.Write($"{node.Data} ");

                    if (node.LeftChild != null) {
                        q.Enqueue(node.LeftChild);
                    }

                    node = node.RightSibling;
                }
            }
        }
        // 들여쓰기 방식으로 트리 출력
        public void PrintIndentTree() {
            PrintIndent(this.Root, 1);
        }

        private void PrintIndent(LCRSNode node,
           int indent) {
            if (node == null) return;

            // 현재노드 출력
            string pad = " ".PadLeft(indent);
            Console.WriteLine($"{pad}{node.Data}");

            // 왼쪽 자식 
            // (자식이므로 Indent 증가)
            PrintIndent(node.LeftChild, indent + 1);

            // 오른쪽 형제 
            // (형제이므로 동일 Indent 사용)
            PrintIndent(node.RightSibling, indent);
        }
    }

    class Program {
        //static void Main(string[] args) {
        //    var A = new LCRSNode("A");
        //    var B = new LCRSNode("B");
        //    var C = new LCRSNode("C");
        //    var D = new LCRSNode("D");
        //    var E = new LCRSNode("E");
        //    var F = new LCRSNode("F");
        //    var G = new LCRSNode("G");

        //    A.LeftChild = B;
        //    B.RightSibling = C;
        //    C.RightSibling = D;
        //    B.LeftChild = E;
        //    E.RightSibling = F;
        //    D.LeftChild = G;

        //    // A의 자식노드들 출력
        //    if (A.LeftChild != null) {
        //        var tmp = A.LeftChild;
        //        Console.WriteLine(tmp.Data);

        //        while (tmp.RightSibling != null) {
        //            tmp = tmp.RightSibling;
        //            Console.WriteLine(tmp.Data);
        //        }
        //    }
        //}

        // 테스트코드
        static void Main(string[] args) {
            LCRSTree tree = new LCRSTree("A");
            var A = tree.Root;
            var B = tree.AddChild(A, "B");
            tree.AddChild(A, "C");
            var D = tree.AddSibling(B, "D");
            tree.AddChild(B, "E");
            tree.AddChild(B, "F");
            tree.AddChild(D, "G");

            //출력: 아래 그림 참조
            tree.PrintIndentTree();

            //출력: A B C D E F G
            tree.PrintLevelOrder();
        }

    }
}
