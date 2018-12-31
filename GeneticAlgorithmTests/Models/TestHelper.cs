
using GeneticAlgorithms;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using System;

namespace GeneticAlgorithmTests.Models
{
    public class TestHelper
    {
        private ExampleGene[] _genes = new ExampleGene[10];
        private const int _randomSeed = 35;
        private Random _random = new Random(_randomSeed);

        public TestHelper()
        {
            PopulateData();
        }

        private void PopulateData()
        {
            _genes[0] = new ExampleGene('A');
            _genes[1] = new ExampleGene('B');
            _genes[2] = new ExampleGene('C');
            _genes[3] = new ExampleGene('D');
            _genes[4] = new ExampleGene('E');
            _genes[5] = new ExampleGene('F');
            _genes[6] = new ExampleGene('G');
            _genes[7] = new ExampleGene('H');
            _genes[8] = new ExampleGene('I');
            _genes[9] = new ExampleGene('J');
        }

        public Chromosome GetChromosome()
        {
            return new Chromosome(_genes);
        }

        public Chromosome GetNewShuffledChromosome()
        {
            var newArray = (Gene[]) _genes.Clone();
            newArray.Shuffle(_random);
            return new Chromosome(newArray);
        }
    }
}
