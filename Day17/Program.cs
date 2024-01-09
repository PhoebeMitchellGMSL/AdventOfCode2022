var input = File.ReadAllLines("input.txt")[0];
Console.WriteLine(input.Length);

var chamber = new Chamber();
var jetIndex = 0;

for (long i = 0; i < 10000; i++)
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
    
    // chamber.Print(xPositions, yPositions);
    // Console.WriteLine();
    chamber.SetPositions(xPositions, yPositions);
}

// chamber.Print(Array.Empty<int>(), Array.Empty<int>());
var patternData = chamber.CheckForPattern();
var remainder = (int)((1000000000000 - patternData.Item4 - 1) % patternData.Item3);
var repeatedCount = (1000000000000 - patternData.Item4 - 1) / patternData.Item3;
Console.WriteLine(patternData.Item2 * repeatedCount + patternData.Item1 + patternData.Item5[..(remainder + 1)].Sum());

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

    public (int, int, int, int, List<int>) CheckForPattern()
    {
        var longest = (0, 0, 0, 0, new List<int>());
        for (var i = 0; i < heightDifferences.Count / 2; i++)
        {
            for (var j = i + 2; j < heightDifferences.Count - j - (j - i); j++)
            {
                if (heightDifferences[i] == heightDifferences[j] && heightDifferences[j] == heightDifferences[j + (j - i)])
                {
                    for (var k = 0; k < j - i; k++)
                    {
                        if (heightDifferences[i + k] != heightDifferences[j + k] ||
                            heightDifferences[j + k] != heightDifferences[j + (j - i) + k] ||
                            heightDifferences[i + k] != heightDifferences[j + (j - i) + k])
                        {
                            // Console.WriteLine();
                            break;
                        }
            
                        // Console.Write($"{heightDifferences[i + k]} + ");
                        // Console.WriteLine($"{i + k}: {heightDifferences[i + k]}, {j + k}: {heightDifferences[j + k]}, {j + (j - i) + k}: {heightDifferences[j + (j - i) + k]}");

                        if (i + k == j - 1)
                        {
                            if (k > longest.Item3)
                            {
                                longest = (heightDifferences[..i].Sum(), heightDifferences[i..j].Sum(), k + 1, i, heightDifferences[i..j]);
                                // Console.WriteLine(((longest.Item2 / longest.Item3) * 1000000000000 + longest.Item1));
                            }
                        }
                    }
                }
            }
        }

        return longest;
    }

    public void SetPositions(int[] xPositions, int[] yPositions)
    {
        var height = HighestRock;
        for (var i = 0; i < xPositions.Length; i++)
        {
            var y = yPositions[i];
            if (y > HighestRock - 1)
            {
                var difference = int.Max(0, y - (HighestRock - 1));
                foreach (var column in columns)
                {
                    column.AddRange(new bool[difference]);
                }

                HighestRock = y + 1;
            }
            
            columns[xPositions[i]][y] = true;
        }

        var heightDifference = HighestRock - height;
        heightDifferences.Add(heightDifference);
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