using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.Home.Shared;

public class DisplayTrailsBase : ComponentBase
{
    [Parameter] public IEnumerable<Trail> Trails { get; set; } = default!;
    [Parameter, EditorRequired] public EventCallback<Trail> OnSelected { get; set; }
}