using System.Collections.Generic;

namespace Algosharp.Sorting.ComparisonSorts
{
    /// <summary>
    /// <para>
    /// Time complexity.
    /// Best: O(n^2)
    /// Average: O(n^2)
    /// Worst: O(n^2) 
    /// </para>
    /// 
    /// <para>
    /// Space complexity: O(1)
    /// </para>
    /// 
    /// <see href="https://en.wikipedia.org/wiki/Selection_sort"/>
    /// </summary>
    public static class SelectionSort
    {
        public static void Perform<T>(T[] array, Comparer<T> comparer = null)
        {
            var equalityComparer = comparer ?? Comparer<T>.Default;

            for (var i = 0; i < array.Length - 1; i++)
            {
                var min = i;
                for (var j = i + 1; j < array.Length; j++)
                    if (equalityComparer.Compare(array[min], array[j]) > 0)
                        min = j;

                if (min == i)
                    continue;

                var t = array[i];
                array[i] = array[min];
                array[min] = t;
            }
        }
    }
}
