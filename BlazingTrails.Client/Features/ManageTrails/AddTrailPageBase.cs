using BlazingTrails.Shared.Features.ManageTrails;
using FluentValidation;
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
    protected MudForm form;
    protected TrailFluentValidator TrailValidator = new TrailFluentValidator();
    protected RouteInstructionFluentValidator RouteInstructionValidator = new RouteInstructionFluentValidator();
    protected TrailDto Trail = new TrailDto();



    protected async Task Submit()
    {
        await form.Validate();
        if (form.IsValid)
        {
            Snackbar.Add("Trail Added!",Severity.Success);
        }
    }

    /// <summary>
    /// A standard AbstractValidator which contains multiple rules and can be shared with the back end API
    /// </summary>
    /// <typeparam name="OrderModel"></typeparam>
    public class TrailFluentValidator : AbstractValidator<TrailDto>
    {
        public TrailFluentValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
            RuleFor(x => x.Route).NotEmpty();
            RuleForEach(x => x.Route)
                .SetValidator(new RouteInstructionFluentValidator());
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<TrailDto>.CreateWithOptions((TrailDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}

public class RouteInstructionFluentValidator : AbstractValidator<TrailDto.RouteInstruction>
{
    public RouteInstructionFluentValidator()
    {
        RuleFor(x => x.Stage).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TrailDto.RouteInstruction>.CreateWithOptions((TrailDto.RouteInstruction)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
 
}
