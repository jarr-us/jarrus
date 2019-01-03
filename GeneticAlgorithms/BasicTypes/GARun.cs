using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Utility;
using System;

namespace GeneticAlgorithms
{
    public class GARun<T> where T : Gene
    {
        public int CurrentGeneration { get; set; }
        public DateTime Start { get; set; }               
        public DateTime End { get; set; }
        public Chromosome<T> BestChromosome { get; set; }
        public Population<T> Population;
        
        public double GetTotalMSToRun() { return (End - Start).TotalMilliseconds; }
    }
}
