const int uppercaseAValue = 65;
const int lowercaseAValue = 97;

var priorityConversionDictionary = new Dictionary<char, int>();

for (var i = 1; i < 27; i++)
{
    priorityConversionDictionary.Add((char)(i + lowercaseAValue - 1), i);
    priorityConversionDictionary.Add((char)(i + uppercaseAValue - 1), i + 26);

}

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