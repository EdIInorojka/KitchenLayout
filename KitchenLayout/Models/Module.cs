public class KitchenElement
{
    public string Name { get; set; }
    public int Width { get; set; }
    public int Depth { get; set; }
    public bool IsCorner { get; set; }
    public (float X, float Y) Position { get; set; }
    public (float X, float Y) PipePosition { get; set; } // Позиция трубы

    public bool IsSuccessful { get; set; }
}


public class KitchenRoom
{
    public float Width { get; set; }
    public float Depth { get; set; }
    public KitchenElement Sink { get; set; } = new KitchenElement { Name = "Умывальник" };
    public KitchenElement Stove { get; set; } = new KitchenElement { Name = "Плита" };
    public List<KitchenElement> Elements { get; set; }

    public bool IsSuccessful { get; set; }
    public string ErrorMessage { get; internal set; }

    public KitchenRoom()
    {
        Elements = new List<KitchenElement> { Sink, Stove };
    }
}