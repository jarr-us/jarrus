using Jarrus.GA.BasicTypes.Chromosomes;
using Jarrus.GA.BasicTypes.Genes;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.ParentSelections;
using Jarrus.GA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jarrus.GA
{
    public class Population
    {
        public Chromosome[] Chromosomes;
        public GAConfiguration Configuration;
        public int GenerationNumber = 1;
        private List<Chromosome> NextGeneration = new List<Chromosome>();
        public HashSet<Chromosome> OptionsInPool = new HashSet<Chromosome>();
        public List<Chromosome> Retired = new List<Chromosome>();
        private Gene[] _possibleValues;
        private Type _geneType;
                
        public Population(GAConfiguration configuration, Chromosome[] chromosomes, params Gene[] possibleValues)
        {
            if (configuration == null || chromosomes == null || chromosomes.Length <= 2 || possibleValues == null || possibleValues.Length <= 2) { throw new ArgumentException("Invalid parameters passed to the Population"); }
            
            Chromosomes = chromosomes;
            Configuration = configuration;
            _possibleValues = possibleValues;

            StandardConstructorLogic();
        }

        public Population(GAConfiguration configuration, Chromosome[] chromosomes, Type geneType)
        {
            if (configuration == null || chromosomes == null || chromosomes.Length <= 2 || geneType == null) { throw new ArgumentException("Invalid parameters passed to the Population"); }
            _geneType = geneType;
            Chromosomes = chromosomes;
            Configuration = configuration;

            StandardConstructorLogic();
        }

        private void StandardConstructorLogic()
        {
            Retire();
            SetChromosomesGenerationAndAge();
            DetermineFitnessScores();
        }

        private Population(GAConfiguration configuration, Chromosome[] chromosomes, int generationNumber, Gene[] possibleValues, List<Chromosome> retired = null)
        {
            Chromosomes = chromosomes;
            Configuration = configuration;
            GenerationNumber = ++generationNumber;
            _possibleValues = possibleValues;
            Retired = retired;

            StandardConstructorLogic();
        }

        private Population(GAConfiguration configuration, Chromosome[] chromosomes, int generationNumber, Type geneType, List<Chromosome> retired = null)
        {
            Chromosomes = chromosomes;
            Configuration = configuration;
            GenerationNumber = ++generationNumber;
            _geneType = geneType;
            Retired = retired;

            StandardConstructorLogic();
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
            if (Configuration.RetirementType == RetirementType.None) { return; }

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

            if (Configuration.IsOrderedConfiguration())
            {
                return new Population(Configuration, NextGeneration.ToArray(), GenerationNumber, _possibleValues, Retired);
            } else
            {
                return new Population(Configuration, NextGeneration.ToArray(), GenerationNumber, _geneType, Retired);
            }
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
            Retired.AddRange(Chromosomes.Where(o => o.ShouldRetire(Configuration)).ToList());
        }

        private double GetBestScore()
        {
            if (Configuration.ScoringType == ScoringType.Lowest)
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
            AddImmigrantsToNextGeneration();
            
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

            if (Configuration.ScoringType == ScoringType.Lowest)
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
            if (Configuration.ImmigrationType != ImmigrationType.Constant) { return; }

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
            if (Configuration.ImmigrationType == ImmigrationType.Dynamic) { return; }
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
            if (Configuration.ImmigrationType != ImmigrationType.Dynamic) { return; }

            if (Configuration.GetNextDouble() <= Configuration.CrossoverRate)
            {
                var parents = Configuration.ParentSelection.GetParents();

                for (int i = 0; i < Configuration.ChildrenPerParents; i++)
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
            if (Configuration.DuplicationType == DuplicationType.Prevent)
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

        private Chromosome GetNewChromosome()
        {
            if (Configuration.IsOrderedConfiguration())
            {
                var newChromosome = new OrderedChromosome(_possibleValues);

                newChromosome.Genes.Shuffle(Configuration.RandomPool);
                newChromosome.FirstName = NameGenerator.GetFirstName(Configuration.RandomFirstNameSeed);
                newChromosome.LastName = NameGenerator.GetLastName(Configuration.RandomLastNameSeed);

                return newChromosome;
            } else
            {
                return new UnorderedChromosome(Configuration.GeneSize, _geneType, Configuration.Random);
            }            
        }

        private Chromosome GetChild(ChromosomeParents parents)
        {
            var child = Configuration.Crossover.Execute(parents.Father, parents.Mother, Configuration);
            return child;
        }
    }
}