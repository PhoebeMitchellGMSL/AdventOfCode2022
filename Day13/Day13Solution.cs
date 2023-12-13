using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Day13;

public static class Day13Solution
{
    public static bool CalculateDay13(string input1, string input2)
    {
        var result = Compare(JsonConvert.DeserializeObject(input1), JsonConvert.DeserializeObject(input2));
        return result == Result.Valid;
    }

    private enum Result
    {
        Valid,
        Invalid,
        Equal
    }

    private static Result Compare(dynamic json1, dynamic json2)
    {
        {
            if (json1 is JValue)
            {
                if (json2 is JValue)
                {
                    if (json1 < json2)
                    {
                        return Result.Valid;
                    }

                    if (json1 > json2)
                    {
                        return Result.Invalid;
                    }
                }
            }
        }
        
        if (json1 is not JValue && json1.Count == 0 && (json2 is JValue || json2.Count > 0))
        {
            return Result.Valid;
        }
        
        if (json2 is not JValue && json2.Count == 0 && (json1 is JValue || json1.Count > 0))
        {
            return Result.Invalid;
        }
        
        for (var i = 0; i < Math.Max(json1 is JValue ? 0 : json1.Count, json2 is JValue ? 0 : json2.Count); i++)
        {
            if (json1 is not JValue && i >= json1.Count || json1 is JValue && i > 0)
            {
                return Result.Valid;
            }

            if (json2 is not JValue && i >= json2.Count || json2 is JValue && i > 0)
            {
                return Result.Invalid;
            }

            {
                if (json1 is JValue)
                {
                    var result = Compare(json1, json2[i]);
                    if (result == Result.Valid)
                    {
                        return Result.Valid;
                    }

                    if (result == Result.Invalid)
                    {
                        return Result.Invalid;
                    }
                    
                    continue;
                }
            }

            {
                if (json2 is JValue)
                {
                    var result = Compare(json1[i], json2);
                    if (result == Result.Valid)
                    {
                        return Result.Valid;
                    }

                    if (result == Result.Invalid)
                    {
                        return Result.Invalid;
                    }
                    
                    continue;
                }
            }

            {
                var result = Compare(json1[i], json2[i]);
                if (result == Result.Valid)
                {
                    return Result.Valid;
                }

                if (result == Result.Invalid)
                {
                    return Result.Invalid;
                }
            }
        }
        
        return Result.Equal;
    }
}