using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithms.Data
{
    public abstract class GeneDAO<T> 
    {
        public abstract T[] FetchOptions(); 
    }
}
