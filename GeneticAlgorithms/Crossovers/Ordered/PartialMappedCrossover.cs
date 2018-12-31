using GeneticAlgorithms.BasicTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithms.Crossovers.Ordered
{
    public class PartialMappedCrossover : Crossover
    {
        private int _firstCrossoverPoint, _secondCrossoverPoint;
        private List<KeyValuePair<Gene, Gene>> _pairsOfGenes = new List<KeyValuePair<Gene, Gene>>();

        protected override Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> configuration)
        {
            DetermineCrossoverPoints(father.Genes.Length, configuration);

            var children = DetermineChildren(father, mother, _firstCrossoverPoint, _secondCrossoverPoint);

            if (configuration.GetNextDouble() < 0.5)
            {
                return children[0];
            } else
            {
                return children[1];
            }
        }

        private void DetermineCrossoverPoints<T>(int numberOfGenes, GAConfiguration<T> configuration) where T : Gene
        {
            _firstCrossoverPoint = configuration.GetRandomInteger(1, numberOfGenes - 1);
            _secondCrossoverPoint = configuration.GetRandomInteger(1, numberOfGenes - 1, _firstCrossoverPoint);

            if (_firstCrossoverPoint > _secondCrossoverPoint)
            {
                var temp = _firstCrossoverPoint;
                _firstCrossoverPoint = _secondCrossoverPoint;
                _secondCrossoverPoint = temp;
            }
        }
        
        public List<Chromosome<T>> DetermineChildren<T>(Chromosome<T> father, Chromosome<T> mother, int firstCrossover, int secondCrossover) where T : Gene
        {
            _firstCrossoverPoint = firstCrossover;
            _secondCrossoverPoint = secondCrossover;

            var parent1Genes = father.Genes;
            var parent1MappingSection = parent1Genes.Skip(_firstCrossoverPoint).Take((_secondCrossoverPoint - _firstCrossoverPoint) + 1).ToArray();

            var parent2Genes = mother.Genes;
            var parent2MappingSection = parent2Genes.Skip(_firstCrossoverPoint).Take((_secondCrossoverPoint - _firstCrossoverPoint) + 1).ToArray();

            var offspring1 = new Chromosome<T>(father.Genes);
            var offspring2 = new Chromosome<T>(mother.Genes);

            offspring2.ReplaceGenes(_firstCrossoverPoint, parent1MappingSection);
            offspring1.ReplaceGenes(_firstCrossoverPoint, parent2MappingSection);

            var length = father.Genes.Length;

            for (int i = 0; i < length; i++)
            {
                if (i >= _firstCrossoverPoint && i <= _secondCrossoverPoint)
                {
                    continue;
                }

                var geneForUffspring1 = GetGeneNotInMappingSection(parent1Genes[i], parent2MappingSection, parent1MappingSection);
                offspring1.ReplaceGenes(i, geneForUffspring1);

                var geneForoffspring2 = GetGeneNotInMappingSection(parent2Genes[i], parent1MappingSection, parent2MappingSection);
                offspring2.ReplaceGenes(i, geneForoffspring2);
            }

            var children = new List<Chromosome<T>>();
            children.Add(offspring1);
            children.Add(offspring2);
            return children;
        }

        private T GetGeneNotInMappingSection<T>(T candidateGene, T[] mappingSection, T[] otherParentMappingSection) where T : Gene
        {
            var indexOnMappingSection = mappingSection
                .Select((item, index) => new { Gene = item, Index = index })
                .FirstOrDefault(g => g.Gene.Equals(candidateGene));

            if (indexOnMappingSection != null)
            {
                return GetGeneNotInMappingSection(otherParentMappingSection[indexOnMappingSection.Index], mappingSection, otherParentMappingSection);
            }

            return candidateGene;
        }
    }
}