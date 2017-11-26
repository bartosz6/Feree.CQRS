using Moq;
using Xunit;

namespace CQRS.Core.Tests
{
    public class BaseQueryDispatcherTests
    {
        [Fact]
        public void Handle_given_query_should_handle_with_proper_handler()
        {
            var query = new Mock<IQuery>();
            var handler = new Mock<IQueryHandler<IQuery, string>>();
            var dispatcher = new BaseQueryDispatcher(handler.Object);

            dispatcher.Dispatch<IQuery, string>(query.Object);
            
            handler.Verify(h => h.Handle(query.Object), Times.Once);
        }
        
        [Fact]
        public void Handle_given_query_should_return_value_from_handler()
        {
            var query = new Mock<IQuery>();
            var handler = new Mock<IQueryHandler<IQuery, string>>();
            var someString = "some string";
            handler.Setup(h => h.Handle(query.Object)).Returns(someString);
            var dispatcher = new BaseQueryDispatcher(handler.Object);

            var result = dispatcher.Dispatch<IQuery, string>(query.Object);
            
            Assert.Equal(someString, result);
        }
    }
}