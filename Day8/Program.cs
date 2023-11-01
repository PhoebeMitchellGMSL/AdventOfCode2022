var lines = File.ReadAllLines("input.txt");

var visibleTreeCount = 0;

// Part 1
for (var y = 0; y < lines.Length; y++)
{
    if (y == 0 || y == lines.Length - 1)
    {
        visibleTreeCount += lines.Length;
        continue;
    }

    for (var x = 0; x < lines[y].Length; x++)
    {
        if (x == 0 || x == lines[y].Length - 1)
        {
            visibleTreeCount++;
            continue;
        }

        var treeInt = int.Parse(lines[y][x].ToString());

        var leftOfTree = lines[y][..x];
        if (leftOfTree.All(character => int.Parse(character.ToString()) < treeInt))
        {
            visibleTreeCount++;
            continue;
        }

        var rightOfTree = lines[y][(x + 1)..];
        if (rightOfTree.All(character => int.Parse(character.ToString()) < treeInt))
        {
            visibleTreeCount++;
            continue;
        }

        var treeVisible = true;
        for (var i = 0; i < y; i++)
        {
            if (int.Parse(lines[i][x].ToString()) >= treeInt)
            {
                treeVisible = false;
                break;
            }
        }
        if (treeVisible)
        {
            visibleTreeCount++;
            continue;
        }

        treeVisible = true;
        for (var i = y + 1; i < lines.Length; i++)
        {
            if (int.Parse(lines[i][x].ToString()) >= treeInt)
            {
                treeVisible = false;
                break;
            }
        }
        if (treeVisible)
        {
            visibleTreeCount++;
        }
    }
}

Console.WriteLine(visibleTreeCount);


// Part 2
var scores = new List<int>();
for (var y = 0; y < lines.Length; y++)
{
    for (var x = 0; x < lines[y].Length; x++)
    {
        var treeInt = int.Parse(lines[y][x].ToString());

        var leftScore = 0;
        for (var i = x - 1; i >= 0; i--)
        {
            leftScore++;
            if (int.Parse(lines[y][i].ToString()) >= treeInt)
            {
                break;
            }
        }

        var rightScore = 0;
        for (var i = x + 1; i < lines[y].Length; i++)
        {
            rightScore++;
            if (int.Parse(lines[y][i].ToString()) >= treeInt)
            {
                break;
            }
        }

        var upScore = 0;
        for (var i = y - 1; i >= 0; i--)
        {
            upScore++;
            if (int.Parse(lines[i][x].ToString()) >= treeInt)
            {
                break;
            }
        }

        var downScore = 0;
        for (var i = y + 1; i < lines.Length; i++)
        {
            downScore++;
            if (int.Parse(lines[i][x].ToString()) >= treeInt)
            {
                break;
            }
        }

        scores.Add(leftScore * rightScore * upScore * downScore);
    }
}

Console.WriteLine(scores.Max());