using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithms.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms
{
    public class Genome<T>
    {
        public Chromosome<T>[] Chromosomes;
        public GAConfiguration<T> Configuration;
        public int GenerationNumber = 1;
        private List<Chromosome<T>> NextGeneration = new List<Chromosome<T>>();
        public HashSet<string> OptionsInPool = new HashSet<string>();
        public List<Chromosome<T>> Retired = new List<Chromosome<T>>();

        public Genome(GAConfiguration<T> configuration, Chromosome<T>[] chromosomes)
        {
            if (configuration == null || chromosomes == null || chromosomes.Length <=2)
            {
                throw new ArgumentException("Invalid parameters passed to the Genome");
            }

            Chromosomes = chromosomes;
            Configuration = configuration;

            SetChromosomesGenerationAndAge();
            DetermineFitnessScores();
        }

        private Genome(GAConfiguration<T> configuration, Chromosome<T>[] chromosomes, int generationNumber, List<Chromosome<T>> retired = null)
        {
            Chromosomes = chromosomes;
            Configuration = configuration;
            GenerationNumber = ++generationNumber;
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
            }
        }

        private void Retire()
        {
            if (Retired == null) { return; }
            foreach (var chromosome in Retired)
            {
                OptionsInPool.Add(chromosome.ToString());
            }
        }

        public Genome<T> Advance()
        {
            SetupNextGenerationObjects();
            DetermineNextGeneration();
            DetermineFitnessScores();

            return new Genome<T>(Configuration, NextGeneration.ToArray(), GenerationNumber, Retired);
        }

        private void DetermineFitnessScores()
        {
            foreach (var chromosome in Chromosomes)
            {
                chromosome.FitnessScore = Configuration.FitnessCalculator.GetFitnessScoreFor(chromosome);
            }
        }

        private void SetupNextGenerationObjects()
        {
            Configuration.ParentSelection.Setup(Chromosomes, Configuration);
            Retired.AddRange(Chromosomes.Where(o => o.ShouldRetire(Configuration)).ToList());
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
            var parents = Configuration.ParentSelection.GetParents();

            for (int i = 0; i < Configuration.ChildrenPerCouple; i++)
            {
                if (Configuration.GetNextDouble() <= Configuration.CrossoverRate)
                {
                    var child = GetChild(parents);
                    Configuration.Mutation.Mutate(child, Configuration);

                    AddToNextGeneration(child);
                }
                else
                {
                    AddToNextGeneration(GetNewChromosome());
                }
            }
        }

        private void AddToNextGeneration(List<Chromosome<T>> chromosomes)
        {
            foreach (var chromosome in chromosomes) { AddToNextGeneration(chromosome); }
        }

        private void AddToNextGeneration(Chromosome<T> chromosome)
        {
            if (Configuration.PreventDuplicationInPool)
            {
                if (!OptionsInPool.Contains(chromosome.ToString()))
                {
                    OptionsInPool.Add(chromosome.ToString());

                    if (NextGeneration.Count < Chromosomes.Length)
                    {
                        NextGeneration.Add(chromosome);
                    }
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

        private Chromosome<T> GetNewChromosome()
        {
            var newChromosome = new Chromosome<T>(Chromosomes.First().Genes);
            newChromosome.Genes.Shuffle(Configuration.Random);
            return newChromosome;
        }

        private Chromosome<T> GetChild(ChromosomeParents<T> parents)
        {
            var child = Configuration.Crossover.Execute(parents.Father, parents.Mother, Configuration);
            return child;
        }
    }
}