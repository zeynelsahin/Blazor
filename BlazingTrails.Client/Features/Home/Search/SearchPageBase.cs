using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazingTrails.Client.Features.Home.Search;

public class SearchPageBase : ComponentBase
{
    [Parameter] public string? SearchTerm { get; set; }
    [Inject] private HttpClient HttpClient { get; set; }

    protected IEnumerable<Trail>? SearchResult;
    protected Trail? SelectedTrail;
    [Parameter] public int? MaxLenght { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var allTrails = await HttpClient.GetFromJsonAsync<IEnumerable<Trail>>("trails/trail-data.json");
            SearchResult = allTrails!.Where(x => x.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) || x.Location.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
    protected readonly List<BreadcrumbItem> Items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
        new BreadcrumbItem("Search", href: "/#", icon: Icons.Material.Filled.Search,disabled:true),
    };
    protected void HandleTrailSelected(Trail trail) => SelectedTrail = trail;

}
