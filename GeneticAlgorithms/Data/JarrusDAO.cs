using GeneralHux.CRUD;
using GeneralHux.ErrorHandling;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Crossovers;
using GeneticAlgorithms.FitnessFunctions;
using GeneticAlgorithms.Mutations;
using GeneticAlgorithms.ParentSelections;
using System;

namespace GeneticAlgorithms.Data
{
    public class JarrusDAO
    {
        private Random random = new Random();
        private int threadId; 

        public GATask<T> CheckoutATaskToRun<T>() where T : Gene
        {
            var sql = "UPDATE TOP (1) [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] SET [Checkout] = GETUTCDATE(), [ComputerName] = @ComputerName ";
            sql += "WHERE [Checkout] IS NULL AND [Priority] = (SELECT MIN([Priority]) FROM [DB_9B8C9C_jarrus].[dbo].[GA_Tasks])";
            var dao = new DAO();

            try
            {
                dao.OpenConnection(Server.JARRUS, sql);
                dao.AddParameter("@ComputerName", GetComputerName());
                dao.Execute();
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to insert GA Task");
                return null;
            }
            finally
            {
                dao.CloseConnection();
            }

            return FetchMyFirstTask<T>();
        }

        private string GetComputerName()
        {
            if (threadId == 0) { threadId = random.Next(); }
            return Environment.MachineName + "::" + threadId;
        }

        public void ClearOutUnfinishedTasks()
        {
            var sql = "UPDATE [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] SET Checkout = NULL, ComputerName = NULL WHERE Checkout < DATEADD(HOUR, -2, GETUTCDATE()) ";
            var dao = new DAO();

            try
            {
                dao.OpenConnection(Server.JARRUS, sql);
                dao.Execute();
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to clear out unfinished tasks");
                throw ex;
            }
            finally
            {
                dao.CloseConnection();
            }
        }

        public GATask<T> FetchMyFirstTask<T>() where T : Gene
        {
            var sql = "SELECT TOP 1 * FROM [DB_9B8C9C_jarrus].[dbo].[GA_Tasks] WHERE [ComputerName] = @ComputerName order by [Priority]";
            var task = new GATask<T>();
            var dao = new DAO();

            try
            {
                dao.OpenConnection(Server.JARRUS, sql);
                dao.AddParameter("@ComputerName", GetComputerName());
                dao.Execute();

                while(dao.HasNextRow())
                {
                    task.UUID = dao.GetGuid("UUID");
                    task.Session = dao.GetString("Session");
                    task.ComputerName = dao.GetString("ComputerName");
                    task.LowestScoreIsBest = dao.GetBoolean("LowestScoreIsBest");
                    task.MaxPopulationSize = dao.GetInt("MaxPopulationSize");
                    task.MaxGenerations = dao.GetInt("MaxGenerations");
                    task.CrossoverRate = (double) dao.GetDecimal("CrossoverRate");
                    task.MutationRate = (double)dao.GetDecimal("MutationRate");
                    task.ElitismRate = (double)dao.GetDecimal("ElitismRate");
                    task.PreventDuplications = dao.GetBoolean("PreventDuplications");
                    task.MaximumLifeSpan = dao.GetInt("MaximumLifeSpan");
                    task.ChildrenPerCouple = dao.GetInt("ChildrenPerCouple");
                    task.RandomSeed = dao.GetInt("RandomSeed");
                    task.RandomPoolGenerationSeed = dao.GetInt("RandomPoolGenerationSeed");

                    AttachFitnessFunctionToTask(task, dao.GetString("SolutionType"));
                    AttachParentSelectionToTask(task, dao.GetString("ParentSelectionType"));
                    AttachMutationToTask(task, dao.GetString("MutationType"));
                    AttachCrossoverToTask(task, dao.GetString("CrossoverType"));
                }

                return task;
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to insert GA Task");
                throw ex;
            }
            finally
            {
                dao.CloseConnection();
            }
        }

        private void AttachFitnessFunctionToTask<T>(GATask<T> task, string className) where T : Gene
        {
            var elementType = Type.GetType(className);
            if (elementType == null) return;
            task.FitnessFunction = (FitnessFunction)(Activator.CreateInstance(elementType));
        }

        private void AttachParentSelectionToTask<T>(GATask<T> task, string className) where T : Gene
        {
            var elementType = Type.GetType(className);
            if (elementType == null) return;
            task.ParentSelection = (ParentSelection<T>)(Activator.CreateInstance(elementType));
        }

