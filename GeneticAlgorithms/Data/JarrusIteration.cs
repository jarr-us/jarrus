using GeneticAlgorithms.BasicTypes;

namespace GeneticAlgorithms.Data
{
    public abstract class JarrusIteration<T> where T : Gene
    {
        public GAConfiguration<T> Configuration;
        protected T[] _data;
        private JarrusDAO dao;
        public GeneticAlgorithm<T> GeneticAlgorithm;
        public string SessionName;

        public JarrusIteration(string session)
        {
            SessionName = session;

            SetSettings();
            FetchData();

            dao = new JarrusDAO();
            GeneticAlgorithm = new GeneticAlgorithm<T>(Configuration, _data);
            GeneticAlgorithm.GARun.Session = SessionName;
        }

        public GARun<T> Run()
        {
            var run = GeneticAlgorithm.Run();
            dao.InsertCompletedRun(run);

            return run;
        }

        protected abstract void FetchData();
        protected abstract void SetSettings();
    }
}