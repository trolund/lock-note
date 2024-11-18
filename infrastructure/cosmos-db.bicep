param cosmosDbAccountName string
param location string
param databaseName string
param containerName string

// Create Cosmos DB Account
resource cosmosDbAccount 'Microsoft.DocumentDB/databaseAccounts@2023-09-15' = {
  name: cosmosDbAccountName
  location: location
  kind: 'GlobalDocumentDB'
  properties: {
    databaseAccountOfferType: 'Standard'
    locations: [
      {
        locationName: location
      }
    ]
  }
}

// // Create Cosmos DB Database
// resource cosmosDbDatabase 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2022-03-01' = {
//   parent: cosmosDbAccount
//   name: databaseName
//   properties: {
//     resource: {
//       id: databaseName
//     }
//   }
// }

// // Create Cosmos DB Container (Collection)
// resource cosmosDbContainer 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases/containers@2022-03-01' = {
//   parent: cosmosDbDatabase
//   name: containerName
//   properties: {
//     resource: {
//       id: containerName
//       partitionKey: {
//         paths: ['/id']
//         kind: 'Hash'
//       }
//     }
//   }
// }

// Output the Cosmos DB connection string
output cosmosDbConnectionString string = cosmosDbAccount.properties.connectionStrings[0]
