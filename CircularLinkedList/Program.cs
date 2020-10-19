using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularLinkedList {
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

    public class CircularLinkedList<T> {
        private DoublyLinkedListNode<T> head;

        public void Add(DoublyLinkedListNode<T> newNode) {
            if (head == null) {
                head = newNode;
                head.Next = head;
                head.Prev = head;
            } else {
                var tail = head.Prev;
                head.Prev = newNode;
                tail.Next = newNode;
                newNode.Prev = tail;
                newNode.Next = head;
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

            if (removeNode == head && head == head.Next) {
                head = null;
            } else {
                removeNode.Prev.Next = removeNode.Next;
                removeNode.Next.Prev = removeNode.Prev;
            }
            removeNode = null;
        }

        public DoublyLinkedListNode<T> GetNode(int index) {
            if (head == null)
                return null;

            var current = head;
            for (int i = 0; i < index; i++) {
                current = current.Next;
                if (current == head) {
                    return null;
                }
            }
            return current;
        }

        public int Count() {
            if (head == null)
                return 0;

            int cnt = 0;
            var current = head;
            do {
                cnt++;
                current = current.Next;
            } while (current != head);
            return cnt;
        }

        // 노드가 원형리스트의 노드인지 확인
        public static bool IsCircular(DoublyLinkedListNode<T> head) {
            if (head == null) {
                return true;
            }

            var current = head;
            while (current != null) {
                current = current.Next;
                if (current == head) {
                    return true;
                }
            }

            return false;
        }

        // 노드가 회전리스트의 일부인지 확인
        public static bool IsCyclic(DoublyLinkedListNode<T> head) {
            var p1 = head;
            var p2 = head;
            
            do {
                p1 = p1.Next;
                p2 = p2.Next;
                if (p1 == null || p2 == null || p2.Next == null) {
                    return false;
                }
                p2 = p2.Next;
            } while (p1 != p2);

            return true;
        }
    }

    class Program {
        static void Main(string[] args) {
            var list = new CircularLinkedList<int>();
            
            for (int i = 0; i < 5; i++) {
                list.Add(new DoublyLinkedListNode<int>(i));
            }

            var node = list.GetNode(2);
            list.Remove(node);

            node = list.GetNode(1);
            list.AddAfter(node, new DoublyLinkedListNode<int>(100));

            int count = list.Count();
            node = list.GetNode(0);
            for (int i = 0; i < count * 2; i++) {
                Console.WriteLine(node.Data);
                node = node.Next;
            }
        }
    }
}
