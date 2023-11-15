var lines = File.ReadLines("input.txt").ToArray();

// Part 1
var xCounts = new List<int>
{
    1
};

foreach (var line in lines)
{
    var splitLine = line.Split(" ");
    switch (splitLine[0])
    {
        case "noop":
            xCounts.Add(xCounts[^1]);
            break;
        case "addx":
            xCounts.Add(xCounts[^1]);
            xCounts.Add(xCounts[^1] + int.Parse(splitLine[1]));
            break;
    }
}

var signalStrengthSum = 0;
for (var i = 20; i <= 220; i += 40)
{
    signalStrengthSum += xCounts[i - 1] * i;
}
Console.WriteLine(signalStrengthSum);

// Part 2
var crt = new string[xCounts.Count];
for (var i = 0; i < xCounts.Count; i++)
{
    var x = xCounts[i];
    var rowIndex = i % 40;
    crt[i] = rowIndex == x || rowIndex == x - 1 || rowIndex == x + 1 ? "#" : ".";
}

for (var i = 0; i < crt.Length; i++)
{
    if (i % 40 == 0 && i != 0)
    {
        Console.WriteLine();
    }
    Console.Write(crt[i]);
}