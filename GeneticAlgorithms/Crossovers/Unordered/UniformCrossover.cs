namespace GeneticAlgorithms.Crossovers.Unordered
{
    public class UniformCrossover : Crossover
    {
        protected override Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> configuration)
        {
            var geneCount = father.Genes.Length;
            var childOne = new Chromosome<T>(geneCount);
            var childTwo = new Chromosome<T>(geneCount);

            for (int i = 0; i < geneCount; i++)
            {
                if (configuration.GetRandomBoolean())
                {
                    childOne.Genes[i] = father.Genes[i];
                    childTwo.Genes[i] = mother.Genes[i];
                }
                else
                {
                    childOne.Genes[i] = mother.Genes[i];
                    childTwo.Genes[i] = father.Genes[i];
                }
            }

            if (configuration.GetNextDouble() < 0.5)
            {
                return childOne;
            }
            else
            {
                return childTwo;
            }
        }
    }
}