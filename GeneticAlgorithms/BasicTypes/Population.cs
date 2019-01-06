using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithms.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    public class Population
    {
        public Chromosome[] Chromosomes;
        public GAConfiguration Configuration;
        public int GenerationNumber = 1;
        private List<Chromosome> NextGeneration = new List<Chromosome>();
        public HashSet<Chromosome> OptionsInPool = new HashSet<Chromosome>();
        public HashSet<Chromosome> Retired = new HashSet<Chromosome>();
        private Gene[] _possibleValues;

        public Population(GAConfiguration configuration, Chromosome[] chromosomes, Gene[] possibleValues)
        {
            if (configuration == null || chromosomes == null || chromosomes.Length <=2 || possibleValues == null || possibleValues.Length <= 2)
            {
                throw new ArgumentException("Invalid parameters passed to the Genome");
            }

            Chromosomes = chromosomes;
            Configuration = configuration;
            _possibleValues = possibleValues;

            SetChromosomesGenerationAndAge();
            DetermineFitnessScores();
        }

        private Population(GAConfiguration configuration, Chromosome[] chromosomes, int generationNumber, Gene[] possibleValues, HashSet<Chromosome> retired = null)
        {
            Chromosomes = chromosomes;
            Configuration = configuration;
            GenerationNumber = ++generationNumber;
            _possibleValues = possibleValues;
            Retired = retired;

            Retire();
            SetChromosomesGenerationAndAge();
            DetermineFitnessScores();
        }

        private void SetChromosomesGenerationAndAge()
        {
            foreach (var chromosome in Chromosomes.Where(o => o.GenerationNumber == 0))
            {
                chromosome.GenerationNumber = GenerationNumber;
            }

            foreach (var chromosome in Chromosomes.Where(o => o.GenerationNumber != 0))
            {
                chromosome.Age++;

                if (chromosome.ShouldRetire(Configuration))
                {
                    Retired.Add(chromosome);
                }
            }
        }

        private void Retire()
        {
            if (Retired == null) { return; }
            foreach (var chromosome in Retired)
            {
                OptionsInPool.Add(chromosome);
            }
        }

        public Population Advance()
        {
            SetupNextGenerationObjects();
            DetermineNextGeneration();
            DetermineFitnessScores();

            return new Population(Configuration, NextGeneration.ToArray(), GenerationNumber, _possibleValues, Retired);
        }

        private void DetermineFitnessScores()
        {
            Parallel.ForEach(Chromosomes.Where(o => o.FitnessScore == 0).ToList(), chromosome =>
            {
                chromosome.FitnessScore = Configuration.FitnessFunction.GetFitnessScoreFor(chromosome);
            });
        }

        private void SetupNextGenerationObjects()
        {
            Configuration.ParentSelection.Setup(Chromosomes, Configuration);            
        }

        private double GetBestScore()
        {
            if (Configuration.LowestScoreIsBest)
            {
                return Chromosomes.Select(o => o.FitnessScore).Min();
            }
            else
            {
                return Chromosomes.Select(o => o.FitnessScore).Max();
            }
        }

        private void DetermineNextGeneration()
        {
            AddElitiesToNextGeneration();
            
            while (NextGeneration.Count < Chromosomes.Length)
            {
                GetNextGenerationChromosome();
            }
        }

        private void AddElitiesToNextGeneration()
        {
            var numberToGrab = (int)(Configuration.ElitismRate * Chromosomes.Length);

            if (Configuration.ElitismRate > 0)
            {
                numberToGrab = Math.Max(1, numberToGrab);
            }

            if (Configuration.LowestScoreIsBest)
            {
                var ordered = Chromosomes.Where(k => !k.ShouldRetire(Configuration))
                    .OrderBy(o => o.FitnessScore).Take(numberToGrab).ToList();
                AddToNextGeneration(ordered);
            }
            else
            {
                var ordered = Chromosomes.OrderByDescending(o => o.FitnessScore).Take(numberToGrab).ToList();
                AddToNextGeneration(ordered);
            }
        }

        private void GetNextGenerationChromosome()
        {
            if (Configuration.GetNextDouble() <= Configuration.CrossoverRate)
            {
                var parents = Configuration.ParentSelection.GetParents();
                
                for (int i = 0; i < Configuration.ChildrenPerCouple; i++)
                {
                    var child = GetChild(parents);
                    Configuration.Mutation.Mutate(child, Configuration);
                    AddToNextGeneration(child);
                }
            }
            else
            {
                AddToNextGeneration(GetNewChromosome());
            }
        }

        private void AddToNextGeneration(List<Chromosome> chromosomes)
        {
            foreach (var chromosome in chromosomes) { AddToNextGeneration(chromosome); }
        }

        private void AddToNextGeneration(Chromosome chromosome)
        {
            if (Configuration.PreventDuplications)
            {
                var count = NextGeneration.Count;
                OptionsInPool.Add(chromosome);

                if (OptionsInPool.Count > count && NextGeneration.Count < Chromosomes.Length)
                {
                    NextGeneration.Add(chromosome);
                }
            }
            else
            {
                if (NextGeneration.Count < Chromosomes.Length)
                {
                    NextGeneration.Add(chromosome);
                }
            }
        }

        private Chromosome GetNewChromosome()
        {
            var newChromosome = new Chromosome(_possibleValues);

            newChromosome.Genes.Shuffle(Configuration.RandomPool);
            newChromosome.FirstName = NameGenerator.GetFirstName(Configuration.RandomFirstNameSeed);
            newChromosome.LastName = NameGenerator.GetLastName(Configuration.RandomLastNameSeed);

            return newChromosome;
        }

        private Chromosome GetChild(ChromosomeParents parents)
        {
            var child = Configuration.Crossover.Execute(parents.Father, parents.Mother, Configuration);
            return child;
        }
    }
}