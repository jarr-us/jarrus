namespace GeneticAlgorithms.Crossovers
{
    public class UniformCrossover : Crossover
    {
        public override int GetId() { return 4; }

        protected override Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> settings)
        {
            var geneCount = father.Genes.Length;
            var child = new Chromosome<T>(geneCount);

            for (int i = 0; i < geneCount; i++)
            {
                if (settings.GetRandomBoolean())
                {
                    child.Genes[i] = father.Genes[i];
                }
                else
                {
                    child.Genes[i] = mother.Genes[i];
                }
            }

            return child;
        }
    }
}