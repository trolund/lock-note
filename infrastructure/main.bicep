param location string = resourceGroup().location
param resourceGroupName string = 'LockNoteGroup'
param cosmosDbAccountName string = 'locknotecosmosdb'
param databaseName string = 'LockNote'
param containerName string = 'Notes'

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'LockNoteAppServicePlan'
  location: location
  sku: {
    name: 'B1'
    tier: 'Basic'
  }
}

resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: 'LockNoteApp'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
  }
}

// Reference the Cosmos DB module
module cosmosDbModule './cosmos-db.bicep' = {
  name: 'LockNoteCosmosDb'
  params: {
    cosmosDbAccountName: cosmosDbAccountName
    location: location
    databaseName: databaseName
    containerName: containerName
  }
}

// You can access the Cosmos DB connection string from the module's output
output cosmosDbConnectionString string = cosmosDbModule.outputs.cosmosDbConnectionString

// resource sqlServer 'Microsoft.Sql/servers@2022-05-01-preview' = {
//   name: 'LockNoteAppSqlServer'
//   location: location
//   properties: {
//     administratorLogin: 'adminuser'
//     administratorLoginPassword: 'P@ssw0rd123!'
//   }
// }

// resource database 'Microsoft.Sql/servers/databases@2022-05-01-preview' = {
//   name: 'LockNoteAppDb'
//   parent: sqlServer
//   location: location
//   properties: {
//     collation: 'SQL_Latin1_General_CP1_CI_AS'
//   }
// }

// resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' = {
//   name: 'LockNoteAppKeyVault'
//   location: location
//   properties: {
//     sku: {
//       family: 'A'
//       name: 'standard'
//     }
//     tenantId: subscription().tenantId
//   }
// }
