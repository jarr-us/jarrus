using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithms.Data
{
    public abstract class GeneDAO<T> where T : Gene
    {
        public abstract T[] FetchOptions(); 
    }
}
