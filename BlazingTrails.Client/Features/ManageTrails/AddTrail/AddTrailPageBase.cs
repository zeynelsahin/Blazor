using BlazingTrails.Client.Features.ManageTrails.Shared;
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
    protected TrailForm TrailForm = default!;
    
    private string? ErrorMessage;
    private bool SubmitSuccessful;
    protected async Task Submit(TrailDto trail,IBrowserFile? image)
    {
      
            var response = await Mediator.Send(new AddTrailRequest(trail));
            if (response.TrailId == -1)
            {
                ErrorMessage = "There was a problem saving your trail.";
                SubmitSuccessful = false;
                Snackbar.Add(ErrorMessage, Severity.Error);
                StateHasChanged();
                return;
            }

            if (image is null)
            {
                SubmitSuccessful = true;
                TrailForm.ResetForm();
                StateHasChanged();
                return;
            }
            
            SubmitSuccessful= await ProcessImage(response.TrailId,image);
            Snackbar.Add("Trail Added!", Severity.Success);
            if (SubmitSuccessful)
            {
                TrailForm.ResetForm();
            }
            StateHasChanged();
        
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
   
} 