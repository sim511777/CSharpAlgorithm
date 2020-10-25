using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackUsingLinkedList {
    // 단순화를 위해 object 데이타 타입 사용
    public class StackUsingLinkedList {
        private Node top = null;

        public void Push(object data) {
            if (top == null) {
                top = new Node(data);
            } else {
                // 노드추가
                var node = new Node(data);
                node.Next = top;
                top = node;
            }
        }

        public object Pop() {
            if (this.IsEmpty) {
                throw new ApplicationException("Empty");
            }

            object data = top.Data;
            top = top.Next;
            return data;
        }

        public object Peek() {
            if (this.IsEmpty) {
                throw new ApplicationException("Empty");
            }

            return top.Data;
        }

        public bool IsEmpty {
            get { return top == null; }
        }

        // StackUsingLinkedList 클래스에서만 
        // 사용하는 Node 클래스
        private class Node {
            public object Data { get; set; }
            public Node Next { get; set; }
            public Node(object data) {
                Data = data;
                Next = null;
            }
        }
    }

    class Program {
        static void Main(string[] args) {
        }
    }
}
