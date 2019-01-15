namespace Jarrus.Models
{
    public class TaskThread
    {
        public TaskRunner TaskRunner = new TaskRunner();
        public bool ShouldBeRunning = false;
        private int _threadNumber;
        public bool IsAlive = false;

        public TaskThread(int threadNumber)
        {
            _threadNumber = threadNumber;
        }

        public void Start()
        {
            ShouldBeRunning = true;
            LoopTaskRunner();
        }

        public void Stop()
        {
            ShouldBeRunning = false;
        }

        private void LoopTaskRunner() {
            IsAlive = true;
            while (ShouldBeRunning) { TaskRunner.RunIteration(); }
            IsAlive = false;
        }
    }
}
