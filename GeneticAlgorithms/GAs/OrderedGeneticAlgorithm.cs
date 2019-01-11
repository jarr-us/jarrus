using Jarrus.GA.Models;
using Jarrus.GA.Solution;
using Jarrus.GA.Utility;
using System;

namespace Jarrus.GA
{
    public class OrderedGeneticAlgorithm : GeneticAlgorithm
    {
        public OrderedGeneticAlgorithm(GAConfiguration configuration, params Gene[] possibleValues)
        {
            Configuration = configuration;            
            PossibleValues = possibleValues;            

            StandardConstructorLogic();
        }

        protected override void ValidateConfiguration()
        {
            if (Configuration == null)
            {
                throw new ArgumentException("An invalid configuration was passed to the GeneticAlgorithm");
            }

            var isOrderedSolution = typeof(JarrusOrderedSolution).IsAssignableFrom(Configuration.Solution.GetType());
            if (!isOrderedSolution || PossibleValues == null || PossibleValues.Length <= 1)
            {
                throw new ArgumentException("An invalid configuration was passed to the GeneticAlgorithm");
            }
        }

        protected override void GenerateInitialPopulation()
        {
            var randomPool = PopulationGenerator.GenerateOrderedPopulation(Configuration, PossibleValues);
            GARun.Population = new OrderedPopulation(Configuration, randomPool, PossibleValues);
            GARun.BestChromosome = GARun.Population.Chromosomes[0];
        }
    }
}
