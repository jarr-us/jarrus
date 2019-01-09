using System;

namespace Jarrus.GA.Utility
{
    public static class ArrayExtensions
    {
        public static T[] Subset<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static void Shuffle<T>(this T[] array, Random random)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + random.Next(n - i);
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }

        public static T[] ShuffleSubset<T>(this T[] array, int start, int end, Random random)
        {
            var subset = array.Subset(start, end - start);
            subset.Shuffle(random);

            var newArray = new T[array.Length];
            var subsetItemsPulled = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i < start || i > end - 1)
                {
                    newArray[i] = array[i];
                }
                else
                {
                    newArray[i] = subset[subsetItemsPulled++];
                }
            }

            return newArray;
        }
    }
}