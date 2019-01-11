using Jarrus.GA.Models;
using System;

namespace Jarrus.GA.Utility
{
    public class PopulationGenerator
    {
        public static Chromosome[] GenerateOrderedPopulation(GAConfiguration configuration, params Gene[] possibleValues)
        {
            if (possibleValues == null || possibleValues.Length <= 1 || configuration == null)
            {
                throw new ArgumentException("Invalid parameters passed to the genome generator");
            }

            var list = new Chromosome[configuration.PopulationSize];

            for (int i = 0; i < configuration.PopulationSize; i++)
            {
                possibleValues.Shuffle(configuration.RandomPool);
                list[i] = new OrderedChromosome((Gene[])possibleValues.Clone());

                list[i].FirstName = NameGenerator.GetFirstName(configuration.RandomFirstNameSeed);
                list[i].LastName = NameGenerator.GetLastName(configuration.RandomLastNameSeed);
            }

            return list;
        }

        public static Chromosome[] GenerateUnorderedPopulation(GAConfiguration configuration, Type unorderedGeneType)
        {
            if (configuration == null || unorderedGeneType == null) { throw new ArgumentException("Invalid parameters passed to the genome generator"); }

            var isUnorderedGene = typeof(UnorderedGene).IsAssignableFrom(unorderedGeneType);
            if (!isUnorderedGene) { throw new ArgumentException("Can not create an unordered population from a non-unordered gene type."); }
            if (configuration.GeneSize <= 1) { throw new ArgumentException("Gene size must be larger than 1."); }
            var list = new Chromosome[configuration.PopulationSize];

            for (int i = 0; i < configuration.PopulationSize; i++)
            {
                list[i] = new UnorderedChromosome(configuration.GeneSize, unorderedGeneType, configuration.Random);

                list[i].FirstName = NameGenerator.GetFirstName(configuration.RandomFirstNameSeed);
                list[i].LastName = NameGenerator.GetLastName(configuration.RandomLastNameSeed);
            }

            return list;
        }
    }
}
