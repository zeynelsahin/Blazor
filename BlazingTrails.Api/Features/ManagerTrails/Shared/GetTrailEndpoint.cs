using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.Home.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManagerTrails.Shared;

public class GetTrailEndpoint: BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetTrailsRequest.Response>
{
    private BlazingTrailsContext _blazingTrailsContext;

    public GetTrailEndpoint(BlazingTrailsContext blazingTrailsContext)
    {
        _blazingTrailsContext = blazingTrailsContext;
    }
    [HttpGet(GetTrailsRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = new CancellationToken())
    {
        var trails = await _blazingTrailsContext.Trails.Include(x => x.Route).ToListAsync(cancellationToken);
        var response = new GetTrailsRequest.Response(trails.Select(trail => new GetTrailsRequest.Trail(
            trail.Id,
            trail.Name,
            trail.Image,
            trail.Location,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description)));

        return Ok(response);
    }
}