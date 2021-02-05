using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CosmosDbExample.Infrastructure.Database
{
    public abstract class CosmosDbObject
    {
        public abstract Task<HttpStatusCode> CreateDatabase(string ADatabaseName, CancellationToken ACancellationToken = default);
        public abstract Task<HttpStatusCode> CreateContainer(string ADatabaseName, string AContainerName, Guid AId, CancellationToken ACancellationToken = default);
        public abstract Task<HttpStatusCode> IsItemExists<T>(Guid Id, CancellationToken ACancellationToken = default) where T : class;
        public abstract Task<T> GetItem<T>(Guid AId, CancellationToken ACancellationToken = default) where T : class;
        public abstract Task<IEnumerable<T>> GetItems<T>(string AQueryString, CancellationToken ACancellationToken = default) where T : class;
        public abstract Task<HttpStatusCode> AddItem<T>(Guid AId, T AItem, CancellationToken ACancellationToken = default);
        public abstract Task<HttpStatusCode> UpdateItem<T>(Guid AId, T AItem, CancellationToken ACancellationToken = default);
        public abstract Task<HttpStatusCode> DeleteItem<T>(Guid AId, CancellationToken ACancellationToken = default);
    }
}
