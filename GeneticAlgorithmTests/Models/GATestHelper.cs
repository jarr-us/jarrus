using GeneticAlgorithms;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using GeneticAlgorithmTests.Models.FitnessCalculators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GeneticAlgorithmTests.Models
{
    public class GATestHelper
    {
        private static Random _random = new Random();

        public static int GetRandomInteger(int min, int max)
        {
            max++;
            return _random.Next(min, max);
        }

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomAlphaNumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private static IEnumerable<char> RandomSequence()
        {
            while (true) { yield return (char)_random.Next(char.MinValue, char.MaxValue); }
        }

        public static Color GetRandomColor()
        {
            return Color.FromArgb(255, GetRandomInteger(0, 255), GetRandomInteger(0, 255), GetRandomInteger(0, 255));
        }

        public static GAConfiguration<T> GetDefaultSettings<T>()
        {
            return new GAConfiguration<T>(
                new RouletteWheelSelection<T>(),
                new TravelingSalesmanFitnessCalculator(),
                new SwapMutation(),
                new OrderedCrossover()
            );
        }
    }
}
