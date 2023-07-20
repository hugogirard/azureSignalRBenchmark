param location string
param name string
param sku object

resource asp 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: name
  location: location
  properties: {
  }
  sku: {
    name: sku.name
    tier: sku.tier
  }
}
