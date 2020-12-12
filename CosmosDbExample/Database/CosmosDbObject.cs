using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace CosmosDbExample.Database
{

    public abstract class CosmosDbObject
    {
        public abstract void InitContainer<T>();
        public abstract Task<HttpStatusCode> CreateDatabase(string ADatabaseName);
        public abstract Task<HttpStatusCode> CreateContainer(string ADatabaseName, string AContainerName, Guid AId);
        public abstract Task<HttpStatusCode> IsItemExists<T>(Guid Id) where T : class;
        public abstract Task<T> GetItem<T>(Guid AId) where T : class;
        public abstract Task<IEnumerable<T>> GetItems<T>(string AQueryString) where T : class;
        public abstract Task<HttpStatusCode> AddItem<T>(Guid AId, T AItem);
        public abstract Task<HttpStatusCode> UpdateItem<T>(Guid AId, T AItem);
        public abstract Task<HttpStatusCode> DeleteItem<T>(Guid AId);
    }

}
