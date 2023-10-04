var lines = File.ReadAllLines("input.txt");
var parsedLines = lines.Select(line => line.Split(",").Select(section => section.Split("-").Select(int.Parse).ToArray()).ToArray()).ToArray();

// Part 1
var part1 = parsedLines.Select(line => line.OrderBy(value => value[1] - value[0]).ToArray()).Count(line => line[0][0] >= line[1][0] && line[0][1] <= line[1][1]);
Console.WriteLine(part1);

// Part 2
var part2 = parsedLines.Select(line => line.OrderBy(value => value[0]).ToArray()).Count(line => line[0][1] >= line[1][0]);
Console.WriteLine(part2);