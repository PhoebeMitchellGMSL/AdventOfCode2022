var lines = File.ReadAllLines("input.txt");

var directorySizes = new Dictionary<string, int>();
var directoryStack = new Stack<string>();

directoryStack.Push("Main");
directorySizes.Add("Main", 0);

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
                    GoUpDirectory();
                    break;
                case "/":
                    while (directoryStack.Count != 1)
                    {
                        GoUpDirectory();
                    }
                    break;
                default:
                    directoryStack.Push(splitLine[2]);
                    directorySizes.TryAdd(splitLine[2], 0);
                    break;
            }
        }
    }
    else if (int.TryParse(splitLine[0], out var size))
    {
        directorySizes[directoryStack.Peek()] += size;
    }
}

var sum = 0;
foreach (var line in lines)
{
    var a = line.Split(" ");
    if (int.TryParse(a[0], out var b))
    {
        sum += b;
    }
}
Console.WriteLine(sum);
Console.WriteLine(directorySizes["Main"]);

var directories = directorySizes.Where(pair => pair.Value <= 100000);
Console.WriteLine(directories.Sum(pair => pair.Value));

return;

void GoUpDirectory()
{
    var previousDirectory = directoryStack.Pop();
    directorySizes[directoryStack.Peek()] += directorySizes[previousDirectory];
}

class Directory
{
    public Directory ParentDirectory { get; }
    public List<Directory> Directories { get;  }= new ();
    public Dictionary<string, int> Files { get; } = new();

    public Directory(Directory parentDirectory)
    {
        this.ParentDirectory = parentDirectory;
    }
}