var streamReader = new StreamReader("input.txt");
var line = streamReader.ReadLine();
while (line == null)
{
    Console.WriteLine(line);
    line = streamReader.ReadLine();
}