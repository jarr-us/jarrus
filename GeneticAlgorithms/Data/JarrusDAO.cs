using GeneralHux.CRUD;
using GeneralHux.ErrorHandling;
using GeneticAlgorithms.BasicTypes;
using System;

namespace GeneticAlgorithms.Data
{
    public class JarrusDAO
    {
        public GARun<T> FetchTaskToRun<T>() where T : Gene
        {
            //var workToDo = new GARun<T>();



            //return workToDo;
            return null;
        }

        public void AssignMyselfWork()
        {
            var sql = "UPDATE [DB_9B8C9C_jarrus].[dbo].[GA_Tasks]";
        }


        public void InsertTask<T>(GARun<T> task, double priority) where T : Gene
        {
            var sql = "INSERT INTO [dbo].[GA_Tasks] ([UUID],[Session],[Priority],[Start],[ComputerName],[SolutionType],[ParentSelectionType],[MutationType],[CrossoverType],[LowestScoreIsBest],[PoolSize],[MaxGenerations],[CrossoverRate],[MutationRate],[ElitismRate],[PreventDuplications],[MaximumLifeSpan],[ChildrenPerCouple],[RandomSeed],[RandomPoolGenerationSeed]) ";
            sql += "VALUES(@UUID, @Session, @Priority, @Start, @ComputerName, @SolutionType, @ParentSelectionType, @MutationType, @CrossoverType, @LowestScoreIsBest, @PoolSize, @MaxGenerations, @CrossoverRate, @MutationRate, @ElitismRate, @PreventDuplications, @MaximumLifeSpan, @ChildrenPerCouple, @RandomSeed, @RandomPoolGenerationSeed)";


        }

        public void InsertCompletedRun<T>(GARun<T> run) where T : Gene
        {
            var dao = new DAO();
            var sql = "INSERT INTO [dbo].[GA_Results]([Session],[Start],[End],[ComputerName],[SolutionType],[ParentSelectionType],[MutationType],[CrossoverType],[LowestScoreIsBest],[PoolSize],[MaxGenerations],[CrossoverRate],[MutationRate],[ElitismRate],[PreventDuplications],[MaximumLifeSpan],[ChildrenPerCouple],[RandomSeed],[RandomPoolGenerationSeed],[BestScore],[BestScoreGeneration],[StringRepresentation]) ";
            sql += "VALUES (@Session,@Start,@End,@ComputerName,@SolutionType,@ParentSelectionType,@MutationType,@CrossoverType,@LowestScoreIsBest,@PoolSize,@MaxGenerations,@CrossoverRate,@MutationRate,@ElitismRate,@PreventDuplications,@MaximumLifeSpan,@ChildrenPerCouple,@RandomSeed,@RandomPoolGenerationSeed,@BestScore,@BestScoreGeneration,@StringRepresentation)";

            try
            {
                dao.OpenConnection(Server.JARRUS, sql);

                dao.AddParameter("@Session", run.Session);
                dao.AddParameter("@Start", run.Start);
                dao.AddParameter("@End", run.End);
                dao.AddParameter("@ComputerName", run.ComputerName);
                dao.AddParameter("@SolutionType", run.FitnessFunction);
                dao.AddParameter("@ParentSelectionType", run.ParentSelection);
                dao.AddParameter("@MutationType", run.Mutation);
                dao.AddParameter("@CrossoverType", run.Crossover);
                dao.AddParameter("@LowestScoreIsBest", run.LowestScoreIsBest);
                dao.AddParameter("@PoolSize", run.PoolSize);
                dao.AddParameter("@MaxGenerations", run.MaxGenerations);
                dao.AddParameter("@CrossoverRate", run.CrossoverRate);
                dao.AddParameter("@MutationRate", run.MutationRate);
                dao.AddParameter("@ElitismRate", run.ElitismRate);
                dao.AddParameter("@PreventDuplications", run.PreventDuplications);
                dao.AddParameter("@MaximumLifeSpan", run.MaximumLifeSpan);
                dao.AddParameter("@ChildrenPerCouple", run.ChildrenPerCouple);
                dao.AddParameter("@RandomSeed", run.RandomSeed);
                dao.AddParameter("@RandomPoolGenerationSeed", run.RandomPoolGenerationSeed);
                dao.AddParameter("@BestScore", run.BestChromosome.FitnessScore);
                dao.AddParameter("@BestScoreGeneration", run.BestChromosome.GenerationNumber);
                dao.AddParameter("@StringRepresentation", run.BestChromosome.ToString());

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
    }
}
