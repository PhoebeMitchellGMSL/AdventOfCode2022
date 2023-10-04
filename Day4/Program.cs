var lines = File.ReadAllLines("input.txt");
var a = lines.Select(line => line.Split(",").Select(section => section.Split("-").Select(s => int.Parse(s)).ToArray())
                .ToArray()).Count(line => line[0][0] >= line[1][0] && line[0][1] <= line[1][1] || line[1][0] >= line[0][0] && line[1][1] <= line[0][1]);

Console.WriteLine(a);