using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueUsingLinkedList {
    // 단순화를 위해 object 데이타 타입 사용
    public class Node {
        public object Data { get; set; }
        public Node Next { get; set; }
        public Node(object data) {
            Data = data;
            Next = null;
        }
    }

    public class QueueUsingLinkedList {
        private Node head = null;

        private Node tail = null;

        public void Enqueue(object data) {
            // Queue가 비어 있는 경우
            if (head == null) {
                head = new Node(data);
                tail = head;
            } else //비어 있지 않은 경우
              {
                tail.Next = new Node(data);
                tail = tail.Next;
            }
        }

        public object Dequeue() {
            // Queue가 비어 있는 경우
            if (head == null) {
                throw new ApplicationException("Empty");
            }

            object data = head.Data;

            // Queue에 하나 남은 경우
            if (head == tail) {
                head = tail = null;
            } else //둘 이상 남은 경우
              {
                head = head.Next;
            }

            return data;
        }
    }

    class Program {
        static void Main(string[] args) {
        }
    }
}
