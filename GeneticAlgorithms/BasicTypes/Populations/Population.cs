using Jarrus.GA.Factory.Enums;
using Jarrus.GA.ParentSelections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Jarrus.GA.Models
{
    public abstract class Population
    {
        public Chromosome[] Chromosomes;
        public GAConfiguration Configuration;
        public int GenerationNumber = 0;
        public List<Chromosome> NextGeneration = new List<Chromosome>();
        public HashSet<Chromosome> OptionsInPool = new HashSet<Chromosome>();
        public List<Chromosome> Retired = new List<Chromosome>();
        protected Gene[] PossibleValues;
        protected Type GeneType;
        public bool UnableToProgress;
            
        protected void StandardConstructorLogic()
        {
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

            foreach (var chromosome in Chromosomes)
            {
                chromosome.Age++;
            }
        }

        private void Retire()
        {
            if (Configuration.RetirementStrategy == RetirementStrategy.None) { return; }

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

            var population = AdvancePopulation();
            population.UnableToProgress = UnableToProgress;
            return population;
        }

        protected abstract Population AdvancePopulation();

        private void DetermineFitnessScores()
        {
            Parallel.ForEach(Chromosomes.Where(o => o.FitnessScore == 0).ToList(), chromosome =>
            {
                chromosome.FitnessScore = Configuration.Solution.GetFitnessScoreFor(chromosome);
            });
        }

        private void SetupNextGenerationObjects()
        {
            Configuration.ParentSelection.Setup(Chromosomes, Configuration);
            Retired.AddRange(Chromosomes.Where(o => o.ShouldRetire(Configuration)).ToList());
        }

        private double GetBestScore()
        {
            if (Configuration.ScoringStrategy == ScoringStrategy.Lowest)
            {
                return Chromosomes.Select(o => o.FitnessScore).Min();
            }
            else
            {
                return Chromosomes.Select(o => o.FitnessScore).Max();
            }
        }

        private int _attemptsToDetermineNextChromosome = 0;
        private void DetermineNextGeneration()
        {
            AddElitiesToNextGeneration();
            AddImmigrantsToNextGeneration();

            while (NextGeneration.Count < Chromosomes.Length && !UnableToProgress)
            {
                GetNextGenerationChromosome();
                _attemptsToDetermineNextChromosome++;
                CheckIfAbleToProgress();
            }
        }

        
        private void CheckIfAbleToProgress()
        {
            if (_attemptsToDetermineNextChromosome >= Configuration.PopulationSize * 2)
            {
                UnableToProgress = true;
            }
        }

        private void AddElitiesToNextGeneration()
        {
            var numberToGrab = (int)(Configuration.ElitismRate * Chromosomes.Length);

            if (Configuration.ElitismRate > 0)
            {
                numberToGrab = Math.Max(1, numberToGrab);
            }

            if (Configuration.ScoringStrategy == ScoringStrategy.Lowest)
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

        private void AddImmigrantsToNextGeneration()
        {
            if (Configuration.ImmigrationStrategy != ImmigrationStrategy.Constant) { return; }

            var numberToGrab = (int)(Configuration.ImmigrationRate * Chromosomes.Length);

            if (Configuration.ImmigrationRate > 0)
            {
                numberToGrab = Math.Max(1, numberToGrab);
            }

            for(int i = 0; i < numberToGrab; i++)
            {
                AddToNextGeneration(GetNewChromosome());
            }            
        }

        private void GetNextGenerationChromosome()
        {
            GetNextGenerationForDynamicImmigration();
            GetNextGenerationForNonDynamicImmigration();
        }

        private void GetNextGenerationForNonDynamicImmigration()
        {
            if (Configuration.ImmigrationStrategy == ImmigrationStrategy.Dynamic) { return; }
            var parents = Configuration.ParentSelection.GetParents();

            if (Configuration.GetNextDouble() <= Configuration.CrossoverRate)
            {
                for (int i = 0; i < Configuration.ChildrenPerParents; i++)
                {
                    var child = GetChild(parents);
                    Configuration.Mutation.Mutate(child, Configuration);

                    AddToNextGeneration(child);
                }
            }
            else
            {
                if (Configuration.GetRandomBoolean())
                {
                    AddToNextGeneration(parents.Father);
                } else
                {
                    AddToNextGeneration(parents.Mother);
                }                
            }
        }

        private void GetNextGenerationForDynamicImmigration()
        {
            if (Configuration.ImmigrationStrategy != ImmigrationStrategy.Dynamic) { return; }

            if (Configuration.GetNextDouble() <= Configuration.CrossoverRate)
            {
                var sw = new Stopwatch();
                sw.Start();

                var parents = Configuration.ParentSelection.GetParents();
                var getParentsTicks = sw.ElapsedTicks;


                for (int i = 0; i < Configuration.ChildrenPerParents; i++)
                {
                    var child = GetChild(parents);
                    var getChildTicks = sw.ElapsedTicks - getParentsTicks;

                    Configuration.Mutation.Mutate(child, Configuration);
                    var mutateTicks = sw.ElapsedTicks - getChildTicks;

                    AddToNextGeneration(child);
                    var addToNextGenTicks = sw.ElapsedTicks - mutateTicks;
                    var one = 1;
                }
            }
            else
            {
                AddToNextGeneration(GetNewChromosome());
            }
        }

        public void AddToNextGeneration(List<Chromosome> chromosomes)
        {
            foreach (var chromosome in chromosomes) { AddToNextGeneration(chromosome); }
        }

        public void AddToNextGeneration(Chromosome chromosome)
        {
            if (Configuration.DuplicationStrategy == DuplicationStrategy.Prevent)
            {
                if (!OptionsInPool.Contains(chromosome))
                {
                    OptionsInPool.Add(chromosome);

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

        protected abstract Chromosome GetNewChromosome();

        private Chromosome GetChild(ChromosomeParents parents)
        {
            var child = Configuration.Crossover.Execute(parents.Father, parents.Mother, Configuration);
            return child;
        }
    }
}