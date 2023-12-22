using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt");
// var lines = new List<string>
// {
//     "Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
//     "Sensor at x=9, y=16: closest beacon is at x=10, y=16",
//     "Sensor at x=13, y=2: closest beacon is at x=15, y=3",
//     "Sensor at x=12, y=14: closest beacon is at x=10, y=16",
//     "Sensor at x=10, y=20: closest beacon is at x=10, y=16",
//     "Sensor at x=14, y=17: closest beacon is at x=10, y=16",
//     "Sensor at x=8, y=7: closest beacon is at x=2, y=10",
//     "Sensor at x=2, y=0: closest beacon is at x=2, y=10",
//     "Sensor at x=0, y=11: closest beacon is at x=2, y=10",
//     "Sensor at x=20, y=14: closest beacon is at x=25, y=17",
//     "Sensor at x=17, y=20: closest beacon is at x=21, y=22",
//     "Sensor at x=16, y=7: closest beacon is at x=15, y=3",
//     "Sensor at x=14, y=3: closest beacon is at x=15, y=3",
//     "Sensor at x=20, y=1: closest beacon is at x=15, y=3"
// };

var minPos = new Vector2Int(int.MaxValue, int.MaxValue);
var maxPos = new Vector2Int(int.MinValue, int.MinValue);

var sensorPositions = new List<Vector2Int>();
var beaconPositions = new List<Vector2Int>();
var manhattanDistances = new List<int>();

foreach (var line in lines)
{
    var coordinates = Regex.Matches(line, @"-?\b\d+\b");
    var sensorPosition = new Vector2Int(int.Parse(coordinates[0].Value), int.Parse(coordinates[1].Value));
    var beaconPosition = new Vector2Int(int.Parse(coordinates[2].Value), int.Parse(coordinates[3].Value));

    sensorPositions.Add(sensorPosition);
    beaconPositions.Add(beaconPosition);
    manhattanDistances.Add((beaconPosition - sensorPosition).Manhattan);
    
    minPos.X = Math.Min(Math.Min(Math.Min(minPos.X, sensorPosition.X), beaconPosition.X), 0);
    minPos.Y = Math.Min(Math.Min(Math.Min(minPos.Y, sensorPosition.Y), beaconPosition.Y), 0);
    maxPos.X = Math.Max(Math.Max(maxPos.X, sensorPosition.X), beaconPosition.X);
    maxPos.Y = Math.Max(Math.Max(maxPos.Y, sensorPosition.Y), beaconPosition.Y);
}

var invalidCount = 0;
var sensorIndices = sensorPositions.Select((s, i) => i).ToArray();
for (var x = minPos.X - manhattanDistances.Max(); x <= maxPos.X + manhattanDistances.Max(); x++)
{
    var currentPosition = new Vector2Int(x, 2000000);
    if (beaconPositions.Contains(currentPosition) || sensorPositions.Contains(currentPosition))
    {
        continue;
    }
    
    if (sensorIndices.Any(i => (currentPosition - sensorPositions[i]).Manhattan <= manhattanDistances[i]))
    {
        invalidCount++;
    }
}
Console.WriteLine(invalidCount);

// Part 2
var boundaryPositions = new List<Vector2Int>();
for (var i = 0; i < sensorPositions.Count; i++)
{
    var top = sensorPositions[i] + new Vector2Int(0, manhattanDistances[i] + 1);
    var bottom = sensorPositions[i] - new Vector2Int(0, manhattanDistances[i] + 1);
    var left = sensorPositions[i] - new Vector2Int(manhattanDistances[i] + 1, 0);
    var right = sensorPositions[i] + new Vector2Int(manhattanDistances[i] + 1, 0);

    var p = top;
    while (p != right)
    {
        boundaryPositions.Add(p);
        p.X += 1;
        p.Y -= 1;
    }
    while (p != bottom)
    {
        boundaryPositions.Add(p);
        p.X -= 1;
        p.Y -= 1;
    }
    while (p != left)
    {
        boundaryPositions.Add(p);
        p.X -= 1;
        p.Y += 1;
    }
    while (p != top)
    {
        boundaryPositions.Add(p);
        p.X += 1;
        p.Y += 1;
    }
}

const int boundary = 4000000;

foreach (var position in boundaryPositions)
{
    if (position.X < 0 || position.X > boundary || position.Y < 0 || position.Y > boundary)
    {
        continue;
    }

    if (sensorIndices.All(i => (position - sensorPositions[i]).Manhattan > manhattanDistances[i]))
    {
        Console.WriteLine((decimal)position.X * 4000000 + position.Y);
        break;
    }
}

internal struct Vector2Int(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set;  } = y;

    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new (a.X + b.X, a.Y + b.Y);
    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new (a.X - b.X, a.Y - b.Y);
    public static Vector2Int operator /(Vector2Int a, int b) => new(a.X / b, a.Y / b);
    public static bool operator ==(Vector2Int a, Vector2Int b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Vector2Int a, Vector2Int b) => a.X != b.X || a.Y != b.Y;

    public int Manhattan => Math.Abs(X) + Math.Abs(Y);
}