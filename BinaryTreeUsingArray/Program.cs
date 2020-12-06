using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeUsingArray {
    public class BinaryTreeUsingArray {
        private object[] arr;

        public BinaryTreeUsingArray(int capacity = 15) {
            arr = new object[capacity];
        }

        public object Root {
            get => arr[0];
            set => arr[0] = value;
        }

        public void SetLeft(int parentIndex, object data) {
            int leftIndex = parentIndex * 2 + 1;
            if (arr[parentIndex] == null || leftIndex >= arr.Length) {
                throw new ApplicationException("Error");
            }
            arr[leftIndex] = data;
        }

        public void SetRight(int parentIndex, object data) {
            int rightIndex = parentIndex * 2 + 2;
            if (arr[parentIndex] == null || rightIndex >= arr.Length) {
                throw new ApplicationException("Error");
            }
            arr[rightIndex] = data;
        }

        public object GetParent(int childIndex) {
            if (childIndex == 0) return null;
            int parentIndex = (childIndex - 1) / 2;
            return arr[parentIndex];
        }
        
        public object GetLeft(int parentIndex) {
            int leftIndex = parentIndex * 2 + 1;
            return arr[leftIndex];
        }

        public object GetRight(int parentIndex) {
            int rightIndex = parentIndex * 2 + 2;
            return arr[rightIndex];
        }

        public void PrintTree() {
            for (int i = 0; i < arr.Length; i++) {
                Console.Write("{0} ", arr[i] ?? "-");
            }
            Console.WriteLine();
        }
    }

    class Program {
        static void Main(string[] args) {
           // 샘플 이진 트리 구성
           //     A
           //   B   C
           // D    F
           //
           var bt = new BinaryTreeUsingArray(7);
           bt.Root = "A";
           bt.SetLeft(0, "B");
           bt.SetRight(0, "C");
           bt.SetLeft(1, "D");
           bt.SetLeft(2, "F");

           //출력: A B C D - F -
           bt.PrintTree(); 

           var data = bt.GetParent(5);
           //출력: C
           Console.WriteLine(data); 

           data = bt.GetLeft(2);
           //출력: F
           Console.WriteLine(data);
        }
    }
}
