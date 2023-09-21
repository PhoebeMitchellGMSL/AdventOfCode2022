var elfCalorieList = new List<int>
{
    new ()
};
using (var streamReader = new StreamReader("input.txt"))
{
    while (streamReader.ReadLine() is { } line) {
        if (line == "")
        {
            elfCalorieList.Add(new());
        }
        else
        {
            elfCalorieList[^1] += int.Parse(line);
        } 
    }
}

var elfWithMostCalories = elfCalorieList.IndexOf(elfCalorieList.Max());
Console.WriteLine($"Elf {elfWithMostCalories + 1} has the most calories: {elfCalorieList[elfWithMostCalories]}.");