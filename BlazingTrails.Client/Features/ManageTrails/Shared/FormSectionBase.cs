using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

public class FormSectionBase: ComponentBase
{
    [Parameter, EditorRequired] public string Title { get; set; } = default!;
    [Parameter, EditorRequired] public string HelpText { get; set; } = default!;
    [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;

}