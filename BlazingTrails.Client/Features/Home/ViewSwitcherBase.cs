using BlazingTrails.Shared.Features;
using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.Home;

public class ViewSwitcherBase: ComponentBase
{
    [Parameter, EditorRequired] public RenderFragment GridTemplate { get; set; } = default!;
    [Parameter, EditorRequired] public RenderFragment TableTemplate { get; set; } = default!;

    [Parameter]public Enums.ViewMode Mode { get; set; }

  
    
    
}