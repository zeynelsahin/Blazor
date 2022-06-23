using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazingTrails.Client.Features.Home.Shared;

public class TrailDetailBase: ComponentBase
{
    
    [Parameter,EditorRequired]
    public Trail? Trail { get; set; }

    protected Trail? ActiveTrail;
    protected  bool Open;
    protected Anchor Anchor;
    void OpenDrawer(Anchor anchor)
    {
        Open = true;
        this.Anchor = anchor;
    }

    protected void CloseDrawer()
    {
        Open = false;
    }

    protected override void OnParametersSet()
    {
        if (Trail!=null)
        {
            ActiveTrail = Trail;
            OpenDrawer(Anchor.End);
        }
    }
}