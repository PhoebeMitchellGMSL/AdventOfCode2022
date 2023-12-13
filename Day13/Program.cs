using Day13;

var lines = File.ReadAllLines("input.txt").ToList();

// Part 1
var index = 0;
var indicesCount = 0;
for (var i = 0; i < lines.Count; i += 3)
{
    index++;
    if (Day13Solution.CalculateDay13(lines[i], lines[i + 1]))
    {
        indicesCount += index;
    }
}
Console.WriteLine(indicesCount);

// Part 2
lines.Add("[[2]]");
lines.Add("[[6]]");
lines.RemoveAll(line => line == "");
var orderedLines = lines.OrderBy(line => line, new PacketComparer()).ToList();

var d1 = orderedLines.IndexOf("[[2]]") + 1;
var d2 = orderedLines.IndexOf("[[6]]") + 1;

Console.WriteLine(d1 * d2);

internal class PacketComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        return Day13Solution.CalculateDay13(x, y) ? -1 : 1;
    }
}