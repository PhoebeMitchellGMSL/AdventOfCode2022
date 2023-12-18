using System.Numerics;

var lines = File.ReadAllLines("input.txt");
// var lines = new string[]
// {
//     "0,10 -> 10,10",
//     "5,5 -> 5,10" 
// };
var minPosition = new Vector2(0, 0);
var maxPosition = new Vector2();

var sequences = new Vector2[lines.Length][];

for (var i = 0; i < sequences.Length; i++)
{
    var vectors = lines[i].Split(" -> ");
    sequences[i] = new Vector2[vectors.Length];
    for (var j = 0; j < vectors.Length; j++)
    {
        var vectorValues = vectors[j].Split(",");
        sequences[i][j] = new Vector2(int.Parse(vectorValues[0]), int.Parse(vectorValues[1]));
        minPosition.X = Math.Min(sequences[i][j].X, minPosition.X);
        minPosition.Y = Math.Min(sequences[i][j].Y, minPosition.Y);
        maxPosition.X = Math.Max(sequences[i][j].X, maxPosition.X);
        maxPosition.Y = Math.Max(sequences[i][j].Y, maxPosition.Y);
    }
}

maxPosition.Y += 1;

var grid = new Dictionary<Vector2, char>();

foreach (var sequence in sequences)
{
    for (var i = 0; i < sequence.Length - 1; i++)
    {
        var difference = sequence[i + 1] - sequence[i];
        var normalisedDifference = new Vector2(Math.Sign(difference.X), Math.Sign(difference.Y));
        var startPosition = sequence[i];
        grid.TryAdd(startPosition, '#');
        while (startPosition != sequence[i + 1])
        {
            startPosition += normalisedDifference;
            grid.TryAdd(startPosition, '#');
        }
    }
}

while (true)
{
    var currentPosition = new Vector2(500, 0);
    
    if (grid.TryGetValue(currentPosition, out _))
    {
        break;
    }
    
    while (true)
    {
        if (currentPosition.Y + 1 > maxPosition.Y)
        {
        }
        else if (!grid.TryGetValue(currentPosition + new Vector2(0, 1), out _))
        {
            currentPosition += new Vector2(0, 1);
            continue;
        }
        else if (!grid.TryGetValue(currentPosition + new Vector2(-1, 1), out _))
        {
            currentPosition += new Vector2(-1, 1);
            continue;
        }
        else if (!grid.TryGetValue(currentPosition + new Vector2(1, 1), out _))
        {
            currentPosition += new Vector2(1, 1);
            continue;
        }
        
        grid.Add(currentPosition, '+');
        
        break;
    }
    
    minPosition.X = Math.Min(minPosition.X, currentPosition.X);
    maxPosition.X = Math.Max(maxPosition.X, currentPosition.X);
}

for (var j = 0; j < maxPosition.Y + 1; j++)
{
    for (var i = minPosition.X; i < maxPosition.X + 1; i++)
    {
        Console.Write(grid.GetValueOrDefault(new Vector2(i, j), '.'));
    }
    Console.WriteLine();
}

Console.WriteLine(grid.Values.Count(value => value == '+'));