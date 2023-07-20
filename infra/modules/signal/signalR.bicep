param location string
param name string
param sku object
@allowed([
  'Serverless'
  'Default'
])
param serviceMode string

resource signalRService 'Microsoft.SignalRService/signalR@2022-08-01-preview' = {
  name: name
  location: location
  sku: sku
  kind: 'SignalR'
  properties: {
    features: [
      {
        flag: 'ServiceMode'
        value: serviceMode        
      }
    ]
  }
}
