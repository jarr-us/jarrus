using Jarrus.GA;
using Jarrus.GA.FitnessFunctions;
using System;

namespace Jarrus.GATests.Models
{
    public class PhraseFitnessFunction : FitnessFunction
    {
        private Random random = new Random();

        public override double GetFitnessScoreFor(Chromosome chromosome)
        {
            return random.Next(50, 100);
        }
    }
}
