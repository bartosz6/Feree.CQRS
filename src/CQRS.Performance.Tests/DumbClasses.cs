using CQRS.Core;

namespace CQRS.Performance.Tests
{
    internal abstract class QueryHandler<T> : IQueryHandler<T, byte> where T : IQuery
    {
        public byte Handle(T query) => 0;
    }
    
    internal class DumbClasses
    {
        internal class Query1 : IQuery {}

        internal class Query2 : IQuery {}

        internal class Query3 : IQuery {}

        internal class Query4 : IQuery {}

        internal class Query5 : IQuery {}

        internal class Query6 : IQuery {}

        internal class Query7 : IQuery {}

        internal class Query8 : IQuery {}

        internal class Query9 : IQuery {}

        internal class Query10 : IQuery {}

        internal class QueryHandler1 : QueryHandler<Query1> {}

        internal class QueryHandler2 : QueryHandler<Query2> {}

        internal class QueryHandler3 : QueryHandler<Query3> {}

        internal class QueryHandler4 : QueryHandler<Query4> {}

        internal class QueryHandler5 : QueryHandler<Query5> {}

        internal class QueryHandler6 : QueryHandler<Query6> {}

        internal class QueryHandler7 : QueryHandler<Query7> {}

        internal class QueryHandler8 : QueryHandler<Query8> {}

        internal class QueryHandler9 : QueryHandler<Query9> {}

        internal class QueryHandler10 : QueryHandler<Query10> {}
    }
}