param cosmosDbAccountName string
param resourceGroupName string
param location string
param databaseName string
param containerName string

// Create Cosmos DB Account
resource cosmosDbAccount 'Microsoft.DocumentDB/databaseAccounts@2022-03-01' = {
  name: cosmosDbAccountName
  location: location
  properties: {
    databaseAccountOfferType: 'Standard'
    kind: 'GlobalDocumentDB' // SQL API is the default choice for document-based DBs
  }
}

// Create Cosmos DB Database
resource cosmosDbDatabase 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2022-03-01' = {
  parent: cosmosDbAccount
  name: databaseName
  properties: {
    resource: {
      id: databaseName
    }
  }
}

// Create Cosmos DB Container (Collection)
resource cosmosDbContainer 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases/containers@2022-03-01' = {
  parent: cosmosDbDatabase
  name: containerName
  properties: {
    resource: {
      id: containerName
      partitionKey: {
        paths: ['/id']
        kind: 'Hash'
      }
    }
  }
}

// Output the Cosmos DB connection string
output cosmosDbConnectionString string = cosmosDbAccount.properties.connectionStrings[0]
