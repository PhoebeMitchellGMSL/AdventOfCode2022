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

var elfWithMostCalories = 0;
for (var i = 0; i < elfCalorieList.Count; i++)
{
    if (elfCalorieList[i] > elfCalorieList[elfWithMostCalories])
    {
        elfWithMostCalories = i;
    }
}
Console.WriteLine($"Elf {elfWithMostCalories + 1} has the most calories: {elfCalorieList[elfWithMostCalories]}.");