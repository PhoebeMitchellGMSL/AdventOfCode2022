var lines = File.ReadAllLines("input.txt");

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
var pressures = new List<int>();
Evaluate("AA", 30, 0, [..interestedValves]);
Console.Write(pressures.Max());

return;

void Evaluate(string id, int time, int pressure, List<string> remainingValves)
{
    if (time <= 0)
    {
        pressures.Add(pressure);
        return;
    }

    if (valveFlowRates[id] > 0)
    {
        pressure += valveFlowRates[id] * (time - 1);
        time--;
    }

    if (time <= 0)
    {
        pressures.Add(pressure);
        return;
    }

    remainingValves.Remove(id);
    
    if (remainingValves.Count == 0)
    {
        pressures.Add(pressure);
    }
    
    foreach (var v in remainingValves)
    {
        Evaluate(v, time - matrix[id][v], pressure, [..remainingValves]);
    }
}