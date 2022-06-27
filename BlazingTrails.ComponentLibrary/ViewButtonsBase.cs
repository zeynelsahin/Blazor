using Microsoft.AspNetCore.Components;

namespace BlazingTrails.ComponentLibrary;

public class ViewButtonsBase : ComponentBase
{
    [Parameter, EditorRequired]
    public EventCallback<ViewSwitcherBase.ViewMode
    > OnChange { get; set; }
}