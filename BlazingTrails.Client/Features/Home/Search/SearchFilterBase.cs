using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.Home.Search;

public class SearchFilterBase : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    protected int MaxLenght;
    [Parameter, EditorRequired] public string SearchTerm { get; set; } = default!;

    protected void FilterSearchResults() => NavigationManager.NavigateTo($"/search/{SearchTerm}/maxlenght/{MaxLenght}");
    protected void ClearSearchFilter()
    {
        MaxLenght = 0;
        NavigationManager.NavigateTo($"/search/{SearchTerm}");
    }
}