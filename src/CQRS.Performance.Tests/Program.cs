using System.Linq;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using CQRS.Core;
using CQRS.Core.Query;


namespace CQRS.Performance.Tests
{
    public class Program
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly DumbClasses.Query6 _queryToTest;
        private readonly DumbClasses.Query10 _asyncQueryToTest;

        public static void Main()
        {
            var config = new ManualConfig();
            config.Add(Job.Core);
            config.Add(DefaultConfig.Instance.GetColumnProviders().ToArray());
            config.Add(DefaultConfig.Instance.GetExporters().ToArray());
            config.Add(DefaultConfig.Instance.GetDiagnosers().ToArray());
            config.Add(DefaultConfig.Instance.GetAnalysers().ToArray());
            config.Add(DefaultConfig.Instance.GetJobs().ToArray());
            config.Add(DefaultConfig.Instance.GetValidators().ToArray());
            config.Add(new MemoryDiagnoser());
            config.UnionRule = ConfigUnionRule.AlwaysUseGlobal;
            
            var summary = BenchmarkRunner.Run<Program>();
            
            var logger = ConsoleLogger.Default;
            MarkdownExporter.Console.ExportToLog(summary, logger);
            ConclusionHelper.Print(logger, config.GetCompositeAnalyser().Analyse(summary).ToList());
        }

        [Benchmark(Description = "Dispatch sync")]
        public void DispatchTest()
        {
            _queryDispatcher.Dispatch<DumbClasses.Query6, byte>(_queryToTest);
        }

        [Benchmark(Description = "Dispatch async")]
        public void DispatchAsyncTest()
        {
            _queryDispatcher.DispatchAsync<DumbClasses.Query10, byte>(_asyncQueryToTest);
        }

        public Program()
        {
            _queryToTest = new DumbClasses.Query6();
            _asyncQueryToTest = new DumbClasses.Query10();
            _queryDispatcher = new QueryDispatcher(
                new DumbClasses.QueryHandler1(), 
                new DumbClasses.QueryHandler2(),
                new DumbClasses.QueryHandler3(),
                new DumbClasses.QueryHandler4(),
                new DumbClasses.QueryHandler5(),
                new DumbClasses.QueryHandler6(),
                new DumbClasses.QueryHandler7(),
                new DumbClasses.QueryHandler8(),
                new DumbClasses.AsyncQueryHandler1(),
                new DumbClasses.QueryHandler9()
                );
        }
    }
}