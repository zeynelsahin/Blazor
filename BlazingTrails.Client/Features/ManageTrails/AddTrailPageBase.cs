using BlazingTrails.Shared.Features.ManageTrails;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Severity = MudBlazor.Severity;

namespace BlazingTrails.Client.Features.ManageTrails;

public class AddTrailPageBase : ComponentBase
{
    protected readonly List<BreadcrumbItem> Items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home", href: "/", icon: Icons.Material.Filled.Home),
        new BreadcrumbItem("Add Trail", href: "/#", icon: Icons.Material.Filled.Add, disabled: true),
    };

    [Inject] public ISnackbar Snackbar { get; set; }
    [Inject] public IMediator Mediator { get; set; }
    protected MudForm form;
    protected TrailDto.TrailValidator TrailValidator = new TrailDto.TrailValidator();
    protected TrailDto.RouteInstructionValidator RouteInstructionValidator = new TrailDto.RouteInstructionValidator();
    protected TrailDto Trail = new TrailDto();

    private string? ErrorMessage;
    private bool SubmitSuccessful;

    protected async Task Submit()
    {
        await form.Validate();
        if (form.IsValid)
        {
            var response = await Mediator.Send(new AddTrailRequest(Trail));
            if (response.TrailId == -1)
            {
                ErrorMessage = "There was a problem saving your trail.";
                SubmitSuccessful = false;
                Snackbar.Add(ErrorMessage, Severity.Error);
                return;
            }

            Trail = new TrailDto();
            ErrorMessage = null;
            SubmitSuccessful = true;
            Snackbar.Add("Trail Added!", Severity.Success);
        }
    }
}