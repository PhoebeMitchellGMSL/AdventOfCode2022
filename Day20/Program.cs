﻿var input = File.ReadAllLines("input.txt").Select(i => long.Parse(i) * 811589153).ToList();
var indices = input.Select((_, i) => i).ToList();

for (var j = 0; j < 10; j++)
{
    for (var i = 0; i < input.Count; i++)
    {
        var index = indices.IndexOf(i);
        var move = index + input[i];
        indices.Remove(i);

        if (move == 0)
        {
            indices.Add(i);
            continue;
        }

        indices.Insert(Mod(move, indices.Count), i);
    }
}

foreach (var i in indices)
{
    Console.Write($"{input[i]} ");
}
Console.WriteLine();

var o = new List<long>();
o.AddRange(indices.Select(i => input[i]));
var zeroIndex = o.IndexOf(0);
Console.WriteLine(o[(1000 + zeroIndex) % input.Count] + o[(2000 + zeroIndex) % input.Count] + o[(3000 + zeroIndex) % input.Count]);

return;

int Mod(long a, long b)
{
    return (int)(a - b * (long)Math.Floor(a / (double)b));
}