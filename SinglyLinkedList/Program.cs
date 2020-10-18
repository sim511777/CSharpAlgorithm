using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglyLinkedList {
    public class SinglyLinkedListNode<T> {
        // 데이터
        public T Data { get; set; }
        // 다음 노드의 포인터
        public SinglyLinkedListNode<T> Next { get; set; }

        // 데이터 생성자
        public SinglyLinkedListNode(T data) {
            Data = data;
            Next = null;
        }
    }

    public class SingliyLinkedlist<T> {
        private SinglyLinkedListNode<T> head;

        // 추가
        public void Add(SinglyLinkedListNode<T> newNode) {
            if (head == null) {
                head = newNode;
            } else {
                // 마지막 노드 서치
                var current = head;
                while (current.Next != null) {
                    current = current.Next;
                }
                // 마지막 노드의 다음 포인터에 추가 노드 지정
                current.Next = newNode;
            }
        }

        // 특정 노드 뒤에 추가
        public void AddAfter(SinglyLinkedListNode<T> current, SinglyLinkedListNode<T> newNode) {
            if (head == null || current == null || newNode == null) {
                throw new InvalidOperationException();
            }

            // 추가 노드의 다음에 특정노드의 다음을 지정
            newNode.Next = current.Next;
            // 특정 노드의 다음에 추가 노드 지정
            current.Next = newNode;
        }

        // 특정 노드 삭제
        public void Remove(SinglyLinkedListNode<T> removeNode) {
            if (head == null || removeNode == null)
                return;

            if (removeNode == head) {
                //  헤드면 헤드의 다음을 헤드로 지정
                head = head.Next;
                removeNode = null;
            } else {
                // 헤드가 아니면 특정 노드의 이전 노드를 서치
                var current = head;
                while (current != null && current.Next != removeNode) {
                    current = current.Next;
                }
                // 이전 노드의 다음을 특정노드의 다음으로 지정
                if (current != null) {
                    current.Next = removeNode.Next;
                    removeNode = null;
                }
            }
        }

        // 인덱스의 노드를 리턴
        public SinglyLinkedListNode<T> GetNode(int index) {
            var current = head;
            // 인덱스 만큼 다음을 찾음
            for (int i = 0; i < index && current != null; i++) {
                current = current.Next;
            }
            return current;
        }

        // 갯수 리턴
        public int Count() {
            int cnt = 0;
            var current = head;
            // null이 나올때까지 다음으로 이동하며 카운트 증가
            while (current != null) {
                cnt++;
                current = current.Next;
            }
            return cnt;
        }
    }

    class Program {
        static void Main(string[] args) {
            var list = new SingliyLinkedlist<int>();
            
            for (int i = 0; i < 5; i++) {
                list.Add(new SinglyLinkedListNode<int>(i));
            }

            var node = list.GetNode(2);
            list.Remove(node);

            node = list.GetNode(1);
            list.AddAfter(node, new SinglyLinkedListNode<int>(100));

            int count = list.Count();

            for (int i = 0; i < count; i++) {
                var n = list.GetNode(i);
                Console.WriteLine(n.Data);
            }
        }
    }
}
