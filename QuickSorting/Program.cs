using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSorting {
    class Program {
        // 함수 진입점
        public static void QuickSort(int[] arr) {
            // 처음 시작은 0번부터 배열의 끝까지 정복
            QuickSort(arr, 0, arr.Length - 1);
        }

        // 부분 정복
        private static void QuickSort(int[] arr, int l, int r) {
            // l과 r이 같다면 그 부분의 요소의 개수는 1이므로 더이상 정복할 필요가 없다.
            if (l < r) {
                // 주어진 부분을 분할 하고 분할 인덱스를 리턴
                int p = partition(arr, l, r);
                
                // 분할 인덱스의 왼쪽 부분과 오른쪽 부분을 각각 정복
                QuickSort(arr, l, p - 1);
                QuickSort(arr, p + 1, r);
            }
        }

        // 분할
        private static int partition(int[] arr, int l, int r) {
            // 피봇은 맨 오른쪽 값을 사용
            int pivot = arr[r];
            int i = (l - 1);
            for (int j = l; j <= r - 1; j++) {
                if (arr[j] <= pivot) {
                    i++;
                    Swap(ref arr[i], ref arr[j]);
                }
            }
            Swap(ref arr[i + 1], ref arr[r]);
            return i + 1;
        }

        private static void Swap(ref int v1, ref int v2) {
            int temp = v1;
            v1 = v2;
            v2 = temp;
        }

        static void Main(string[] args) {
            var rnd = new Random();
            var arr = Enumerable.Range(0, 20).OrderBy(n => rnd.Next()).ToArray();
            Console.WriteLine(string.Join(" " , arr));
            QuickSort(arr);
            Console.WriteLine(string.Join(" " , arr));
        }
    }
}
