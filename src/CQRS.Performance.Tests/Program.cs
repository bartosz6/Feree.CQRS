using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using CQRS.Core;


namespace CQRS.Performance.Tests
{
    [CoreJob]
    [Config("columns=AllStatistics")]
    [MemoryDiagnoser]
    public class Program
    {
        private readonly QueryDispatcher _queryDispatcher;

        public static void Main()
        {
            BenchmarkRunner.Run<Program>();
        }

        [Benchmark(Description = "Dispatch sync, 1 out of 10")]
        public void DispatchTest()
        {
            _queryDispatcher.Dispatch<DumbClasses.Query6, byte>(new DumbClasses.Query6());
        }

        [Benchmark(Description = "Dispatch async, 1 out of 10")]
        public void DispatchAsyncTest()
        {
            _queryDispatcher.DispatchAsync<DumbClasses.Query10, byte>(new DumbClasses.Query10());
        }

        public Program()
        {
            _queryDispatcher = new QueryDispatcher(
                new DumbClasses.QueryHandler1(), 
                new DumbClasses.QueryHandler2(),
                new DumbClasses.QueryHandler3(),
                new DumbClasses.QueryHandler4(),
                new DumbClasses.QueryHandler5(),
                new DumbClasses.QueryHandler6(),
                new DumbClasses.QueryHandler7(),
                new DumbClasses.QueryHandler8(),
                new DumbClasses.QueryHandler9(),
                new DumbClasses.AsyncQueryHandler1(),
                new DumbClasses.QueryHandler10()
                );
        }
    }
}