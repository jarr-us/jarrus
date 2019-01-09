﻿using Jarrus.GA.BasicTypes.Genes;
using Jarrus.GA.Enums;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jarrus.GA
{
    [DebuggerDisplay("{FirstName} {LastName} {FitnessScore}")]
    public class Chromosome
    {
        public int Age, GenerationNumber;
        public Gene[] Genes;
        public double FitnessScore;
        public int Children;
        public FirstName FirstName;
        public LastName LastName;

        public List<Chromosome> Parents = new List<Chromosome>();
        public List<LastName> Lineage = new List<LastName>();

        public Chromosome(int geneSize)
        {
            Genes = new Gene[geneSize];
        }

        public Chromosome(params Gene[] genes)
        {
            Genes = (Gene[])genes.Clone();
        }

        public void SetParents(params Chromosome[] parents)
        {
            foreach(var parent in parents)
            {
                Parents.Add(parent);
            }

            SetLineage();
        }

        private void SetLineage()
        {
            foreach(var parent in Parents)
            {
                Lineage.Add(parent.LastName);

                foreach(var grandparent in parent.Parents)
                {
                    Lineage.Add(grandparent.LastName);

                    foreach(var gg in grandparent.Parents)
                    {
                        Lineage.Add(gg.LastName);

                        foreach(var ggg in gg.Parents)
                        {
                            Lineage.Add(ggg.LastName);

                            foreach(var gggg in ggg.Parents)
                            {
                                Lineage.Add(gggg.LastName);
                            }
                        }
                    }
                }
            }
        }

        public bool ShouldRetire(GAConfiguration settings)
        {
            return Age >= settings.MaximumLifeSpan && settings.MaximumLifeSpan != 0;
        }

        public override string ToString() { return string.Join(",", (IEnumerable<Gene>) Genes); }

        public static bool operator ==(Chromosome obj1, Chromosome obj2)
        {
            if (ReferenceEquals(obj1, obj2)) { return true; }
            if (ReferenceEquals(obj1, null)) { return false; }
            if (ReferenceEquals(obj2, null)) { return false; }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Chromosome obj1, Chromosome obj2) { return !(obj1 == obj2); }

        public override bool Equals(object obj)
        {
            var castedObject = (Chromosome)obj;

            for(int i = 0; i < Genes.Length; i++)
            {
                if (Genes[i] != castedObject.Genes[i])
                {
                    return false;
                }
            }

            return true;
        }

        public void ReplaceGenes(int startingPosition, params Gene[] genes)
        {
            var counter = 0;
            for(int i = startingPosition; i < startingPosition + genes.Length; i++)
            {
                Genes[i] = genes[counter++];
            }
        }

        public override int GetHashCode() {
            int sum = 0;

            for(int i = 0; i < Genes.Length; i++)
            {
                sum += (i + 1) * Genes[i].GetHashCode();
            }

            return sum;
        }
    }
}
