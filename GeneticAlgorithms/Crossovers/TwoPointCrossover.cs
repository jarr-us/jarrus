namespace GeneticAlgorithms.Crossovers
{
    public class TwoPointCrossover : Crossover
    {
        public override int GetId() { return 3; }

        protected override Chromosome<T> Perform<T>(Chromosome<T> father, Chromosome<T> mother, GAConfiguration<T> settings)
        {
            var geneCount = father.Genes.Length;
            var child = new Chromosome<T>(geneCount);

            var firstCrossoverPoint = settings.GetRandomInteger(1, father.Genes.Length - 2);
            var secondCrossoverPoint = settings.GetRandomInteger(firstCrossoverPoint + 1, father.Genes.Length);

            for (int i = 0; i < geneCount; i++)
            {
                if (i < firstCrossoverPoint)
                {
                    child.Genes[i] = father.Genes[i];
                }
                else if (i < secondCrossoverPoint)
                {
                    child.Genes[i] = mother.Genes[i];
                }
                else
                {
                    child.Genes[i] = father.Genes[i];
                }
            }

            return child;
        }
    }
}
