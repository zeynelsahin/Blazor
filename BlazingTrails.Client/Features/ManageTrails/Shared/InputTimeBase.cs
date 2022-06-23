using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

public class InputTimeBase: ComponentBase
{
    [Inject] public ISnackbar Snackbar { get; set; }
    [Parameter] public TrailValidator Validator { get; set; }
    [Parameter,EditorRequired] public TrailDto Trail { get;set; }
    protected TrailDto model = new TrailDto();
    
}