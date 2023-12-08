namespace _2023.Solutions;

public static class Day8
{
    public static string Part1(List<string> input)
    {
        var dirSequence = new DirectionSequence(input[0]);
        input.RemoveRange(0, 2);
        var nodeList = PopulateNodeList(input);

        var currentNode = nodeList.First(x => x.ThisNode == "AAA");

        return FindNumberOfSteps(currentNode, dirSequence, 1).ToString();
    }

    public static string Part2(List<string> input)
    {
        var dirStr = input[0];
        var dirSequence = new DirectionSequence(dirStr);
        input.RemoveRange(0, 2);
        var nodeList = PopulateNodeList(input);

        var currentNodes = nodeList.FindAll(x => x.ThisNode[2] == 'A');
        var multiples = new List<long>();

        foreach (var node in currentNodes)
        {
            multiples.Add(FindNumberOfSteps(node, new DirectionSequence(dirStr), 2));
        }

        long count = 0;

        while (true)
        {
            var match = true;
            foreach (var node in currentNodes)
            {
                if (node.ThisNode[2] != 'Z')
                {
                    match = false;
                    break;
                }
            }

            if (match)
            {
                break;
            }

            count++;
            var nextDir = dirSequence.GetNextDirection();
            for (var i = 0; i < currentNodes.Count; i++)
            {
                currentNodes[i] = nextDir == 'L' ? currentNodes[i].LeftNode : currentNodes[i].RightNode;
            }
        }

        return count.ToString();
    }

    private static long FindNumberOfSteps(Node currentNode, DirectionSequence dirSequence, int part)
    {
        long count = 0;
        while (true)
        {
            if (part == 1)
            {
                if (currentNode.ThisNode == "ZZZ")
                {
                    break;
                }
            }

            if (part == 2)
            {
                if (currentNode.ThisNode[2] == 'Z')
                {
                    break;
                }
            }

            count++;
            currentNode = dirSequence.GetNextDirection() == 'L' ? currentNode.LeftNode : currentNode.RightNode;
        }

        return count;
    }

    private static List<Node> PopulateNodeList(List<string> nodeInputs)
    {
        var nodeList = new List<Node>();
        foreach (var line in nodeInputs)
        {
            nodeList.Add(new Node(line.Split('=')[0].TrimEnd(), line.Split(',')[0].Split('(')[1], line.Split(',')[1].Split(')')[0].TrimStart()));
        }

        foreach (var node in nodeList)
        {
            node.LeftNode = nodeList.First(x => x.ThisNode == node.LeftNodeStr);
            node.RightNode = nodeList.First(x => x.ThisNode == node.RightNodeStr);
        }

        return nodeList;
    }

    private class DirectionSequence
    {
        private string _dirs;
        private int _currentDir;

        internal DirectionSequence(string givenDirs)
        {
            _dirs = givenDirs;
            _currentDir = 0;
        }

        internal char GetNextDirection()
        {
            var dirToReturn = _dirs[_currentDir];
            if (_currentDir < _dirs.Length - 1)
            {
                _currentDir++;
            }
            else
            {
                _currentDir = 0;
            }

            return dirToReturn;
        }
    }

    private class Node
    {
        internal string ThisNode { get; set; }
        internal string LeftNodeStr;
        internal string RightNodeStr;
        internal Node LeftNode { get; set; }
        internal Node RightNode { get; set; }

        internal Node(string nodeValue, string leftNodeStr, string rightNodeStr)
        {
            ThisNode = nodeValue;
            LeftNodeStr = leftNodeStr;
            RightNodeStr = rightNodeStr;
        }

        internal int FindZZZNode(int currentIteration, DirectionSequence directionSequence)
        {
            if (ThisNode == "ZZZ")
            {
                return currentIteration;
            }

            currentIteration++;
            if (directionSequence.GetNextDirection() == 'L')
            {
                return LeftNode.FindZZZNode(currentIteration, directionSequence);
            }
            else
            {
                return RightNode.FindZZZNode(currentIteration, directionSequence);
            }
        }

    }
}