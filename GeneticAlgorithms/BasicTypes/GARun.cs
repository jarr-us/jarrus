using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using System;

namespace GeneticAlgorithms
{
    public class GARun
    {
        public int CurrentGeneration { get; set; }
        public DateTime Start { get; set; }               
        public DateTime End { get; set; }
        public Chromosome BestChromosome { get; set; }
        public Population Population;
        
        public double GetTotalMSToRun() { return (End - Start).TotalMilliseconds; }
    }
}
