using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CosmosDbExample.Infrastructure.Database
{
    public interface ICosmosDbService
    {
        Task<HttpStatusCode> CreateDatabase(string ADatabaseName, CancellationToken ACancellationToken = default);
        Task<HttpStatusCode> CreateContainer(string ADatabaseName, string AContainerName, Guid AId, CancellationToken ACancellationToken = default);
        Task<HttpStatusCode> IsItemExists<T>(Guid Id, CancellationToken ACancellationToken = default) where T : class;
        Task<T> GetItem<T>(Guid AId, CancellationToken ACancellationToken = default) where T : class;
        Task<IEnumerable<T>> GetItems<T>(string AQueryString, CancellationToken ACancellationToken = default) where T : class;
        Task<HttpStatusCode> AddItem<T>(Guid AId, T AItem, CancellationToken ACancellationToken = default);
        Task<HttpStatusCode> UpdateItem<T>(Guid AId, T AItem, CancellationToken ACancellationToken = default);
        Task<HttpStatusCode> DeleteItem<T>(Guid AId, CancellationToken ACancellationToken = default);
    }
}
