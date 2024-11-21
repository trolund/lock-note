param location string = resourceGroup().location
param cosmosDbAccountName string = 'locknotecosmosdb'

// Reference the Cosmos DB module
module cosmosDbModule './cosmos-db.bicep' = {
  name: 'LockNoteCosmosDb'
  params: {
    cosmosDbAccountName: cosmosDbAccountName
    location: location
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'LockNoteAppServicePlan'
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
}

resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: 'LockNoteApp'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'COSMOS_DB_CONNECTION_STRING'
          value: cosmosDbModule.outputs.cosmosDbConnectionString
        }
      ]
    }
  }
}

output connectionString string = cosmosDbModule.outputs.cosmosDbConnectionString
