using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 10, 5, 2, 1, 8, 7, 4, 9, 3, 6 };

            Console.WriteLine(string.Join(",", numbers));
            Console.ReadLine();
        }

        private List<int> QuickSort(int[] numberArray)
        {
            return new List<int>();
        }

        private void quicksort(ref List<int> v, int left, int right)
        {
            if (left < right)
            {
                int key = v[left];
                int low = left;
                int high = right;
                while (low < high)
                {
                    while (low < high && v[high] > key)
                    {
                        high--;
                    }
                    v[low++] = v[high];
                    while (low < high && v[low] < key)
                    {
                        low++;
                    }
                    v[high--] = v[low];
                }
                v[low] = key;
                quicksort(ref v, left, low - 1);
                quicksort(ref v, low + 1, right);
            }
        }
    }
}
