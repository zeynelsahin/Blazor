using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Severity = MudBlazor.Severity;

namespace BlazingTrails.Client.Features.ManageTrails.AddTrail;

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
    protected readonly TrailDto.TrailValidator TrailValidator = new TrailDto.TrailValidator();
    protected readonly TrailDto.RouteInstructionValidator RouteInstructionValidator = new TrailDto.RouteInstructionValidator();
    protected TrailDto Trail = new TrailDto();

    private string? ErrorMessage;
    private bool SubmitSuccessful;
    protected IBrowserFile? Image;
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
                StateHasChanged();
                return;
            }

            if (Image is null)
            {
                SubmitSuccessful = true;
                ResetForm();
                StateHasChanged();
                return;
            }
            
            SubmitSuccessful= await ProcessImage(response.TrailId,Image);
            Snackbar.Add("Trail Added!", Severity.Success);
        }
    }
    private async Task<bool>ProcessImage(int trailId, IBrowserFile trailImage)
    {
        var imageUploadResponse = await Mediator.Send(new UploadTrailImageRequest(trailId, trailImage));
 
        if (string.IsNullOrWhiteSpace(imageUploadResponse.ImageName))
        {
            ErrorMessage = "Your trail was saved,but there was a problem uploading the image.";
            return false;
        }

        return true;
    }
    protected void UploadFiles(InputFileChangeEventArgs e)
    {
        Image = e.File;
        Snackbar.Add(Image.Name +" Added", Severity.Info);
        StateHasChanged();
        
    }
    private void ResetForm()
    {
        Trail = new TrailDto();
        ErrorMessage = null;
        SubmitSuccessful = true;
    }
} 