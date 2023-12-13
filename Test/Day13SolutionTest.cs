using Day13;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Test;

public class Day13SolutionTest
{
    [TestCase("[1,1,3,1,1]", "[1,1,5,1,1]", true)]
    [TestCase("[[1],[2,3,4]]", "[[1],4]", true)]
    [TestCase("[9]", "[[8,7,6]]", false)]
    [TestCase("[[4,4],4,4]", "[[4,4],4,4,4]", true)]
    [TestCase("[7,7,7,7]", "[7,7,7]", false)]
    [TestCase("[]", "[3]", true)]
    [TestCase("[[[]]]", "[[]]", false)]
    [TestCase("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", false)]
    public void D13(string input1, string input2, bool expectedOutput)
    {
        Assert.That(Day13Solution.CalculateDay13(input1, input2), Is.EqualTo(expectedOutput));
    }
}