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
        public List<GATask> CheckoutTasks(int numberOfTasks)
        {
            var sql = GetCheckoutTaskSQL(numberOfTasks);
            var dao = new DAO();
            var list = new List<GATask>();

            try
            {
                dao.OpenConnection(Server.JARRUS, GetCheckoutTaskSQL(numberOfTasks));
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
            } catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex);
            } finally
            {
                dao.CloseConnection();
            }

            return list;
        }

        private string GetCheckoutTaskSQL(int numberOfTasksToCheckout)
        {
            var sb = new StringBuilder();

            sb.Append("BEGIN TRANSACTION;");

            sb.Append("DECLARE @CurrentTime datetime = GETUTCDATE();");
            sb.Append("DECLARE @ComputerName varchar(255) = '");
            sb.Append(GetComputerName());
            sb.Append("'; ");

            sb.Append(";WITH subset AS (SELECT TOP ");
            sb.Append(numberOfTasksToCheckout);
            sb.Append(" *, ROW_NUMBER() OVER(ORDER BY [Priority]) as RowId FROM [DB_9B8C9C_jarrus].[dbo].[GA_Tasks]  WITH (HOLDLOCK, ROWLOCK)");
            sb.Append("where (checkout is null or [checkout] < DATEADD(HOUR, -1, GETUTCDATE())) order by [Priority]) ");
            sb.Append("UPDATE subset SET[Checkout] = GETUTCDATE(), [ComputerName] = @ComputerName ");
            sb.Append("OUTPUT INSERTED.[UUID],INSERTED.[Session],INSERTED.[Priority],INSERTED.[Checkout],INSERTED.[ComputerName],INSERTED.[SolutionStrategy],INSERTED.[ParentSelectionStrategy] ");
            sb.Append(",INSERTED.[MutationStrategy],INSERTED.[CrossoverStrategy],INSERTED.[ImmigrationStrategy],INSERTED.[RetirementStrategy],INSERTED.[ScoringStrategy],INSERTED.[DuplicationStrategy] ");
            sb.Append(",INSERTED.[PopulationSize],INSERTED.[MaxGenerations],INSERTED.[CrossoverRate],INSERTED.[MutationRate],INSERTED.[ElitismRate],INSERTED.[ImmigrationRate]");
            sb.Append(",INSERTED.[MaxRetirement],INSERTED.[ChildrenPerParents],INSERTED.[RandomSeed],INSERTED.[RandomPoolGenerationSeed];");

            sb.Append("COMMIT TRANSACTION;");

            return sb.ToString();
        }

        private string GetComputerName()
        {
            return Environment.MachineName;
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
                    dao.AddSqlStatementToTransaction(GetDeleteTaskSql(complete.Config.TaskUUID));
                    dao.AddSqlStatementToTransaction(GetCompletedRunSql(complete));                    
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
