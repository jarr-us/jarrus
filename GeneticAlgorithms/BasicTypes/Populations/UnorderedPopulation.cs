using Jarrus.GA.Solution;
using Jarrus.GA.Utility;
using System;
using System.Collections.Generic;

namespace Jarrus.GA.Models
{
    public class UnorderedPopulation : Population
    {
        public UnorderedPopulation(GAConfiguration configuration, Chromosome[] chromosomes, Type geneType)
        {
            if (configuration == null || chromosomes == null || chromosomes.Length <= 2 || geneType == null) { throw new ArgumentException("Invalid parameters passed to the Population"); }
            GeneType = geneType;
            Chromosomes = chromosomes;
            Configuration = configuration;

            StandardConstructorLogic();
        }

        protected UnorderedPopulation(GAConfiguration configuration, Chromosome[] chromosomes, int generationNumber, Type geneType, List<Chromosome> retired = null)
        {
            Chromosomes = chromosomes;
            Configuration = configuration;
            GenerationNumber = ++generationNumber;
            GeneType = geneType;
            Retired = retired;

            StandardConstructorLogic();
        }

        protected override Population AdvancePopulation()
        {
            return new UnorderedPopulation(Configuration, NextGeneration.ToArray(), GenerationNumber, GeneType, Retired);
        }

        protected override Chromosome GetNewChromosome()
        {
            var newChromosome = new UnorderedChromosome(Configuration.Solution, Configuration.Random);

            newChromosome.FirstName = NameGenerator.GetFirstName(Configuration.RandomFirstNameSeed);
            newChromosome.LastName = NameGenerator.GetLastName(Configuration.RandomLastNameSeed);

            return newChromosome;
        }
    }
}
