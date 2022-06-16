using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazingTrails.Client.Features.Home;

public class TrailSearchBase: ComponentBase
{
    [Inject] 
    private NavigationManager NavigationManager { get; set; }
    protected string SearchTerm = "";
    protected void SearchForTrail(KeyboardEventArgs keyboardEventArgs)
    {
        if (keyboardEventArgs.Key != "Enter") return;
        NavigationManager.NavigateTo($"/search/{SearchTerm}");
    }
}