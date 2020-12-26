using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSorting {
    class Program {
        public static void QuickSort(int[] arr) {
            QuickSort(arr, 0, arr.Length - 1);
        }

        private static void QuickSort(int[] arr, int l, int r) {
            if (l < r) {
                int p = partition(arr, l, r);
                QuickSort(arr, l, p - 1);
                QuickSort(arr, p + 1, r);
            }
        }

        private static int partition(int[] arr, int l, int r) {
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
