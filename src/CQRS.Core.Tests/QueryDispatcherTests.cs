using System;
using System.Threading.Tasks;
using CQRS.Core.Query;
using Moq;
using Xunit;

namespace CQRS.Core.Tests
{
    public class QueryDispatcherTests
    {
        private readonly QueryDispatcher _queryDispatcher;
        private readonly Mock<IQueryHandler<Query3, string>> _handlerMock;
        private readonly Mock<IAsyncQueryHandler<Query4, string>> _asyncHandlerMock;

        public QueryDispatcherTests()
        {
            _handlerMock = new Mock<IQueryHandler<Query3, string>>();
            _asyncHandlerMock = new Mock<IAsyncQueryHandler<Query4, string>>();
            _queryDispatcher = new QueryDispatcher(
                _handlerMock.Object, 
                new Handler1(), 
                new Handler2(), 
                _asyncHandlerMock.Object);
        }

        [Fact]
        public void Dispatch_given_query_should_handle_with_proper_handler()
        {
            var query3 = new Query3();
            
            _queryDispatcher.Dispatch<Query3, string>(query3);

            _handlerMock.Verify(h => h.Handle(query3), Times.Once);
        }

        [Fact]
        public void Dispatch_given_query_should_return_value_from_handler()
        {
            var result = _queryDispatcher.Dispatch<Query1, byte>(new Query1());

            Assert.Equal(1, result);
        }

        [Fact]
        public void Dispatch_given_query_when_no_query_handler_should_throw_exception()
        {
            Assert.Throws<InvalidOperationException>(() => _queryDispatcher.Dispatch<QueryNoHandler, string>(new QueryNoHandler()));
        }

        [Fact]
        public async Task DispatchAsync_given_query_should_handle_with_sync_dispatch_encapsuladed_by_task()
        {
            var query4 = new Query4();
            
            await _queryDispatcher.DispatchAsync<Query4, string>(query4);

            _asyncHandlerMock.Verify(h => h.Handle(query4), Times.Once);
        }

        public class Query1 : IQuery {}
        public class Query2 : IQuery {}
        public class Query3 : IQuery {}
        public class Query4 : IQuery {}
        public class QueryNoHandler : IQuery {}

        public class Handler1 : IQueryHandler<Query1, byte>
        {
            public byte Handle(Query1 query) => 1;
        }
        
        public class Handler2 : IQueryHandler<Query2, byte>
        {
            public byte Handle(Query2 query) => 2;
        }
    }
}