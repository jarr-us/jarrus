using Jarrus.GA.BasicTypes.Attributes;
using Jarrus.GA.Factory.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jarrus.GA.BasicTypes.Genes
{
    public abstract class Gene
    {
        public abstract override string ToString();
        public abstract override bool Equals(object obj);
        public override int GetHashCode() { return base.GetHashCode(); }
        private Random _random;

        public Gene() { }

        public Gene(Random random)
        {
            _random = random ?? throw new ArgumentException("When instantiating a Gene, the random object may not be null.");
            Mutate(MutationType.Random);
        }

        public void Mutate(MutationType type) {
            PerformRandomMutation(type);
            PerformBoundaryMutation(type);
            PerformFlipMutation(type);
        }

        private void PerformRandomMutation(MutationType type)
        {
            if (type != MutationType.Random) { return; }

            var attributes = new List<object>();

            foreach (var field in GetMyFields())
            {
                var attributeObjects = field.GetCustomAttributes(typeof(GeneOptionAttribute), false);

                if (attributeObjects.Length > 0)
                {
                    var geneAttributes = attributeObjects.Cast<GeneOptionAttribute>().ToArray();
                    var attribute = geneAttributes[0];

                    SetFieldWithRandomValue(field, attribute.IntValues);
                    SetFieldWithRandomValue(field, attribute.CharValues);
                    SetFieldWithRandomValue(field, attribute.StringValues);
                }
            }
        }

        private void PerformBoundaryMutation(MutationType type)
        {
            if (type != MutationType.Boundary) { return; }

            var attributes = new List<object>();

            foreach (var field in GetMyFields())
            {
                var attributeObjects = field.GetCustomAttributes(typeof(GeneOptionAttribute), false);

                if (attributeObjects.Length > 0)
                {
                    var geneAttributes = attributeObjects.Cast<GeneOptionAttribute>().ToArray();
                    var attribute = geneAttributes[0];
                    var shouldSetToTopValue = _random.Next(0, 2) == 1;

                    SetFieldWithBoundaryValue(field, attribute.IntValues, shouldSetToTopValue);
                    SetFieldWithBoundaryValue(field, attribute.CharValues, shouldSetToTopValue);
                    SetFieldWithBoundaryValue(field, attribute.StringValues, shouldSetToTopValue);
                }
            }
        }

        private void PerformFlipMutation(MutationType type)
        {
            if (type != MutationType.Flip) { return; }

            var attributes = new List<object>();

            foreach (var field in GetMyFields())
            {
                var attributeObjects = field.GetCustomAttributes(typeof(GeneOptionAttribute), false);

                if (attributeObjects.Length > 0)
                {
                    var geneAttributes = attributeObjects.Cast<GeneOptionAttribute>().ToArray();
                    var attribute = geneAttributes[0];

                    SetFieldWithFlippedValue(field);
                    SetFieldWithFlippedValue(field);
                    SetFieldWithFlippedValue(field);
                }
            }
        }

        private List<FieldInfo> GetMyFields()
        {
            var myType = GetType();
            return new List<FieldInfo>(myType.GetFields());
        }

        private void SetFieldWithRandomValue<T>(FieldInfo field, T[] options)
        {
            if (options == null || options.Length == 0) { return; }
            var randomIndex = _random.Next(options.Length);
            var randomValue = options[randomIndex];
            field.SetValue(this, randomValue);
        }

        private void SetFieldWithBoundaryValue<T>(FieldInfo field, T[] options, bool setToTopBoundary)
        {
            if (options == null || options.Length == 0) { return; }
            var index = setToTopBoundary ? options.Length - 1 : 0;
            field.SetValue(this, options[index]);
        }

        private void SetFieldWithFlippedValue(FieldInfo field)
        {
            var boolValue = (bool) field.GetValue(this);
            field.SetValue(this, !boolValue);
        }

        public int GetNumberOfGeneAttributes()
        {
            var attributes = new List<object>();
            var myType = GetType();
            var fields = new List<FieldInfo>(myType.GetFields());

            foreach(var field in fields)
            {
                attributes.AddRange(field.GetCustomAttributes(typeof(GeneOptionAttribute), false));
            }

            return attributes.Count;
        }

        public static bool operator ==(Gene obj1, Gene obj2)
        {
            if (ReferenceEquals(obj1, obj2)) { return true; }
            if (ReferenceEquals(obj1, null)) { return false; }
            if (ReferenceEquals(obj2, null)) { return false; }

            return obj1.Equals(obj2);
        }
        
        public static bool operator !=(Gene obj1, Gene obj2) { return !(obj1 == obj2); }        
    }
}