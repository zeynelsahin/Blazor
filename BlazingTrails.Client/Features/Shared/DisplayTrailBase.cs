using BlazingTrails.Client.Features.Home;
using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.Shared;

public class DisplayTrailBase: ComponentBase
{
     [Parameter] public IEnumerable<Trail> Trails { get; set; }
}