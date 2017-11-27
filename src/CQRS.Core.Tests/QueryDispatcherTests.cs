using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace CQRS.Core.Tests
{
    public class QueryDispatcherTests
    {
        [Fact]
        public void Dispatch_given_query_should_handle_with_proper_handler()
        {
            var query = new Mock<IQuery>();
            var handler = new Mock<IQueryHandler<IQuery, string>>();
            var dispatcher = new QueryDispatcher(handler.Object);

            dispatcher.Dispatch<IQuery, string>(query.Object);

            handler.Verify(h => h.Handle(query.Object), Times.Once);
        }

        [Fact]
        public void Dispatch_given_query_should_return_value_from_handler()
        {
            var query = new Mock<IQuery>();
            var handler = new Mock<IQueryHandler<IQuery, string>>();
            var someString = "some string";
            var dispatcher = new QueryDispatcher(handler.Object);
            handler.Setup(h => h.Handle(query.Object)).Returns(someString);

            var result = dispatcher.Dispatch<IQuery, string>(query.Object);

            Assert.Equal(someString, result);
        }

        [Fact]
        public void Dispatch_given_query_when_no_query_handler_should_throw_exception()
        {
            var query = new Mock<IQuery>();

            var dispatcher = new QueryDispatcher();

            Assert.Throws<InvalidOperationException>(() => dispatcher.Dispatch<IQuery, string>(query.Object));
        }

        [Fact]
        public void Dispatch_given_query_when_more_than_one_query_handler_should_throw_exception()
        {
            var query = new Mock<IQuery>();
            var handler1 = new Mock<IQueryHandler<IQuery, string>>();
            var handler2 = new Mock<IQueryHandler<IQuery, string>>();

            var dispatcher = new QueryDispatcher(handler1.Object, handler2.Object);

            Assert.Throws<InvalidOperationException>(() => dispatcher.Dispatch<IQuery, string>(query.Object));
        }
        
        [Fact]
        public void DispatchAsync_given_query_should_handle_with_proper_handler()
        {
            var query = new Mock<IQuery>();
            var handler = new Mock<IAsyncQueryHandler<IQuery, string>>();
            var dispatcher = new QueryDispatcher(handler.Object);

            dispatcher.DispatchAsync<IQuery, string>(query.Object);

            handler.Verify(h => h.HandleAsync(query.Object), Times.Once);
        }

        [Fact]
        public async Task DispatchAsync_given_query_should_return_value_from_handler()
        {
            var query = new Mock<IQuery>();
            var handler = new Mock<IAsyncQueryHandler<IQuery, string>>();
            var someStringTask = new TaskFactory().StartNew(() => "some string");
            var dispatcher = new QueryDispatcher(handler.Object);
            handler.Setup(h => h.HandleAsync(query.Object)).Returns(someStringTask);

            var result = await dispatcher.DispatchAsync<IQuery, string>(query.Object).ConfigureAwait(false);

            Assert.Equal(await someStringTask.ConfigureAwait(false), result);
        }

        [Fact]
        public async Task DispatchAsync_given_query_when_no_query_handler_should_throw_exception()
        {
            var query = new Mock<IQuery>();

            var dispatcher = new QueryDispatcher();

            await Assert.ThrowsAsync<InvalidOperationException>(() => dispatcher.DispatchAsync<IQuery, string>(query.Object)).ConfigureAwait(false);
        }

        [Fact]
        public async Task DispatchAsync_given_query_when_more_than_one_query_handler_should_throw_exception()
        {
            var query = new Mock<IQuery>();
            var handler1 = new Mock<IAsyncQueryHandler<IQuery, string>>();
            var handler2 = new Mock<IAsyncQueryHandler<IQuery, string>>();

            var dispatcher = new QueryDispatcher(handler1.Object, handler2.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => dispatcher.DispatchAsync<IQuery, string>(query.Object)).ConfigureAwait(false);
        }
    }
}