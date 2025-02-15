param location string = resourceGroup().location
param cosmosDbAccountName string = 'locknotecosmosdb'
param functionStorageAccountName string = 'locknotefuncappstorage'

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

resource funcServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'LockNoteFuncAppPlan'
  location: location
  kind: 'functionapp'
  sku: {
    tier: 'Dynamic' // ✅ Consumption Plan (Pay-per-use)
    name: 'Y1'
  }
}

resource funcStorageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: functionStorageAccountName
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

resource functionApp 'Microsoft.Web/sites@2022-03-01' = {
  name: 'LockNoteFuncApp'
  location: location
  kind: 'functionapp'
  properties: {
    serverFarmId: funcServicePlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${funcStorageAccount.name};AccountKey=${funcStorageAccount.listKeys().keys[0].value};EndpointSuffix=core.windows.net'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
        {
          name: 'COSMOS_DB_CONNECTION_STRING'
          value: cosmosDbModule.outputs.cosmosDbConnectionString
        }
      ]
    }
  }
}

output functionAppUrl string = functionApp.properties.defaultHostName
output connectionString string = cosmosDbModule.outputs.cosmosDbConnectionString
