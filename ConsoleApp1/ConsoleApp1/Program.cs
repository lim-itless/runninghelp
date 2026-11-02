using System;

public abstract class Building
{
    public string Name { get; private set; }
    public int BaseProsperity { get; private set; }

    protected Building(string name, int baseProsperity)
    {
        Name = name;
        BaseProsperity = baseProsperity;
    }

    public abstract int GetDailyProsperity(Building[,] village, int row, int column);

    protected int CountNeighbors<T>(Building[,] village, int row, int column, bool includeDiagonals) where T : Building
    {
        int count = 0;

        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
            {
                if (rowOffset == 0 && columnOffset == 0)
                {
                    continue;
                }

                if (!includeDiagonals && rowOffset != 0 && columnOffset != 0)
                {
                    continue;
                }

                int nextRow = row + rowOffset;
                int nextColumn = column + columnOffset;

                if (nextRow < 0 || nextRow >= village.GetLength(0) || nextColumn < 0 || nextColumn >= village.GetLength(1))
                {
                    continue;
                }

                if (village[nextRow, nextColumn] is T)
                {
                    count++;
                }
            }
        }

        return count;
    }
}

public class House : Building
{
    public House() : base("집", 10)
    {}

    public override int GetDailyProsperity(Building[,] village, int row, int column)
    {
        int prosperity = BaseProsperity;
        int houseCount = CountNeighbors<House>(village, row, column, false);
        prosperity += houseCount * 5;

        int marketCount = CountNeighbors<Market>(village, row, column, false);

        if (marketCount > 0)
        {
            prosperity += 10;
        }

        int parkCount = CountNeighbors<Park>(village, row, column, true);
        prosperity += parkCount * 5;

        return prosperity;
    }
}

public class Market : Building
{
    public Market() : base("시장", 20)
    {}

    public override int GetDailyProsperity(Building[,] village, int row, int column)
    {
        int prosperity = BaseProsperity;

        if (HasMarketNetwork(village, row, column))
        {
            prosperity = 35;
        }

        int houseCount = CountNeighbors<House>(village, row, column, false);
        prosperity += houseCount * 10;

        int parkCount = CountNeighbors<Park>(village, row, column, true);
        prosperity += parkCount * 5;

        return prosperity;
    }

    private bool HasMarketNetwork(Building[,] village, int row, int column)
    {
        for (int c = 0; c < village.GetLength(1); c++)
        {
            if (c != column && village[row, c] is Market)
            {
                return true;
            }
        }

        for (int r = 0; r < village.GetLength(0); r++)
        {
            if (r != row && village[r, column] is Market)
            {
                return true;
            }
        }

        return false;
    }
}

public class Park : Building
{
    public Park() : base("공원", 0)
    {}

    public override int GetDailyProsperity(Building[,] village, int row, int column)
    {
        return 0;
    }
}

public class Program
{
    static void Main()
    {
        Building[,] village = new Building[5, 5];

        village[0, 0] = new House();
        village[0, 1] = new Market();
        village[0, 3] = new Market();

        village[1, 0] = new House();
        village[1, 1] = new Park();
        village[1, 2] = new House();

        village[2, 1] = new Market();
        village[2, 2] = new Park();

        int totalProsperity = 0;

        Console.WriteLine("===== 마을 번영도 계산 =====");

        for (int row = 0; row < village.GetLength(0); row++)
        {
            for (int column = 0; column < village.GetLength(1); column++)
            {
                Building building = village[row, column];

                if (building == null)
                {
                    continue;
                }

                int prosperity = building.GetDailyProsperity(village, row, column);
                totalProsperity += prosperity;

                Console.WriteLine($"[{row}, {column}] {building.Name} → 번영도 : {prosperity}");
            }
        }

        Console.WriteLine($"\n마을의 하루 총 번영도 : {totalProsperity}");
    }
}