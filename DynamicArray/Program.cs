using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicArray {
    public class DynamicArray {
        private object[] arr;
        private const int GROWTH_FACTOR = 2;

        public int Count { get; private set; }
        public int Capacity {
            get { return arr.Length; }
        }

        public DynamicArray(int capacity = 16) {
            arr = new object[capacity];
            Count = 0;
        }

        public void Add(object element) {
            // 꽉 찼을 경우 버퍼 용량 키움
            if (Count >= Capacity) {
                // 기존 용량의 2배 증가
                int newCapacity = Capacity * GROWTH_FACTOR;
                var temp = new object[newCapacity];
                // 새 버퍼에 기존 요소 모두 복사
                for (int i = 0; i < Count; i++) {
                    temp[i] = arr[i];
                }
                // 새 버퍼를 내부 버퍼로 지정
                arr = temp;
            }
            // 새 요소 추가
            arr[Count] = element;
            Count++;
        }

        // 요소 리턴
        public object Get(int index) {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            return arr[index];
        }
    }

    class Program {
        static void Main(string[] args) {
            DynamicArray da = new DynamicArray();
            for (int i = 0; i < 100; i++) {
                da.Add(1);
                Console.WriteLine($"Count={da.Count}, Capacity={da.Capacity}");
            }
        }
    }
}
