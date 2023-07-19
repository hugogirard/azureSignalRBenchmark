targetScope = 'subscription'

param location string = 'eastus'

var rgName = 'rg-signalR-test'

var suffix = uniqueString(rg.id)

resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: rgName
  location: location
}

module signalRDefault 'modules/signalR.bicep' = {
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
