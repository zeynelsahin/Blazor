using BlazingTrails.Client.Features.Home.Shared;
using BlazingTrails.ComponentLibrary;

using BlazingTrails.Shared.Features.Home.Shared;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.Home;

public class HomePageBase: ComponentBase
{
    [Inject] public IMediator Mediator { get; set; }
    protected IEnumerable<Trail>? Trails;
    protected Trail? _selectedTrail;
    protected string searchString1 = "";
    protected bool FilterFunc1(Trail trail) => FilterFunc(trail, searchString1);

    protected ViewSwitcherBase.ViewMode Mode;

    protected bool FilterFunc(Trail trail, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (trail.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (trail.Location.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var response = await Mediator.Send(new GetTrailsRequest());
            Trails = response.Trails.Select(x => new Trail()
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                Location = x.Location,
                Length = x.Length,
                TimeInMinutes = x.TimeInMinutes
            });

        }
        catch (Exception exception)
        {
            Console.WriteLine($"There was a problem loading trail data: {exception.Message}");
        }
        // try
        // {
        //     Trails = await HttpClient.GetFromJsonAsync<IEnumerable<Trail>>("trails/trail-data.json");
        // }
        // catch (Exception exception)
        // {
        //     Console.WriteLine($"There was a problem loading trail data: {exception.Message}");
        // }
    }
    protected void HandleTrailSelected(Trail trail)
    {
        _selectedTrail = trail;
    }
    protected void HandleViewMode(ViewSwitcherBase.ViewMode mode)
    {
        Mode = mode;
    }

}