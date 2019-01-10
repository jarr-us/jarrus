using System;
using Jarrus.GA.FitnessFunctions;
using Jarrus.GA.Solution;

namespace Jarrus.GATests.Models
{
    public class PhraseSolution : JarrusUnorderedSolution
    {
        public override FitnessFunction GetFitnessFunction() { return new PhraseFitnessFunction(); }
        public override int GetGeneSize() { return 10; }
        public override Type GetGeneType() { return typeof(PhraseGene); }
    }
}
