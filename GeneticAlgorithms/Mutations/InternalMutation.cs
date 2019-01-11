using Jarrus.GA.Models;

namespace Jarrus.GA.Mutations
{
    public class InternalMutation : Mutation
    {
        protected override void Perform(GAConfiguration settings, Chromosome chromosome, int geneIndex)
        {
            var gene = chromosome.Genes[geneIndex];

        }
    }
}