using System;
using System.Linq;
using Jarrus.GA.Models;
using Jarrus.GA.Solution;

namespace Jarrus.GATests.Models
{
    public class PhraseSolution : JarrusUnorderedSolution
    {
        public const string Shakespeare = "to be, or not to be, that is the question";

        public override double GetFitnessScoreFor(Chromosome chromosome)
        {
            var genes = chromosome.Genes.Cast<PhraseGene>().ToArray();
            string geneValue = new string(genes.Select(o => o.Value).ToArray());
            return GetDifferences(Shakespeare, geneValue);
        }

        public int GetDifferences(string s, string t)
        {
            if (s.Length != t.Length) { throw new ArgumentException("The length of the strings must be equal."); }

            var differences = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != t[i]) { differences++; }
            }

            return differences;
        }

        public override int GetGeneSize() { return Shakespeare.Length; }
        public override Type GetGeneType() { return typeof(PhraseGene); }

        public override bool ShouldTerminate(Population population)
        {
            var minScore = population.Chromosomes.Select(o => o.FitnessScore).Min();
            return minScore == 0;
        }
    }
}
