using GeneticAlgorithms.BasicTypes;
using System;

namespace GeneticAlgorithms.Utility
{
    public class PopulationGenerator
    {
        public static Chromosome<T>[] Generate<T>(T[] possibleValues, GAConfiguration<T> configuration) where T : Gene
        {
            if (possibleValues == null || possibleValues.Length <= 1 || configuration == null)
            {
                throw new ArgumentException("Invalid parameters passed to the genome generator");
            }

            var list = new Chromosome<T>[configuration.MaxPopulationSize];

            for (int i = 0; i < configuration.MaxPopulationSize; i++)
            {
                possibleValues.Shuffle(configuration.RandomPool);
                list[i] = new Chromosome<T>((T[])possibleValues.Clone());

                list[i].FirstName = NameGenerator.GetFirstName(configuration.RandomFirstNameSeed);
                list[i].LastName = NameGenerator.GetLastName(configuration.RandomLastNameSeed);
            }

            return list;
        }
    }
}
