using Microsoft.AspNetCore.Components;

namespace BlazingTrails.ComponentLibrary;

public class ViewSwitcherBase: ComponentBase
{
    [Parameter, EditorRequired] public RenderFragment GridTemplate { get; set; } = default!;
    [Parameter, EditorRequired] public RenderFragment TableTemplate { get; set; } = default!;

    [Parameter]public ViewMode Mode { get; set; }

    public enum ViewMode
    {
        Grid,Table
    }
    
    
}