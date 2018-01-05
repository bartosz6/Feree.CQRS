using System;
using System.Threading.Tasks;
using CQRS.Core.Query;
using Moq;
using Xunit;

namespace CQRS.Core.Tests
{
    public class QueryDispatcherTests
    {
        private readonly IQueryDispatcher _dispatcher;

        private class TestQueryHandler : QueryHandler<TestQuery, string>
        {
            protected override string Handle(TestQuery query) => query.Number.ToString();
        }

        private class AsyncTestQueryHandler : AsyncQueryHandler<AsyncTestQuery, string>
        {
            protected override Task<string> Handle(AsyncTestQuery query) =>  new TaskFactory().StartNew(() => query.Number.ToString());
        }

        private struct TestQuery : IQuery<string>
        {
            public TestQuery(int number)
            {
                Number = number;
            }

            public int Number { get; }
        }
        
        private struct AsyncTestQuery : IQuery<string>
        {
            public AsyncTestQuery(int number)
            {
                Number = number;
            }

            public int Number { get; }
        }

        public QueryDispatcherTests()
        {
            this._dispatcher = new QueryDispatcher(query =>
            {
                switch (query)
                {
                    case TestQuery testQuery:
                        return new TestQueryHandler();
                    case AsyncTestQuery testQuery:
                        return new AsyncTestQueryHandler();
                    default:
                        throw new ArgumentException();
                }
            });
        }

        [Fact]
        public async Task Dispatch_GivenQuery_ReturnsResultOfHandler()
        {
            var query = new TestQuery(5);

            var result = await _dispatcher.Dispatch(query);
            
            Assert.Equal(result, "5");
        }
        
        [Fact]
        public async Task Dispatch_GivenAsyncQuery_ReturnsResultOfHandler()
        {
            var query = new AsyncTestQuery(5);

            var result = await _dispatcher.Dispatch(query);
            
            Assert.Equal(result, "5");
        }
    }
}