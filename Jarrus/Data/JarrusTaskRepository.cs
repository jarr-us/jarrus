using Jarrus.GA.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Jarrus.Data
{
    public class JarrusTaskRepository
    {
        private List<GATask> _tasksToRun = new List<GATask>();
        private List<TaskCompleted> _completedRuns = new List<TaskCompleted>();
        private JarrusDAO _dao = new JarrusDAO();
        private const int TASKS_PER_BATCH = 250;
        private Thread _insertThread, _fetchThread;
        private object _completedLock = new object();
        private object _tasksToRunLock = new object();
        private const int STANDARD_WAIT_TIME = 15000;
        public int NumberOfRunsCompleted;

        public static JarrusTaskRepository Instance = new JarrusTaskRepository();

        private JarrusTaskRepository()
        {
            _insertThread = new Thread(InsertLoop);
            _insertThread.Start();

            _fetchThread = new Thread(FetchLoop);
            _fetchThread.Start();
        }

        public int GetNumberOfCompletedRunsToInsert() { return _completedRuns.Count(); }
        public int GetNumberOfTasksToRun() { return _tasksToRun.Count(); }

        private void InsertLoop()
        {
            while(MainForm.IsRunning)
            {
                List<TaskCompleted> tasksToInsert;

                lock (_completedLock)
                {
                    tasksToInsert = _completedRuns;
                    _completedRuns = new List<TaskCompleted>();
                }

                try {
                    _dao.InsertCompletedRunsAndClearTasks(tasksToInsert);                    
                }
                catch(Exception) {
                    lock (_completedLock)
                    {
                        _completedRuns.AddRange(tasksToInsert);
                    }
                }

                Thread.Sleep(STANDARD_WAIT_TIME);
            }
        }

        private void FetchLoop()
        {
            var sw = new Stopwatch();

            while (MainForm.IsRunning)
            {
                if (_tasksToRun.Count() < TASKS_PER_BATCH / 2)
                {
                    var tasks = _dao.CheckoutTasks(TASKS_PER_BATCH);
                    lock(_tasksToRunLock) { _tasksToRun.AddRange(tasks); }                    
                } else
                {
                    Thread.Sleep(STANDARD_WAIT_TIME);
                }
            }
        }

        public GATask GetTask()
        {
            GATask first;
            lock (_tasksToRunLock)
            {
                first = _tasksToRun.FirstOrDefault();
                if (first != null) { _tasksToRun.Remove(first); }
            }

            return first;
        }

        public void InsertCompletedRun(TaskCompleted completed)
        {
            lock (_completedLock) { _completedRuns.Add(completed); }
            NumberOfRunsCompleted++;
        }
    }
}
