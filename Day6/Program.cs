var input = File.ReadAllLines("input.txt")[0];

// Set marker to 4 for part 1, 14 for part 2
const int marker = /*4*/14;

for (var i = 0; i < input.Length; i++)
{
    if (input.Substring(i, marker).Distinct().Count() == marker)
    {
        Console.WriteLine(i + marker);
        break;
    }
}