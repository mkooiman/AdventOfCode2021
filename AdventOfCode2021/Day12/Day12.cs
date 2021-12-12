using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day12
{
    internal sealed class Day12
    {
        private const string TestFile1 = "../../Day12/input_test1.txt";
        private const string TestFile2 = "../../Day12/input_test2.txt";
        private const string TestFile3 = "../../Day12/input_test3.txt";
        private const string RealFile = "../../Day12/input.txt";

        private const string InputFile = RealFile;

        public static void Day12Pt1()
        {
            var paths = File.ReadAllLines(InputFile).Select(s => s.Split('-')).ToList();
            var nodes = PathsToNodes(paths);
            nodes = FilterDeadEnds(nodes);
            int count = FindAllPaths(nodes["start"]);
            Console.WriteLine(count);
        }
        
        public static void Day12Pt2()
        {
            var paths = File.ReadAllLines(InputFile).Select(s => s.Split('-')).ToList();
            var nodes = PathsToNodes(paths);
            int count = FindAllPaths(nodes["start"], null,1);
            Console.WriteLine(count);
        }

        private static int FindAllPaths(Node startNode, HashSet<string> visited = null, int cheat = 0)
        {
            if (startNode.Name == "end")
            {
                // Console.WriteLine(visited.Aggregate("", (s1,s2)=> s1 + "," + s2));
                return 1;
            }
            if (visited == null)
            {
                visited = new HashSet<string>();
            }
            int count = 0;
            if(startNode.Small)
                visited.Add(startNode.Name);
            
            foreach (var node in startNode.Connected)
            {
                if (!node.Small || !visited.Contains(node.Name))
                {
                    var newHash = new HashSet<string>(visited);
                    count += FindAllPaths(node,newHash, cheat);
                }else if (cheat > 0 && node.Name !="start" && node.Name != "end")
                {
                    var newHash = new HashSet<string>(visited);
                    count+= FindAllPaths(node, newHash, cheat - 1);
                }
                
            }

            return count;
        }

        private static Dictionary<string, Node> FilterDeadEnds(Dictionary<string, Node> nodes)
        {
            var toRemove = new List<string>();
            foreach (var keyValuePair in nodes)
            {
                var val = keyValuePair.Value;
                if (val.Small)
                {
                    if (val.Connected.All(v => !v.Small))
                    {
                        toRemove.Add(keyValuePair.Key);
                    }    
                }
            }
            foreach (string s in toRemove)
            {
                nodes.Remove(s);
            }

            return nodes;
        }

        private static Dictionary<string, Node> PathsToNodes(IEnumerable<string[]> paths){
            var nodes = new Dictionary<string,Node>();
            foreach (var strings in paths)
            {
                var from = strings[0];
                var to = strings[1];
                if (!nodes.ContainsKey(from))
                {
                    nodes.Add(from, new Node(from));
                }

                if (!nodes.ContainsKey(to))
                {
                    nodes.Add(to, new Node(to));
                }

                nodes[from].AddChild(nodes[to]);
                nodes[to].AddChild(nodes[from]);
            }

            return nodes;
        }
    }

    
    
    internal class Node
    {
        public Node(string name)
        {
            Name = name;
            Small = name[0] > 'Z';
        }

        public bool Small { get; set; }
        public string Name { get; set; }
        public List<Node> Connected { get; set; } = new List<Node>();
        
        public void AddChild(Node node)
        {
            Connected.Add(node);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}