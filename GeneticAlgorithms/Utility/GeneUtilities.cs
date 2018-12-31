using GeneticAlgorithms.BasicTypes;
using System;

namespace GeneticAlgorithms.Utility
{
    public static class GeneExtensions
    {
        private static Random Random = new Random();

        public static Gene[] Subset(this Gene[] data, int index, int length)
        {
            Gene[] result = new Gene[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static void Shuffle(this Gene[] array, Random random = null)
        {
            if (random == null) { random = Random; }

            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + random.Next(n - i);
                Gene t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }

        public static Gene[] ShuffleSubset(this Gene[] array, int start, int end, Random random = null)
        {
            var subset = array.Subset(start, end - start);
            subset.Shuffle(random);

            var newArray = new Gene[array.Length];
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