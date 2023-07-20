namespace Thermo.Api.Hub;

public class ThermoHub : Microsoft.AspNetCore.SignalR.Hub
{
    public ThermoHub()
    {
        
    }

    public int MyProperty { get; set; }
}
