using System.Numerics;

var lines = File.ReadAllLines("input.txt");

const int ropeLength = 10;
var positions = new Vector2[ropeLength];

var tailPositions = new HashSet<Vector2>()
{
    Vector2.Zero
};

var motionDictionary = new Dictionary<string, Vector2>()
{
    { "R", new Vector2(1, 0) },
    { "L", new Vector2(-1, 0) },
    { "U", new Vector2(0, 1) },
    { "D", new Vector2(0, -1) }
};

foreach (var line in lines)
{
    var motion = line.Split(' ');
    var direction = motionDictionary[motion[0]];
    var stepCount = int.Parse(motion[1]);
    for (var i = 0; i < stepCount; i++)
    {
        positions[0] += direction;

        for (var j = 1; j < positions.Length; j++)
        {
            var difference = positions[j - 1] - positions[j];
            if (difference.Length() >= 2)
            {
                positions[j] += new Vector2(
                    Math.Abs(difference.X) > 0 ? Math.Sign(difference.X) : 0,
                    Math.Abs(difference.Y) > 0 ? Math.Sign(difference.Y) : 0);
            }
        }

        tailPositions.Add(positions[^1]);
    }
}

Console.WriteLine(tailPositions.Count);