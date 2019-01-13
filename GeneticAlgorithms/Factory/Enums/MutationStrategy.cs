namespace Jarrus.GA.Factory.Enums
{
    public enum MutationStrategy
    {
        //Ordered
        Insert = 1,
        Inversion = 2,
        Scramble = 3,
        Swap = 4,

        //Unordered
        Flip = 5,
        Boundary = 6,
        Random = 7
    }
}
