using System.Net.Http.Json;
using BlazingTrails.Client.Features.Home.Shared;
using BlazingTrails.Shared.Features.Home.Shared;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazingTrails.Client.Features.Home.Search;

public class SearchPageBase : ComponentBase
{
    [Parameter] public string? SearchTerm { get; set; }
    [Inject] private IMediator Mediator { get; set; }

    protected IEnumerable<Trail>? SearchResults;
    private IEnumerable<Trail>? _cachedSearchResults;
    protected Trail? SelectedTrail;
    [Parameter, SupplyParameterFromQuery] public int? MaxLenght { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? MaxTime { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var response = await Mediator.Send(new GetTrailsRequest());
            var allTrails= response.Trails.Select(x=> new Trail {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                Location = x.Location,
                Length = x.Length,
                TimeInMinutes = x.TimeInMinutes
            });
            SearchResults = allTrails.Where(x => x.Name.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase)
                                                  || x.Location.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase));
            _cachedSearchResults = SearchResults;
            UpdateFilters();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"There was a problem loading trail data: {exception.Message}");
        }
        // try
        // {
        //     var allTrails = await HttpClient.GetFromJsonAsync<IEnumerable<Trail>>("trails/trail-data.json");
        //     SearchResult = allTrails!.Where(x => x.Name.Contains(SearchTerm!, StringComparison.OrdinalIgnoreCase) || x.Location.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase));
        //     _cachedSearchResult = SearchResult;
        // }
        // catch (Exception exception)
        // {
        //     Console.WriteLine(exception);
        //     throw;
        // }
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
            SearchResults = _cachedSearchResults.Where(trail => filters.All(filter => filter(trail)));
        }
        else
        {
            SearchResults = _cachedSearchResults;
        }
    }
}