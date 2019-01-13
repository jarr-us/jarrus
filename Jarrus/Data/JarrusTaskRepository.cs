using Jarrus.GA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarrus.Data
{
    public class JarrusTaskRepository
    {
        private List<GATask> _tasksToRun = new List<GATask>();
        private List<TaskCompleted> _completedRuns = new List<TaskCompleted>();
        private JarrusDAO _dao = new JarrusDAO();
        private const int TASKS_PER_BATCH = 35;

        public GATask GetTask()
        {
            if (_tasksToRun.Count == 0) { InsertBatch(); }

            var first = _tasksToRun.FirstOrDefault();
            if (first != null) { _tasksToRun.Remove(first); }
            return first;
        }

        public void InsertCompletedRun(TaskCompleted completed)
        {
            _completedRuns.Add(completed);
        }

        private void RefreshTaskList()
        {
            try
            {
                if (_tasksToRun.Count == 0)
                {
                    _dao.CheckoutTasks(TASKS_PER_BATCH);
                }

                _tasksToRun.AddRange(_dao.FetchMyTasks());
            }
            catch (Exception) { }            
        }

        private void InsertBatch()
        {
            try
            {
                _dao.InsertCompletedRunsAndClearTasks(_completedRuns);
                _completedRuns = new List<TaskCompleted>();
                RefreshTaskList();
            } catch (Exception) { }            
        }
    }
}
