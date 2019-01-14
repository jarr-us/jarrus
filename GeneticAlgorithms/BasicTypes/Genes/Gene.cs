using Jarrus.GA.Models.Attributes;
using Jarrus.GA.Factory.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Jarrus.GA.BasicTypes.Genes;

namespace Jarrus.GA.Models
{
    public abstract class Gene
    {
        public abstract override string ToString();
        public abstract override int GetHashCode();
        private Random _random;

        public Gene() { }

        public Gene(Random random)
        {
            _random = random ?? throw new ArgumentException("When instantiating a Gene, the random object may not be null.");            
            Mutate(MutationStrategy.Random);
        }


        public void Mutate(MutationStrategy type) {
            PerformRandomMutation(type);
            PerformBoundaryMutation(type);
            PerformFlipMutation(type);
        }
        
        private void PerformRandomMutation(MutationStrategy type)
        {
            if (type != MutationStrategy.Random) { return; }
            var attributes = new List<object>();
                        
            var fields = GeneFieldRepository.Instance.GetFieldsFor(this);
            foreach (var f in fields)
            {
                SetFieldWithRandomValue(f.Field, f.Attribute.IntValues);
                SetFieldWithRandomValue(f.Field, f.Attribute.CharValues);
                SetFieldWithRandomValue(f.Field, f.Attribute.StringValues);
            }
        }

        private void PerformBoundaryMutation(MutationStrategy type)
        {
            if (type != MutationStrategy.Boundary) { return; }

           
            var fields = GeneFieldRepository.Instance.GetFieldsFor(this);
            foreach(var f in fields)
            {
                var shouldSetToTopValue = _random.Next(0, 2) == 1;

                SetFieldWithBoundaryValue(f.Field, f.Attribute.IntValues, shouldSetToTopValue);
                SetFieldWithBoundaryValue(f.Field, f.Attribute.CharValues, shouldSetToTopValue);
                SetFieldWithBoundaryValue(f.Field, f.Attribute.StringValues, shouldSetToTopValue);
            }
        }

        public List<FieldInfo> GetMyFields()
        {
            var myType = GetType();
            return new List<FieldInfo>(myType.GetFields());
        }

        private void PerformFlipMutation(MutationStrategy type)
        {
            if (type != MutationStrategy.Flip) { return; }

            var fields = GeneFieldRepository.Instance.GetFieldsFor(this);
            foreach (var f in fields)
            {
                SetFieldWithFlippedValue(f.Field);
                SetFieldWithFlippedValue(f.Field);
                SetFieldWithFlippedValue(f.Field);
            }
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

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
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