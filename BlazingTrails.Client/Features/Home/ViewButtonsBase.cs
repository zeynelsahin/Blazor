using BlazingTrails.Shared.Features;
using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.Home;

public class ViewButtonsBase : ComponentBase
{
    [Parameter, EditorRequired]
    public EventCallback<Enums.ViewMode
    > OnChange { get; set; }
}