        private void AttachMutationToTask<T>(GATask<T> task, string className) where T : Gene
        {
            var elementType = Type.GetType(className);
            if (elementType == null) return;
            task.Mutation = (Mutation)(Activator.CreateInstance(elementType));
        }

        private void AttachCrossoverToTask<T>(GATask<T> task, string className) where T : Gene
        {
            var elementType = Type.GetType(className);
            if (elementType == null) return;
            task.Crossover = (Crossover)(Activator.CreateInstance(elementType));
        }

        public void InsertTask<T>(GATask<T> task, double priority) where T : Gene
        {
            var sql = "INSERT INTO [dbo].[GA_Tasks] ([Session],[Priority],[Checkout],[ComputerName],[SolutionType],[ParentSelectionType],[MutationType],[CrossoverType],[LowestScoreIsBest],[MaxPopulationSize],[MaxGenerations],[CrossoverRate],[MutationRate],[ElitismRate],[PreventDuplications],[MaximumLifeSpan],[ChildrenPerCouple],[RandomSeed],[RandomPoolGenerationSeed]) ";
            sql += "VALUES(@Session, @Priority, @Checkout, @ComputerName, @SolutionType, @ParentSelectionType, @MutationType, @CrossoverType, @LowestScoreIsBest, @MaxPopulationSize, @MaxGenerations, @CrossoverRate, @MutationRate, @ElitismRate, @PreventDuplications, @MaximumLifeSpan, @ChildrenPerCouple, @RandomSeed, @RandomPoolGenerationSeed)";

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
                throw ex;
            }
            finally
            {
                dao.CloseConnection();
            }
        }

        public void InsertCompletedRun<T>(GAConfiguration<T> config, GARun<T> run) where T : Gene
        {
            var dao = new DAO();
            var sql = "INSERT INTO [dbo].[GA_Results]([Session],[Start],[End],[ComputerName],[SolutionType],[ParentSelectionType],[MutationType],[CrossoverType],[LowestScoreIsBest],[MaxPopulationSize],[MaxGenerations],[CrossoverRate],[MutationRate],[ElitismRate],[PreventDuplications],[MaximumLifeSpan],[ChildrenPerCouple],[RandomSeed],[RandomPoolGenerationSeed],[BestScore],[BestScoreGeneration],[StringRepresentation]) ";
            sql += "VALUES (@Session,@Start,@End,@ComputerName,@SolutionType,@ParentSelectionType,@MutationType,@CrossoverType,@LowestScoreIsBest,@MaxPopulationSize,@MaxGenerations,@CrossoverRate,@MutationRate,@ElitismRate,@PreventDuplications,@MaximumLifeSpan,@ChildrenPerCouple,@RandomSeed,@RandomPoolGenerationSeed,@BestScore,@BestScoreGeneration,@StringRepresentation)";

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
                throw ex;
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
                throw ex;
            }
            finally
            {
                dao.CloseConnection();
            }
        }

        private void AddGATaskParameters<T>(DAO dao, GATask<T> task) where T: Gene
        {
            dao.AddParameter("@Session", task.Session);
            dao.AddParameter("@ComputerName", task.ComputerName);
            dao.AddParameter("@SolutionType", task.FitnessFunction.GetType().FullName + ", " + task.FitnessFunction.GetType().Assembly);
            dao.AddParameter("@ParentSelectionType", task.ParentSelection.GetType().FullName + ", " + task.ParentSelection.GetType().Assembly);
            dao.AddParameter("@MutationType", task.Mutation.GetType().FullName + ", " + task.Mutation.GetType().Assembly);
            dao.AddParameter("@CrossoverType", task.Crossover.GetType().FullName + ", " + task.Crossover.GetType().Assembly);
            dao.AddParameter("@LowestScoreIsBest", task.LowestScoreIsBest);
            dao.AddParameter("@MaxPopulationSize", task.MaxPopulationSize);
            dao.AddParameter("@MaxGenerations", task.MaxGenerations);
            dao.AddParameter("@CrossoverRate", task.CrossoverRate);
            dao.AddParameter("@MutationRate", task.MutationRate);
            dao.AddParameter("@ElitismRate", task.ElitismRate);
            dao.AddParameter("@PreventDuplications", task.PreventDuplications);
            dao.AddParameter("@MaximumLifeSpan", task.MaximumLifeSpan);
            dao.AddParameter("@ChildrenPerCouple", task.ChildrenPerCouple);
            dao.AddParameter("@RandomSeed", task.RandomSeed);
            dao.AddParameter("@RandomPoolGenerationSeed", task.RandomPoolGenerationSeed);
        }
    }
}
