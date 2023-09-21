const int winScore = 6;

var scores = new Dictionary<char, int>()
{
    { 'X', 1 },
    { 'Y', 2 },
    { 'Z', 3 }
};

var winningCombinations = new[]
{
    ('A', 'Y'),
    ('B', 'Z'),
    ('C', 'X')
};

var score = 0;
using (var streamReader = new StreamReader("input.txt")) {
    while (streamReader.ReadLine() is { } line)
    {
        if (winningCombinations.Contains((line[0], line[2])))
        {
            score += winScore;
        }

        score += scores[line[2]];
    }
}
Console.WriteLine(score);