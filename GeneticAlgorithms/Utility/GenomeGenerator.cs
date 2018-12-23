using System;

namespace GeneticAlgorithms.Utility
{
    public class GenomeGenerator
    {
        public static Chromosome<T>[] Generate<T>(T[] possibleValues, GAConfiguration<T> configuration)
        {
            if (possibleValues == null || possibleValues.Length <= 1 || configuration == null)
            {
                throw new ArgumentException("Invalid parameters passed to the genome generator");
            }

            var list = new Chromosome<T>[configuration.PoolSize];

            for (int i = 0; i < configuration.PoolSize; i++)
            {
                possibleValues.Shuffle(configuration.RandomPool);
                list[i] = new Chromosome<T>((T[])possibleValues.Clone());
            }

            return list;
        }
    }
}
