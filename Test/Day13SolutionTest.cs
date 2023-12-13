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
    [TestCase("[3]", "[]", false)]
    [TestCase("[[3]]", "[]", false)]
    [TestCase("[[5,3,6,9],[[7,9],8,[[1,8],0,[1],8,[]],10,6],[1],[2,[[7,2,5],[2,7]],[[],10,[],[5,10,1],10],[6,[1,1],[7,6,10,2,1],0],[]],[[8,[0],0],5,4,[],1]]", "[[[5,[0,8,0,9,0],[1,8,9],[1,9]],[8,4,[],[3,10,4],3],1,9,[]],[[1,10,2],6],[5,5,10],[[1,[0,5,0],[2,6,1],4,7],[[1]],[10,[1,0],2,[],4],[[8,8,7,8],[2,6]]]]", true)]
    [TestCase("[[1],[2,3,4]]", "[[1],4]", true)]
    [TestCase("[[1],4]", "[[1],[2,3,4]]", false)]
    [TestCase("[[[[],[10,0,1],4]],[[0],7],[0,[],[]],[],[]]", "[[7,[[],[1],[5,5,6],[10,3],5],10,10],[[]],[5,[2,2],1,4]]", true)]
    [TestCase("[[8,[[7]]]]", "[[[[8]]]]", false)]
    [TestCase("[[8,[[7]]]", "[[[[8],[3]]]]", true)]
    [TestCase("[[],5]", "[[],1]", false)]
    [TestCase("[[],5]", "[[],6]", true)]
    [TestCase("[]", "[[]]", true)]
    public void D13(string input1, string input2, bool expectedOutput)
    {
        Assert.That(Day13Solution.CalculateDay13(input1, input2), Is.EqualTo(expectedOutput));
    }
}