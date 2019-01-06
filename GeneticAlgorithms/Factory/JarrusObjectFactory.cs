﻿using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Crossovers.Ordered;
using GeneticAlgorithms.Crossovers.Unordered;
using GeneticAlgorithms.Factory.Enums;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithms.Utility;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithms.Factory
{
    public class JarrusObjectFactory
    {
        public static JarrusObjectFactory Instance = new JarrusObjectFactory();

        private Dictionary<CrossoverType, string> _crossovers = new Dictionary<CrossoverType, string>();
        private Dictionary<MutationType, string> _mutations = new Dictionary<MutationType, string>();
        private Dictionary<ParentSelectionType, string> _parentSelection = new Dictionary<ParentSelectionType, string>();

        private JarrusObjectFactory()
        {
            SetupCrossovers();
            SetupMutations();
            SetupParentSelections();
        }

        private void SetupCrossovers()
        {
            _crossovers.Add(CrossoverType.Order, new OrderCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverType.Cycle, new CycleCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverType.AlternatingPosition, new AlternatingPositionCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverType.PartialMapped, new PartialMappedCrossover().GetType().AssemblyQualifiedName);

            _crossovers.Add(CrossoverType.SinglePoint, new SinglePointCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverType.TwoPoint, new TwoPointCrossover().GetType().AssemblyQualifiedName);
            _crossovers.Add(CrossoverType.Uniform, new UniformCrossover().GetType().AssemblyQualifiedName);
        }

        private void SetupMutations()
        {
            _mutations.Add(MutationType.Insert, new InsertMutation().GetType().AssemblyQualifiedName);
            _mutations.Add(MutationType.Inversion, new InversionMutation().GetType().AssemblyQualifiedName);
            _mutations.Add(MutationType.Scramble, new ScrambleMutation().GetType().AssemblyQualifiedName);
            _mutations.Add(MutationType.Swap, new SwapMutation().GetType().AssemblyQualifiedName);
        }

        private void SetupParentSelections()
        {
            _parentSelection.Add(ParentSelectionType.RouletteWheel, new RouletteWheelSelection().GetType().AssemblyQualifiedName);
            _parentSelection.Add(ParentSelectionType.StochasticUniversalSamplingSelection, new StochasticUniversalSamplingSelection().GetType().AssemblyQualifiedName);
        }

        public Crossover GetCrossover(CrossoverType type) { return (Crossover)Reflection.GetObjectFromType(_crossovers[type]); }
        public Mutation GetMutation(MutationType type) { return (Mutation)Reflection.GetObjectFromType(_mutations[type]); }
        public ParentSelection GetParentSelection(ParentSelectionType type) { return (ParentSelection)Reflection.GetObjectFromType(_parentSelection[type]); }
    }
}
