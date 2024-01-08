var input = File.ReadAllText("input.txt");

var chamber = new Chamber();
var jetIndex = 0;

for (long i = 0; i < 5000; i++)
{
    var rockIndex = i % 5;
    
    int[] xPositions = rockIndex switch
    {
        0 => [2, 3, 4, 5],
        1 => [3, 2, 3, 4, 3],
        2 => [4, 4, 2, 3, 4],
        3 => [2, 2, 2, 2],
        4 => [2, 3, 2, 3]
    };
    int[] yPositions = rockIndex switch
    {
        0 => [chamber.HighestRock + 3, chamber.HighestRock + 3, chamber.HighestRock + 3, chamber.HighestRock + 3],
        1 => [chamber.HighestRock + 5, chamber.HighestRock + 4, chamber.HighestRock + 4, chamber.HighestRock + 4, chamber.HighestRock + 3],
        2 => [chamber.HighestRock + 5, chamber.HighestRock + 4, chamber.HighestRock + 3, chamber.HighestRock + 3, chamber.HighestRock + 3],
        3 => [chamber.HighestRock + 6, chamber.HighestRock + 5, chamber.HighestRock + 4, chamber.HighestRock + 3],
        4 => [chamber.HighestRock + 4, chamber.HighestRock + 4, chamber.HighestRock + 3, chamber.HighestRock + 3]
    };

    while (true)
    {
        // chamber.Print(xPositions, yPositions);
        // Console.WriteLine();
        
        var jetDirection = input[jetIndex] == '<' ? -1 : 1;
        jetIndex = (jetIndex + 1) % input.Length;
        
        var canMoveX = true;
        for (var j = 0; j < xPositions.Length; j++)
        {
            if (!chamber.GetPosition(xPositions[j] + jetDirection, yPositions[j]))
            {
                canMoveX = false;
            }
        }

        if (canMoveX)
        {
            for (var j = 0; j < xPositions.Length; j++)
            {
                xPositions[j] += jetDirection;
            }
        }
        
        // chamber.Print(xPositions, yPositions);
        // Console.WriteLine();
        
        var canMoveY = true;
        for (var j = 0; j < yPositions.Length; j++)
        {
            if (!chamber.GetPosition(xPositions[j], yPositions[j] - 1))
            {
                canMoveY = false;
            }
        }
        
        if (!canMoveY)
        {
            // chamber.Print(xPositions, yPositions);
            // Console.WriteLine();
            // var a = chamber.CheckForPattern();
            // if (a.Item2 != 0)
            // {
            //     Console.WriteLine($"{a.Item1} {a.Item2}");
            // }
            break;
        }

        for (var j = 0; j < yPositions.Length; j++)
        {
            yPositions[j] -= 1;
        }
    }

    for (var j = 0; j < xPositions.Length; j++)
    {
        chamber.SetPosition(xPositions[j], yPositions[j]);
    }
}

// chamber.Print(Array.Empty<int>(), Array.Empty<int>());
var a = chamber.CheckForPattern();
Console.WriteLine(chamber.HighestRock);
Console.WriteLine((a.Item2 / a.Item3) * 1000000000000 + a.Item1);

internal class Chamber
{
    private readonly List<bool>[] columns = new List<bool>[7];

    public List<int> heightDifferences { get; } = [];
    
    public int HighestRock { get; set; }
    
    public Chamber()
    {
        for (var i = 0; i < columns.Length; i++)
        {
            columns[i] = [false];
        }
    }

    public (int, int, int) CheckForPattern()
    {
        var longest = (0, 0, 0);
        for (var i = 0; i < heightDifferences.Count / 2; i++)
        {
            for (var j = i + 2; j < heightDifferences.Count - (j - i); j++)
            {
                if (heightDifferences[i] == heightDifferences[j])
                {
                    for (var k = 0; k < j - i; k++)
                    {
                        if (heightDifferences[i + k] != heightDifferences[j + k])
                        {
                            Console.WriteLine();
                            break;
                        }
                        
                        Console.WriteLine($"{i + k}: {heightDifferences[i + k]}, {j + k}: {heightDifferences[j + k]}");

                        if (i + k == j - 1)
                        {
                            if (k > longest.Item2)
                            {
                                longest = (heightDifferences[..i].Sum(), heightDifferences[i..(i + k)].Sum(), k);
                            }
                        }
                    }
                }
            }
        }

        return longest;
    }

    public void SetPosition(int x, int y)
    {
        if (y > HighestRock - 1)
        {
            var difference = y + 1 - (HighestRock - 1);
            heightDifferences.Add(difference);
            foreach (var column in columns)
            {
                column.AddRange(new bool[difference]);
            }
            HighestRock = y + 1;
        }
        
        columns[x][y] = true;
    }

    public bool GetPosition(int x, int y)
    {
        if (x < 0 || x >= columns.Length || y < 0)
        {
            return false;
        }
        
        if (y >= HighestRock)
        {
            return true;
        }

        return !columns[x][y];
    }

    public void Print(int[] currentPosX, int[] currentPosY)
    {
        var maxY = 0;
        if (currentPosY.Length > 0)
        {
            maxY = currentPosY.Max();
        }
        
        for (var y = Math.Max(HighestRock - 1, maxY); y >= 0; y--)
        {
            for (var x = 0; x < columns.Length; x++)
            {
                var shouldPrint = true;
                for (var i = 0; i < currentPosX.Length; i++)
                {
                    if (x == currentPosX[i] && y == currentPosY[i])
                    {
                        Console.Write('#');
                        shouldPrint = false;
                    }
                }

                if (shouldPrint)
                {
                    try
                    {
                        Console.Write(columns[x][y] ? '#' : '.');
                    }
                    catch
                    {
                        Console.Write('.');
                    }
                }
            }

            Console.WriteLine();
        }
    }
}