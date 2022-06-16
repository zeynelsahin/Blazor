using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.Home;

public class HomePageBase: ComponentBase
{
    protected IEnumerable<Trail>? Trails;
    protected Trail? _selectedTrail;
    [Inject] private HttpClient HttpClient { get; set; } = default;
    protected override async Task OnInitializedAsync()
    {
        
        try
        {
            Trails = await HttpClient.GetFromJsonAsync<IEnumerable<Trail>>("trails/trail-data.json");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"There was a problem loading trail data: {exception.Message}");
        }
    }
    protected void HandleTrailSelected(Trail trail)
    {
        _selectedTrail = trail;
    }
}