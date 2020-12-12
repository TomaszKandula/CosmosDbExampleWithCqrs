﻿using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using CosmosDbExample.Settings;

namespace CosmosDbExample.Database
{

    public class CosmosDbService : CosmosDbObject, ICosmosDbService
    {

        private Container FContainer { get; set; }
        private CosmosClient FCosmosClient { get; set; }
        private string FDatabaseName { get; set; }

        public CosmosDbService(CosmosDbSettings AConfiguration)
        {

            var LAccount  = AConfiguration.Account;
            var LKey      = AConfiguration.Key;

            FDatabaseName  = AConfiguration.DatabaseName;

            FCosmosClient = new CosmosClient(LAccount, LKey, new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });

            FContainer = null;

        }

        public override void InitContainer<T>() 
        {
            // Names of CosmosDb container and item model used in the container must be the same
            var LModelName = typeof(T).Name;
            FContainer = FCosmosClient.GetContainer(FDatabaseName, LModelName);
        }

        public override async Task<HttpStatusCode> CreateDatabase(string ADatabaseName) 
        {

            try
            {
                var LDbResponse = await FCosmosClient.CreateDatabaseIfNotExistsAsync(ADatabaseName);
                return LDbResponse.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.Created)
            {
                return LException.StatusCode;
            }

        }

        public override async Task<HttpStatusCode> CreateContainer(string ADatabaseName, string AContainerName, Guid AId) 
        {

            try
            {
                var LDatabase = FCosmosClient.GetDatabase(ADatabaseName);
                var LContainerResponse = await LDatabase.CreateContainerIfNotExistsAsync(AId.ToString(), AContainerName);
                return LContainerResponse.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.Created)
            {
                return LException.StatusCode;
            }

        }

        public override async Task<HttpStatusCode> IsItemExists<T>(Guid Id) where T : class
        {

            var LModelName = typeof(T).Name;
            var LModelSymbol = LModelName[0..1].ToLower();
            var LQuery = $"select * from {LModelName} {LModelSymbol} where {LModelSymbol}.id = \"{Id}\"";
            var LItems = await GetItems<T>(LQuery);
            if (!LItems.Any())
            {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.OK;

        }

        public override async Task<T> GetItem<T>(Guid AId) where T : class
        {

            try
            {
                var LResponse = await FContainer.ReadItemAsync<T>(AId.ToString(), new PartitionKey(AId.ToString()));
                return LResponse.Resource;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

        }

        public override async Task<IEnumerable<T>> GetItems<T>(string AQueryString) where T : class
        {

            try 
            {

                var LQuery = FContainer.GetItemQueryIterator<T>(new QueryDefinition(AQueryString));
                var LResults = new List<T>();

                while (LQuery.HasMoreResults)
                {
                    var LResponse = await LQuery.ReadNextAsync();
                    LResults.AddRange(LResponse);
                }

                return LResults;

            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }          
            
        }

        public override async Task<HttpStatusCode> AddItem<T>(Guid AId, T AItem)
        {

            try 
            {
                var Response = await FContainer.CreateItemAsync<T>(AItem, new PartitionKey(AId.ToString()));
                return Response.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.Created)
            {
                return LException.StatusCode;
            }

        }

        public override async Task<HttpStatusCode> UpdateItem<T>(Guid AId, T AItem)
        {

            try
            {
                var Response = await FContainer.UpsertItemAsync<T>(AItem, new PartitionKey(AId.ToString()));
                return Response.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return LException.StatusCode;
            }

        }

        public override async Task<HttpStatusCode> DeleteItem<T>(Guid AId)
        {

            try
            {
                var Response = await FContainer.DeleteItemAsync<T>(AId.ToString(), new PartitionKey(AId.ToString()));
                return Response.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.NoContent)
            {
                return LException.StatusCode;
            }

        }

    }

}
