param location string
param suffix string

resource cache 'Microsoft.Cache/redis@2023-05-01-preview' = {
  name: 'cache-${suffix}'
  location: location
  properties: {
    sku: {
      capacity: 1
      family: 'C'
      name: 'Standard'
    }
  }
}
