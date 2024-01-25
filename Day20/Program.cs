var input = File.ReadAllLines("input.txt").Select((s, i) => new Position { Value = int.Parse(s)}).ToList();
var output = new List<Position>(input);

foreach (var position in input)
{
    var index = output.IndexOf(position);
    var next = index + position.Value;
    
    if (next == 0)
    {
        output.Add(position);
        output.RemoveAt(index);
    }
    
    if (next > 0 && next < input.Count)
    {
        output.RemoveAt(index);
        output.Insert(Mod(index + position.Value, input.Count), position);
    }
    
    if (next < 0)
    {
        output.Insert(Mod(index + position.Value, input.Count), position);
        output.RemoveAt(index);
    }

    if (next >= input.Count)
    {
        output.RemoveAt(index);
        output.Insert(Mod(index + position.Value + 1, input.Count), position);
    }
    
    // var a = output.Select(p => p.Value).ToList();
    // foreach (var b in a)
    // {
    //     Console.Write($"{b} ");
    // }
    // Console.WriteLine();
}

var o = output.Select(p => p.Value).ToList();
foreach (var i in o)
{
    Console.Write($"{i} ");
}
Console.WriteLine();

var zeroIndex = o.IndexOf(0);
Console.WriteLine(o[(1000 + zeroIndex) % input.Count] + o[(2000 + zeroIndex) % input.Count] + o[(3000 + zeroIndex) % input.Count]);

return;

int Mod(int a, int b)
{
    return a - b * (int)Math.Floor(a / (float)b);
}

internal class Position
{
    public int Value;
}