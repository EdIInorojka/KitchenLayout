using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    [BindProperty] public int RoomWidth { get; set; }
    [BindProperty] public int RoomLength { get; set; }

    [BindProperty] public int StoveWidth { get; set; }
    [BindProperty] public int StoveDepth { get; set; }
    [BindProperty] public bool IsStoveCorner { get; set; }

    [BindProperty] public int SinkWidth { get; set; }
    [BindProperty] public int SinkDepth { get; set; }
    [BindProperty] public bool IsSinkCorner { get; set; }

    [BindProperty] public int SinkX { get; set; }
    [BindProperty] public int SinkY { get; set; }

    public List<Module> Elements { get; set; } = new List<Module>();

    public void OnGet()
    {
    }

    public void OnPost()
    {
        var sink = new Module
        {
            Name = "Мойка",
            Width = SinkWidth,
            Depth = SinkDepth,
            IsCorner = IsSinkCorner
        };

        var stove = new Module
        {
            Name = "Плита",
            Width = StoveWidth,
            Depth = StoveDepth,
            IsCorner = IsStoveCorner
        };

        var layoutService = new KitchenService(RoomWidth, RoomLength, sink, stove);
        Elements = layoutService.GenerateLayout(SinkX, SinkY);
    }
}
