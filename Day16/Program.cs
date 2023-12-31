﻿var lines = File.ReadAllLines("input.txt");

var valveFlowRates = new Dictionary<string, int>();
var valveConnections = new Dictionary<string, string[]>();

foreach (var line in lines)
{
    var splitLine = line.Split();
    valveFlowRates.Add(splitLine[1], int.Parse(splitLine[4].Split("=")[1][..^1]));
    valveConnections.Add(splitLine[1], splitLine[9..].Select(id => id.Replace(",", "")).ToArray());
}

var matrix = new Dictionary<string, Dictionary<string, int>>();
foreach (var valve in valveFlowRates.Keys)
{
    matrix.Add(valve, new Dictionary<string, int>());
    foreach (var v in valveFlowRates.Keys)
    {
        matrix[valve].Add(v, int.MaxValue);
    }
}

foreach (var valve in valveFlowRates.Keys)
{
    matrix[valve][valve] = 0;
    foreach (var neighbour in valveConnections[valve])
    {
        matrix[valve][neighbour] = 1;
    }
}

foreach (var x in valveFlowRates.Keys)
{
    foreach (var y in valveFlowRates.Keys)
    {
        foreach (var z in valveFlowRates.Keys)
        {
            var comparison = matrix[y][x] == int.MaxValue || matrix[x][z] == int.MaxValue ? int.MaxValue : matrix[y][x] + matrix[x][z];
            matrix[y][z] = Math.Min(matrix[y][z], comparison);
        }
    }
}

var interestedValves = valveFlowRates.Where(pair => pair.Value != 0 || pair.Key == "AA").Select(pair => pair.Key);

var pressuresAndValvesDict = new Dictionary<HashSet<string>, int>(HashSet<string>.CreateSetComparer());

Evaluate("AA", 26, 0, [..interestedValves], []);
// Console.WriteLine(pressuresAndValves.Max());

var results = pressuresAndValvesDict.OrderByDescending(x => x.Value).ToList();

var best = (0, 0, new HashSet<string>(), new HashSet<string>());

foreach (var result in results)
{
    foreach (var r in results)
    {
        if (result.Key.SetEquals(r.Key))
        {
            continue;
        }

        if (result.Value + r.Value <= best.Item1 + best.Item2)
        {
            continue;
        }
        
        if (result.Key.All(x => r.Key.All(y => x != y)))
        {
            best = (result.Value, r.Value, result.Key, r.Key);
        }
    }
}

foreach (var valve in best.Item3)
{
    Console.Write($"{valve} ");
}
Console.WriteLine();
foreach (var valve in best.Item4)
{
    Console.Write($"{valve} ");
}
Console.WriteLine();
Console.WriteLine($"{best.Item1} + {best.Item2} = {best.Item1 + best.Item2}");
return;

void AddPressure(int pressure, HashSet<string> visited)
{
    if (visited.Count == 1)
    {
        return;
    }
    
    if (pressuresAndValvesDict.TryGetValue(visited, out var p))
    {
        if (pressure > p)
        {
            pressuresAndValvesDict[visited] = pressure;
        }
    }
    else
    {
        pressuresAndValvesDict.Add([..visited], p);
    }
}

void Evaluate(string id, int time, int pressure, List<string> remainingValves, HashSet<string> visited)
{
    if (time <= 0)
    {
        AddPressure(pressure, visited);
        return;
    }

    if (id != "AA")
    {
        visited.Add(id);
    }
    
    if (valveFlowRates[id] > 0)
    {
        pressure += valveFlowRates[id] * (time - 1);
        time--;
    }

    if (time <= 0)
    {
        AddPressure(pressure, visited);
        return;
    }

    remainingValves.Remove(id);
    
    if (remainingValves.Count == 0)
    {
        AddPressure(pressure, visited);
    }
    
    foreach (var v in remainingValves)
    {
        if (id != "AA")
        {
            AddPressure(pressure, visited);
        }

        Evaluate(v, time - matrix[id][v], pressure, [..remainingValves], [..visited]);
    }
}