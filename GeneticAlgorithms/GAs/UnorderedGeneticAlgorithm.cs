using Jarrus.GA.Solution;
using Jarrus.GA.Utility;
using System;

namespace Jarrus.GA
{
    public class UnorderedGeneticAlgorithm : GeneticAlgorithm
    {
        public UnorderedGeneticAlgorithm(GAConfiguration configuration, Type unorderedGeneType)
        {
            Configuration = configuration;
            GeneType = unorderedGeneType;
            StandardConstructorLogic();
        }

        protected override void ValidateConfiguration()
        {
            if (Configuration == null)
            {
                throw new ArgumentException("An invalid configuration was passed to the GeneticAlgorithm");
            }

            var isUnorderedSolution = typeof(JarrusUnorderedSolution).IsAssignableFrom(Configuration.Solution.GetType());
            if (!isUnorderedSolution || Configuration == null || GeneType == null)
            {
                throw new ArgumentException("An invalid configuration was passed to the GeneticAlgorithm");
            }
        }

        protected override void GenerateInitialPopulation()
        {
            var randomPool = PopulationGenerator.GenerateUnorderedPopulation(Configuration, GeneType);
            GARun.Population = new Population(Configuration, randomPool, GeneType);
            GARun.BestChromosome = GARun.Population.Chromosomes[0];
        }
    }
}
