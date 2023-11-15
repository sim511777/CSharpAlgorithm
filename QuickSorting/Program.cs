using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSorting {
    class Program {
        public static void QuickSort(int[] arr) {
            QuickSort_ms(arr, 0, arr.Length - 1);
        }

        private static void QuickSort(int[] A, int lo, int hi) {
            if (lo < hi) {
                int p = Partition_Hoare(A, lo, hi);
                QuickSort(A, lo, p - 1);
                QuickSort(A, p + 1, hi);
            }
        }

        private static void QuickSort_non_recursive(int[] arr) {
            var stack = new Stack<Tuple<int, int>>();
            stack.Push(Tuple.Create(0, arr.Length - 1));
            while (stack.Count != 0) {
                var tuple = stack.Pop();
                int lo = tuple.Item1;
                int hi = tuple.Item2;
                if (lo < hi) {
                    int p = Partition_Hoare(arr, lo, hi);
                    stack.Push(Tuple.Create(lo, p - 1));
                    stack.Push(Tuple.Create(p + 1, hi));
                }
            }
        }
        
        private static void QuickSort_ms(int[] map, int left, int right) {
            do {
                int i = left;
                int j = right;
                int x = map[i + ((j - i) >> 1)];
                do {
                    while (i < map.Length && x > map[i]) i++;
                    while (j >= 0 && x < map[j]) j--;
                    if (i > j) break;
                    if (i < j) {
                        int temp = map[i];
                        map[i] = map[j];
                        map[j] = temp;
                    }
                    i++;
                    j--;
                } while (i <= j);
                if (j - left <= right - i) {
                    if (left < j) QuickSort_ms(map, left, j);
                    left = i;
                }
                else {
                    if (i < right) QuickSort_ms(map, i, right);
                    right = j;
                }
            } while (left < right);
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

        private static int Partition_Hoare_0(int[] A, int lo, int hi) {
            int pivot = A[(lo + hi) / 2];
            int i = lo;
            int j = hi;
            while (true) {
                while (A[i] < pivot) i++;
                while (A[j] > pivot) j--;
                if (i >= j)
                    return i;
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
            QuickSort_non_recursive(arr);
            Console.WriteLine(string.Join(" " , arr));
        }
    }
}
