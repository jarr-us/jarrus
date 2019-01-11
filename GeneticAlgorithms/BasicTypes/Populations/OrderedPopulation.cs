using Jarrus.GA.Utility;
using System;
using System.Collections.Generic;

namespace Jarrus.GA.Models
{
    public class OrderedPopulation : Population
    {
        public OrderedPopulation(GAConfiguration configuration, Chromosome[] chromosomes, params Gene[] possibleValues)
        {
            if (configuration == null || chromosomes == null || chromosomes.Length <= 2 || possibleValues == null || possibleValues.Length <= 2) {
                throw new ArgumentException("Invalid parameters passed to the Population");
            }

            Chromosomes = chromosomes;
            Configuration = configuration;
            PossibleValues = possibleValues;

            StandardConstructorLogic();
        }

        protected OrderedPopulation(GAConfiguration configuration, Chromosome[] chromosomes, int generationNumber, Gene[] possibleValues, List<Chromosome> retired = null)
        {
            Chromosomes = chromosomes;
            Configuration = configuration;
            GenerationNumber = ++generationNumber;
            PossibleValues = possibleValues;
            Retired = retired;

            StandardConstructorLogic();
        }

        protected override Population AdvancePopulation()
        {
            return new OrderedPopulation(Configuration, NextGeneration.ToArray(), GenerationNumber, PossibleValues, Retired);
        }

        protected override Chromosome GetNewChromosome()
        {
            var newChromosome = new OrderedChromosome(PossibleValues);

            newChromosome.Genes.Shuffle(Configuration.RandomPool);
            newChromosome.FirstName = NameGenerator.GetFirstName(Configuration.RandomFirstNameSeed);
            newChromosome.LastName = NameGenerator.GetLastName(Configuration.RandomLastNameSeed);

            return newChromosome;
        }
    }
}
