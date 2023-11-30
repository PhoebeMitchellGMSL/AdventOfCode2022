var lines = File.ReadAllLines("input.txt");

var startNodes = new List<Node>();
Node? end = null;
var nodes = new List<Node>[lines.Length];
var allNodes = new List<Node>();

for (var y = 0; y < lines.Length; y++)
{
    nodes[y] = new List<Node>();
    for (var x = 0; x < lines[y].Length; x++)
    {
        var node = new Node(x, y, lines[y][x]);
        nodes[y].Add(node);
        allNodes.Add(node);
        if (lines[y][x] == 'S')
        {
            startNodes.Add(node);
        }
        if (lines[y][x] == 'E')
        {
            end = node;
        }
    }
}

foreach (var nodeList in nodes)
{
    nodeList.ForEach(node => node.FindNeighbours(nodes));
}

startNodes = allNodes.Where(node => node.Value == 'a').ToList(); // Comment for part 1
var pathLengths = new List<int>();

int i = 0;
foreach (var startNode in startNodes)
{
    var unvisitedNodes = new List<Node>(allNodes);
    foreach (var node in allNodes)
    {
        node.Reset();
    }

    end.Distance = 0;
    
    while (unvisitedNodes.Count > 0)
    {
        var node = unvisitedNodes.MinBy(n => n.Distance);
        if (node == startNode)
        {
            break;
        }
        node.EvaluateNeighbours();
        unvisitedNodes.Remove(node);
    }

    var activeNode = startNode;
    var pathLength = 0;
    while (activeNode != end)
    {
        activeNode = activeNode.Previous;

        if (activeNode == null)
        {
            break;
        }
        
        pathLength++;
    }

    if (activeNode == end)
    {
        pathLengths.Add(pathLength);
    }
}

Console.WriteLine(pathLengths.Min());

internal class Node
{
    private readonly int xPos;
    private readonly int yPos;
    private readonly List<Node> neighbours = new();
    private bool visited;
    
    public Node(int xPos, int yPos, char value)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.Value = value == 'S' ? 'a' : value;
        this.Value = value == 'E' ? 'z' : this.Value;
    }
    
    public int Distance { get; set; } = int.MaxValue;
    public Node? Previous { get; private set; }
    public char Value { get; }

    public void FindNeighbours(List<Node>[] nodes)
    {
        for (var y = yPos - 1; y <= yPos + 1; y++)
        {
            for (var x = xPos - 1; x <= xPos + 1; x++)
            {
                if (x == xPos && y == yPos || x != xPos && y != yPos || x < 0 || x >= nodes[0].Count || y < 0 || y >= nodes.Length || this.Value > nodes[y][x].Value + 1)
                {
                    continue;
                }
                
                neighbours.Add(nodes[y][x]);
            }
        }
    }

    public void EvaluateNeighbours()
    {
        foreach (var node in neighbours.Where(node => !node.visited && Distance + 1 < node.Distance))
        {
            node.Previous = this;
            node.Distance = Distance + 1;
        }
        visited = true;
    }

    public void Reset()
    {
        visited = false;
        Distance = int.MaxValue;
        Previous = null;
    }
}