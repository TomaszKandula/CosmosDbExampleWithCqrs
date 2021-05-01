# Cosmos DB with CQRS pattern example

This repository is an example of Microsoft Cosmos DB (NoSQL database). The presented implementation uses the CQRS pattern (no event sourcing).

## Introduction

Cosmos DB available on Azure Cloud is a NoSQL database - a document-based database with key-value pairs that do not have a schema. In general, like most NoSQL offers:

1. Scalability: Like SQL databases, NoSQL databases are easily scalable. However, they scale horizontally (not vertically like SQL), meaning that one must add more servers to existing NoSQL databases. Still, NoSQL usually scales better for more extensive and more powerful applications in any case.
1. Community: NoSQL is relatively new compared to SQL databases meaning that sometimes there will be less documented support for utilizing the database. Au contraire, the popularity of NoSQL is rapidly growing in the industry.
1. Flexibility: with a NoSQL database, there is more flexibility to store the data without a pre-defined structure, which is applicable depending on the application.

As for Cosmos DB, advantages:

1. Performance.
1. High Availability.
1. Elasticity in scale.
1. Injects unstructured data at a very high speed.
1. No real need for administrative work; it indexes everything on its own.

Disadvantages:

1. ANSI SQL support.
1. Can be very expensive.
1. No enough insights into how many resources (DTUs) an individual query uses.

More here: [Introduction to Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/introduction).

## Tech-Stack

1. .NET Core 3.1.
1. C# language.
1. Web API.
1. SwaggerUI.
1. MediatR.
1. Azure Cosmos DB.

No tests are provided this time.

## ICosmosDbService

This interface provides basic CRUD functionality for given item model:

```csharp
public interface ICosmosDbService
{
    Task<HttpStatusCode> CreateDatabase(string ADatabaseName, CancellationToken ACancellationToken = default);
    Task<HttpStatusCode> CreateContainer(string ADatabaseName, string AContainerName, Guid AId, CancellationToken ACancellationToken = default);
    Task<HttpStatusCode> IsItemExists<T>(Guid AId, CancellationToken ACancellationToken = default) where T : class;
    Task<T> GetItem<T>(Guid AId, CancellationToken ACancellationToken = default) where T : class;
    Task<IEnumerable<T>> GetItems<T>(string AQueryString, CancellationToken ACancellationToken = default) where T : class;
    Task<HttpStatusCode> AddItem<T>(Guid AId, T AItem, CancellationToken ACancellationToken = default);
    Task<HttpStatusCode> UpdateItem<T>(Guid AId, T AItem, CancellationToken ACancellationToken = default);
    Task<HttpStatusCode> DeleteItem<T>(Guid AId, CancellationToken ACancellationToken = default);
}
```

All methods are asynchronous. The usage is very straightforward:

```csharp
public async Task<Unit> Handle(AddNewArticleCommand ARequest, CancellationToken ACancellationToken)
{
    var LNewGuid = Guid.NewGuid();
    
    await FCosmosDbService.AddItem(LNewGuid, new Articles
    {
        Id = ARequest.Id,
        Title = ARequest.Title,
        Desc = ARequest.Desc,
        Status = ARequest.Status,
        Likes = ARequest.Likes,
        ReadCount = ARequest.ReadCount
    }, ACancellationToken);
    
    return await Task.FromResult(Unit.Value);
}
```

After the call, one should check returned `HttpStatusCode` (not provided in the examples).

## How to run example

### Prerequisites

1. Install Azure Cosmos DB Emulator.
1. Run it; by default, it will be accessible at [locahost:8081](https://localhost:8081/_explorer/index.html).
1. Configure `secrets.json`: 

```json
{
  "CosmosDb": 
  {
    "DatabaseName": "<database_name>",
    "Account": "<uri>",
    "Key": "<primary_key>"
  }
}
```

From the Quickstart tab, copy:

1. URI to Account.
1. Primary Key to Key.

## Run

In Visual Studio or JetBrains Rider, build and run. Navigate to Swagger, and have fun!
