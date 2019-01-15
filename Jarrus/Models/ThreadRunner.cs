using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jarrus.Models
{
    public class ThreadRunner
    {
        public static ThreadRunner Instance = new ThreadRunner();

        private const int MIN_THREADS = 1;
        private const int MAX_THREADS = 8;
        private List<TaskThread> _threads = new List<TaskThread>(); 
        private int _numberOfThreads = 1;
        private object threadChangeLock = new object();
        private Thread _t;

        private ThreadRunner()
        {
            for (int i = 0; i < MAX_THREADS; i++) { _threads.Add(new TaskThread(i + 1)); }
            _t = new Thread(RunLoopedThread);
            _t.Start();
        }

        public void SetThreadCount(int threadCount)
        {
            _numberOfThreads = threadCount;
            var t = new Thread(ThreadedChangeThreadCount);
            t.Start();
        }

        private void ThreadedChangeThreadCount()
        {
            lock (threadChangeLock)
            {
                for (int i = 0; i < MAX_THREADS; i++)
                {
                    _threads[i].Stop();
                }

                while (_threads.Where(o => o.IsAlive).Any()) { Thread.Sleep(1000); }
                while (_t.IsAlive) { Thread.Sleep(1000); }

                _t = new Thread(RunLoopedThread);
                _t.Start();
            }
        }

        public void Shutdown()
        {
            lock (threadChangeLock)
            {
                for (int i = 0; i < MAX_THREADS; i++)
                {
                    _threads[i].Stop();
                }
            }
        }

        private void RunLoopedThread()
        {
            var threadsToRun = _threads.Take(_numberOfThreads);
            Parallel.ForEach(threadsToRun, thread =>
            {
                thread.Start();
            });
        }

        public TaskThread GetMainTaskThread() { return _threads[0]; }
    }
}
