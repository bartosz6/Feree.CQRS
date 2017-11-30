using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;
using CQRS.Core;


namespace CQRS.Performance.Tests
{
    [CoreJob]
    public class Program
    {
        private QueryDispatcher _queryDispatcher;

        public static void Main()
        {
            BenchmarkRunner.Run<Program>();
        }

        [Benchmark]
        public void DispatchTest()
        {
            _queryDispatcher.Dispatch<Query6, string>(new Query6());
        }

        [IterationSetup]
        public void Setup()
        {
            _queryDispatcher = new QueryDispatcher(
                new QueryHandler1(), 
                new QueryHandler2(),
                new QueryHandler3(),
                new QueryHandler4(),
                new QueryHandler5(),
                new QueryHandler6(),
                new QueryHandler7(),
                new QueryHandler8(),
                new QueryHandler9(),
                new QueryHandler10(),
                new QueryHandler11(),
                new QueryHandler12(),
                new QueryHandler13(),
                new QueryHandler14(),
                new QueryHandler15()
                );
        }

        private const string X = "x";
        
        public abstract class QueryHandler<T> : IQueryHandler<T, string> where T : IQuery
        {
            public string Handle(T query) => X; //$"{typeof(T).Name} {DateTime.UtcNow.Ticks}";
        }

        public class Query1 : IQuery {}
        public class Query2 : IQuery {}
        public class Query3 : IQuery {}
        public class Query4 : IQuery {}
        public class Query5 : IQuery {}
        public class Query6 : IQuery {}
        public class Query7 : IQuery {}
        public class Query8 : IQuery {}
        public class Query9 : IQuery {}
        public class Query10 : IQuery {}
        public class Query11 : IQuery {}
        public class Query12 : IQuery {}
        public class Query13 : IQuery {}
        public class Query14 : IQuery {}
        public class Query15 : IQuery {}

        public class QueryHandler1 : QueryHandler<Query1> {}
        public class QueryHandler2 : QueryHandler<Query2> {}
        public class QueryHandler3 : QueryHandler<Query3> {}
        public class QueryHandler4 : QueryHandler<Query4> {}
        public class QueryHandler5 : QueryHandler<Query5> {}
        public class QueryHandler6 : QueryHandler<Query6> {}
        public class QueryHandler7 : QueryHandler<Query7> {}
        public class QueryHandler8 : QueryHandler<Query8> {}
        public class QueryHandler9 : QueryHandler<Query9> {}
        public class QueryHandler10 : QueryHandler<Query10> {}
        public class QueryHandler11 : QueryHandler<Query11> {}
        public class QueryHandler12 : QueryHandler<Query12> {}
        public class QueryHandler13 : QueryHandler<Query13> {}
        public class QueryHandler14 : QueryHandler<Query14> {}
        public class QueryHandler15 : QueryHandler<Query15> {}

    }
}