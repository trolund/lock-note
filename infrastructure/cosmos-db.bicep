param cosmosDbAccountName string
param location string
param databaseName string
param containerName string

// Create a Cosmos DB account
resource cosmosDbAccount 'Microsoft.DocumentDB/databaseAccounts@2024-08-15' = {
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
    consistencyPolicy: {
      defaultConsistencyLevel: 'Session'
    }
  }
}

// Create a database
// resource database 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2024-08-15' = {
//   name: databaseName
//   parent: cosmosDbAccount
//   location: location
// }

// Output the Cosmos DB connection string
output cosmosDbConnectionString string = cosmosDbAccount.listConnectionStrings().connectionStrings[0].connectionString
