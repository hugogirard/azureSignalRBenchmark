targetScope = 'subscription'

param location string = 'eastus'

var rgName = 'rg-signalR-test'

var suffix = uniqueString(rg.id)

resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: rgName
  location: location
}

module signalRDefault 'modules/signal/signalR.bicep' = {
  scope: resourceGroup(rgName)
  name: 'signalRDefault'
  params: {
    location: location
    name: 'sig-${suffix}'
    serviceMode: 'Default'
    sku: {
      name: 'Premium_P1'
      tier: 'Premium'
      size: 'P1'
      capacity: 1
    }
  }
}

module cache 'modules/cache/redis.bicep' = {
  scope: resourceGroup(rgName)
  name: 'cache'
  params: {
    location: location
    suffix: suffix
  }
}

module aspAppServer 'modules/web/asp.bicep' = {
  scope: resourceGroup(rgName)
  name: 'asp-appserver-${suffix}'
  params: {
    location: location
    name: 'asp-appserver-${suffix}'
    sku: {
      name: 'P1V3'
      tier: 'PremiumV3'
    }
  }
}
