var lines = File.ReadAllLines("input.txt");

var qualities = new List<int>();

foreach (var line in lines)
{
    var oreRobotBlueprint = new Blueprint();
    var clayRobotBlueprint = new Blueprint();
    var obsidianRobotBlueprint = new Blueprint();
    var geodeRobotBlueprint = new Blueprint();
    
    var splitLine = line.Split();
    oreRobotBlueprint.ore = int.Parse(splitLine[6]);
    clayRobotBlueprint.ore = int.Parse(splitLine[12]);
    obsidianRobotBlueprint.ore = int.Parse(splitLine[18]);
    obsidianRobotBlueprint.clay = int.Parse(splitLine[21]);
    geodeRobotBlueprint.ore = int.Parse(splitLine[27]);
    geodeRobotBlueprint.obsidian = int.Parse(splitLine[30]);

    var pathQualities = new List<int>();
    var datas = new Queue<Data>();
    datas.Enqueue(new Data
    {
        oreRobots = 1,
        oreRobotBlueprint = oreRobotBlueprint,
        clayRobotBlueprint = clayRobotBlueprint,
        obsidianRobotBlueprint = obsidianRobotBlueprint,
        geodeRobotBlueprint = geodeRobotBlueprint,
        pathQualities = pathQualities
    });
    
    while (datas.TryDequeue(out var d))
    {
        CalculatePath(d, datas);
    }

    qualities.Add(pathQualities.Max());
}

var p = qualities.Aggregate(1, (current, q) => current * q);

Console.WriteLine(p);

void CalculatePath(
    Data data,
    Queue<Data> datas)
{
    if (datas.Count > 100000)
    {
        return;
    }
    
    for (var i = data.startMinute; i < 32; i++)
    {
        if (!data.ignoreGeode && data.obsidian >= data.geodeRobotBlueprint.obsidian && data.ore >= data.geodeRobotBlueprint.ore)
        {
            var d = data;
            d.ignoreGeode = true;
            d.startMinute = i;
            datas.Enqueue(d);
            data.obsidian -= data.geodeRobotBlueprint.obsidian;
            data.ore -= data.geodeRobotBlueprint.ore;
            data.geodeRobotsBuffer++;
        }
        else if (!data.ignoreObsidian && data.clay >= data.obsidianRobotBlueprint.clay && data.ore >= data.obsidianRobotBlueprint.ore)
        {   
            var d = data;
            d.ignoreObsidian = true;
            d.startMinute = i;
            datas.Enqueue(d);
            data.clay -= data.obsidianRobotBlueprint.clay;
            data.ore -= data.obsidianRobotBlueprint.ore;
            data.obsidianRobotsBuffer++;
        }
        else if (!data.ignoreClay && data.ore >= data.clayRobotBlueprint.ore)
        {
            var d = data;
            d.ignoreClay = true;
            d.startMinute = i;
            datas.Enqueue(d);
            data.ore -= data.clayRobotBlueprint.ore;
            data.clayRobotsBuffer++;
        }
        else if (!data.ignoreOre && data.ore >= data.oreRobotBlueprint.ore)
        {
            var d = data;
            d.ignoreOre = true;
            d.startMinute = i;
            datas.Enqueue(d);
            data.ore -= data.oreRobotBlueprint.ore;
            data.oreRobotsBuffer++;
        }

        data.ignoreGeode = false;
        data.ignoreObsidian = false;
        data.ignoreClay = false;
        data.ignoreOre = false;
        
        data.ore += data.oreRobots;
        data.clay += data.clayRobots;
        data.obsidian += data.obsidianRobots;
        data.geode += data.geodeRobots;

        data.oreRobots += data.oreRobotsBuffer;
        data.clayRobots += data.clayRobotsBuffer;
        data.obsidianRobots += data.obsidianRobotsBuffer;
        data.geodeRobots += data.geodeRobotsBuffer;

        data.oreRobotsBuffer = 0;
        data.clayRobotsBuffer = 0;
        data.obsidianRobotsBuffer = 0;
        data.geodeRobotsBuffer = 0;
    }
    
    data.pathQualities.Add(data.geode);
}

internal struct Blueprint
{
    public int ore;
    public int clay;
    public int obsidian;
}

internal struct Data(int id)
{
    public int startMinute;
    
    public int ore = 0;
    public int clay = 0;
    public int obsidian = 0;
    public int geode = 0;
    
    public int oreRobots = 1;
    public int clayRobots = 0;
    public int obsidianRobots = 0;
    public int geodeRobots = 0;

    public int oreRobotsBuffer = 0;
    public int clayRobotsBuffer = 0;
    public int obsidianRobotsBuffer = 0;
    public int geodeRobotsBuffer = 0;

    public Blueprint oreRobotBlueprint;
    public Blueprint clayRobotBlueprint;
    public Blueprint obsidianRobotBlueprint;
    public Blueprint geodeRobotBlueprint;
    public List<int> pathQualities;
    public bool ignoreGeode = false;
    public bool ignoreObsidian = false;
    public bool ignoreClay = false;
    public bool ignoreOre = false;
}