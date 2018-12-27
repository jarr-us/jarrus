namespace GeneticAlgorithms.Crossovers
{
    public class SinglePointCrossover : Crossover
    {
        public override int GetId() { return 2; }

        protected override Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> settings)
        {
            var geneCount = father.Genes.Length;
            var child = new Chromosome<T>(geneCount);
            var crossoverPoint = settings.GetRandomInteger(1, father.Genes.Length - 2);

            for (int i = 0; i < geneCount; i++)
            {
                if (i < crossoverPoint)
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
