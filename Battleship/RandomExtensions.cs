// Пункт 3 — extension method для Random, принимает GameSettings
static class RandomExtensions
{
    public static Ship NextShip(this Random random, GameSettings settings)
    {
        int length = random.Next(settings.MinShipLength, settings.MaxShipLength + 1);
        bool isHorizontal = random.Next(2) == 0;

        if (isHorizontal)
        {
            int x = random.Next(0, settings.Rows - length + 1);
            int y = random.Next(0, settings.Columns);
            return new HorizontalShip(new Position(x, y), length);
        }

        int vx = random.Next(0, settings.Rows);
        int vy = random.Next(0, settings.Columns - length + 1);
        return new VerticalShip(new Position(vx, vy), length);
    }
}