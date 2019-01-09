using Jarrus.GA.BasicTypes.Genes;
using System;

namespace Jarrus.GA.Utility
{
    public class PopulationGenerator
    {
        public static Chromosome[] Generate(Gene[] possibleValues, GAConfiguration configuration)
        {
            if (possibleValues == null || possibleValues.Length <= 1 || configuration == null)
            {
                throw new ArgumentException("Invalid parameters passed to the genome generator");
            }

            var list = new Chromosome[configuration.MaxPopulationSize];

            for (int i = 0; i < configuration.MaxPopulationSize; i++)
            {
                possibleValues.Shuffle(configuration.RandomPool);
                list[i] = new Chromosome((Gene[])possibleValues.Clone());

                list[i].FirstName = NameGenerator.GetFirstName(configuration.RandomFirstNameSeed);
                list[i].LastName = NameGenerator.GetLastName(configuration.RandomLastNameSeed);
            }

            return list;
        }
    }
}
