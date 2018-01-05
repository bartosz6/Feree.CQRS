using System.Threading.Tasks;
using CQRS.Core.Query;

namespace CQRS.Performance.Tests
{
    internal class DumbClasses
    {
        internal class Query1 : IQuery<int>
        {
        }

        internal class Query2 : IQuery<int>
        {
        }

        internal class Query3 : IQuery<int>
        {
        }

        internal class Query4 : IQuery<int>
        {
        }

        internal class Query5 : IQuery<int>
        {
        }

        internal class Query6 : IQuery<int>
        {
        }

        internal class Query7 : IQuery<int>
        {
        }

        internal class Query8 : IQuery<int>
        {
        }

        internal class Query9 : IQuery<int>
        {
        }

        internal class Query10 : IQuery<int>
        {
        }

        internal class QueryHandler1 : QueryHandler<Query1, int>
        {
            protected override int Handle(Query1 query) => 0;
        }

        internal class QueryHandler2 : QueryHandler<Query2, int>
        {
            protected override int Handle(Query2 query) => 0;
        }

        internal class QueryHandler3 : QueryHandler<Query3, int>
        {
            protected override int Handle(Query3 query) => 0;
        }

        internal class QueryHandler4 : QueryHandler<Query4, int>
        {
            protected override int Handle(Query4 query) => 0;
        }

        internal class QueryHandler5 : QueryHandler<Query5, int>
        {
            protected override int Handle(Query5 query) => 0;
        }

        internal class QueryHandler6 : QueryHandler<Query6, int>
        {
            protected override int Handle(Query6 query) => 0;
        }

        internal class QueryHandler7 : QueryHandler<Query7, int>
        {
            protected override int Handle(Query7 query) => 0;
        }

        internal class QueryHandler8 : QueryHandler<Query8, int>
        {
            protected override int Handle(Query8 query) => 0;
        }

        internal class QueryHandler9 : QueryHandler<Query9, int>
        {
            protected override int Handle(Query9 query) => 0;
        }

        private static readonly TaskFactory TaskFactory = new TaskFactory();
        
        internal class AsyncQueryHandler1 : AsyncQueryHandler<Query10, int>
        {
            protected override Task<int> Handle(Query10 query) => TaskFactory.StartNew(() => 0);
        }
    }
}