using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueUsingCircularArray {
    public class QueUsingCircularArray {
        private object[] a;
        private int front;
        private int rear;

        public QueUsingCircularArray(int queueSize = 16) {
            a = new object[queueSize];
            front = -1;
            rear = -1;
        }

        public void Enqueue(object data) {
            // 큐가 가득차 있는지 체크
            if ((rear + 1) % a.Length == front) {
                throw new ApplicationException("Full");
            } else {
                if (front == -1) {
                    front++;
                }
                // 데이터 추가
                rear = (rear + 1) % a.Length;
                a[rear] = data;
            }
        }

        public object Dequeue() {
            // 쿠가 비었는지 체크
            if (front == -1 && rear == -1) {
                throw new ApplicationException("Empty");
            } else {
                object data = a[front];
                if (front == rear) {
                    front = -1;
                    rear = -1;
                } else {
                    front = (front + 1) % a.Length;
                }

                return data;
            }
        }
    }

    public class QueueUsingCircularArray2 {
        private object[] a;
        private int front = 0;
        private int rear = 0;

        public QueueUsingCircularArray2(int queueSize = 16) {
            a = new object[queueSize];
        }

        public void Enqueue(object data) {
            if ((rear + 1) % a.Length == front) //Full
            {
                throw new ApplicationException("Full");
            }
            a[rear] = data;
            rear = (rear + 1) % a.Length;
        }

        public object Dequeue() {
            if (front == rear) //Empty
            {
                throw new ApplicationException("Empty");
            }

            object data = a[front];
            front = (front + 1) % a.Length;
            return data;
        }
    }

    public class QueueUsingCircularArray3 {
        private object[] a;
        private int front = 0;
        private int rear = 0;

        public int Count { get; private set; } = 0;

        public QueueUsingCircularArray3(int queueSize = 16) {
            a = new object[queueSize];
        }

        public void Enqueue(object data) {
            if (Count == a.Length) //Full
            {
                throw new ApplicationException("Full");
            }

            a[rear] = data;
            rear = (rear + 1) % a.Length;
            Count++;  //증가
        }

        public object Dequeue() {
            if (Count == 0) //Empty
            {
                throw new ApplicationException("Empty");
            }

            object data = a[front];
            front = (front + 1) % a.Length;
            Count--; //감소
            return data;
        }
    }

    class Program {
        static void Main(string[] args) {
        }
    }
}
