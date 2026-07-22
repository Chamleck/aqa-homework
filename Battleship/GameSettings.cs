class GameSettings
{
    public int Rows { get; }
    public int Columns { get; }
    public int MinShipLength { get; }
    public int MaxShipLength { get; }

    public GameSettings(int rows, int columns, int minShipLength, int maxShipLength)
    {
        Rows = rows;
        Columns = columns;
        MinShipLength = minShipLength;
        MaxShipLength = maxShipLength;
    }
}