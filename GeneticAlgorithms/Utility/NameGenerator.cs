using GeneticAlgorithms.Enums;
using System;
using System.Linq;

namespace GeneticAlgorithms.Utility
{
    public class NameGenerator
    {
        private static NameGenerator _generator = new NameGenerator();

        public static FirstName GetFirstName(Random random)
        {
            if (random == null) { return FirstName.Aadhya; }
            var numberSelected = random.Next(1, _generator._firstNameEnumSize);
            return ((FirstName)numberSelected);
        }

        public static LastName GetLastName(Random random)
        {
            if (random == null) { return LastName.Abbott; }
            var numberSelected = random.Next(1, _generator._lastNameEnumSize);
            return (LastName)numberSelected;
        }

        private int _firstNameEnumSize, _lastNameEnumSize;

        private NameGenerator()
        {
            _firstNameEnumSize = (int) Enum.GetValues(typeof(FirstName)).Cast<FirstName>().Last();
            _lastNameEnumSize = (int)Enum.GetValues(typeof(LastName)).Cast<LastName>().Last();
        }
    }
}
