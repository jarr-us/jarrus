using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithms.ParentSelections
{
    public class ChromosomeParents<T> where T : Gene
    {
        public Chromosome<T> Father;
        public Chromosome<T> Mother;
    }
}
