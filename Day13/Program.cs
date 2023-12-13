using System.Text.Json;
using System.Text.Json.Serialization;
using Day13;
using Newtonsoft.Json;

var lines = File.ReadAllLines("input.txt");

var index = 0;
var indicesCount = 0;
// for (var i = 0; i < lines.Length; i += 3)
// {
    dynamic? json = JsonConvert.DeserializeObject(lines[0]);
    // foreach (var a in json)
    // {
    //     Console.WriteLine(json);
    // }
    
    Console.WriteLine(json[1]);
    // index++;
    // Console.WriteLine(lines[i]);
    // Console.WriteLine(lines[i + 1]);
    // if (Day13Solution.CalculateDay13(lines[i], lines[i + 1]))
    // {
    //     Console.WriteLine(true);
    //     indicesCount += index;
    // }
    // else
    // {
    //     Console.WriteLine(false);
    // }
    // Console.WriteLine();
// }
Console.WriteLine(indicesCount);
