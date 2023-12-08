using Common.Helpers;
using DailyCode.Base;
using DailyCode.Year2023.Day08.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DailyCode.Year2023.Day08
{
    public class Day08(string sessionId) : BaseDay(sessionId)
    {
        private char[] _instructions;
        private List<Node> _nodes;

        protected override void SetupData(List<string> fileInputs)
        {
            var rgx = new Regex("(\\w{3}) = \\((\\w{3}), (\\w{3})\\)");

            _instructions = fileInputs[0].Trim('\r').ToCharArray();

            List<Node> nodes = [];
            foreach (var fileInput in fileInputs.Skip(1).Select(x => x.Trim('\r')).Where(x => !string.IsNullOrEmpty(x)))
            {
                var match = rgx.Match(fileInput);
                nodes.Add(new Node(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value));
            }

            _nodes = nodes;
            _nodes.Where(x => x.Name.EndsWith('A')).ToList().ForEach(x => x.CreateNetwork(nodes));
        }

        protected override string RunPart1()
        {
            var startNode = _nodes.First(x => x.Name.Equals("AAA"));
            var totalSteps = 0;
            var currentNode = startNode;

            var index = 0;
            do
            {
                if (_instructions.Length == index) index = 0;
                currentNode = currentNode.HandleInstruction(_instructions[index++]);
                ++totalSteps;
            }
            while (!currentNode.Name.Equals("ZZZ"));

            return totalSteps.ToString();
        }

        protected override string RunPart2()
        {
            var nodes = _nodes.Where(x => x.Name.EndsWith('A'))
                .Select(x => new NodeWrapper(x))
                .ToList();

            foreach (var node in nodes)
            {
                var currentNode = node.StartNode;
                var index = 0;
                do
                {
                    if (_instructions.Length == index) index = 0;
                    currentNode = currentNode.HandleInstruction(_instructions[index++]);
                    
                    if (currentNode.Name.EndsWith('Z')) node.FoundPattern = true;

                    if(!node.FoundPattern)
                        node.Total++;
                }
                while (!node.FoundPattern);
            }

            return MathHelper.LCM(nodes.Select(x => (long)x.Total).ToArray()).ToString();
        }
    }
}