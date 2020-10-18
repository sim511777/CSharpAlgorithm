using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList {
    public class DoublyLinkedListNode<T> {
        // 데이터
        public T Data { get; set; }
        // 다음 노드의 포인터
        public DoublyLinkedListNode<T> Next { get; set; }
        // 이전 노드의 포인터
        public DoublyLinkedListNode<T> Prev { get; set; }

        // 데이터 생성자
        public DoublyLinkedListNode(T data) : this(data, null, null) {
        }

        // 데이터 생성자
        public DoublyLinkedListNode(T data, DoublyLinkedListNode<T> prev, DoublyLinkedListNode<T> next) {
            Data = data;
            Prev = prev;
            Next = next;
        }
    }

    public class DoublyLinkedList<T> {
        private DoublyLinkedListNode<T> head;

        public void Add(DoublyLinkedListNode<T> newNode) {
            if (head == null) {
                head = newNode;
            } else {
                var current = head;
                while (current.Next != null) {
                    current = current.Next;
                }

                current.Next = newNode;
                newNode.Prev = current;
            }
        }

        public void AddAfter(DoublyLinkedListNode<T> current, DoublyLinkedListNode<T> newNode) {
            if (head == null || current == null || newNode == null) {
                throw new InvalidOperationException();
            }

            newNode.Prev = current;
            newNode.Next = current.Next;
            if (current.Next != null)
                current.Next.Prev = newNode;
            current.Next = newNode;
        }

        public void Remove(DoublyLinkedListNode<T> removeNode) {
            if (head == null || removeNode == null) {
                return;
            }

            if (removeNode == head) {
                head = head.Next;
                if (head != null) {
                    head.Prev = null;
                }
            } else {
                removeNode.Prev.Next = removeNode.Next;
                if (removeNode.Next != null) {
                    removeNode.Next.Prev = removeNode.Prev;
                }
                removeNode = null;
            }
        }

        public DoublyLinkedListNode<T> GetNode(int index) {
            var current = head;
            for (int i = 0; i < index && current != null; i++) {
                current = current.Next;
            }
            return current;
        }

        public int Count() {
            int cnt = 0;
            var current = head;
            while (current != null) {
                cnt++;
                current = current.Next;
            }
            return cnt;
        }
    }

    class Program {
        static void Main(string[] args) {
            var list = new DoublyLinkedList<int>();
            
            for (int i = 0; i < 5; i++) {
                list.Add(new DoublyLinkedListNode<int>(i));
            }

            var node = list.GetNode(2);
            list.Remove(node);

            node = list.GetNode(1);
            list.AddAfter(node, new DoublyLinkedListNode<int>(100));

            int count = list.Count();

            for (int i = 0; i < count; i++) {
                var n = list.GetNode(i);
                Console.WriteLine(n.Data);
            }

            node = list.GetNode(4);
            for (int i = 0; i < count; i++) {
                Console.WriteLine(node.Data);
                node = node.Prev;
            }
        }
    }
}
