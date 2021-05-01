using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using CosmosDbExample.Settings;

namespace CosmosDbExample.Infrastructure.Database
{
    public class CosmosDbService : CosmosDbObject, ICosmosDbService
    {
        private Container Container { get; set; }
        
        private CosmosClient CosmosClient { get; set; }
        
        private string DatabaseName { get; set; }
        
        private int DefaultThroughput { get; set; }

        public CosmosDbService(CosmosDbSettings AConfiguration)
        {
            var LAccount = AConfiguration.Account;
            var LKey = AConfiguration.Key;

            DatabaseName = AConfiguration.DatabaseName;

            CosmosClient = new CosmosClient(LAccount, LKey, new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });

            Container = null;
            DefaultThroughput = 400;
        }

        private void InitContainer<T>() 
        {
            // Names of CosmosDb container and item model used in the container must be the same
            var LModelName = typeof(T).Name;
            Container = CosmosClient.GetContainer(DatabaseName, LModelName);
        }

        public override async Task<HttpStatusCode> CreateDatabase(string ADatabaseName, CancellationToken ACancellationToken = default) 
        {
            try
            {
                var LDbResponse = await CosmosClient.CreateDatabaseIfNotExistsAsync(ADatabaseName, DefaultThroughput, null, ACancellationToken);
                return LDbResponse.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.Created)
            {
                return LException.StatusCode;
            }
        }

        public override async Task<HttpStatusCode> CreateContainer(string ADatabaseName, string AContainerName, Guid AId, CancellationToken ACancellationToken = default) 
        {
            try
            {
                var LDatabase = CosmosClient.GetDatabase(ADatabaseName);
                var LContainerResponse = await LDatabase.CreateContainerIfNotExistsAsync(AId.ToString(), AContainerName, DefaultThroughput, null, ACancellationToken);
                return LContainerResponse.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.Created)
            {
                return LException.StatusCode;
            }
        }

        public override async Task<HttpStatusCode> IsItemExists<T>(Guid AId, CancellationToken ACancellationToken = default) where T : class
        {
            var LModelName = typeof(T).Name;
            var LModelSymbol = LModelName[..1].ToLower();
            var LQuery = $"select * from {LModelName} {LModelSymbol} where {LModelSymbol}.id = \"{AId}\"";
            var LItems = await GetItems<T>(LQuery, ACancellationToken);

            if (!LItems.Any())
            {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.OK;
        }

        public override async Task<T> GetItem<T>(Guid AId, CancellationToken ACancellationToken = default) where T : class
        {
            try
            {
                if (Container == null) InitContainer<T>();
                var LResponse = await Container.ReadItemAsync<T>(AId.ToString(), new PartitionKey(AId.ToString()), null, ACancellationToken);
                return LResponse.Resource;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
        }

        public override async Task<IEnumerable<T>> GetItems<T>(string AQueryString, CancellationToken ACancellationToken = default) where T : class
        {
            try 
            {
                if (Container == null) InitContainer<T>();
                var LQuery = Container.GetItemQueryIterator<T>(new QueryDefinition(AQueryString));
                var LResults = new List<T>();

                while (LQuery.HasMoreResults)
                {
                    var LResponse = await LQuery.ReadNextAsync(ACancellationToken);
                    LResults.AddRange(LResponse);
                }

                return LResults;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }         
        }

        public override async Task<HttpStatusCode> AddItem<T>(Guid AId, T AItem, CancellationToken ACancellationToken = default)
        {
            try 
            {
                if (Container == null) InitContainer<T>();
                var LResponse = await Container.CreateItemAsync(AItem, new PartitionKey(AId.ToString()), null, ACancellationToken);
                return LResponse.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.Created)
            {
                return LException.StatusCode;
            }
        }

        public override async Task<HttpStatusCode> UpdateItem<T>(Guid AId, T AItem, CancellationToken ACancellationToken = default)
        {
            try
            {
                if (Container == null) InitContainer<T>();
                var LResponse = await Container.UpsertItemAsync(AItem, new PartitionKey(AId.ToString()), null, ACancellationToken);
                return LResponse.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return LException.StatusCode;
            }
        }

        public override async Task<HttpStatusCode> DeleteItem<T>(Guid AId, CancellationToken ACancellationToken = default)
        {
            try
            {
                if (Container == null) InitContainer<T>();
                var LResponse = await Container.DeleteItemAsync<T>(AId.ToString(), new PartitionKey(AId.ToString()), null, ACancellationToken);
                return LResponse.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.NoContent)
            {
                return LException.StatusCode;
            }
        }
    }
}
