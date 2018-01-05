using System;
using System.Linq;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
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
            _queryDispatcher.Dispatch(_queryToTest);
        }

        [Benchmark(Description = "Dispatch async")]
        public void DispatchAsyncTest()
        {
            _queryDispatcher.Dispatch(_asyncQueryToTest);
        }

        public Program()
        {
            _queryToTest = new DumbClasses.Query6();
            _asyncQueryToTest = new DumbClasses.Query10();
            _queryDispatcher = new QueryDispatcher(query =>
            {
                switch (query)
                {
                    case DumbClasses.Query1 q:
                        return new DumbClasses.QueryHandler1();
                    case DumbClasses.Query2 q:
                        return new DumbClasses.QueryHandler2();
                    case DumbClasses.Query3 q:
                        return new DumbClasses.QueryHandler3();
                    case DumbClasses.Query4 q:
                        return new DumbClasses.QueryHandler4();
                    case DumbClasses.Query5 q:
                        return new DumbClasses.QueryHandler5();
                    case DumbClasses.Query6 q:
                        return new DumbClasses.QueryHandler6();
                    case DumbClasses.Query7 q:
                        return new DumbClasses.QueryHandler7();
                    case DumbClasses.Query8 q:
                        return new DumbClasses.QueryHandler8();
                    case DumbClasses.Query10 q:
                        return new DumbClasses.AsyncQueryHandler1();
                    case DumbClasses.Query9 q:
                        return new DumbClasses.QueryHandler9();
                    default:
                        throw new InvalidOperationException();
                }
            });
        }
    }
}