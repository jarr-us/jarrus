using GeneralHux.CRUD;
using GeneralHux.ErrorHandling;
using Jarrus.GA;
using Jarrus.GA.Models;
using Jarrus.GA.Factory.Enums;
using Jarrus.GA.Solution;
using Jarrus.GA.Utility;
using System;

namespace Jarrus.Data
{
    public class JarrusDAO
    {
        private Random random = new Random();
        private int threadId;

        public GATask CheckoutATaskToRun()
        {
            var sql = "UPDATE TOP (1) [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] SET [Checkout] = GETUTCDATE(), [ComputerName] = @ComputerName ";
            sql += "WHERE [uuid] = (select top 1 uuid FROM [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] where checkout is null or [checkout] < DATEADD(HOUR, -1, GETUTCDATE()) order by priority)";
            var dao = new DAO();

            try
            {
                dao.OpenConnection(Server.JARRUS, sql);
                dao.AddParameter("@ComputerName", GetComputerName());
                dao.Execute();
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to checkout a GA Task");
                return null;
            }
            finally
            {
                dao.CloseConnection();
            }

            return FetchMyFirstTask();
        }

        private string GetComputerName()
        {
            if (threadId == 0) { threadId = random.Next(); }
            return Environment.MachineName + "::" + threadId;
        }

        public GATask FetchMyFirstTask()
        {
            var sql = "SELECT TOP 1 * FROM [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] WHERE [ComputerName] = @ComputerName order by [Priority]";
            var dao = new DAO();

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

                    return task;
                }
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to fetch a GA Task");
            }
            finally
            {
                dao.CloseConnection();
            }

            return null;
        }

        public void InsertTask(GATask task, double priority)
        {
            var sql = "INSERT INTO [dbo].[GA_Tasks] ([Session],[Priority],[Checkout],[ComputerName],[SolutionStrategy],[ParentSelectionStrategy],[MutationStrategy],[CrossoverStrategy],[ImmigrationStrategy],[RetirementStrategy],[ScoringStrategy],[PopulationSize],[MaxGenerations],[CrossoverRate],[MutationRate],[ElitismRate],[ImmigrationRate],[DuplicationStrategy],[MaxRetirement],[ChildrenPerParents],[RandomSeed],[RandomPoolGenerationSeed]) ";
            sql += "VALUES(@Session, @Priority, @Checkout, @ComputerName, @SolutionStrategy, @ParentSelectionStrategy, @MutationStrategy, @CrossoverStrategy, @ImmigrationStrategy, @RetirementStrategy, @ScoringStrategy, @PopulationSize, @MaxGenerations, @CrossoverRate, @MutationRate, @ElitismRate, @ImmigrationRate, @DuplicationStrategy, @MaxRetirement, @ChildrenPerParents, @RandomSeed, @RandomPoolGenerationSeed)";

            var dao = new DAO();
            try
            {
                dao.OpenConnection(Server.JARRUS, sql);

                dao.AddParameter("@Priority", priority);
                dao.AddParameter("@Checkout", null);
                AddGATaskParameters(dao, task);

                dao.Execute();
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to insert GA Task");
            }
            finally
            {
                dao.CloseConnection();
            }
        }

        public void InsertCompletedRun(GAConfiguration config, GARun run)
        {
            var dao = new DAO();
            var sql = "INSERT INTO [dbo].[GA_Results]([Session],[Start],[End],[ComputerName],[SolutionStrategy],[ParentSelectionStrategy],[MutationStrategy],[CrossoverStrategy],[ImmigrationStrategy],[RetirementStrategy],[ScoringStrategy],[PopulationSize],[MaxGenerations],[CrossoverRate],[MutationRate],[ElitismRate],[ImmigrationRate],[DuplicationStrategy],[MaxRetirement],[ChildrenPerParents],[RandomSeed],[RandomPoolGenerationSeed],[BestScore],[BestScoreGeneration],[StringRepresentation]) ";
            sql += "VALUES (@Session,@Start,@End,@ComputerName,@SolutionStrategy,@ParentSelectionStrategy,@MutationStrategy,@CrossoverStrategy,@ImmigrationStrategy,@RetirementStrategy,@ScoringStrategy,@PopulationSize,@MaxGenerations,@CrossoverRate,@MutationRate,@ElitismRate,@ImmigrationRate,@DuplicationStrategy,@MaxRetirement,@ChildrenPerParents,@RandomSeed,@RandomPoolGenerationSeed,@BestScore,@BestScoreGeneration,@StringRepresentation)";

            try
            {
                dao.OpenConnection(Server.JARRUS, sql);

                dao.AddParameter("@Start", run.Start);
                dao.AddParameter("@End", run.End);
                dao.AddParameter("@BestScore", run.BestChromosome.FitnessScore);
                dao.AddParameter("@BestScoreGeneration", run.BestChromosome.GenerationNumber);
                dao.AddParameter("@StringRepresentation", run.BestChromosome.ToString());
                AddGATaskParameters(dao, config);

                dao.Execute();
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to insert GA Run Result");
            }
            finally
            {
                dao.CloseConnection();
            }
        }

        public void DeleteTask(string uuid)
        {
            var dao = new DAO();
            var sql = "delete FROM [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] where uuid = @UUID";

            try
            {
                dao.OpenConnection(Server.JARRUS, sql);

                dao.AddParameter("@UUID", uuid);

                dao.Execute();
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to delete UUID");
            }
            finally
            {
                dao.CloseConnection();
            }
        }

        private void AddGATaskParameters(DAO dao, GAProperties properties)
        {
            dao.AddParameter("@Session", properties.Session);
            dao.AddParameter("@ComputerName", properties.ComputerName);

            dao.AddParameter("@SolutionStrategy", properties.Solution.GetType().AssemblyQualifiedName);
            dao.AddParameter("@ParentSelectionStrategy", properties.ParentSelectionStrategy.ToString());
            dao.AddParameter("@MutationStrategy", properties.MutationStrategy.ToString());
            dao.AddParameter("@ImmigrationStrategy", properties.ImmigrationStrategy.ToString());
            dao.AddParameter("@RetirementStrategy", properties.RetirementStrategy.ToString());
            dao.AddParameter("@CrossoverStrategy", properties.CrossoverStrategy.ToString());
            dao.AddParameter("@ScoringStrategy", properties.ScoringStrategy.ToString());
            dao.AddParameter("@DuplicationStrategy", properties.DuplicationStrategy.ToString());

            dao.AddParameter("@MaxGenerations", properties.MaxGenerations);
            dao.AddParameter("@CrossoverRate", properties.CrossoverRate);
            dao.AddParameter("@MutationRate", properties.MutationRate);
            dao.AddParameter("@ElitismRate", properties.ElitismRate);
            dao.AddParameter("@ImmigrationRate", properties.ImmigrationRate);

            dao.AddParameter("@PopulationSize", properties.PopulationSize);
            dao.AddParameter("@MaxRetirement", properties.MaxRetirement);
            dao.AddParameter("@ChildrenPerParents", properties.ChildrenPerParents);
            dao.AddParameter("@RandomSeed", properties.RandomSeed);
            dao.AddParameter("@RandomPoolGenerationSeed", properties.RandomPoolGenerationSeed);
        }
    }
}
