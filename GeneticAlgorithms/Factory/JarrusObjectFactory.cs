using Jarrus.GA.Crossovers;
using Jarrus.GA.Crossovers.Ordered;
using Jarrus.GA.Crossovers.Unordered;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Mutations;
using Jarrus.GA.ParentSelections;
using Jarrus.GA.Utility;
using System;
using System.Collections.Generic;

namespace Jarrus.GA.Factory
{
    public class JarrusObjectFactory
    {
        public static JarrusObjectFactory Instance = new JarrusObjectFactory();

        private Dictionary<CrossoverStrategy, string> _crossovers = new Dictionary<CrossoverStrategy, string>();
        private Dictionary<MutationStrategy, string> _mutations = new Dictionary<MutationStrategy, string>();
        private Dictionary<ParentSelectionStrategy, string> _parentSelection = new Dictionary<ParentSelectionStrategy, string>();

        private JarrusObjectFactory()
        {
            SetupCrossovers();
            SetupMutations();
            SetupParentSelections();
        }

        private void SetupCrossovers()
        {
            _crossovers.Add(CrossoverStrategy.Order, new OrderCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverStrategy.Cycle, new CycleCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverStrategy.AlternatingPosition, new AlternatingPositionCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverStrategy.PartialMapped, new PartialMappedCrossover().GetType().AssemblyQualifiedName);

            _crossovers.Add(CrossoverStrategy.SinglePoint, new SinglePointCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverStrategy.TwoPoint, new TwoPointCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverStrategy.Uniform, new UniformOrderedCrossover().GetType().AssemblyQualifiedName);
        }

        private void SetupMutations()
        {
            _mutations.Add(MutationStrategy.Insert, new InsertMutation().GetType().AssemblyQualifiedName);
            _mutations.Add(MutationStrategy.Inversion, new InversionMutation().GetType().AssemblyQualifiedName);
            _mutations.Add(MutationStrategy.Scramble, new ScrambleMutation().GetType().AssemblyQualifiedName);
            _mutations.Add(MutationStrategy.Swap, new SwapMutation().GetType().AssemblyQualifiedName);

            _mutations.Add(MutationStrategy.Flip, new InternalMutation().GetType().AssemblyQualifiedName);
            _mutations.Add(MutationStrategy.Boundary, new InternalMutation().GetType().AssemblyQualifiedName);
            _mutations.Add(MutationStrategy.Random, new InternalMutation().GetType().AssemblyQualifiedName);
        }

        private void SetupParentSelections()
        {
            _parentSelection.Add(ParentSelectionStrategy.RouletteWheel, new RouletteWheelSelection().GetType().AssemblyQualifiedName);
            _parentSelection.Add(ParentSelectionStrategy.Rank, new RankSelection().GetType().AssemblyQualifiedName);
            _parentSelection.Add(ParentSelectionStrategy.TournamentTwo, new TournamentTwoSelection().GetType().AssemblyQualifiedName);
            _parentSelection.Add(ParentSelectionStrategy.TournamentThree, new TournamentThreeSelection().GetType().AssemblyQualifiedName);
            _parentSelection.Add(ParentSelectionStrategy.TournamentFour, new TournamentFourSelection().GetType().AssemblyQualifiedName);
            _parentSelection.Add(ParentSelectionStrategy.TournamentFive, new TournamentFiveSelection().GetType().AssemblyQualifiedName);
            _parentSelection.Add(ParentSelectionStrategy.StochasticUniversalSamplingSelection, new StochasticUniversalSamplingSelection().GetType().AssemblyQualifiedName);
        }

        public Mutation GetMutation(MutationStrategy type) {
            var mutation = (Mutation)Reflection.GetObjectFromType(_mutations[type]);
            mutation.MutationType = type;
            return mutation;
        }

        public Crossover GetCrossover(CrossoverStrategy type) { return (Crossover)Reflection.GetObjectFromType(_crossovers[type]); }        
        public ParentSelection GetParentSelection(ParentSelectionStrategy type) { return (ParentSelection)Reflection.GetObjectFromType(_parentSelection[type]); }
    }
}
