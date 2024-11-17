resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'LockNoteAppServicePlan'
  location: resourceGroup().location
  sku: {
    name: 'B1'
    tier: 'Basic'
  }
}

resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: 'NoteApp'
  location: resourceGroup().location
  properties: {
    serverFarmId: appServicePlan.id
  }
}

resource sqlServer 'Microsoft.Sql/servers@2022-05-01-preview' = {
  name: 'LockNoteAppSqlServer'
  location: resourceGroup().location
  properties: {
    administratorLogin: 'adminuser'
    administratorLoginPassword: 'P@ssw0rd123!'
  }
}

resource database 'Microsoft.Sql/servers/databases@2022-05-01-preview' = {
  name: 'LockNoteAppDb'
  parent: sqlServer
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
  }
}

resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' = {
  name: 'LockNoteAppKeyVault'
  location: resourceGroup().location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: subscription().tenantId
  }
}
