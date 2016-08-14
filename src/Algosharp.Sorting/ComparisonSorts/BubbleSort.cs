using System.Collections.Generic;

namespace Algosharp.Sorting.ComparisonSorts
{
    /// <summary>
    /// <para>
    /// Time complexity.
    /// Best: O(n)
    /// Average: O(n^2)
    /// Worst: O(n^2) 
    /// </para>
    /// 
    /// <para>
    /// Space complexity: O(1)
    /// </para>
    /// 
    /// <see href="https://en.wikipedia.org/wiki/Bubble_sort"/>
    /// </summary>
    public class BubbleSort
    {
        public static void Perform<T>(T[] array, Comparer<T> comparer = null)
        {
            var equalityComparer = comparer ?? Comparer<T>.Default;

            var n = array.Length - 1;
            int lastSwap;
            do
            {
                lastSwap = 0;
                for (var j = 0; j < n; j++)
                {
                    if (equalityComparer.Compare(array[j], array[j + 1]) <= 0)
                        continue;

                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;

                    lastSwap = j;
                }

                n = lastSwap;
            } while (lastSwap != 0);
        }
    }
}