using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

public class TrailFormBase : ComponentBase
{
    protected TrailDto _trail = new TrailDto();
    [Inject] public ISnackbar Snackbar { get; set; }
    protected MudForm form;
    protected readonly TrailValidator TrailValidator = new TrailValidator();
    protected readonly RouteInstructionValidator RouteInstructionValidator = new RouteInstructionValidator();


    [Parameter, EditorRequired] public Func<TrailDto, IBrowserFile?, Task> OnSubmit { get; set; } = default!;
    [Parameter] public TrailDto? Trail { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _trail.Route = new List<TrailDto.RouteInstruction>() { new TrailDto.RouteInstruction() };
    }

    private string? ErrorMessage;
    private bool SubmitSuccessful;
    protected IBrowserFile? TrailImage;
    protected async Task SubmitForm()
    {
        await form.Validate();
        if (form.IsValid)
        {
            await OnSubmit(_trail, TrailImage);
        }
    }

    protected override void OnParametersSet()
    {
        if (Trail != null)
        {
            _trail.Id = Trail.Id;
            _trail.Name = Trail.Name;
            _trail.Description = Trail.Description;
            _trail.Location = Trail.Location;
            _trail.Image = Trail.Image;
            _trail.ImageAction = ImageAction.None;
            _trail.Length = Trail.Length;
            _trail.TimeInMinutes = Trail.TimeInMinutes;

            _trail.Route.Clear();
            _trail.Route.AddRange(Trail.Route.Select(routeInstruction => new TrailDto.RouteInstruction
            {
                Stage = routeInstruction.Stage,
                Description = routeInstruction.Description
            }));
        }
    }

    protected void UploadFiles(InputFileChangeEventArgs e)
    {
        TrailImage = e.File;
        _trail.ImageAction = ImageAction.Add;
        Snackbar.Add(TrailImage.Name + " Added", Severity.Info);
    }

    protected void RemoveTrailImage()
    {
        _trail.Image = null;
        _trail.ImageAction = ImageAction.Remove;
    }

    public void ResetForm()
    {
        _trail = new TrailDto();
        TrailImage = null;
    }
}