using System.Numerics;

var lines = File.ReadAllLines("input.txt");
// var lines = new string[]
// {
//     "0,0 -> 10,0",
//     "10,0 -> 10,10" 
// };
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
        maxPosition.X = Math.Max(sequences[i][j].X, maxPosition.X);
        maxPosition.Y = Math.Max(sequences[i][j].Y, maxPosition.Y);
    }
}

var grid = new char[(int)maxPosition.X + 1, (int)maxPosition.Y + 1];
for (var i = 0; i < maxPosition.X + 1; i++)
{
    for (var j = 0; j < maxPosition.Y + 1; j++)
    {
        grid[i, j] = '.';
    }
}

foreach (var sequence in sequences)
{
    for (var i = 0; i < sequence.Length - 1; i++)
    {
        var difference = sequence[i + 1] - sequence[i];
        var normalisedDifference = new Vector2(Math.Sign(difference.X), Math.Sign(difference.Y));
        var startPosition = sequence[i];
        grid[(int)startPosition.X, (int)startPosition.Y] = '#';
        while (startPosition != sequence[i + 1])
        {
            startPosition += normalisedDifference;
            grid[(int)startPosition.X, (int)startPosition.Y] = '#';
        }
    }
}

for (var j = 0; j < maxPosition.Y + 1; j++)
{
    for (var i = 0; i < maxPosition.X + 1; i++)
    {
        Console.Write(grid[i,j]);
    }
    Console.WriteLine();
}