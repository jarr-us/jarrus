using GeneralHux.CRUD;
using GeneralHux.ErrorHandling;
using GeneticAlgorithms.BasicTypes;
using GeneticAlgorithms.Factory.Enums;
using GeneticAlgorithms.Solution;
using GeneticAlgorithms.Utility;
using System;

namespace GeneticAlgorithms.Data
{
    public class JarrusDAO
    {
        private Random random = new Random();
        private int threadId; 

        public GATask CheckoutATaskToRun()
        {
            var sql = "UPDATE TOP (1) [DB_9B8C9C_jarrus].[dbo].[GA_Task] SET [Checkout] = GETUTCDATE(), [ComputerName] = @ComputerName ";
            sql += "WHERE [Checkout] IS NULL AND [Priority] = (SELECT MIN([Priority]) FROM [DB_9B8C9C_jarrus].[dbo].[GA_Task])";
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

            return FetchMyFirstTask();
        }

        private string GetComputerName()
        {
            if (threadId == 0) { threadId = random.Next(); }
            return Environment.MachineName + "::" + threadId;
        }

        public GATask FetchMyFirstTask()
        {
            var sql = "SELECT TOP 1 * FROM [DB_9B8C9C_jarrus].[dbo].[GA_Task] WHERE [ComputerName] = @ComputerName order by [Priority]";            
            var dao = new DAO();

            try
            {
                dao.OpenConnection(Server.JARRUS, sql);
                dao.AddParameter("@ComputerName", GetComputerName());
                dao.Execute();

                while(dao.HasNextRow())
                {
                    var solution = dao.GetString("SolutionType");

                    var task = new GATask((JarrusSolution) Reflection.GetObjectFromType(solution));

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

                    task.CrossoverType = (CrossoverType)Enum.Parse(typeof(CrossoverType), dao.GetString("CrossoverType"));
                    task.MutationType = (MutationType)Enum.Parse(typeof(MutationType), dao.GetString("MutationType"));
                    task.ParentSelectionType = (ParentSelectionType)Enum.Parse(typeof(ParentSelectionType), dao.GetString("ParentSelectionType"));

                    return task;
                }
            }
            catch (Exception ex)
            {
                ErrorHandlingSystem.HandleError(ex, "Unable to fetch a GA Task");
                throw ex;
            }
            finally
            {
                dao.CloseConnection();
            }

            return null;
        }

        public void InsertTask(GATask task, double priority)
        {
            var sql = "INSERT INTO [dbo].[GA_Task] ([Session],[Priority],[Checkout],[ComputerName],[SolutionType],[ParentSelectionType],[MutationType],[CrossoverType],[LowestScoreIsBest],[MaxPopulationSize],[MaxGenerations],[CrossoverRate],[MutationRate],[ElitismRate],[PreventDuplications],[MaximumLifeSpan],[ChildrenPerCouple],[RandomSeed],[RandomPoolGenerationSeed]) ";
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

        public void InsertCompletedRun(GAConfiguration config, GARun run)
        {
            var dao = new DAO();
            var sql = "INSERT INTO [dbo].[GA_Result]([Session],[Start],[End],[ComputerName],[SolutionType],[ParentSelectionType],[MutationType],[CrossoverType],[LowestScoreIsBest],[MaxPopulationSize],[MaxGenerations],[CrossoverRate],[MutationRate],[ElitismRate],[PreventDuplications],[MaximumLifeSpan],[ChildrenPerCouple],[RandomSeed],[RandomPoolGenerationSeed],[BestScore],[BestScoreGeneration],[StringRepresentation]) ";
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
            var sql = "delete FROM [DB_9B8C9C_jarrus].[dbo].[GA_Task] where uuid = @UUID";

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

        private void AddGATaskParameters(DAO dao, GAProperties properties)
        {
            dao.AddParameter("@Session", properties.Session);
            dao.AddParameter("@ComputerName", properties.ComputerName);
            dao.AddParameter("@SolutionType", properties.Solution.GetType().AssemblyQualifiedName);
            dao.AddParameter("@ParentSelectionType", properties.ParentSelectionType.ToString());
            dao.AddParameter("@MutationType", properties.MutationType.ToString());
            dao.AddParameter("@CrossoverType", properties.CrossoverType.ToString());
            dao.AddParameter("@LowestScoreIsBest", properties.LowestScoreIsBest);
            dao.AddParameter("@MaxPopulationSize", properties.MaxPopulationSize);
            dao.AddParameter("@MaxGenerations", properties.MaxGenerations);
            dao.AddParameter("@CrossoverRate", properties.CrossoverRate);
            dao.AddParameter("@MutationRate", properties.MutationRate);
            dao.AddParameter("@ElitismRate", properties.ElitismRate);
            dao.AddParameter("@PreventDuplications", properties.PreventDuplications);
            dao.AddParameter("@MaximumLifeSpan", properties.MaximumLifeSpan);
            dao.AddParameter("@ChildrenPerCouple", properties.ChildrenPerCouple);
            dao.AddParameter("@RandomSeed", properties.RandomSeed);
            dao.AddParameter("@RandomPoolGenerationSeed", properties.RandomPoolGenerationSeed);
        }
    }
}
