# Cosmos DB with CQRS pattern example

This repository is an example of Microsoft Cosmos DB (NoSQL database). Presented implementation uses CQRS pattern (no event sourcing).

## Introduction

Cosmos DB available on Azure Cloud is NoSQL database - a document-based database with key-value pairs that does not have a schema. In general, like most NoSQL offers:

1. Scalability: similar to SQL databases, NoSQL databases are easily scalable. However, they scale horizontally (not veritically liek SQL) meaning that ones must add more servers to existing NoSQL databases; but in any case, NoSQL usually scales better for larger and more powerful applications.
1. Community: NoSQL is relatively new compared to SQL databases meaning that sometimes there will be less documented support for utilizing the database. au contraire, the popularity of NoSQL is rapidly growing in the industry.
1. Flexibility: with a NoSQL database, there is more flexibility to store the data without a pre-defined structure, which is useful depending on the application.

As for Cosmos DB, advantages:

1. Performance.
1. High Avalibility.
1. Elasticity in scale.
1. Injects unstructured data at a very high speed.
1. No real need for administrative work, it indexes everything on it's own.

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

## ICosmosDbService

This interface provides basic CRUD functionality for given item model:

```csharp
public interface ICosmosDbService
{
  Task<HttpStatusCode> CreateDatabase(string ADatabaseName);
  Task<HttpStatusCode> CreateContainer(string ADatabaseName, string AContainerName, Guid AId);
  Task<HttpStatusCode> IsItemExists<T>(Guid Id) where T : class;
  Task<T> GetItem<T>(Guid AId) where T : class;
  Task<IEnumerable<T>> GetItems<T>(string AQueryString) where T : class;
  Task<HttpStatusCode> AddItem<T>(Guid AId, T AItem);
  Task<HttpStatusCode> UpdateItem<T>(Guid AId, T AItem);
  Task<HttpStatusCode> DeleteItem<T>(Guid AId);
}
```

All methods are asynchronous. The usage is very straightforward, after the call, check returned `HttpStatusCode`.

## How to run example

### Prerequisites

1. Install Azure Cosmos DB Emulator.
1. Run it, by default it will be accessible at [locahost:8081](https://localhost:8081/_explorer/index.html).
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

From Quickstart tab, copy:

1. URI to Account.
1. Primary Key to Key.

### Run

In Visual Studio, build and run. Navigate to Swagger and have fun!
