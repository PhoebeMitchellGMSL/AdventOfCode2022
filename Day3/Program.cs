const int uppercaseAValue = 65;
const int lowercaseAValue = 97;

var priorityConversionDictionary = new Dictionary<char, int>();

for (var i = 1; i < 27; i++)
{
    priorityConversionDictionary.Add((char)(i + lowercaseAValue - 1), i);
    priorityConversionDictionary.Add((char)(i + uppercaseAValue - 1), i + 26);

}

// Part 1
var lines = File.ReadAllLines("input.txt");
var prioritySum = 0;
foreach (var line in lines)
{
    var compartment1 = line[..(line.Length / 2)];
    var compartment2 = line[(line.Length / 2)..];
    var duplicateItem = compartment1.First(c1 => compartment2.Any(c2 => c1 == c2));
    prioritySum += priorityConversionDictionary[duplicateItem];
}
Console.WriteLine(prioritySum);

// Part 2
var elfGroups = new List<(string, string, string)>();
for (var i = 0; i < lines.Length; i += 3)
{
    elfGroups.Add((lines[i], lines[i + 1], lines[i + 2]));
}

var badgePrioritySum = 0;
foreach (var elfGroup in elfGroups)
{
    var duplicateItem = elfGroup.Item1.First(c1 => elfGroup.Item2.Any(c2 => c1 == c2) && elfGroup.Item3.Any(c3 => c1 == c3));
    badgePrioritySum += priorityConversionDictionary[duplicateItem];
}
Console.WriteLine(badgePrioritySum);