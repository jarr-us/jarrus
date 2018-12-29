using GeneticAlgorithms;
using GeneticAlgorithms.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Jarrus.Metadata
{
    public class FamilyLineage<T>
    {
        private Population<T> _population;
        private Dictionary<LastName, int> _lastNameUsage;
        private IOrderedEnumerable<KeyValuePair<LastName, int>> _ranking;
        public double TotalLineages;

        public FamilyLineage(Population<T> pop)
        {
            _population = pop;
            _lastNameUsage = new Dictionary<LastName, int>();

            DetermineLineageStrength();
            DetermineRanking();
        }

        private void DetermineLineageStrength()
        {
            foreach (var chromosome in _population.Chromosomes)
            {
                var lineage = chromosome.Lineage;

                foreach (var line in lineage)
                {
                    if (!_lastNameUsage.ContainsKey(line)) { _lastNameUsage.Add(line, 0); }
                    _lastNameUsage[line] = _lastNameUsage[line] + 1;
                }
            }
        }

        private void DetermineRanking()
        {
            TotalLineages = _lastNameUsage.Values.Sum();
            _ranking = _lastNameUsage.OrderByDescending(x => x.Value);
        }

        public LastName GetFamilyAtRanking(int i)
        {
            if (_ranking.Count() > i)
            {
                return _ranking.ElementAt(i).Key;
            }

            return 0;
        }

        public int GetCountOfFamilyAtRanking(int i)
        {
            if (_ranking.Count() > i)
            {
                return _ranking.ElementAt(i).Value;
            }

            return 0;
        }

        public int GetRankingCount() { return _ranking.Count(); }
    }
}
