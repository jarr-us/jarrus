namespace Jarrus.GA.Crossovers.Unordered
{
    public class UniformCrossover : Crossover
    {
        protected override Chromosome Perform(Chromosome father, Chromosome mother, GAConfiguration configuration)
        {
            var geneCount = father.Genes.Length;
            var childOne = new Chromosome(geneCount);
            var childTwo = new Chromosome(geneCount);

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