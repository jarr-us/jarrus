using Jarrus.GA.Models;

namespace Jarrus.Data
{
    public class TaskCompleted
    {
        public GARun Run;
        public GAConfiguration Config;

        public TaskCompleted(GARun run, GAConfiguration config)
        {
            Config = config;
            Run = run;
        }
    }
}
