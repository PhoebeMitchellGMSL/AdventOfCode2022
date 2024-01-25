var input = File.ReadAllLines("input.txt").Select((s, i) => (i, int.Parse(s))).ToList();


var zeroIndex2 = File.ReadAllLines("input.txt").Select(int.Parse).ToList().IndexOf(0);

for (var i = 0; i < input.Count; i++)
{
    var pair = input[i];
    
    var oldIndex = input[i].Item1;
    var negative = pair.Item1 + pair.Item2 <= 0 ? -1 : 0;
    var positive = pair.Item1 + pair.Item2 > input.Count ? 1 : 0;
    var newIndex = Mod(pair.Item1 + pair.Item2 + negative + positive, input.Count) ;
    
    pair.Item1 = newIndex;
    
    var sign = Math.Sign(newIndex - oldIndex);

    for (var j = 0; j < input.Count; j++)
    {
        if (i == j)
        {
            continue;
        }
        
        if (input[j].Item1 > oldIndex && input[j].Item1 <= newIndex)
        {
            var p = input[j];
            p.Item1--;
            input[j] = p;
        }
        else if (input[j].Item1 >= newIndex && input[j].Item1 < oldIndex)
        {
            var p = input[j];
            p.Item1++;
            input[j] = p;
        }
    }
    
    input[i] = pair;
    // foreach (var a in input.OrderBy(p => p.Item1).Select(p => p.Item2).ToList())
    // {
    //     Console.Write($"{a} ");
    // }
    //
    // Console.WriteLine();
}

var output = input.OrderBy(p => p.Item1).Select(p => p.Item2).ToList();
foreach (var a in output)
{
    Console.Write($"{a} ");
}

Console.WriteLine();
var zeroIndex = output.IndexOf(0);
Console.WriteLine(output[(1000 + zeroIndex) % output.Count] + output[(2000 + zeroIndex) % output.Count] + output[(3000 + zeroIndex) % output.Count]);

// var indexes = new int[input.Count];
//
// for (var i = 0; i < indexes.Length; i++)
// {
//     indexes[i] = i;
// }
//
// for (var i = 0; i < input.Count; i++)
// {
//     var move = input[indexes[i]];
//     var moveSign = Math.Sign(move);
//     for (var j = 0; j != move; j += moveSign)
//     {
//         var index = Mod(indexes[i] + moveSign, input.Count);
//         (input[indexes[i]], input[index]) = (input[index], input[indexes[i]]);
//         for (var k = 0; k < indexes.Length; k++)
//         {
//             if (indexes[k] == input[input])
//             {
//                 indexes[k] = Mod(indexes[k] - moveSign, indexes.Length);
//             }
//         }
//         indexes[i] = Mod(indexes[i] + moveSign, input.Count);
//         var a = Mod(i + moveSign, indexes.Length);
//         var b = Mod(indexes[Mod(i + moveSign, indexes.Length)] - moveSign, input.Count);
//         
//     }
// }

// foreach (var i in input)
// {
//     Console.WriteLine(i);
// }

return;

int Mod(int a, int b)
{
    return a - b * (int)Math.Floor(a / (float)b);
}