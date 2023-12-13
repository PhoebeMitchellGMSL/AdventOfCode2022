namespace Day13;

public static class Day13Solution
{
    public static bool CalculateDay13(string input1, string input2)
    {
        var splitInput1 = SplitInput(input1);
        var splitInput2 = SplitInput(input2);

        return Compare(splitInput1, splitInput2);
    }

    private static bool Compare(List<string> splitInput1, List<string> splitInput2)
    {
        for (var i = 0; i < Math.Max(splitInput1.Count, splitInput2.Count); i++)
        {
            if (i == splitInput1.Count || splitInput1[i] == "")
            {
                return true;
            }

            if (i == splitInput2.Count || splitInput2[i] == "")
            {
                return false;
            }

            {
                if (int.TryParse(splitInput1[i], out var i1) && int.TryParse(splitInput2[i], out var i2))
                {
                    if (i1 > i2)
                    {
                        return false;
                    }

                    if (i1 < i2)
                    {
                        return true;
                    }

                    continue;
                }
            }

            var split1 = SplitInput(splitInput1[i]);
            var split2 = SplitInput(splitInput2[i]);
            var result = Compare(split1, split2);

            if (!result)
            {
                return false;
            }
        }

        return true;
    }

    private static List<string> SplitInput(string input)
    {
        if (input.Length == 1)
        {
            return [input];
        }
        
        if (int.TryParse(input[1..^1], out var i1))
        {
            return [i1.ToString()];
        }
        
        var bracketDepth = 0;
        var previousSplitPoint = 1;
        var splitInput = new List<string>();
        for (var i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '[':
                    bracketDepth++;
                    break;
                case ']':
                    bracketDepth--;
                    if (bracketDepth == 0)
                    {
                        splitInput.Add(input[previousSplitPoint..i]);
                    }
                    break;
                case ',':
                    if (bracketDepth == 1)
                    {
                        splitInput.Add(input[previousSplitPoint..i]);
                        previousSplitPoint = i + 1;
                    }
                    break;
            }
        }

        return splitInput;
    }
}