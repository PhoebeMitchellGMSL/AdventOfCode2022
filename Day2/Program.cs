const int winScore = 6;
const int drawScore = 3;

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

var drawingCombinations = new[]
{
    ('A', 'X'),
    ('B', 'Y'),
    ('C', 'Z')
};

var score = 0;
using (var streamReader = new StreamReader("input.txt")) {
    while (streamReader.ReadLine() is { } line)
    {
        if (winningCombinations.Contains((line[0], line[2])))
        {
            score += winScore;
        }

        if (drawingCombinations.Contains((line[0], line[2])))
        {
            score += drawScore;
        }

        score += scores[line[2]];
    }
}
Console.WriteLine(score);