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

        private static void QuickSort(int[] A, int lo, int hi) {
            if (lo < hi) {
                int p = Partition_Hoare(A, lo, hi);
                QuickSort(A, lo, p - 1);
                QuickSort(A, p + 1, hi);
            }
        }

        private static int Partition_Lomuto(int[] A, int lo, int hi) {
            int pivot = A[hi];
            int i = lo;
            for (int j = lo; j <= hi; j++) {
                if (A[j] < pivot) {
                    Swap(ref A[i], ref A[j]);
                    i++;
                }
            }
            Swap(ref A[i], ref A[hi]);
            return i;
        }

        private static int Partition_Hoare(int[] A, int lo, int hi) {
            int pivot = A[(lo + hi) / 2];
            int i = lo - 1;
            int j = hi + 1;
            while (true) {
                do i++; while (A[i] < pivot);
                do j--; while (A[j] > pivot);
                if (i >= j)
                    return j;
                Swap(ref A[i], ref A[j]);
            }
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
