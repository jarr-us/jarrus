using GeneralHux.ErrorHandling;
using Jarrus.GA;
using Jarrus.GA.Models;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Solution;
using Jarrus.GA.Utility;
using System;
using System.Text;
using GeneralHux.CRUD;
using System.Collections.Generic;
using System.Globalization;

namespace Jarrus.Data
{
    public class JarrusDAO
    {
        private Random random = new Random();
        private int threadId;

        public void CheckoutTasks(int numberOfTasks)
        {
            var sql = GetCheckoutTaskSQL(numberOfTasks);
            var dao = new DAO();
            dao.SimpleExecute(Server.JARRUS, sql);
        }

        private string GetCheckoutTaskSQL(int numberOfTasksToCheckout)
        {
            var sb = new StringBuilder();

            sb.Append(";WITH subset AS (SELECT TOP ");
            sb.Append(numberOfTasksToCheckout);
            sb.Append(" * FROM [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] where checkout is null or [checkout] < DATEADD(HOUR, -1, GETUTCDATE()) order by priority) ");
            sb.Append("UPDATE subset SET[Checkout] = GETUTCDATE(), [ComputerName] = '");
            sb.Append(GetComputerName());
            sb.Append("' ");

            return sb.ToString();
        }

        private string GetComputerName()
        {
            if (threadId == 0) { threadId = random.Next(); }
            return Environment.MachineName + "::" + threadId;
        }

        public List<GATask> FetchMyTasks()
        {
            var sql = "SELECT * FROM [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] WHERE [ComputerName] = @ComputerName order by [Priority]";            
            var dao = new DAO();
            var list = new List<GATask>();

            try
            {
                dao.OpenConnection(Server.JARRUS, sql);
                dao.AddParameter("@ComputerName", GetComputerName());
                dao.Execute();

                while (dao.HasNextRow())
                {
                    var solution = dao.GetString("SolutionStrategy");

                    var task = new GATask((JarrusSolution)Reflection.GetObjectFromType(solution));

                    task.UUID = dao.GetGuid("UUID");
                    task.Session = dao.GetString("Session");
                    task.ComputerName = dao.GetString("ComputerName");
                    task.PopulationSize = dao.GetInt("PopulationSize");
                    task.MaxGenerations = dao.GetInt("MaxGenerations");
                    task.CrossoverRate = (double)dao.GetDecimal("CrossoverRate");
                    task.MutationRate = (double)dao.GetDecimal("MutationRate");
                    task.ElitismRate = (double)dao.GetDecimal("ElitismRate");
                    task.ImmigrationRate = (double)dao.GetDecimal("ImmigrationRate");
                    task.MaxRetirement = dao.GetInt("MaxRetirement");
                    task.ChildrenPerParents = dao.GetInt("ChildrenPerParents");
                    task.RandomSeed = dao.GetInt("RandomSeed");
                    task.RandomPoolGenerationSeed = dao.GetInt("RandomPoolGenerationSeed");

                    task.DuplicationStrategy = (DuplicationStrategy)Enum.Parse(typeof(DuplicationStrategy), dao.GetString("DuplicationStrategy"));
                    task.ScoringStrategy = (ScoringStrategy)Enum.Parse(typeof(ScoringStrategy), dao.GetString("ScoringStrategy"));
                    task.ImmigrationStrategy = (ImmigrationStrategy)Enum.Parse(typeof(ImmigrationStrategy), dao.GetString("ImmigrationStrategy"));
                    task.RetirementStrategy = (RetirementStrategy)Enum.Parse(typeof(RetirementStrategy), dao.GetString("RetirementStrategy"));
                    task.CrossoverStrategy = (CrossoverStrategy)Enum.Parse(typeof(CrossoverStrategy), dao.GetString("CrossoverStrategy"));
                    task.MutationStrategy = (MutationStrategy)Enum.Parse(typeof(MutationStrategy), dao.GetString("MutationStrategy"));
                    task.ParentSelectionStrategy = (ParentSelectionStrategy)Enum.Parse(typeof(ParentSelectionStrategy), dao.GetString("ParentSelectionStrategy"));

                    list.Add(task);
                }
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to fetch GA Tasks");
                return new List<GATask>();
            }
            finally
            {
                dao.CloseConnection();
            }

            return list;
        }

        public void InsertCompletedRunsAndClearTasks(List<TaskCompleted> completed)
        {
            if (completed == null || completed.Count == 0) { return; }
            var dao = new DAO();

            try
            {
                dao.OpenTransaction(Server.JARRUS);

                foreach(var complete in completed)
                {
                    dao.AddSqlStatementToTransaction(GetCompletedRunSql(complete));
                    dao.AddSqlStatementToTransaction(GetDeleteTaskSql(complete.Config.TaskUUID));
                }                

                dao.ExecuteTransaction();
            } catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex);
                throw ex;
            } finally
            {
                dao.CloseConnection();
            }
        }

        private string GetCompletedRunSql(TaskCompleted completed)
        {
            var run = completed.Run;
            var config = completed.Config;

            var sb = new StringBuilder();

            sb.Append("INSERT INTO [dbo].[GA_Results]([Session],[Start],[End],[ComputerName],[SolutionStrategy],[ParentSelectionStrategy],[MutationStrategy],[CrossoverStrategy],[ImmigrationStrategy],[RetirementStrategy],[ScoringStrategy],[PopulationSize],[MaxGenerations],[CrossoverRate],[MutationRate],[ElitismRate],[ImmigrationRate],[DuplicationStrategy],[MaxRetirement],[ChildrenPerParents],[RandomSeed],[RandomPoolGenerationSeed],[BestScore],[BestScoreGeneration],[StringRepresentation]) ");
            sb.Append(" VALUES ( ");

            sb.Append("'" + config.Session + "',");
            sb.Append("'" + run.Start.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "',");
            sb.Append("'" + run.End.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "',");
            sb.Append("'" + GetComputerName() + "',");
            sb.Append("'" + config.Solution.GetType().AssemblyQualifiedName + "',");
            sb.Append("'" + config.ParentSelectionStrategy.ToString() + "',");
            sb.Append("'" + config.MutationStrategy.ToString() + "',");
            sb.Append("'" + config.CrossoverStrategy.ToString() + "',");
            sb.Append("'" + config.ImmigrationStrategy.ToString() + "',");
            sb.Append("'" + config.RetirementStrategy.ToString() + "',");
            sb.Append("'" + config.ScoringStrategy.ToString() + "',");
            sb.Append(config.PopulationSize + ",");
            sb.Append(config.MaxGenerations + ",");
            sb.Append(config.CrossoverRate + ",");
            sb.Append(config.MutationRate + ",");
            sb.Append(config.ElitismRate + ",");
            sb.Append(config.ImmigrationRate + ",");
            sb.Append("'" + config.DuplicationStrategy.ToString() + "',");
            sb.Append(config.MaxRetirement + ",");
            sb.Append(config.ChildrenPerParents + ",");
            sb.Append(config.RandomSeed + ",");
            sb.Append(config.RandomPoolGenerationSeed + ",");
            sb.Append(run.BestChromosome.FitnessScore + ",");
            sb.Append(run.BestChromosome.GenerationNumber + ",");
            sb.Append("'" + run.BestChromosome.ToString() + "'");

            sb.Append(")");
            return sb.ToString();
        }
        
        private string GetDeleteTaskSql(string uuid)
        {
            return "delete FROM [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] where uuid = '" + uuid + "'";
        }
    }
}
