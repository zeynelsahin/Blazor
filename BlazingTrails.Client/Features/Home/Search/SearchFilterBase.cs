using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.Home.Search;

public class SearchFilterBase : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    protected int MaxLenght;
    protected int MaxTime;
    [Parameter, EditorRequired] public string SearchTerm { get; set; } = default!;

    protected void FilterSearchResults()
    {
        var uriWithQuerystring = NavigationManager.GetUriWithQueryParameters(new Dictionary<string, object?>()
        {
            [nameof(SearchPage.MaxLenght)] = MaxLenght == 0 ? null : MaxLenght,
            [nameof(SearchPage.MaxTime)] = MaxTime == 0 ? null : MaxTime
        });
        
        NavigationManager.NavigateTo(uriWithQuerystring);
    }

    protected void ClearSearchFilter()
    {
        MaxLenght = 0;
        MaxTime = 0;
        NavigationManager.NavigateTo($"/search/{SearchTerm}");
    }
}