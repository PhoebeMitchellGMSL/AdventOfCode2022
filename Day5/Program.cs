﻿var lines = File.ReadAllLines("input.txt");
var stacks = new List<char>[10];
var startIndex = 0;

for (var i = 0; i < lines.Length; i++)
{
    for (var j = 0; j < lines[i].Length; j++)
    {
        if (char.IsDigit(lines[i][j]) && int.TryParse(lines[i][j].ToString(), out var index))
        {
            stacks[index] = new();
            for (var k = i - 1; k >= 0; k--)
            {
                if (lines[k][j] != ' ')
                {
                    stacks[index].Add(lines[k][j]);
                }
            }
        }
    }

    if (lines[i] == "")
    {
        startIndex = i + 1;
        break;
    }
}

foreach (var stack in stacks[1..^1])
{
    Console.WriteLine(stack.Count);
    foreach (var a in stack)
    {
        Console.Write(a);
    }

    Console.WriteLine();
}

foreach (var line in lines[startIndex..])
{
    var instructions = line.Split(" ").Where(s => int.TryParse(s, out _)).Select(int.Parse).ToArray();
    for (var i = 0; i < instructions[0]; i++)
    {
        if (stacks[instructions[1]].Count > 0)
        {
            stacks[instructions[2]].Add(stacks[instructions[1]][^1]);
            stacks[instructions[1]].RemoveAt(stacks[instructions[1]].Count - 1);
        }
    }
}

foreach (var stack in stacks[1..^1])
{
    foreach (var a in stack)
    {
        Console.Write(a);
    }

    Console.WriteLine();
}