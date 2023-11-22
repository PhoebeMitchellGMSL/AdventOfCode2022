var input = File.ReadAllText("input.txt");
var monkeyDatas = input.Split("\r\n\r\n");
var monkeys = new List<Monkey>();
long testProduct = 1;

foreach (var monkeyData in monkeyDatas)
{
    var monkeyLines = monkeyData.Split("\n");
    var items = monkeyLines[1].Split(": ").ToArray()[1].Split(", ").Select(long.Parse).ToList();
    var operationArray = monkeyLines[2].Trim().Split(" ")[4..];
    Func<long, long> operation = operationArray[0] == "*"
        ? item => item * (operationArray[1] == "old" ? item : long.Parse(operationArray[1]))
        : item => item + (operationArray[1] == "old" ? item : long.Parse(operationArray[1]));
    var test = int.Parse(monkeyLines[3].Trim().Split(" ")[3]);
    testProduct *= test;
    var testTrue = int.Parse(monkeyLines[4].Trim().Split(" ")[5]);
    var testFalse = int.Parse(monkeyLines[5].Trim().Split(" ")[5]);
    monkeys.Add(new Monkey(items, operation, test, testTrue, testFalse, monkeys));
}

for (var i = 0; i < 10000; i++)
{
    foreach (var monkey in monkeys)
    {
        monkey.Inspect(testProduct);
    }
}

var orderedMonkeys = monkeys.OrderBy(monkey => monkey.InspectionCount).ToArray();
Console.WriteLine(orderedMonkeys[^1].InspectionCount * orderedMonkeys[^2].InspectionCount);

internal class Monkey(IList<long> items, Func<long, long> operation, int test, int testTrue, int testFalse, IReadOnlyList<Monkey> monkeys)
{
    public long InspectionCount { get; private set; }
    
    public void Inspect(long testProduct)
    {
        for (var i = 0; i < items.Count; i++)
        {
            this.InspectionCount++;
            items[i] = operation.Invoke(items[i]) % testProduct;
            // items[i] /= 3;
            monkeys[items[i] % test == 0 ? testTrue : testFalse].Catch(items[i]);
        }
        items.Clear();
    }

    private void Catch(long item)
    {
        items.Add(item);
    }
}