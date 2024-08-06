public enum ChunkUnit
{
    Small,
    Medium,
    Large,
    XLarge
}

public class ChunkUnitData
{
    public ChunkUnit Size;
    public int WidthInCells;
    public int HeightInCells;

    public ChunkUnitData(ChunkUnit size, int width, int height)
    {
        Size = size;
        WidthInCells = width;
        HeightInCells = height;
    }
}

public static class ChunkUnits
{
    public static readonly ChunkUnitData Small = new ChunkUnitData(ChunkUnit.Small, 1, 1);
    public static readonly ChunkUnitData Medium = new ChunkUnitData(ChunkUnit.Medium, 2, 2);
    public static readonly ChunkUnitData Large = new ChunkUnitData(ChunkUnit.Large, 3, 3);
    public static readonly ChunkUnitData XLarge = new ChunkUnitData(ChunkUnit.XLarge, 4, 4);
}
