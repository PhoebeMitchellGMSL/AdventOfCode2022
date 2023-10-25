var lines = File.ReadAllLines("input.txt");

var rootDirectory = new Directory();
var activeDirectory = rootDirectory;

var directories = new List<Directory>();

foreach (var line in lines)
{
    var splitLine = line.Split(" ");
    if (splitLine[0] == "$")
    {
        if (splitLine[1] == "cd")
        {
            switch (splitLine[2])
            {
                case "..":
                    activeDirectory = activeDirectory.ParentDirectory;
                    break;
                case "/":
                    activeDirectory = rootDirectory;
                    break;
                default:
                    activeDirectory.Directories.TryAdd(splitLine[2], new Directory(activeDirectory));
                    activeDirectory = activeDirectory.Directories[splitLine[2]];
                    directories.Add(activeDirectory);
                    break;
            }
        }
    }
    else if (int.TryParse(splitLine[0], out var size))
    {
        activeDirectory.Files.TryAdd(splitLine[1], 0);
        activeDirectory.Files[splitLine[1]] += size;
    }
}

var smallestDirectories = directories.Where(directory => directory.CalculateSize() <= 100000);
Console.WriteLine(smallestDirectories.Sum(directory => directory.CalculateSize()));

class Directory
{
    public Directory? ParentDirectory { get; }
    public Dictionary<string, Directory> Directories { get; } = new ();
    public Dictionary<string, int> Files { get; } = new();

    public Directory()
    {
    }

    public Directory(Directory parentDirectory)
    {
        this.ParentDirectory = parentDirectory;
    }

    public int CalculateSize()
    {
        return Files.Values.Sum() + Directories.Values.Sum(directory => directory.CalculateSize());
    }
}