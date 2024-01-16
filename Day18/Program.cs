var lines = File.ReadAllLines("input.txt").Select(line => line.Split(",").Select(int.Parse).ToArray()).ToArray();

var xMax = lines.Select(line => line[0]).Max() + 1;
var yMax = lines.Select(line => line[1]).Max() + 1;
var zMax = lines.Select(line => line[2]).Max() + 1;

var cube = new bool[xMax, yMax, zMax];
foreach (var line in lines)
{
    cube[line[0], line[1], line[2]] = true;
}

var positionsToFill = new List<(int, int, int)>();
for (var x = 0; x < xMax; x++)
{
    for (var y = 0; y < yMax; y++)
    {
        for (var z = 0; z < zMax; z++)
        {
            positionsToFill.Add((x, y, z));
        }
    }
}

var allCheckedPositions = new List<(int, int, int)>();
List<(int, int, int)> checkedPositions;
foreach (var position in positionsToFill)
{
    if (allCheckedPositions.Contains(position))
    {
        continue;
    }
    
    checkedPositions = [];
    FillPosition(position);
    if (!checkedPositions.Any(p => 
            position.Item1 == 0 || p.Item1 == xMax - 1 ||
            position.Item2 == 0 || p.Item2 == yMax - 1 ||
            position.Item3 == 0 || p.Item3 == zMax - 1))
    {
        foreach (var p in checkedPositions)
        {
            cube[p.Item1, p.Item2, p.Item3] = true;
        }
    }
    allCheckedPositions.AddRange(checkedPositions);
}

var previous = false;
var surface = 0;

for (var y = 0; y < yMax; y++)
{
    for (var z = 0; z < zMax; z++)
    {
        for (var x = 0; x < xMax; x++)
        {
            if (cube[x, y, z])
            {
                if (!previous)
                {
                    surface++;
                }
                previous = true;
            }
            else
            {
                if (previous)
                {
                    surface++;
                }
                previous = false;
            }
        }
        if (previous)
        {
            surface++;
        }
        previous = false;
    }
}

for (var x = 0; x < xMax; x++)
{
    for (var y = 0; y < yMax; y++)
    {
        for (var z = 0; z < zMax; z++)
        {
            if (cube[x, y, z])
            {
                if (!previous)
                {
                    surface++;
                }
                previous = true;
            }
            else
            {
                if (previous)
                {
                    surface++;
                }
                previous = false;
            }
        }
        if (previous)
        {
            surface++;
        }
        previous = false;
    }
}

for (var z = 0; z < zMax; z++)
{
    for (var x = 0; x < xMax; x++)
    {
        for (var y = 0; y < yMax; y++)
        {
            if (cube[x, y, z])
            {
                if (!previous)
                {
                    surface++;
                }
                previous = true;
            }
            else
            {
                if (previous)
                {
                    surface++;
                }
                previous = false;
            }
        }
        if (previous)
        {
            surface++;
        }
        previous = false;
    }
}

Console.WriteLine(surface);

return;

void FillPosition((int, int, int) position)
{
    if (position.Item1 < 0 || position.Item2 < 0 || position.Item3 < 0 || position.Item1 == xMax || position.Item2 == yMax || position.Item3 == zMax || checkedPositions.Contains(position) || cube[position.Item1, position.Item2, position.Item3])
    {
        return;
    }
    
    checkedPositions.Add(position);
    
    FillPosition((position.Item1 + 1, position.Item2, position.Item3));
    FillPosition((position.Item1 - 1, position.Item2, position.Item3));
    FillPosition((position.Item1, position.Item2 + 1, position.Item3));
    FillPosition((position.Item1, position.Item2 - 1, position.Item3));
    FillPosition((position.Item1, position.Item2, position.Item3 + 1));
    FillPosition((position.Item1, position.Item2, position.Item3 - 1));
}
