﻿using Jarrus.GA.Models;
using Jarrus.GA.Solution;
using System.Collections.Generic;
using System.Linq;

namespace Jarrus.GATests.Models.FitnessCalculators
{
    public class SimpleTravelingSalesmanSolution : JarrusOrderedSolution
    {
        private Dictionary<char, Dictionary<char, int>> _dictionary = new Dictionary<char, Dictionary<char, int>>();

        public override Gene[] GetOptions()
        {
            return new OrderedChromosome(
                new TravelingSalesmanGene('A'),
                new TravelingSalesmanGene('B'),
                new TravelingSalesmanGene('C'),
                new TravelingSalesmanGene('D')
            ).Genes;
        }

        public override bool ShouldTerminate(Population population) { return false; }

        public SimpleTravelingSalesmanSolution()
        {
            _dictionary.Add('A', new Dictionary<char, int>());
            _dictionary['A'].Add('B', 10);
            _dictionary['A'].Add('C', 15);
            _dictionary['A'].Add('D', 20);

            _dictionary.Add('B', new Dictionary<char, int>());
            _dictionary['B'].Add('A', 10);
            _dictionary['B'].Add('C', 35);
            _dictionary['B'].Add('D', 25);

            _dictionary.Add('C', new Dictionary<char, int>());
            _dictionary['C'].Add('A', 15);
            _dictionary['C'].Add('B', 35);
            _dictionary['C'].Add('D', 30);

            _dictionary.Add('D', new Dictionary<char, int>());
            _dictionary['D'].Add('A', 20);
            _dictionary['D'].Add('B', 25);
            _dictionary['D'].Add('C', 30);
        }

        public override double GetFitnessScoreFor(Chromosome chromosome)
        {
            var genes = chromosome.Genes.Cast<TravelingSalesmanGene>().ToArray();

            var val = GetDistanceBetween(genes[0].Value, genes[1].Value);

            val += GetDistanceBetween(genes[1].Value, genes[2].Value);
            val += GetDistanceBetween(genes[2].Value, genes[3].Value);
            val += GetDistanceBetween(genes[3].Value, genes[0].Value);

            return val;
        }

        private int GetDistanceBetween(char cityOne, char cityTwo)
        {
            return _dictionary[cityOne][cityTwo];
        }
    }
}
