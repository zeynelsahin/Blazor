using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazingTrails.Client.Features.Home.Search;

public class SearchPageBase : ComponentBase
{
    [Parameter] public string? SearchTerm { get; set; }
    [Inject] private HttpClient HttpClient { get; set; }

    protected IEnumerable<Trail>? SearchResult;
    private IEnumerable<Trail>? _cachedSearchResult;
    protected Trail? SelectedTrail;
    [Parameter, SupplyParameterFromQuery] public int? MaxLenght { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? MaxTime { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var allTrails = await HttpClient.GetFromJsonAsync<IEnumerable<Trail>>("trails/trail-data.json");
            SearchResult = allTrails!.Where(x => x.Name.Contains(SearchTerm!, StringComparison.OrdinalIgnoreCase) || x.Location.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase));
            _cachedSearchResult = SearchResult;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    protected override void OnParametersSet() => UpdateFilters();

    protected readonly List<BreadcrumbItem> Items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
        new BreadcrumbItem("Search", href: "/#", icon: Icons.Material.Filled.Search, disabled: true),
    };

    protected void HandleTrailSelected(Trail trail) => SelectedTrail = trail;

    private void UpdateFilters()
    {
        var filters = new List<Func<Trail, bool>>();
        if (MaxLenght is not null&& MaxLenght>0)
        {
            filters.Add(x=>x.Length<=MaxLenght);
        }

        if (MaxTime is not null&& MaxTime>0)
        {
            filters.Add(x=>x.TimeInMinutes<=MaxTime*60);
        }

        if (filters.Any())
        {
            SearchResult = _cachedSearchResult.Where(trail => filters.All(filter => filter(trail)));
        }
        else
        {
            SearchResult = _cachedSearchResult;
        }
    }
}