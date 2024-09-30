public class KitchenService
{
    private int roomWidth;
    private int roomLength;
    private Module sink;
    private Module stove;
    private int[,] grid;

    public KitchenService(int width, int length, Module sink, Module stove)
    {
        roomWidth = width;
        roomLength = length;
        this.sink = sink;
        this.stove = stove;
        grid = new int[roomWidth * 2, roomLength * 2];
    }

    public List<Module> GenerateLayout(int sinkX, int sinkY)
    {
        var elements = new List<Module> { sink, stove };
        PlaceElement(sink, sinkX, sinkY);

        if (sink.IsPlacedSuccessfully)
        {
            PlaceElement(stove, sinkX + sink.Width, sinkY);
        }

        return elements;
    }

    private void PlaceElement(Module element, int x, int y)
    {
        int gridX = x * 2;
        int gridY = y * 2;

        if (gridX < 0 || gridY < 0 || gridX + element.Width * 2 > roomWidth * 2 || gridY + element.Depth * 2 > roomLength * 2)
        {
            element.IsPlacedSuccessfully = false;
            element.ErrorMessage = "Не влезла";
            return;
        }

        for (int i = gridX; i < gridX + element.Width * 2; i++)
        {
            for (int j = gridY; j < gridY + element.Depth * 2; j++)
            {
                if (grid[i, j] != 0)
                {
                    element.IsPlacedSuccessfully = false;
                    element.ErrorMessage = "Соприкасается с предыдущим модулем";
                    return;
                }
            }
        }

        for (int i = gridX; i < gridX + element.Width * 2; i++)
        {
            for (int j = gridY; j < gridY + element.Depth * 2; j++)
            {
                grid[i, j] = 1;
            }
        }

        element.IsPlacedSuccessfully = true;
        element.X = x;
        element.Y = y;
    }
}
