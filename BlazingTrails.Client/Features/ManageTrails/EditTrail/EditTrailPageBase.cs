using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail;

public class EditTrailPageBase: ComponentBase
{
    protected readonly List<BreadcrumbItem> Items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
        new BreadcrumbItem("Edit Trail", href: "/#", icon: Icons.Material.Filled.Add, disabled: true),
    };

    private bool _submitSuccessful;
    private string? _errorMessage;
    protected TrailDto _trail = new TrailDto();
    [Inject] public IMediator Mediator { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }
    [Parameter] public int TrailId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var response = await Mediator.Send(new GetTrailRequest(TrailId)); 

        if (response.Trail is not null)
        {
            _trail.Id = TrailId;
            _trail.Name = response.Trail.Name;
            _trail.Description = response.Trail.Description;
            _trail.Location = response.Trail.Location;
            _trail.Image = response.Trail.Image;
            _trail.Length = response.Trail.Length;
            _trail.TimeInMinutes = response.Trail.TimeInMinutes;
            _trail.Route.AddRange(response.Trail.RouteInstructions.Select(ri => new TrailDto.RouteInstruction
            {
                Stage = ri.Stage,
                Description = ri.Description
            }));
        }
        else
        {
            _errorMessage = "There was a problem loading the trail.";
        }
    }
    protected async Task Submit(TrailDto trail, IBrowserFile? image)
    {
        var response = await Mediator.Send(new EditTrailRequest(trail));
        if (!response.IsSuccess)
        {
            _submitSuccessful = false;
            _errorMessage = "There was a problem saving your trail.";
        }
        else
        {
            _trail.Name = trail.Name;
            _trail.Description = trail.Description;
            _trail.Location = trail.Location;
            _trail.Length = trail.Length;
            _trail.TimeInMinutes = trail.TimeInMinutes;
            _trail.Route.Clear();
            _trail.Route.AddRange(trail.Route.Select(ri => new TrailDto.RouteInstruction
            {
                Stage = ri.Stage,
                Description = ri.Description
            }));

            _submitSuccessful = true;

            if (trail.ImageAction == ImageAction.Add) _submitSuccessful = await ProcessImage(trail.Id, image!);
            if (trail.ImageAction == ImageAction.Remove) _trail.Image = null;
        }

        Snackbar.Add("Edit trail successful!", Severity.Success);
        StateHasChanged();
    }

    private async Task<bool> ProcessImage(int trailId, IBrowserFile trailImage)
    {
        var imageUploadResponse = await Mediator.Send(new UploadTrailImageRequest(trailId, trailImage));

        if (string.IsNullOrWhiteSpace(imageUploadResponse.ImageName))
        {
            _errorMessage = "Your trail was saved, but there was a problem uploading the image.";
            return false;
        }

        _trail.Image = imageUploadResponse.ImageName;
        return true;
    }
}